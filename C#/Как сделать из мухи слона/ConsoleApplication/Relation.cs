
namespace ConsoleApplication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Relation
    {
        public Node leftNode { get; set; }

        public Node rightNode { get; set; }
        
        public Relation(Node l, Node r)
        {
            leftNode = l;
            rightNode = r;
        }

        public override string ToString()
        {
            return leftNode.ToString() + " + " + rightNode.ToString();
        }
    }
    

    static class RelationsCombainer
    {

        public static void trace(params object[] list)
        {
            //return;

            foreach (var o in list)
            {
                Console.Write(o + " ");
            }

            Console.WriteLine();
        }

        public static void transformNodesByRelations()
        {
            var nodes = NodesFabric.getInstance().nodes;
            
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

        public static void findPaths(string wordFirth, string wordSecond)
        {

            var nodeStart = NodesFabric.getInstance().find(wordFirth);//.GetHashCode()];

            var nodeEnd = NodesFabric.getInstance().find(wordSecond);//.GetHashCode()];

            if (nodeStart == null || nodeEnd == null)
                throw new Exception("Ошибочка в поиске");

            var tmpPath = new Path();
            var removePath = new Path();
            var tailRelation = new Path();

            tailRelation.nodes.Add(null);
            tailRelation.nodes.Add(null);

            start(nodeStart, nodeStart,  nodeEnd,   tmpPath );
        }

        static int COUNTER = 0;

        // recursive path
        public static void start(Node startNode, Node current, Node end,   Path tmpPath)
        {
            tmpPath.Add(current);

            current.isVisited = true;

            bool stepBack = false;
            bool started = false;

            if (current != end)
            {
                for(int i=current.visitedCounter; i < current.relations.Count; i++)
                {
                    if (!current.relations[i].leftNode.isVisited )
                    {
                        started = true;

                        current.visitedCounter++;

                        start(startNode, current.relations[i].leftNode, end,   tmpPath );
                    }
                    else if (!current.relations[i].rightNode.isVisited)
                    {
                        started = true;

                        current.visitedCounter++;

                        start(startNode, current.relations[i].rightNode, end,   tmpPath );
                    }// pass

                    stepBack = true;
                }

                if ( !started )
                {
                    trace("Тупик: ", current.ToString());
                }// pass

                if ( current.visitedCounter == current.relations.Count && current == startNode)
                {
                    stepBack = false;
                }// pass

            }
            else
            {
                trace("");
                trace(tmpPath);

                stepBack = true;

                COUNTER++;
            }

            if ( stepBack )
            {
                Node newStartNode;

                if ( tmpPath.nodes.Count >= 2 )
                {
                    tmpPath.nodes.RemoveAt(tmpPath.nodes.Count - 1);

                    current.visitedCounter = 0;
                    current.isVisited = false;

                    newStartNode = tmpPath.nodes.Last();
                } else
                {
                    newStartNode = current;
                }
                
                tmpPath.nodes.RemoveAt(tmpPath.nodes.Count - 1);// because than added in start

                start(startNode, newStartNode, end, tmpPath);
            }// pass
        }
    }


}
