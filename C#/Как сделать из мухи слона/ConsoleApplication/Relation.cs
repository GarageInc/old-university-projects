using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{

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

    class RelationsFabric
    {
        protected static RelationsFabric instance;

        public Dictionary<int, Relation> relations { get; set; }

        public RelationsFabric()
        {
            relations = new Dictionary<int, Relation>();
        }

        public static RelationsFabric getInstance()
        {
            if (instance == null)
            {
                instance = new RelationsFabric();
            }

            return instance;
        }

        public void createNewRelation(Node left, Node right)
        {
            var relation = new Relation(left, right);

            int hash = (left.rootWord + right.rootWord).GetHashCode();
            if (!relations.ContainsKey(hash))
            {
                relations.Add(hash, relation);

                left.relations.Add(relation);
                right.relations.Add(relation);
            }
        }
    }

    static class RelationsCombainer
    {

        public static void trace(params object[] list)
        {
            foreach (var o in list)
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

            for (int i = 0; i < nodes.Count; i++)
            {
                for (int j = i + 1; j < nodes.Count; j++)
                {
                    if (metric.GetDistance(nodes[i].rootWord, nodes[i].rootWord, Math.Max(nodes[i].rootWord.Length, nodes[j].rootWord.Length)) == 1)
                    {
                        trace(nodes[i].rootWord, nodes[j].rootWord);

                        RelationsFabric.getInstance().createNewRelation(nodes[i], nodes[j]);

                    }// pass
                }
            }

        }

        public static List<Path> findPaths(string wordFirth, string wordSecond)
        {
            var paths = new List<Path>();

            var nodeFirst = NodesFabric.getInstance().nodes[wordFirth.GetHashCode()];

            var nodeSecond = NodesFabric.getInstance().nodes[wordSecond.GetHashCode()];

            if (nodeFirst == null || nodeSecond == null)
                throw new Exception("Ошибочка в поиске");

            start(nodeFirst, nodeSecond);
         

            return paths;
        }

        public static void 
    }


}
