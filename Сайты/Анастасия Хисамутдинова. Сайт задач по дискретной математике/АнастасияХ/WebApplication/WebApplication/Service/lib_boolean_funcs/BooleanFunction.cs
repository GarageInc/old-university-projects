using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WebApplication.Service.lib_boolean_funcs
{
    class BooleanFunction
    {

        public enum Properties { SelfAdjoint = 1, Linear = 2, Monotone = 4 }

        public delegate bool PropertyTest(BooleanFunction function);

        public static readonly List<PropertyTest> CompletenessTests;

        protected int variableCount;
        protected bool[] vector;
        protected BooleanFormula zhegalkinPolynomial = null;
        protected BooleanFormulaSet formulas;
        protected static readonly Random random;

        static BooleanFunction()
        {
            random = new Random(System.DateTime.Now.Millisecond);
            CompletenessTests = new List<PropertyTest>();
            CompletenessTests.Add(x => !x.IsSelfAdjoint());
            CompletenessTests.Add(x => !x.IsLinear());
            CompletenessTests.Add(x => !x.IsMonotone());
            CompletenessTests.Add(x => !x.KeepsConstantZero());
            CompletenessTests.Add(x => !x.KeepsConstantOne());
        }

        //public BooleanFunction(bool[] vector)
        //{
        //    this.variableCount = (int)Math.Log(vector.Length, 2);
        //    formulas = new BooleanFormulaSet();
        //    this.vector = (bool[])vector.Clone();
        //}

        public BooleanFunction(params bool[] vector)
        {
            this.variableCount = (int)Math.Log(vector.Length, 2);
            formulas = new BooleanFormulaSet();
            this.vector = (bool[])vector.Clone();
        }

        public BooleanFunction(int variableCount)
        {
            this.variableCount = variableCount;
            formulas = new BooleanFormulaSet();
            this.vector = BooleanVector.RandomBooleanVector(1 << variableCount);
        }

        public BooleanFunction(int variableCount, bool selfAdjoint, bool linear, bool monotone, bool balanced)
        {
            this.variableCount = variableCount;
            formulas = new BooleanFormulaSet();
            do
                this.vector = BooleanVector.RandomBooleanVector(1 << variableCount);
            while ((selfAdjoint && !IsSelfAdjoint()) || (linear && !IsLinear())
                    || (monotone && !IsMonotone()) || (balanced && !IsBalanced()));
        }

        public BooleanFunction(int variableCount, bool notSelfAdjoint, bool notLinear, bool notMonotone)
        {
            this.variableCount = variableCount;
            formulas = new BooleanFormulaSet();
            do
                this.vector = BooleanVector.RandomBooleanVector(1 << variableCount);
            while ((notSelfAdjoint && IsSelfAdjoint()) || (notLinear && IsLinear())
                    || (notMonotone && IsMonotone()));
        }

        public BooleanFunction(int variableCount, bool keepsZero, bool keepsOne)
        {
            this.variableCount = variableCount;
            formulas = new BooleanFormulaSet();
            do
                this.vector = BooleanVector.RandomBooleanVector(1 << variableCount);
            while ((!keepsZero && KeepsConstantZero()) || (keepsZero && !KeepsConstantZero())
                    || (!keepsOne && KeepsConstantOne()) || (keepsOne && !KeepsConstantOne()));
        }

        public BooleanFunction(int variableCount, int formulaDepth)
        {
            this.variableCount = variableCount;
            BooleanFormula formula = BooleanFormula.DepthBoundedRandomBooleanFormula(formulaDepth, variableCount);
            formulas = new BooleanFormulaSet();
            formulas.Add(formula);
            this.vector = formula.FormulaValues();
        }

        public bool this[int i] { get { return vector[i]; } }

        public int VariableCount { get { return variableCount; } }

        public BooleanFormula ZhegalkinPolynomial
        {
            get
            {
                if (zhegalkinPolynomial == null)
                {
                    zhegalkinPolynomial = BooleanFormula.ZhegalkinPolynomial(BooleanFunction.ZhegalkinVector(this));
                    formulas.Add(zhegalkinPolynomial);
                }
                return zhegalkinPolynomial;
            }
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

        public string ToLaTeXFormulaString(int formulaIndex)
        {
            if (formulas != null)
                if (formulas.Count > formulaIndex)
                    return formulas[formulaIndex].ToLaTeXString();
            return "";
        }

        public string ToLaTeXFormulaString()
        {
            return ToLaTeXFormulaString(0);
        }

        /// <summary>
        /// Рекурсивно преобразует входной вектор в вектор коэффициентов полинома Жегалкина.
        /// </summary>
        /// <param name="vector">Вектор значений функции</param>
        /// <param name="startIndex">Начальный индекс в преобразуемом отрезке вектора</param>
        /// <param name="length">Длина преобразуемого отрезка вектора</param>
        protected static void ZhegalkinVector(bool[] vector, int startIndex, int length)
        {
            if (length > 1)
            {
                int halfLength = length >> 1;
                ZhegalkinVector(vector, startIndex, halfLength);
                ZhegalkinVector(vector, startIndex + halfLength, halfLength);
                for (int i = 0; i < halfLength; i++)
                    vector[startIndex + halfLength + i] ^= vector[startIndex + i];
            }
        }

        /// <summary>
        /// Построение вектора коэффициентов полинома Жегалкина для булевой функции
        /// </summary>
        /// <param name="function">Входная булева функция</param>
        /// <returns>Булевский вектор коэффициентов полинома Жегалкина</returns>
        public static bool[] ZhegalkinVector(BooleanFunction function)
        {
            bool[] result = (bool[])function.vector.Clone();
            ZhegalkinVector(result, 0, result.Length);
            return result;
        }

        public bool EssentiallyDependsOn(int variableIndex)
        {
            variableIndex = variableCount - variableIndex;
            bool isEssential = false;
            for (int j = 0; j < vector.Length; j++)//проверяем пары соседних наборов
                if (((1 << variableIndex) & j) == 0)
                {
                    int neighbour = j | (1 << variableIndex);
                    if (vector[j] != vector[neighbour])
                        isEssential = true;
                }
            return isEssential;
        }

        public bool IsBalanced()
        {
            return IsBalanced(this.vector);
        }

        public static bool IsBalanced(bool[] vector)
        {
            int count = 0;
            foreach (bool b in vector)
                if (b)
                    count++;
            return count * 2 == vector.Length;
        }

        public bool IsLinear()
        {
            return IsLinear(this.vector, this.variableCount);
        }

        public static bool IsLinear(bool[] vector, int variableCount)
        {
            if (!IsBalanced(vector))
                return false;
            //int variableCount = (int)Math.Log(vector.Length, 2);
            for (int i = 0; i < variableCount; i++)//для всех переменных
            {
                bool isEssential = false; //важна существенность
                bool switching = true;//разные значения на всех соседних наборах
                for (int j = 0; j < vector.Length; j++)//проверяем пары соседних наборов
                    if (((1 << i) & j) == 0)
                    {
                        int neighbour = j | (1 << i);
                        if (vector[j] != vector[neighbour])
                            isEssential = true;
                        else
                            switching = false;
                    }
                if (isEssential && !switching)
                    return false;
            }
            return true;
        }

        public bool IsSelfAdjoint()
        {
            return IsSelfAdjoint(this.vector);
        }

        public static bool IsSelfAdjoint(bool[] vector)
        {
            for (int i = 0; i < vector.Length / 2; i++)
                if (vector[i] == vector[vector.Length - 1 - i])
                    return false;
            return true;
        }

        public bool IsMonotone()
        {
            return IsMonotone(vector, variableCount);
        }

        public static bool IsMonotone(bool[] vector, int variableCount)
        {
            for (int i = 0; i < variableCount; i++)//для всех переменных
                for (int j = 0; j < vector.Length; j++)//проверяем монотонность на парах соседних наборов
                    if (((1 << i) & j) == 0)//0 на i-й позиции
                        if (vector[j] && !vector[j | (1 << i)])//нарушение на соседних наборах
                            return false;
            return true;
        }

        public bool KeepsConstantZero()
        {
            return !vector[0];
        }

        public bool KeepsConstantOne()
        {
            return vector[vector.Length - 1];
        }

        public string ToLaTeXBooleanTable()
        {
            string table = "\\begin{array}{";
            for (int i = 1; i <= variableCount; table += "c", i++) ;
            table += "cc}";
            for (int i = 1; i <= variableCount; table += "x_" + i.ToString() + "&", i++) ;
            table += "& f\\\\\\\\";
            for (int i = 0; i < vector.Length; i++)
            {
                for (int j = variableCount - 1; j >= 0; j--)
                    table += ((i >> j) & 1) + " & ";
                table += "& " + (vector[i] ? "1" : "0") + "\\\\";
            }
            return table + "\\end{array}";
        }

    }
}