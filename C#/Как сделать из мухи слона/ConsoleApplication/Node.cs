
namespace ConsoleApplication
{
    using System.Collections.Generic;
    using System;

    class Node
    {
        public string rootWord { get; set; }

        public bool isVisited { get; set; }

        public int lastVisitedIndex { get; set; }

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

            return left.rootWord != right.rootWord;
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

            return nodes[hash];
        }
    }

    
    

   

 
}
