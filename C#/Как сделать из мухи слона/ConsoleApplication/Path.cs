using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    class Path
    {
        protected List<Node> nodes { get; set; }

        public Path()
        {
            nodes = new List<Node>();
        }

        override public string ToString()
        {
            var result = "";

            foreach (var node in nodes)
            {
                result += " -> " + node.ToString();
            }

            return result;
        }

        public void Add(Node node)
        {
            this.nodes.Add(node);
        }
    }
}
