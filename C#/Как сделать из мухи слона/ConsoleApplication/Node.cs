using System.Collections.Generic;
using System;

namespace ConsoleApplication
{

    class Node
    {
        public string rootWord { get; set; }
        
        public List<Relation> relations;

        public Node( string word )
        {
            rootWord = word;

            relations = new List<Relation>();
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
    }

    class Relation
    {
        public Node leftNode { get; set; }

        public Node rightNode { get; set; }

        public bool isVisited { get; set; }

        public Relation(Node l, Node r)
        {
            leftNode = l;
            rightNode = r;
            isVisited = false;
        }

    }

    class Path
    {
        public List<Node> nodes { get; set; }

        public Path()
        {
            nodes = new List<Node>();
        }
    }

    static class RelationsCombainer
    {

        public static void trace(params object[] list)
        {
            foreach(var o in list)
            {
                Console.Write(o + " ");
            }

            Console.WriteLine();
        }

        public static void transformNodesByRelations()
        {
            var nodes = NodesFabric.getInstance().nodes;

            // строим связи только те, в которых расстояние Дамерау-Левенштейна == 1

            var metric = new DamerauLevensteinMetric();

            for(int i=0; i<nodes.Count; i++)
            {
                for (int j = i + 1; j < nodes.Count; j++)
                { 
                    if (metric.GetDistance(nodes[i].rootWord, nodes[i].rootWord, Math.Max(nodes[i].rootWord.Length, nodes[j].rootWord.Length )) == 1)
                    {
                        trace(nodes[i].rootWord, nodes[j].rootWord);


                    }// pass
                }
            }

        } 

        public static List<Path> findPaths()
        {
            var paths = new List<Path>();



            return paths;
        }
    }

 
}
