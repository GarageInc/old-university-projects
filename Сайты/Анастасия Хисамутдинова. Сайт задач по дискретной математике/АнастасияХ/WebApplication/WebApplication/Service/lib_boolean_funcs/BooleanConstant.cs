using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Service.lib_boolean_funcs
{
    class BooleanConstant : BooleanFormulaTerm
    {
        protected bool value = false;

        public BooleanConstant(bool value) { this.value = value; }

        public override string ToString()
        {
            return (value ? "1" : "0");
        }

        public override string ToLaTeXString()
        {
            return this.ToString();
        }

        public override bool FormulaValue(BooleanFormulaInput input)
        {
            return value;
        }

        public override BooleanFormula Substitute(List<BooleanVariable> variables, List<BooleanFormula> subformulas)
        {
            return this;
        }

        public override List<BooleanVariable> Variables { get { return new List<BooleanVariable>(); } }

    }
}