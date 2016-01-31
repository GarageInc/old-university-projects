using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Milja
{
    class Polynom
    {
        public List<double> koeff;
        public List<int> degrees;

        public Polynom()
        {
            koeff = new List<double>();
            degrees = new List<int>();
        }

        public static Polynom operator *(Polynom s1, LinearPolynom s2)
        {
            Polynom s = new Polynom();
            Polynom t1 = new Polynom();
            Polynom t2 = new Polynom();
            for (int i = 0; i < s1.degrees.Count; i++)
            {
                if (s1.koeff[i] * s2.koeff[0] != 0)
                {
                    t1.degrees.Add(s1.degrees[i]);
                    t1.koeff.Add(s1.koeff[i] * s2.koeff[0]);
                }
                if (s1.koeff[i] * s2.koeff[1] != 0)
                {
                    t2.degrees.Add(s1.degrees[i] + 1);
                    t2.koeff.Add(s1.koeff[i] * s2.koeff[1]);
                }
            }
            int k = 0, j = 0;
            for (; (k < t1.degrees.Count) && (j < t2.degrees.Count); )
            {
                if (t1.degrees[k] == t2.degrees[j])
                {
                    s.degrees.Add(t1.degrees[k]);
                    s.koeff.Add(t2.koeff[j] + t1.koeff[k]);
                    k++;
                    j++;
                }
                else
                {
                    if (t1.degrees[k] > t2.degrees[j])
                    {
                        s.degrees.Add(t2.degrees[j]);
                        s.koeff.Add(t2.koeff[j]);
                        j++;
                    }
                    else
                    {
                        if (t1.degrees[k] < t2.degrees[j])
                        {
                            s.degrees.Add(t1.degrees[k]);
                            s.koeff.Add(t1.koeff[k]);
                            k++;
                        }
                    }
                }
            }
            for (; k < t1.degrees.Count; )
            {
                s.degrees.Add(t1.degrees[k]);
                s.koeff.Add(t1.koeff[k]);
                k++;
            }
            for (; j < t2.degrees.Count; )
            {
                s.degrees.Add(t2.degrees[j]);
                s.koeff.Add(t2.koeff[j]);
                j++;
            }
            return s;
        }

        public static Polynom operator /(Polynom s1, LinearPolynom s2)
        {
            Polynom s = new Polynom();
            if (s2.koeff[1] == 0)
            {
                if (s2.koeff[0] == 0)
                {
                    Console.WriteLine("Divided by zero.");
                    return null;
                }
                for (int i = 0; i < s1.degrees.Count; i++)
                {
                    s.degrees.Add(s1.degrees[i]);
                    s.koeff.Add(s1.koeff[i] / s2.koeff[0]);
                }
                return s;
            }
            if (s2.koeff[0] == 0)
            {
                if (s1.degrees[0] > 0)
                {
                    s.degrees.Add(s1.degrees[0] - 1);
                    s.koeff.Add(s1.koeff[0] / s2.koeff[1]);
                }
                for (int i = 1; i < s1.degrees.Count; i++)
                {
                    s.degrees.Add(s1.degrees[i] - 1);
                    s.koeff.Add(s1.koeff[i] / s2.koeff[1]);
                }
                return s;
            }
            double x = - s2.koeff[0] / s2.koeff[1];
            s.degrees.Add(s1.degrees[s1.degrees.Count - 1] - 1);
            s.koeff.Add(s1.koeff[s1.koeff.Count - 1]);
            for (int i = s1.degrees[s1.degrees.Count - 1] - 1, j = s1.degrees.Count - 1; i > 0; i--)
            {
                for (;(j >= 0) && (s1.degrees[j] > i); j--) ;

                if ((j >= 0) && (s1.degrees[j] == i))
                {
                    if (s.koeff[s.koeff.Count - 1] * x + s1.koeff[j] != 0)
                    {
                        if (s.degrees[s.degrees.Count - 1] == i)
                        {
                            s.degrees.Add(i - 1);
                            s.koeff.Add(s.koeff[s.koeff.Count - 1] * x + s1.koeff[j]);
                        }
                        else
                        {
                            s.degrees.Add(i - 1);
                            s.koeff.Add(s1.koeff[j]);

                        }
                    }
                }
                else
                {
                    if (s.degrees[s.degrees.Count - 1] == i)
                    {
                        s.degrees.Add(i - 1);
                        s.koeff.Add(s.koeff[s.koeff.Count - 1] * x);
                    }
                }
            }
            s.degrees.Reverse();
            s.koeff.Reverse();
            for (int i = 0; i < s.koeff.Count; i++)
                s.koeff[i] /= s2.koeff[1];
            return s;
        }

        public void Collect()
        {
            for(int i = 0;i < degrees.Count;i++)
                for(int j = 0;j < degrees.Count - i - 1;j++)
                    if (degrees[j] > degrees[j + 1])
                    {
                        int d = degrees[j];
                        degrees[j] = degrees[j + 1];
                        degrees[j + 1] = d;
                        double t = koeff[i];
                        koeff[i] = koeff[i + 1];
                        koeff[i + 1] = t;
                    }
            List<double> s1 = new List<double>();
            List<int> s2 = new List<int>();
            for (int i = 0; i < degrees.Count; i++)
            {
                s2.Add(degrees[i]);
                s1.Add(koeff[i]);
                for (; (i < degrees.Count - 1) && (degrees[i] == degrees[i + 1]); i++)
                    s1[s1.Count - 1] += koeff[i + 1];
            }
            degrees = s2;
            koeff = s1;
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0, j = 0; i < degrees.Count; i++)
            {
                for (; j < degrees[i]; j++)
                    s += "0 ";
                s += koeff[i].ToString() + ' ';
                j++;
            }
            return s;
        }
    }
}
