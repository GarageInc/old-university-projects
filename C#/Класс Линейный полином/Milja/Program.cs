using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Milja
{
    class Program
    {
        static void Main(string[] args)
        {
            
            StreamWriter wr = new StreamWriter("result.txt");
            Polynom scl = new Polynom();
            LinearPolynom scl1 = new LinearPolynom();
            int n = 0;
            while (n != 4)
            {
                n = Visualisation.Choice();
                if (n != 4)
                {
                    
                    switch (n)
                    {
                        case 1:
                            {
                                StreamReader rd = new StreamReader("polynom.txt");
                                Visualisation.Read(ref scl, rd);
                                Visualisation.Read(ref scl1);
                                wr.WriteLine("Polynom:");
                                Visualisation.Write(scl, wr);
                                wr.WriteLine("Linear Polynom:");
                                Visualisation.Write(scl1, wr);
                                Polynom t = scl * scl1;
                                wr.WriteLine("Result:");
                                Visualisation.Write(t, wr);
                            }
                            break;
                        case 2:
                            {
                                StreamReader rd = new StreamReader("polynom.txt");
                                Visualisation.Read(ref scl, rd);
                                Visualisation.Read(ref scl1);
                                wr.WriteLine("Polynom:");
                                Visualisation.Write(scl, wr);
                                wr.WriteLine("Linear Polynom:");
                                Visualisation.Write(scl1, wr);
                                Polynom s = scl / scl1;
                                wr.WriteLine("Result:");
                                Visualisation.Write(s, wr);
                            }
                            break;
                        case 3:
                            {
                                StreamReader rd = new StreamReader("polynom1.txt");
                                Visualisation.Read(ref scl, rd);
                                scl.Collect();
                                wr.WriteLine("Result:");
                                Visualisation.Write(scl, wr);
                            }
                            break;
                    }
                }
            }
            wr.Close();
        }
    }
}
