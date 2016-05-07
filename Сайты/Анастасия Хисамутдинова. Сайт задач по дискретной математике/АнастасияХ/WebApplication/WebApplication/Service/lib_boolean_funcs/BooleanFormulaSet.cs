using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WebApplication.Service.lib_boolean_funcs
{
    class BooleanFormulaSet : List<BooleanFormula>
    {

        public string ToLaTeXString()
        {
            StringBuilder result = new StringBuilder();
            result.Append("\\{");
            bool empty = true;
            foreach (BooleanFormula formula in this)
            {
                if (empty)
                    empty = false;
                else
                    result.Append(", ");
                result.Append(formula.ToLaTeXString());
            }
            result.Append("\\}");
            return result.ToString();
        }

        public bool IsComplete()
        {
            bool found;
            foreach (BooleanFunction.PropertyTest hasProperty in BooleanFunction.CompletenessTests)
            {
                found = false;
                foreach (BooleanFormula formula in this)
                    if (!found && (formula != null) && hasProperty(formula.RealizedFunction))
                        found = true;
                if (!found)
                    return false;
            }
            return true;
        }

        public bool IsBasis()
        {
            if (!IsComplete())
                return false;
            BooleanFormula formula;
            for (int i = 0; i < this.Count; i++)
            {
                formula = this[i];
                this.RemoveAt(i);
                bool isComplete = this.IsComplete();
                this.Insert(i, formula);
                if (isComplete)
                    return false;
            }
            return true;
        }
    }
}