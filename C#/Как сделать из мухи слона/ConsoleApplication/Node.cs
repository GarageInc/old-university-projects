
namespace ConsoleApplication
{
    using System.Collections.Generic;
    using System;
    using System.Linq;

    class Node
    {
        public string rootWord { get; set; }

        public bool isVisited { get; set; }

        public int visitedCounter { get; set; }
        
        public List<Relation> relations;

        public Node( string word )
        {
            rootWord = word;

            relations = new List<Relation>();

            isVisited = false;
        }

        public override string ToString()
        {
            return rootWord;
        }

        public static bool operator !=(Node left, Node right)
        {
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
                return !ReferenceEquals(left, right);

            return left.rootWord != right.rootWord;
        }

        public static bool operator ==(Node left, Node right)
        {
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
                return ReferenceEquals(left, right);

            return left.rootWord == right.rootWord;
        }
    }

    class NodesFabric
    {
        protected static NodesFabric instance;

        public Dictionary<int, Node> nodes { get; set; }

        public NodesFabric()
        {
            nodes = new Dictionary<int, Node>();
        }

        public static NodesFabric getInstance()
        {
            if(instance == null)
            {
                instance = new NodesFabric();
            }

            return instance;
        }

        public void createNewNode(string word)
        {
            int hashCode = (word).GetHashCode();

            if (!nodes.ContainsKey(hashCode))
            {
                nodes.Add(hashCode, new Node(word));
            }
        }

        public Node find(string word)
        {
            var hash = word.GetHashCode();

            if (nodes.ContainsKey(hash))
            {

                return nodes[hash];
            }
            else
            {
                return null;
            }

        }

        public  void transformNodesByRelations()
        {
            var nodes = getInstance().nodes;

            var metric = new DamerauLevensteinMetric();

            for (int i = 0; i < nodes.Count; i++)
            {
                for (int j = i + 1; j < nodes.Count; j++)
                {
                    var first = nodes.ElementAt(i).Value.rootWord;
                    var second = nodes.ElementAt(j).Value.rootWord;

                    var distance = metric.GetDistance(
                        first,
                        second,
                        -1);

                    if (distance == 1)
                    {
                        Relation relation = new Relation(nodes.ElementAt(i).Value, nodes.ElementAt(j).Value);

                        nodes.ElementAt(i).Value.relations.Add(relation);
                        nodes.ElementAt(j).Value.relations.Add(relation);

                    }// pass
                }
            }

        }
    }

    
    

   

 
}
