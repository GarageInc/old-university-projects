
namespace ConsoleApplication
{
    using System.Collections.Generic;

    class Path
    {
        public List<Node> nodes { get; set; }

        public Path()
        {
            nodes = new List<Node>();
        }

        override public string ToString()
        {
            var result = "|";

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
