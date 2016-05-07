using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Service.lib_boolean_funcs
{
    abstract class BooleanFormulaTerm : BooleanFormula
    {
        public override IEnumerable SubFormulas() { yield break; }

        public override BooleanFormula DeepClone() { return this.ShallowClone(); }

        public override int Depth { get { return 0; } }
    }
}