package matrix;

//Класс матрица
public class MyMatrix
	{
		private double[][] matr;
                private double[][] false_matr;
		private int w,h; //Размерность высоты и ширины

		//Функции взять-вернуть параметры матрицы
                public final double[][] getM()
		{
			return matr;
		}

		public final void setM(double[][] value)
		{
			matr = value;
		}
                
		public final int getW()
		{
			return w;
		}

		public final void setW(int value)
		{
			w = value;
		}

		public final int getH()
		{
			return h;
		}

		public final void setH(int value)
		{
			h = value;
		}

		//Конструктор - передается массив двумерный и его размерность(Высота h и ширина w)
		public MyMatrix(double[][] a, int h, int w)
		{
                    matr = new double[h][w];
                    for(int i=0; i<h; i++)
                        for(int j=0; j<w; j++)
                            matr[i][j]=a[i][j];
		    this.w = w;
                    this.h = h;
		}

		//Получаем элемент
		public final double GetElement(int i, int j)
		{
			return matr[i][j];
		}

		//Изменяем элемент
		public final void SetElement(double value, int i, int j)
		{
			matr[i][j] = value;
		}

		//Перегружаем операции сложения матриц
		//По умолчанию - матрицы должны быть одной размерности
		public static MyMatrix OpAddition(MyMatrix a, MyMatrix b)
		{
			//Проверяем размерности матриц
			if (a.getW() != b.getW() || a.getH() != b.getH()) //Нужно только одно соответствие
			{
				throw new IllegalArgumentException("Не совпадают размерности складываемых матриц");
			}
			double[][] new_mass = new double[a.getH()][b.getW()];
			for (int i = 0; i < a.getH() & i < b.getH(); i++)
			{
				for (int j = 0; j < a.getW() & j < b.getW(); j++)
				{
					new_mass[i][j] = a.GetElement(i, j) + b.GetElement(i, j);
				}
			}

			return new MyMatrix(new_mass, a.h, a.w);
		}

		//Перегружаем умножение матриц
		public static MyMatrix OpMultiply(MyMatrix A, MyMatrix B)
		{
			double[][] new_mass;
                        new_mass = new double[A.h][B.w];
			if (A.h != B.getW()) //Нужно только одно соответствие
			{
				throw new IllegalArgumentException("Не совпадают размерности умножаемых матриц");
			}

			for (int i = 0; i < A.getW(); ++i)
			{
				for (int j = 0; j < B.getH(); ++j)
				{
					new_mass[i][j] = 0;
					//Непосредственное суммирование
					for (int k = 0; k < A.w; ++k)
					{ //ТРЕТИЙ цикл, до A.m=B.n
						new_mass[i][j] += A.GetElement(i, k) * B.GetElement(k, j); //Собираем сумму произведений
					}
				}
			}
			return new MyMatrix(new_mass, A.getH(), B.getW());
		}      
                // статический метод для своппинга 2х линейных массивов; 
                private static void swapArray(double[][] matrix, int index, int lengthOfMatrix)
                {
                        for (int i = 0; i < lengthOfMatrix; i++)
                        {
                                double temp = matrix[index+1][i];
                                matrix[index+1][i]=matrix[index][i];
                                matrix[index][i]=temp;                            
                        }
                }
                
                // сортировка матрицы по первым элементам строк модифицированным методом пузырька;
                private static int sortArrayByFirstElenemt(double[][] matrix, int n)
                {
                        int comparisons = 0;// было new int()
                        boolean flagOfSwapping = true;
                        int numberOfIteration = 0;// было new int()
                        while (flagOfSwapping) // цикл выполняется пока есть хотя бы одна перестановка в ходе итерации;
                        {
                                flagOfSwapping = false;
                                for (int i = 0; i < n - 1 - numberOfIteration; ++i)
                                {
                                        if (matrix[i][0] < matrix[i + 1][0])
                                        {
                                                swapArray(matrix, i, n);
                                                flagOfSwapping = true;
                                                ++comparisons;
                                        }
                                }
                                numberOfIteration++;
                        }
                        return comparisons;
                }
                
                // статический метод вывода матрицы на экран;
                private static void printMatrix(double[][] matrix, int n)
                {
                        System.out.println();
                        for (int i = 0; i < n; ++i)
                        {
                                for (int j = 0; j < n; ++j)
                                {
                                        System.out.printf("%0.2f\t", matrix[i][j]);
                                }
                                System.out.println();
                        }
                }
                // статический метод выделения подматрицы размера n - 1;
                private static double[][] generateSubMatrix(double[][] matrix, int n)
                {
                        double[][] subMatrix = new double[n - 1][n - 1];
                        for (int i = 1; i < n; ++i) // выделяется подматрица со строк [1, n);
                        {
                                for (int j = 1; j < n; ++j) // .. cтолбцов [1; n);
                                {
                                        subMatrix[i - 1][j - 1] = matrix[i][j];
                                }
                        }
                        return subMatrix;
                }
                // статический метод проверки первого столбца на равенства нулевому;
                private static boolean allOfFirstElenemtAreZero(double[][] matrix, int n)
                {
                        for (int i = 0; i < n; ++i)
                        {
                                if (matrix[i][0] != 0)
                                {
                                        return false;
                                }
                        }
                        return true;
                }
	
                // статический метод вычисления определителя матрицы;
                public double Determinant(int n)
                {
                        if(h!=w)
                        {
                            throw new IllegalArgumentException("Не квадратная матрица");
                        };;
                        // Самый первый рекурсивный вызов - инициализация
                        if(n==this.h)
                        {
                            false_matr = new double[h][w];
                            for(int i=0; i<h; i++)
                                for(int j=0; j<w; j++)
                                    false_matr[i][j]=matr[i][j];
                        };;
                        
                        if (n == 1) // если матрица первого порядка, то ее определитель есть единственный элемент;
                        {
                                return this.false_matr[0][0]; // возвращаем его;
                        };;
                        
                        if (allOfFirstElenemtAreZero(false_matr, n)) // если есть нулевой столбец, то определитель равен 0;
                        {
                                return 0;
                        };;
                        
                        for (int i = 1; i < n; ++i) // итерация методом Гаусса;
                        {
                                double index = false_matr[i][0] / false_matr[0][0];
                                for (int j = 0; j < n; ++j)
                                {
                                        false_matr[i][j]-=index*false_matr[0][j];
                                }
                        } // где зануляется первый столбец с 1 по n - 1 строки;
                        double a = false_matr[0][0];
                        false_matr=generateSubMatrix(false_matr, n);                        
                        return a*Math.pow(-1, sortArrayByFirstElenemt(false_matr, n-1)) * Determinant(n - 1);
                        // возвращаем определитель рекурсивно;
                }
                
                //Вывод матрицы на экран
		public final void Print()
		{
			for (int i = 0; i < h & i < h; i++)
			{
				for (int j = 0; j < w & j < w; j++)
				//печатаем
				{
					System.out.print(matr[i][j] + "  ");
				}
				System.out.println();
			}
		}
        }

