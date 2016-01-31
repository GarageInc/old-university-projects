using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Milja
{
    class LinearPolynom:Polynom
    {
        public LinearPolynom()
        {
            koeff = new List<double>(2);
            degrees = new List<int>(2);
        }

    }
}
