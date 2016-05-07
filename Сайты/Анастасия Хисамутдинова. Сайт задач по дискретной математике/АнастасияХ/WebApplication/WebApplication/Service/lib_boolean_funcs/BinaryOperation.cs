using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WebApplication.Service.lib_boolean_funcs
{
    class BinaryOperation : BooleanFormula
    {
        BooleanFormula argument1, argument2;

        public BinaryOperation(BooleanOperations type, BooleanFormula argument1, BooleanFormula argument2)
        {
            if ((argument1 == null) || (argument2 == null))
                throw new Exception(BooleanFormula.NULL_SUBFORMULA);
            this.type = type;
            this.argument1 = argument1;
            this.argument2 = argument2;
        }

        public BooleanFormula FirstChild { get { return argument1; } }

        public BooleanFormula LastChild { get { return argument2; } }

        public override IEnumerable SubFormulas()
        {
            yield return argument1;
            yield return argument2;
        }

        public override BooleanFormula DeepClone()
        {
            return new BinaryOperation(this.type, this.argument1.DeepClone(), this.argument2.DeepClone());
        }

        public override BooleanFormula Substitute(List<BooleanVariable> variables, List<BooleanFormula> subformulas)
        {
            return new BinaryOperation(this.type, this.argument1.Substitute(variables, subformulas), this.argument2.Substitute(variables, subformulas));
        }

        public override int Depth { get { return 1 + Math.Max(argument1.Depth, argument2.Depth); } }

        public override string ToString()
        {
            return "(" + argument1.ToString() + " " + type + " " + argument2.ToString() + ")";
        }

        public override string ToLaTeXString()
        {
            StringBuilder formulaString = new StringBuilder();
            if ((argument1 is BinaryOperation) && (argument1.Type != BooleanOperations.AND) &&
                        ((argument1.Type != this.Type) || (argument1.Type == BooleanOperations.IMPLICATION) ||
                         (argument1.Type == BooleanOperations.SHEFFER_STROKE) || (argument1.Type == BooleanOperations.PIERCE_ARROW)))
            {
                formulaString.Append('(');
                formulaString.Append(argument1.ToLaTeXString());
                formulaString.Append(')');
            }
            else
                formulaString.Append(argument1.ToLaTeXString());
            formulaString.Append(BooleanFormula.LaTeXOperations[(int)this.type]);
            if ((argument2 is BinaryOperation) && (argument2.Type != BooleanOperations.AND) &&
                        ((argument2.Type != this.Type) || (argument2.Type == BooleanOperations.IMPLICATION) ||
                         (argument2.Type == BooleanOperations.SHEFFER_STROKE) || (argument2.Type == BooleanOperations.PIERCE_ARROW)))
            {
                formulaString.Append('(');
                formulaString.Append(argument2.ToLaTeXString());
                formulaString.Append(')');
            }
            else
                formulaString.Append(argument2.ToLaTeXString());
            return formulaString.ToString();
        }

        public override bool FormulaValue(BooleanFormulaInput input)
        {
            switch (type)
            {
                case BooleanOperations.AND: return argument1.FormulaValue(input) && argument2.FormulaValue(input);
                case BooleanOperations.OR: return argument1.FormulaValue(input) || argument2.FormulaValue(input);
                case BooleanOperations.IMPLICATION: return !argument1.FormulaValue(input) || argument2.FormulaValue(input);
                case BooleanOperations.XOR: return argument1.FormulaValue(input) != argument2.FormulaValue(input);
                case BooleanOperations.EQUIVALENCE: return argument1.FormulaValue(input) == argument2.FormulaValue(input);
                case BooleanOperations.SHEFFER_STROKE: return !(argument1.FormulaValue(input) && argument2.FormulaValue(input));
                case BooleanOperations.PIERCE_ARROW: return !(argument1.FormulaValue(input) || argument2.FormulaValue(input));
            }
            return false;
        }

        public override List<BooleanVariable> Variables
        {
            get
            {
                List<BooleanVariable> list = argument1.Variables;
                List<BooleanVariable> list2 = argument2.Variables;
                variables = new List<BooleanVariable>();
                foreach (BooleanVariable variable in list)
                    variables.Add(variable);
                bool found;
                foreach (BooleanVariable variable2 in list2)
                {
                    found = false;
                    foreach (BooleanVariable variable in variables)
                        if (variable.ToString() == variable2.ToString())
                            found = true;
                    if (!found)
                        variables.Add(variable2);
                    //if (!variables.Contains(variable2))
                    //    variables.Add(variable2);
                }
                return variables;
            }
        }
    }
}