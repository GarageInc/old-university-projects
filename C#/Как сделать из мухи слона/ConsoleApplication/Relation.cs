
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

            start( nodeStart,  nodeEnd, ref tmpPath, ref tailRelation);
        }

        static int COUNTER = 0;
        // recursive path
        public static void start(Node current, Node end, ref Path tmpPath, ref Path tail)
        {
            tmpPath.Add(current);

            current.isVisited = true;

            bool started = false;

            // tailRelation.leftNode = tailRelation.rightNode;

            if (current != end)
            {
                foreach (Relation relation in current.relations)
                {
                    if (!relation.leftNode.isVisited)
                    {
                        started = true;
                        
                        start( relation.leftNode, end, ref tmpPath, ref  tail);
                    }
                    else if (!relation.rightNode.isVisited)
                    {
                        started = true;
                        
                        start( relation.rightNode, end, ref tmpPath, ref tail);
                    }// pass
                }

                if ( !started )
                {
                    trace("Тупик: ", current.ToString());

                    if (tmpPath.nodes.Count >= 2)
                    {
                        if (tail.nodes[0] != null)
                        {
                            tail.nodes[0].isVisited = false;
                        }// pass

                        tail.nodes.RemoveAt(0);

                        tail.nodes.Add(tmpPath.nodes[tmpPath.nodes.Count - 1]);

                        //
                        tmpPath.nodes.RemoveAt(tmpPath.nodes.Count - 1);

                        var newStartNode = tmpPath.nodes.Last();

                        tmpPath.nodes.RemoveAt(tmpPath.nodes.Count - 1);

                        start(newStartNode, end, ref tmpPath, ref tail);

                        // current.isVisited = false;
                    }// pass
                }// pass
            }
            else
            {
                COUNTER++;
                trace("");
                trace(tmpPath);

                if (tmpPath.nodes.Count >= 2)
                {
                    tmpPath.nodes.Last().isVisited = false;// current

                    tmpPath.nodes.RemoveAt(tmpPath.nodes.Count - 1);

                    tmpPath.nodes.Last().isVisited = true;
                    
                    var newStartNode = tmpPath.nodes.Last();

                    tmpPath.nodes.RemoveAt(tmpPath.nodes.Count - 1);

                    start(newStartNode, end, ref tmpPath, ref tail);

                    // current.isVisited = false;
                }// pass
            }
            
        }
    }


}
