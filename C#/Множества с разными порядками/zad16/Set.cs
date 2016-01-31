using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Регина
{
    class Set
    {
        protected List<int> elements;

        public Set()
        {
            elements = new List<int>();
        }

        public void Add(int x)
        {
            for (int i = 0; i < elements.Count; i++)
                if (x == elements[i])
                    return;
            elements.Add(x);
        }

        public void Erase(int x)
        {
            elements.Remove(x);
        }

        public List<int> Elements
        {
            get
            {
                return elements;
            }
        }

        public static Set operator |(Set s1, Set s2)
        {
            Set s = new Set();
            int i = 0, j = 0;
            for (; (i < s1.elements.Count) && (j < s2.elements.Count); )
                if (s1.elements[i] < s2.elements[j])
                {
                    s.Add(s1.elements[i]);
                    i++;
                }
                else
                {
                    s.Add(s2.elements[j]);
                    j++;
                }
            for (; i < s1.elements.Count; i++)
                s.Add(s1.elements[i]);
            for (; j < s2.elements.Count; j++)
                s.Add(s2.elements[j]);
            return s;
        }

        public static Set operator &(Set s1, Set s2)
        {
            Set s = new Set();
            for (int i = 0;i < s1.elements.Count; i++)
                if (s2.Find(s1.elements[i]) != -1)
                    s.Add(s1.elements[i]);
            return s;
        }

        public static Set operator /(Set s1, Set s2)
        {
            Set s = new Set();
            for (int i = 0; i < s1.elements.Count; i++)
                if (s2.Find(s1.elements[i]) == -1)
                    s.Add(s1.elements[i]);
            return s;
        }

        public static Set operator +(Set s1, Set s2)
        {
            Set s = new Set();
            s1.elements.Sort();
            s2.elements.Sort();
            int i = 0, j = 0;
            for(;(i < s1.elements.Count) && (j < s2.elements.Count);)
                if (s1.elements[i] < s2.elements[j])
                {
                    s.Add(s1.elements[i]);
                    i++;
                }
                else
                {
                    s.Add(s2.elements[j]);
                    j++;
                }
            for (; i < s1.elements.Count; i++)
                s.Add(s1.elements[i]);
            for (; j < s2.elements.Count;j++)
                s.Add(s2.elements[j]);
            return s;
        }

        public static Set operator *(Set s1, Set s2)
        {
            Set s = new Set();
            s1.elements.Sort();
            s2.elements.Sort();
            int i = 0, j = 0;
            for (;(i < s1.elements.Count) && (j < s2.elements.Count); )
            {
                if (s1.elements[i] == s2.elements[j])
                {
                    s.Add(s1.elements[i]);
                    i++;
                    j++;
                }
                else
                {
                    for (; (j < s2.elements.Count) && (s1.elements[i] > s2.elements[j]); j++);
                    if (s1.elements[i] < s2.elements[j])
                        i++;
                }
            }
            return s;
        }

        public static Set operator -(Set s1, Set s2)
        {
            Set s = new Set();
            s1.elements.Sort();
            s2.elements.Sort();
            int i = 0, j = 0;
            for (; (i < s1.elements.Count) && (j < s2.elements.Count); )
            {
                if (s1.elements[i] == s2.elements[j])
                {
                    i++;
                    j++;
                }
                else
                {
                    for (; (j < s2.elements.Count) && (s1.elements[i] > s2.elements[j]); j++) ;
                    if (s1.elements[i] < s2.elements[j])
                    {
                        s.Add(s1.elements[i]);
                        i++;
                    }
                }
            }
            return s;
        }

        public int Find(int x)
        {
            for (int i = 0; i < elements.Count; i++)
                if (elements[i] == x)
                    return i;
            return -1;
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < elements.Count; i++)
                s += elements[i] + " ";
            return s;
        }
    }
}
