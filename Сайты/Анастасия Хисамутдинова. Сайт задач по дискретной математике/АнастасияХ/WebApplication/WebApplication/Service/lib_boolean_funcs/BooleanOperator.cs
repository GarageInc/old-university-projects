using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WebApplication.Service.lib_boolean_funcs
{
    enum BooleanOperations { NOT, AND, OR, IMPLICATION, XOR, EQUIVALENCE, SHEFFER_STROKE, PIERCE_ARROW, IDENTITY, CONSTANT }

    abstract class BooleanFormula
    {
        protected static readonly string[] LaTeXOperations = { "\\neg", " ", "\\vee ", "\\rightarrow ", "\\oplus ", "\\sim ", "\\mid ", "\\downarrow " };
        protected BooleanOperations type;

        public BooleanOperations Type { get { return type; } }

        protected List<BooleanVariable> variables = null;

        public abstract List<BooleanVariable> Variables
        {
            get;
            //set 
            //{
            //    if (variables == null)
            //        variables = new List<Variable>();
            //    foreach (Variable variable in value)
            //        variables.Add(variable);
            //} 
        }

        public abstract IEnumerable SubFormulas();

        public abstract int Depth { get; }

        static readonly Random random;

        static BooleanFormula()
        {
            random = new Random(DateTime.Now.Millisecond);
        }

        protected BooleanFunction function = null;

        public BooleanFunction RealizedFunction
        {
            get
            {
                if (function == null)
                    function = new BooleanFunction(FormulaValues());
                return function;
            }
        }

        protected Diagram diagram = null;

        public Diagram CharacteristicDiagram
        {
            get
            {
                if (diagram == null)
                    diagram = new Diagram(this);
                return diagram;
            }
        }

        public static BooleanFormula RandomFormula(int variableCount, int sizeBound, int depthBound)
        {
            if (depthBound > sizeBound)
                return SizeBoundedRandomBooleanFormula(sizeBound, variableCount);
            return DepthBoundedRandomBooleanFormula(depthBound, variableCount);
        }

        public static BooleanFormula DepthBoundedRandomBooleanFormula(int depth, int variableCount)
        {
            if (depth == 0)
                return new BooleanVariable("x", random.Next(1, variableCount + 1));
            else
            {
                int type = random.Next(8);
                BooleanFormula argument;
                if (type == 0)//запрет последовательных отрицаний
                {
                    do
                        argument = DepthBoundedRandomBooleanFormula(depth - 1, variableCount);
                    while (argument is UnaryOperation);
                    return new UnaryOperation(argument);
                }
                if (depth == 1)//запрет одинаковых термов у связки
                {
                    int index1 = random.Next(1, variableCount + 1);
                    int index2 = index1 + random.Next(1, variableCount);
                    if (index2 > variableCount)
                        index2 = index2 % variableCount;
                    return new BinaryOperation((BooleanOperations)type, new BooleanVariable("x", index1), new BooleanVariable("x", index2));
                }
                byte coin = (byte)random.Next(2);
                return new BinaryOperation((BooleanOperations)type,
                                            DepthBoundedRandomBooleanFormula(coin == 1 ? (depth - 1) : random.Next(depth), variableCount),
                                            DepthBoundedRandomBooleanFormula(coin == 0 ? (depth - 1) : random.Next(depth), variableCount));
            }
        }

        public static BooleanFormula SizeBoundedRandomBooleanFormula(int size, int variableCount)
        {
            if (size == 0)
                return new BooleanVariable("x", random.Next(1, variableCount + 1));
            else
            {
                int type = random.Next(8);
                BooleanFormula argument;
                if (type == 0)//запрет последовательных отрицаний
                {
                    do
                        argument = SizeBoundedRandomBooleanFormula(size, variableCount);
                    while (argument is UnaryOperation);
                    return new UnaryOperation(argument);
                }
                if (size == 1)//запрет одинаковых термов у связки
                {
                    int index1 = random.Next(1, variableCount + 1);
                    int index2 = index1 + random.Next(1, variableCount);
                    if (index2 > variableCount)
                        index2 = index2 % variableCount;
                    return new BinaryOperation((BooleanOperations)type, new BooleanVariable("x", index1), new BooleanVariable("x", index2));
                }
                int leftSize = random.Next(size); //выбор размера подформул
                return new BinaryOperation((BooleanOperations)type,
                                            SizeBoundedRandomBooleanFormula(leftSize, variableCount),
                                            SizeBoundedRandomBooleanFormula(size - 1 - leftSize, variableCount));
            }
        }

        public override string ToString() { return type.ToString(); }

        public abstract string ToLaTeXString();

        public abstract bool FormulaValue(BooleanFormulaInput input);

        public static bool[] FormulaValues(BooleanFormula formula)
        {
            return FormulaValues(formula, formula.Variables);
        }

        public static bool[] FormulaValues(BooleanFormula formula, List<BooleanVariable> variableList)
        {
            int variableCount = variableList.Count;
            if (variableCount == 0)
                variableCount = 1;
            int length = 1 << variableCount;
            bool[] values = new bool[length];
            bool[] input = new bool[variableCount];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < variableCount; j++)
                    input[variableCount - j - 1] = ((i >> j) & 1) == 1;
                values[i] = formula.FormulaValue(new BooleanFormulaInput(input, variableList));
            }
            return values;
        }

        public bool[] FormulaValues()
        {
            return BooleanFormula.FormulaValues(this);
        }

        public bool[] FormulaValues(List<BooleanVariable> variableList)
        {
            return BooleanFormula.FormulaValues(this, variableList);
        }

        /// <summary>
        /// Построение полинома Жегалкина для булевой функции.
        /// </summary>
        /// <param name="function">Булева функция</param>
        /// <returns>Полином Жегалкина</returns>
        public static BooleanFormula ZhegalkinPolynomial(BooleanFunction function)
        {
            return ZhegalkinPolynomial(BooleanFunction.ZhegalkinVector(function));
        }

        /// <summary>
        /// Построение полинома Жегалкина по набору коэффициентов.
        /// </summary>
        /// <param name="coefficients">Коэффициенты полинома Жегалкина</param>
        /// <returns>Формула для заданного набора коэффициентов</returns>
        public static BooleanFormula ZhegalkinPolynomial(bool[] coefficients)
        {
            if (coefficients.Length == 0)
                throw new Exception("Длина набора коэффициентов должна быть степенью двойки!");
            int variableCount = (int)Math.Log(coefficients.Length, 2);
            if (variableCount == 0)
                variableCount = 1;
            List<BooleanVariable> variables = new List<BooleanVariable>();
            for (int i = 0; i < variableCount; i++)
                variables.Add(new BooleanVariable("x", i + 1));
            return ZhegalkinPolynomial(coefficients, variables);
        }

        /// <summary>
        /// Построение полинома Жегалкина по набору коэффициентов с учетом порядка переменных.
        /// </summary>
        /// <param name="coefficients">Коэффициенты полинома Жегалкина</param>
        /// <param name="variables">Набор переменных в заданном формулой порядке.</param>
        /// <returns>Формула для заданного набора коэффициентов</returns>
        public static BooleanFormula ZhegalkinPolynomial(bool[] coefficients, List<BooleanVariable> variables)
        {
            if (coefficients.Length == 0)
                throw new Exception("Длина набора коэффициентов должна быть степенью двойки!");
            BooleanFormula result;
            //создать список монотонных конъюнкций
            List<BooleanFormula> monomials = new List<BooleanFormula>();
            for (int index = 1; index < coefficients.Length; index++)
                //если коэффициент равен 1 создаем моном
                if (coefficients[index])
                {
                    result = null;
                    //определяем список переменных, входящих в моном
                    for (int j = 0; j < variables.Count; j++)
                        if (((index >> j) & 1) == 1)
                            //(variableCount - j)-ая переменная из списка variables входит в моном
                            if (result == null)
                                result = variables[variables.Count - j - 1].ShallowClone();
                            else
                                result = new BinaryOperation(BooleanOperations.AND, variables[variables.Count - j - 1].ShallowClone(), result);
                    monomials.Add(result);
                }
            //отсортировать по длине
            monomials.Sort((f1, f2) => f1.Depth.CompareTo(f2.Depth));
            //свободный член как вырожденный случай монотонной конъюнкции обрабатываем отдельно
            if (coefficients[0])
                monomials.Insert(0, new BooleanConstant(true));
            //если список пуст, добавить константу 0
            if (monomials.Count == 0)
                monomials.Add(new BooleanConstant(false));
            //соединить связкой XOR
            result = monomials[monomials.Count - 1];
            for (int i = monomials.Count - 2; i >= 0; i--)
                result = new BinaryOperation(BooleanOperations.XOR, result, monomials[i]);
            return result;
        }

        /// <summary>
        /// Построение полинома Жегалкина по формуле.
        /// </summary>
        /// <param name="formula">Исходная формула</param>
        /// <returns>Полином Жегалкина</returns>
        public static BooleanFormula ZhegalkinPolynomial(BooleanFormula formula)
        {
            return ZhegalkinPolynomial(BooleanFunction.ZhegalkinVector(formula.RealizedFunction), formula.Variables);
        }

        /// <summary>
        /// Совершенная ДНФ для заданной функции.
        /// </summary>
        /// <param name="function">Булева функция</param>
        /// <returns>Совершенная ДНФ булевой функции</returns>
        public static BooleanFormula PerfectDNF(BooleanFunction function)
        {
            List<BooleanVariable> variables = new List<BooleanVariable>();
            for (int i = 0; i < function.VariableCount; i++)
                variables.Add(new BooleanVariable("x", i + 1));
            return PerfectDNF(function, variables);
        }

        /// <summary>
        /// Совершенная ДНФ для булевой функции на заданном наборе переменных.
        /// </summary>
        /// <param name="function">Булева функция</param>
        /// <param name="variables">Набор переменных, определяющий порядок и имена переменных</param>
        /// <returns>Совершенная ДНФ булевой функции</returns>
        public static BooleanFormula PerfectDNF(BooleanFunction function, List<BooleanVariable> variables)
        {
            if (function.VariableCount > variables.Count)
                throw new Exception("Недостаточно переменных в списке!");
            BooleanFormula result;
            //создать список элементарных конъюнкций
            List<BooleanFormula> conjunctions = new List<BooleanFormula>();
            int length = 1 << function.VariableCount;
            for (int index = 0; index < length; index++)
                //если значение на наборе равно 1 создаем эл. конъюнкцию
                if (function[index])
                {
                    //отдельно последняя переменная (инициализация)
                    if ((index & 1) == 1)
                        result = variables[variables.Count - 1].ShallowClone();
                    else
                        result = new UnaryOperation(variables[variables.Count - 1].ShallowClone());
                    //определяем какие переменные с отрицанием, какие - без
                    for (int j = 1; j < function.VariableCount; j++)
                        if (((index >> j) & 1) == 1)
                            //(variableCount - j)-ая переменная из списка variables входит без отрицания
                            result = new BinaryOperation(BooleanOperations.AND, variables[variables.Count - j - 1].ShallowClone(), result);
                        else//с отрицанием
                            result = new BinaryOperation(BooleanOperations.AND, new UnaryOperation(variables[variables.Count - j - 1].ShallowClone()), result);
                    conjunctions.Add(result);
                }
            //если список пуст, добавить константу 0
            if (conjunctions.Count == 0)
                conjunctions.Add(new BooleanConstant(false));
            //соединить связкой OR
            result = conjunctions[conjunctions.Count - 1];
            for (int i = conjunctions.Count - 2; i >= 0; i--)
                result = new BinaryOperation(BooleanOperations.OR, conjunctions[i], result);
            return result;
        }

        /// <summary>
        /// Построение совершенной ДНФ по формуле.
        /// </summary>
        /// <param name="formula">Исходная формула</param>
        /// <returns>Совершенная ДНФ</returns>
        public static BooleanFormula PerfectDNF(BooleanFormula formula)
        {
            return PerfectDNF(formula.RealizedFunction, formula.Variables);
        }

        /// <summary>
        /// Совершенная КНФ для заданной функции.
        /// </summary>
        /// <param name="function">Булева функция</param>
        /// <returns>Совершенная КНФ булевой функции</returns>
        public static BooleanFormula PerfectCNF(BooleanFunction function)
        {
            List<BooleanVariable> variables = new List<BooleanVariable>();
            for (int i = 0; i < function.VariableCount; i++)
                variables.Add(new BooleanVariable("x", i + 1));
            return PerfectCNF(function, variables);
        }

        /// <summary>
        /// Совершенная КНФ для булевой функции на заданном наборе переменных.
        /// </summary>
        /// <param name="function">Булева функция</param>
        /// <param name="variables">Набор переменных, определяющий порядок и имена переменных</param>
        /// <returns>Совершенная КНФ булевой функции</returns>
        public static BooleanFormula PerfectCNF(BooleanFunction function, List<BooleanVariable> variables)
        {
            if (function.VariableCount > variables.Count)
                throw new Exception("Недостаточно переменных в списке!");
            BooleanFormula result;
            //создать список элементарных конъюнкций
            List<BooleanFormula> disjunctions = new List<BooleanFormula>();
            int length = 1 << function.VariableCount;
            for (int index = 0; index < length; index++)
                //если значение на наборе равно 1 создаем эл. конъюнкцию
                if (!function[index])
                {
                    //отдельно последняя переменная (инициализация)
                    if ((index & 1) == 0)
                        result = variables[variables.Count - 1].ShallowClone();
                    else
                        result = new UnaryOperation(variables[variables.Count - 1].ShallowClone());
                    //определяем какие переменные с отрицанием, какие - без
                    for (int j = 1; j < function.VariableCount; j++)
                        if (((index >> j) & 1) == 0)
                            //(variableCount - j)-ая переменная из списка variables входит без отрицания
                            result = new BinaryOperation(BooleanOperations.OR, variables[variables.Count - j - 1].ShallowClone(), result);
                        else//с отрицанием
                            result = new BinaryOperation(BooleanOperations.OR, new UnaryOperation(variables[variables.Count - j - 1].ShallowClone()), result);
                    disjunctions.Add(result);
                }
            //если список пуст, добавить константу 1
            if (disjunctions.Count == 0)
                disjunctions.Add(new BooleanConstant(true));
            //соединить связкой AND
            result = disjunctions[disjunctions.Count - 1];
            for (int i = disjunctions.Count - 2; i >= 0; i--)
                result = new BinaryOperation(BooleanOperations.AND, disjunctions[i], result);
            return result;
        }

        /// <summary>
        /// Построение совершенной КНФ по формуле.
        /// </summary>
        /// <param name="formula">Исходная формула</param>
        /// <returns>Совершенная КНФ</returns>
        public static BooleanFormula PerfectCNF(BooleanFormula formula)
        {
            return PerfectCNF(formula.RealizedFunction, formula.Variables);
        }

        public BooleanFormula GetZhegalkinPolynomial() { return ZhegalkinPolynomial(this); }

        public abstract BooleanFormula Substitute(List<BooleanVariable> variables, List<BooleanFormula> subformulas);

        public const string NULL_SUBFORMULA = "The subformula cannot be null!";

        public static string ToPGFDiagram(BooleanFormula formula)
        {
            Diagram diagram = new Diagram(formula);
            return diagram.ToPGFString();
        }

        public string ToPGFDiagram()
        {
            return ToPGFDiagram(this);
        }

        public abstract BooleanFormula DeepClone();

        public BooleanFormula ShallowClone()
        {
            return (BooleanFormula)this.MemberwiseClone();
        }

        public class Diagram
        {
            protected BooleanFormula formula;
            protected Node[,] nodes; //таблица всех вершин диаргаммы
            protected Node root = null;//корень дерева, соответствующего диаграмме
            protected int depth = 0;
            protected int width = 0;

            public Diagram(BooleanFormula formula)
            {
                this.formula = formula;
                depth = formula.Depth + 1;
                root = Measure(formula, 0);
                width = root.Width * 2;
                nodes = new Node[depth, width * 2];
                Arrange();

            }

            /// <summary>
            /// Фаза измерения параметров диаграммы (ширины уровней) и создание информационного дерева,
            /// повторяющего структуру формулы.
            /// Для каждой подформулы создается узел, содержащий информацию о ней, включая необходимую ширину
            /// соответствующей ей части диаграммы. Т.о., в корне будет храниться ширина всего дерева до
            /// уплотнения.
            /// </summary>
            /// <param name="formula">Обрабатываемая подформула</param>
            /// <param name="depth">Глубина подформулы</param>
            /// <returns>Узел дерева, соответствующей формуле formula</returns>
            protected Node Measure(BooleanFormula formula, int depth)
            {
                Node node = new Node(formula, depth);
                if (formula is BooleanFormulaTerm)
                    node.Width = 1;
                else
                {
                    node.Width = 0;
                    foreach (BooleanFormula subFormula in formula.SubFormulas())
                        //Добавляется потомок, соответствующий подформуле, и его ширина добавляется к ширине родителя
                        node.Width += node.AppendChild(Measure(subFormula, depth + 1)).Width;
                }
                return node;
            }

            /// <summary>
            /// Фаза размещения вершин на диаграмме. Вычисляются отступы вершин,
            /// после чего происходит "уплотнение" поддеревьев.
            /// </summary>
            protected void Arrange()
            {
                CalculateIndents(root);
                PushToCenter(root);
            }

            /// <summary>
            /// Рекурсивная процедура вычисления сдвигов для вершин диаграммы.
            /// Дополнительно заполняется таблица вершин диаграммы nodes для
            /// последующего уплотнения.
            /// </summary>
            /// <param name="node">Обрабатываемый узел в диаграмме</param>
            protected void CalculateIndents(Node node)
            {
                node.Indent = (node.ParentNode != null) ? (node.ParentNode.Indent) : 0;//ширина уже построенного дерева сохраняется в поле Indent
                foreach (Node child in node.ChildNodes)
                {
                    CalculateIndents(child);
                    node.Indent += child.Width;
                }
                //отступ равен половине ширины + ширина уже построенного слева дерева (хранится у родителя)
                node.Indent = ((node.ParentNode != null) ? (node.ParentNode.Indent) : 0) + node.Width / 2.0;
                nodes[node.Level, node.PositionInLevel] = node;
            }

            /// <summary>
            /// Рекурсивная процедура уплотнения поддеревьев, которые сдвигаются
            /// ближе к центру дерева, если справа (или слева) достаточно места.
            /// Для анализа используется таблица вершин диаграммы.
            /// </summary>
            /// <param name="node">Обрабатываемый узел в диаграмме (поддерево)</param>            
            protected void PushToCenter(Node node)
            {
                foreach (Node child in node.ChildNodes)
                    PushToCenter(child);
                //разместить посередине между потомками
                //могут появиться вершины с отступом x.25 и x.75
                if (node.ChildNodes.Count == 1)
                {
                    nodes[node.Level, node.PositionInLevel] = null;
                    node.Indent = node.ChildNodes[0].Indent;
                    nodes[node.Level, node.PositionInLevel] = node;
                }
                else if (node.ChildNodes.Count == 2)
                {
                    nodes[node.Level, node.PositionInLevel] = null;
                    node.Indent = (node.ChildNodes[0].Indent + node.ChildNodes[1].Indent) / 2;
                    nodes[node.Level, node.PositionInLevel] = node;
                }
                //сдвинуть поддерево к центру
                if (node.ParentNode != null)
                {
                    double center = node.ParentNode.Indent;
                    if (node.Indent == center)//единственный потомок
                        return;

                    int minDistance = this.width;
                    int neighbourIndex;
                    Node child = node;
                    if (node.Indent < center)//левый потомок: 
                    {
                        //просматриваем всех его правых потомков вычисляя минимум
                        //до центра или ближайшего правого поддерева
                        while (child != null)
                        {
                            for (neighbourIndex = child.PositionInLevel + 1; (neighbourIndex < width) && (nodes[child.Level, neighbourIndex] == null); neighbourIndex++) ;
                            if (neighbourIndex < width)
                                minDistance = Math.Min(neighbourIndex - child.PositionInLevel, minDistance);
                            child = child.LastChild;
                        }
                        if (minDistance > 2)
                            MoveSubTree(node, Math.Min((minDistance - 2) / 2.0, center - 0.5 - node.Indent));
                    }
                    else
                    {
                        while (child != null)
                        {
                            for (neighbourIndex = child.PositionInLevel - 1; (neighbourIndex >= 0) && (nodes[child.Level, neighbourIndex] == null); neighbourIndex--) ;
                            if (neighbourIndex >= 0)
                                minDistance = Math.Min(child.PositionInLevel - neighbourIndex, minDistance);
                            child = child.FirstChild;
                        }
                        if (minDistance > 2)
                            MoveSubTree(node, Math.Max(-(minDistance - 2) / 2.0, center + 0.5 - node.Indent));
                    }
                }
            }

            /// <summary>
            /// Сдвиг поддерева. Обновляется как сдвиг внутри каждой вершины поддерева,
            /// так и записи в таблице всех вершин диаграммы.
            /// </summary>
            /// <param name="node">Корень поддерева</param>
            /// <param name="shift">Размер сдвига</param>
            protected void MoveSubTree(Node node, double shift)
            {
                foreach (Node child in node.ChildNodes)
                    MoveSubTree(child, shift);
                nodes[node.Level, node.PositionInLevel] = null;
                node.Indent += shift;
                nodes[node.Level, node.PositionInLevel] = node;
            }

            /// <summary>
            /// PGF LaTeX разметка для диаграммы
            /// </summary>
            /// <returns>Строка с PGF рисунком</returns>
            public string ToPGFString()
            {
                int minY = -depth;
                int maxX = width;
                //int minX = -maxX;

                //minY--; minX--; maxX++;
                return "\\begin{pgfpicture}{0cm}{" + (minY + 1) + ".3cm}{" + maxX.ToString() + "cm}{0.4cm}\\pgfsetendarrow{\\pgfarrowsingle}" +
                    root.ToPGFString() + "\\end{pgfpicture}";
            }

            public class Node
            {
                protected BooleanFormula formula;
                protected Node parent = null;
                protected List<Node> children;
                protected int width;//ширина поддерева
                protected int level;//уровень диаграммы
                protected double indent; //отступ при отрисовке

                public Node(BooleanFormula formula, int level)
                {
                    this.formula = formula;
                    this.level = level;
                    children = new List<Node>();
                }

                /// <summary>
                /// Добавляет потомка к текущему узлу
                /// </summary>
                /// <param name="child">Новый потомок</param>
                /// <returns>Новый потомок</returns>
                public Node AppendChild(Node child)
                {
                    if ((child != null) && (!children.Contains(child)))
                    {
                        children.Add(child);
                        child.parent = this;
                    }
                    return child;
                }

                public int Level { get { return level; } }

                /// <summary>
                /// Свойство, позволяющее найти вершину в таблице вершин диаграммы.
                /// Вычисляется по отступу Indent, хранимому в вершине.
                /// </summary>
                public int PositionInLevel { get { return (int)(this.indent * 2); } } // В случае отступов с точностью до 0.25 может вызывать коллизии???                

                public List<Node> ChildNodes { get { return children; } }

                public Node ParentNode { get { return parent; } }

                public bool HasChildNodes { get { return children.Count > 0; } }

                public Node FirstChild
                {
                    get
                    {
                        if (children.Count == 0)
                            return null;
                        return children[0];
                    }
                }

                public Node LastChild
                {
                    get
                    {
                        if (children.Count == 0)
                            return null;
                        return children[children.Count - 1];
                    }
                }

                public double Indent { get { return indent; } set { indent = value; } }

                public int Width { get { return width; } set { width = value; } }

                public string PGFIndex { get { return level + "_" + PositionInLevel; } }

                /// <summary>
                /// Вычисление горизонтальной позиции метки вершины на основе
                /// сдвига вершины и ее расположения в дереве.
                /// </summary>
                /// <returns>Горизонтальная позиция метки вершины</returns>
                protected double LabelIndent()
                {
                    bool isLeftChild = false;
                    if (ParentNode != null)
                        isLeftChild = (ParentNode.indent > this.indent);
                    bool isTheOnlyChild = false;
                    if (ParentNode != null)
                        isTheOnlyChild = (ParentNode.ChildNodes.Count == 1);
                    double labelIndent = Math.Truncate(this.indent);
                    if (isLeftChild)
                    {
                        labelIndent = this.indent - 0.25;
                        if (this.formula is BooleanFormulaTerm)
                            labelIndent += 0.25;
                    }
                    else
                    {
                        labelIndent = this.indent + 0.25;
                        if (this.formula is BooleanFormulaTerm)
                            labelIndent -= 0.1;
                    }
                    if (this.formula is BooleanFormulaTerm)
                        if (isTheOnlyChild)
                            labelIndent -= 0.1;
                    return labelIndent;
                }

                /// <summary>
                /// Каждый узел генерирует разметку для соответствующей
                /// вершины диаграммы, рекурсивно запускает процедуру для потомков и соединяется с ними дугами.
                /// </summary>
                /// <returns></returns>
                public string ToPGFString()
                {
                    StringBuilder result = new StringBuilder();
                    //добавить свое описание
                    System.Globalization.CultureInfo formatter = new System.Globalization.CultureInfo("en-US");//для форматирования вещественных чисел через точку
                    result.Append("\\pgfnodecircle{Node" + PGFIndex + "}[fill]{\\pgfxy(" + indent.ToString(formatter) + "," + (-this.level).ToString() + ")}{0.05cm}");
                    //добавить метку\pgfnodebox{NodeBox10a}[virtual]{\pgfxy(4.75,0.85)}{\em p}{1pt}{1pt}
                    string label = (this.formula is BooleanFormulaTerm) ? this.formula.ToLaTeXString() : BooleanFormula.LaTeXOperations[(int)this.formula.type];
                    if (label == " ")
                        label = "\\&";
                    double labelHeight = -this.level + ((this.formula is BooleanFormulaTerm) ? -0.25 : 0.15);
                    result.Append("\\pgfnodebox{NodeBox" + PGFIndex + "a}[virtual]{\\pgfxy(" +
                                    LabelIndent().ToString(formatter) + "," + labelHeight.ToString(formatter) + ")}{$" +
                                    label + "$}{1pt}{1pt}");
                    //то же самое для потомков + связи с ними
                    foreach (Node child in this.ChildNodes)
                    {
                        result.Append(child.ToPGFString());
                        result.Append("\\pgfnodeconnline{Node" + this.PGFIndex + "}{Node" + child.PGFIndex + "}");
                    }
                    return result.ToString();
                }
            }
        }

    }
}