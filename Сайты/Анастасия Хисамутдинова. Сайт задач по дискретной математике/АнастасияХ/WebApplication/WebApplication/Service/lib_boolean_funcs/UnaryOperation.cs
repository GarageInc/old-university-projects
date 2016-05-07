using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Service.lib_boolean_funcs
{
    class UnaryOperation : BooleanFormula
    {
        BooleanFormula argument;

        public UnaryOperation(BooleanFormula argument)
        {
            if (argument == null)
                throw new Exception(NULL_SUBFORMULA);
            this.type = BooleanOperations.NOT;
            this.argument = argument;
        }

        public BooleanFormula FirstChild { get { return argument; } }

        public override IEnumerable SubFormulas()
        {
            yield return argument;
        }

        public override BooleanFormula DeepClone()
        {
            return new UnaryOperation(this.argument.DeepClone());
        }

        public override BooleanFormula Substitute(List<BooleanVariable> variables, List<BooleanFormula> subformulas)
        {
            return new UnaryOperation(this.argument.Substitute(variables, subformulas));
        }

        public override int Depth { get { return 1 + argument.Depth; } }

        public override string ToString()
        {
            return type.ToString() + " (" + argument.ToString() + ")";
        }

        public override string ToLaTeXString()
        {
            if (argument is BooleanVariable)//специальная обработка отрицаний над переменными
                return "\\overline{" + (argument as BooleanVariable).Name + "}_" + (argument as BooleanVariable).Index;
            return "\\overline{" + argument.ToLaTeXString() + "}";
        }

        public override bool FormulaValue(BooleanFormulaInput input)
        {
            return !argument.FormulaValue(input);
        }

        public override List<BooleanVariable> Variables { get { return variables = argument.Variables; } }
    }
}