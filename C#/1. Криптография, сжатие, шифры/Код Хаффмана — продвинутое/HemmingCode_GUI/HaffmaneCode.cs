using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace HaffmaneCode_GUI
{
    // Класс дерево Хаффмана
    public class HuffmanTree
    {
        private List<Node> nodes = new List<Node>();
        public Node Root { get; set; }
        public Dictionary<char, int> ЧастотаСимвола = new Dictionary<char, int>();// Частоты символов

        public void Build(string source)
        {
            for (int i = 0; i < source.Length; i++)
            {
                if (!ЧастотаСимвола.ContainsKey(source[i]))
                {
                    ЧастотаСимвола.Add(source[i], 0);
                }

                ЧастотаСимвола[source[i]]++;
            }

            foreach (KeyValuePair<char, int> symbol in ЧастотаСимвола)
            {
                nodes.Add(new Node() { Symbol = symbol.Key, Frequency = symbol.Value });
            }

            while (nodes.Count > 1)
            {
                List<Node> orderedNodes = nodes.OrderBy(node => node.Frequency).ToList<Node>();

                if (orderedNodes.Count >= 2)
                {
                    // Берем 2 первых элемента
                    List<Node> taken = orderedNodes.Take(2).ToList<Node>();

                    // Создадим родительский узел комбинацией частот
                    Node parent = new Node()
                    {
                        Symbol = '*',
                        Frequency = taken[0].Frequency + taken[1].Frequency,
                        Left = taken[0],
                        Right = taken[1]
                    };

                    nodes.Remove(taken[0]);
                    nodes.Remove(taken[1]);
                    nodes.Add(parent);
                }

                this.Root = nodes.FirstOrDefault();

            }

        }

        public string str;

        // Кодирование
        public BitArray Encode(string source)
        {
            List<bool> encodedSource = new List<bool>();

            for (int i = 0; i < source.Length; i++)
            {
               // str += "'"+source[i]+"':[";
                List<bool> encodedSymbol = this.Root.Traverse(source[i], new List<bool>());
                foreach (bool ee in encodedSymbol)
                {
                    str+=ee?"1":"0";
                }
                //str += "] ";
                //str += '\n';
                encodedSource.AddRange(encodedSymbol);
            }

            BitArray bits = new BitArray(encodedSource.ToArray());

            return bits;
        }

        // Декодирование
        public string Decode(BitArray bits)
        {
            Node current = this.Root;
            string decoded = "";

            foreach (bool bit in bits)
            {
                if (bit)
                {
                    if (current.Right != null)
                    {
                        current = current.Right;
                    }
                }
                else
                {
                    if (current.Left != null)
                    {
                        current = current.Left;
                    }
                }

                // Это лист
                if (IsLeaf(current))
                {
                    decoded += current.Symbol;
                    current = this.Root;
                }
            }

            return decoded;
        }

        public bool IsLeaf(Node node)
        {
            return (node.Left == null && node.Right == null);
        }

    }

}
