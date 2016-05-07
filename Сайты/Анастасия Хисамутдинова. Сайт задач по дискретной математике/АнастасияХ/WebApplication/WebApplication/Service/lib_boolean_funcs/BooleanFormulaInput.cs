using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Service.lib_boolean_funcs
{
    class BooleanFormulaInput
    {
        protected bool[] input;
        protected List<BooleanVariable> variables;

        public BooleanFormulaInput(bool[] input, List<BooleanVariable> variables)
        {
            this.input = input;
            this.variables = variables;
        }

        public bool ValueOf(BooleanVariable variable)
        {
            int index = -1;
            for (int i = 0; (index < 0) && (i < variables.Count); i++)
                if (variables[i].ToString() == variable.ToString())
                    index = i;
            return input[index];
        }
    }
}