using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WebApplication.Service.lib_boolean_funcs
{
    class BooleanVector
    {
        protected bool[] vector;
        protected static readonly Random random;

        public int Length { get { return vector.Length; } }

        public bool this[int i] { get { return vector[i]; } }

        public BooleanVector(int length)
        {
            this.vector = RandomBooleanVector(length);
        }

        public BooleanVector(int number, int length)
        {
            this.vector = new bool[length];
            for (int pos = 0; pos < length; pos++)
            {
                vector[length - pos - 1] = (((number >> pos) & 1) == 1);
            }
        }

        //public BooleanVector(bool[] array)
        //{
        //    this.vector = new bool[array.Length];
        //    array.CopyTo(this.vector, 0);
        //}

        public BooleanVector(params bool[] array)
        {
            this.vector = (bool[])array.Clone();
        }

        static BooleanVector()
        {
            random = new Random(DateTime.Now.Millisecond);
        }

        public static bool[] RandomBooleanVector(int length)
        {
            bool[] vector = new bool[length];
            for (uint i = 0; i < length; i++)
                vector[i] = (random.Next(0, 2) == 1);
            return vector;
        }

        public bool[] ToBoolArray()
        {
            return (bool[])vector.Clone();
        }

        public static int Distance(BooleanVector BooleanVector1, BooleanVector BooleanVector2)
        {
            int count = 0;
            for (int i = 0; i < BooleanVector1.Length; i++)
                if (BooleanVector1[i] != BooleanVector2[i])
                    count++;
            return count;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(vector.Length + 2);
            sb.Append('(');
            for (int i = 0; i < vector.Length; i++)
                sb.Append(vector[i] ? '1' : '0');
            sb.Append(')');
            return sb.ToString();
        }

        public static bool operator <(BooleanVector vector1, BooleanVector vector2)
        {
            if (vector1.Length != vector2.Length)
                return false;
            int count = 0;
            for (int i = 0; i < vector1.Length; i++)
                if (vector1[i] && !vector2[i])
                    return false;
                else if (!vector1[i] && vector2[i])
                    count++;
            return count > 0;
        }

        public static bool operator >(BooleanVector vector1, BooleanVector vector2)
        {
            return vector2 < vector1;
        }

        public static bool operator ==(BooleanVector vector1, BooleanVector vector2)
        {
            if (vector1.Length != vector2.Length)
                return false;
            bool equal = true;
            for (int i = 0; equal && i < vector1.Length; i++)
                equal = (vector1[i] == vector2[i]);
            return equal;
        }

        public static bool operator !=(BooleanVector vector1, BooleanVector vector2)
        {
            return !(vector1 == vector2);
        }

        public static bool operator <=(BooleanVector vector1, BooleanVector vector2)
        {
            return vector1 == vector2 || vector1 < vector2;
        }

        public static bool operator >=(BooleanVector vector1, BooleanVector vector2)
        {
            return vector2 <= vector1;
        }

        public static bool Incomparable(BooleanVector vector1, BooleanVector vector2)
        {
            return !((vector1 <= vector2) || (vector2 <= vector1));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is BooleanVector)
                return this == (obj as BooleanVector);
            else
                return false;
        }

    }
}