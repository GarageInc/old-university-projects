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

            var d = metric.GetDistance("тон", "слон", -1);
        }

        public static List<Path> findPaths(string wordFirth, string wordSecond)
        {
            var paths = new List<Path>();

            var nodeStart = NodesFabric.getInstance().nodes[wordFirth.GetHashCode()];

            var nodeEnd = NodesFabric.getInstance().nodes[wordSecond.GetHashCode()];

            if (nodeStart == null || nodeEnd == null)
                throw new Exception("Ошибочка в поиске");

            var tmpPath = new Path();

            tmpPath.nodes.Add( nodeStart );
            start(ref paths, ref tmpPath, nodeStart,  nodeEnd);
         
            return paths;
        }

        // recursive path
        public static void start(ref List<Path> paths, ref Path tmpPath, Node current,  Node end)
        {
            if ( current != end )
            {
                bool started = false;

                foreach( Relation relation in current.relations )
                {
                    if ( !relation.isVisited )
                    {
                        started = true;

                        relation.isVisited = true;
                        
                        if ( relation.leftNode != current )
                        {
                            tmpPath.nodes.Add(relation.leftNode);

                            start( ref paths, ref tmpPath, relation.leftNode,  end );
                        } else
                        {
                            tmpPath.nodes.Add(relation.rightNode);

                            start( ref paths, ref tmpPath, relation.rightNode, end );
                        }
                    }
                }

                if ( !started )
                {
                    trace("Тупик: ", current.ToString());

                    // делаем шаг назад, говорим - что сюда пройти уже нельзя и ищем новый путь
                }
            }
            else
            {                
                trace("FIND PATH!");

                trace( tmpPath );
            }


        }
    }


}
