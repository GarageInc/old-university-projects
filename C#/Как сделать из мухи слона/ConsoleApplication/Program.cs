
namespace ConsoleApplication
{

    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using System.IO;


    class Program
    {
        public static void change(Node node)
        {
            node.rootWord = "3";
        }

        static void Main(string[] args)
        {
            string fileTwoWords = "input.txt";
            string fileDictionary = "dictionary.txt";

            StreamReader sr = new StreamReader(fileTwoWords, Encoding.GetEncoding(1251));
            string[] readedResult = sr.ReadToEnd().Split('\n');

            string wordFirst = readedResult[0].Trim();
            string wordSecond = readedResult[1].Trim();

            sr = new StreamReader(fileDictionary, Encoding.GetEncoding(1251));

            readedResult = sr.ReadToEnd().Split('\n');
            sr.Close();

            List<Node> nodes = new List<Node>();
            foreach (string word in readedResult)
            {
                NodesFabric.getInstance().createNewNode(word.Trim());
            }

            NodesFabric.getInstance().transformNodesByRelations();
            
            RelationsCombainer.findPaths(wordFirst, wordSecond);

            Console.ReadKey();
        }
    }
}
