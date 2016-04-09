using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {

            string fileTwoWords = "input.txt";
            string fileDictionary = "dictionary.txt";

            StreamReader sr = new StreamReader(fileTwoWords);
            string[] readedResult = sr.ReadToEnd().Split('\n');

            string wordFirst = readedResult[ 0 ];
            string wordSecond = readedResult[ 1 ];

            sr = new StreamReader(fileDictionary);

            readedResult = sr.ReadToEnd().Split('\n');
            sr.Close();

            List<Node> nodes = new List<Node>();
            foreach(string word in readedResult)
            {
                NodesFabric.getInstance().createNewNode(word);
            }

            RelationsCombainer.transformNodesByRelations();
            var paths = RelationsCombainer.findPaths(wordFirst, wordSecond);

            Console.ReadKey();
        }
    }
}
