using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Service.lib_boolean_funcs
{
    class BooleanVariable : BooleanFormulaTerm
    {
        protected string name;
        protected int index;
        protected bool indexed = false;

        public BooleanVariable(string name) { this.name = name; }

        public BooleanVariable(string name, int index)
        {
            this.name = name;
            this.index = index;
            indexed = true;
        }

        public string Name { get { return name; } }

        public int Index { get { return indexed ? index : -1; } }

        public override string ToString()
        {
            return name + (indexed ? ("_" + index.ToString()) : "");
        }

        public override string ToLaTeXString()
        {
            return this.ToString();
        }

        public override bool FormulaValue(BooleanFormulaInput input)
        {
            return input.ValueOf(this);
        }

        public override BooleanFormula Substitute(List<BooleanVariable> variables, List<BooleanFormula> subformulas)
        {
            for (int i = 0; (i < variables.Count); i++)
                if (variables[i].ToString() == this.ToString())
                    return subformulas[i].DeepClone();
            return this;
        }

        //public static bool operator ==(Variable term1, Variable term2)
        //{
        //    if (term1 == null)
        //        if (term2 == null)
        //            return true;
        //        else
        //            return false;
        //    if (term2 == null)
        //        return false;
        //    return term1.ToString() == term2.ToString();
        //}

        //public static bool operator !=(Variable term1, Variable term2)
        //{
        //    return !(term1 == term2);
        //}

        //public override bool Equals(object obj)
        //{
        //    if (obj is Variable)
        //        return this == (obj as Variable);
        //    return false;
        //}

        //public override int GetHashCode()
        //{
        //    return index;
        //}

        public override List<BooleanVariable> Variables
        {
            get
            {
                if (variables == null)
                {
                    variables = new List<BooleanVariable>();
                    variables.Add(this);
                }
                return variables;
            }
        }
    }
}