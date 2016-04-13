
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
        
        public static void findPaths(string wordFirth, string wordSecond)
        {
            var nodeStart = NodesFabric.getInstance().find(wordFirth);//.GetHashCode()];

            var nodeEnd = NodesFabric.getInstance().find(wordSecond);//.GetHashCode()];

            if (nodeStart == null || nodeEnd == null)
                throw new Exception("Ошибочка в поиске");

            var tmpPath = new Path();
            var removePath = new Path();

            start(ref nodeStart, nodeStart, ref nodeEnd,   tmpPath );
        }
        
        // recursive path
        public static void start(ref Node startNode, Node current, ref Node end,   Path tmpPath)
        {
            tmpPath.Add(current);

            current.isVisited = true;

            bool stepBack = false;
            Node newStartNode = null;

            if (current != end)
            {
                for(int i = current.visitedCounter; i < current.relations.Count ; i++)
                {
                    current.visitedCounter++;

                    if ( !current.relations[i].leftNode.isVisited )
                    {
                        newStartNode = current.relations[i].leftNode;
                        break;
                    }
                    else if ( !current.relations[i].rightNode.isVisited)
                    {
                        newStartNode = current.relations[i].rightNode;
                        break;
                    }// pass
                }

                if ( newStartNode != null )
                {
                    start(ref startNode, newStartNode, ref end, tmpPath);

                    return;
                }
                else
                {
                    trace("\nТупик: ", current.ToString());
                }

                if ( current.visitedCounter == current.relations.Count && current == startNode)
                {
                    stepBack = false;
                } else
                {
                    stepBack = true;
                }

            }
            else
            {
                trace("\n", tmpPath);

                stepBack = true;                
            }

            if ( stepBack )
            {
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

                start(ref startNode, newStartNode, ref end, tmpPath);
                return;

            }// pass

        }
    }


}
