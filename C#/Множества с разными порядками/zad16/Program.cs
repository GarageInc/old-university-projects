using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Регина
{
    class Program
    {
        static void Main(string[] args)
        {
            
            StreamWriter wr = new StreamWriter("result.txt");
            Set scl = new Set();
            Set scl1 = new Set();
            int n = 0;
            while (n != 7)
            {
                n = Visualisation.Choice();
                if (n != 7)
                {
                    
                    switch (n)
                    {
                        case 1:
                            {
                                StreamReader rd = new StreamReader("set.txt");
                                StreamReader rd1 = new StreamReader("set1.txt");
                                Visualisation.Read(ref scl, rd);
                                Visualisation.Read(ref scl1, rd1);
                                Set t = scl | scl1;
                                wr.WriteLine("First set:");
                                Visualisation.Write(scl, wr);
                                wr.WriteLine("Second set:");
                                Visualisation.Write(scl1, wr);
                                wr.WriteLine("Result:");
                                Visualisation.Write(t, wr);
                            }
                            break;
                        case 2:
                            {
                                StreamReader rd = new StreamReader("set.txt");
                                StreamReader rd1 = new StreamReader("set1.txt");
                                Visualisation.Read(ref scl, rd);
                                Visualisation.Read(ref scl1, rd1);
                                Set t = scl & scl1;
                                wr.WriteLine("First set:");
                                Visualisation.Write(scl, wr);
                                wr.WriteLine("Second set:");
                                Visualisation.Write(scl1, wr);
                                wr.WriteLine("Result:");
                                Visualisation.Write(t, wr);
                            }
                            break;
                        case 3:
                            {
                                StreamReader rd = new StreamReader("set.txt");
                                StreamReader rd1 = new StreamReader("set1.txt");
                                Visualisation.Read(ref scl, rd);
                                Visualisation.Read(ref scl1, rd1);
                                Set t = scl / scl1;
                                wr.WriteLine("First set:");
                                Visualisation.Write(scl, wr);
                                wr.WriteLine("Second set:");
                                Visualisation.Write(scl1, wr);
                                wr.WriteLine("Result:");
                                Visualisation.Write(t, wr);
                            }
                            break;
                        case 4:
                            {
                                StreamReader rd = new StreamReader("set.txt");
                                StreamReader rd1 = new StreamReader("set1.txt");
                                Visualisation.Read(ref scl, rd);
                                Visualisation.Read(ref scl1, rd1);
                                Set t = scl + scl1;
                                wr.WriteLine("First set:");
                                Visualisation.Write(scl, wr);
                                wr.WriteLine("Second set:");
                                Visualisation.Write(scl1, wr);
                                wr.WriteLine("Result:");
                                Visualisation.Write(t, wr);
                            }
                            break;
                        case 5:
                            {
                                StreamReader rd = new StreamReader("set.txt");
                                StreamReader rd1 = new StreamReader("set1.txt");
                                Visualisation.Read(ref scl, rd);
                                Visualisation.Read(ref scl1, rd1);
                                Set t = scl * scl1;
                                wr.WriteLine("First set:");
                                Visualisation.Write(scl, wr);
                                wr.WriteLine("Second set:");
                                Visualisation.Write(scl1, wr);
                                wr.WriteLine("Result:");
                                Visualisation.Write(t, wr);
                            }
                            break;
                        case 6:
                            {
                                StreamReader rd = new StreamReader("set.txt");
                                StreamReader rd1 = new StreamReader("set1.txt");
                                Visualisation.Read(ref scl, rd);
                                Visualisation.Read(ref scl1, rd1);
                                Set t = scl - scl1;
                                wr.WriteLine("First set:");
                                Visualisation.Write(scl, wr);
                                wr.WriteLine("Second set:");
                                Visualisation.Write(scl1, wr);
                                wr.WriteLine("Result:");
                                Visualisation.Write(t, wr);
                            }
                            break;
                    }
                }
            }
            wr.Close();
        }
    }
}
