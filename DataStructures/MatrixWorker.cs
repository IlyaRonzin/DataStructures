using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class MatrixWorker
    {   
        public int[,] matrix {  get; set; }
        // Конструктор для заполнения матрицы n x m данными с клавиатуры (заполнение по столбцам)
        public MatrixWorker(int n, int m)
        {
            matrix = new int[n, m];
            Console.WriteLine("Введите элементы матрицы (заполнение по столбцам):");
            for (int col = 0; col < m; col++)
            {
                for (int row = 0; row < n; row++)
                {
                    int value;
                    while (true)
                    {
                        Console.Write($"Элемент [{row}, {col}]: ");
                        string input = Console.ReadLine();

                        if (int.TryParse(input, out value))
                        {
                            matrix[row, col] = value;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Ошибка: введите целое число.");
                        }
                    }
                }
            }
        }
        // Конструктор для заполнения матрицы n x n случайными числами, зависящими от побочной диагонали
        public MatrixWorker(int n)
        {
            matrix = new int[n, n];
            Random rand = new Random();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i + j < n - 1) // Над побочной диагональю
                    {
                        matrix[i, j] = rand.Next(-12, 4566);
                    }
                    else // На побочной диагонали и ниже
                    {
                        matrix[i, j] = rand.Next(-1024, 1025);
                    }
                }
            }
        }
        //Конструктор для заполнения матрицы лесенкой
        public MatrixWorker(int n, bool isStairs)
        {
            matrix = new int[n, n];
            int value = 1;

            for (int sum = 2 * (n - 1); sum >= 0; sum--)
            {
                for (int i = Math.Min(n - 1, sum); i >= 0; i--)
                {
                    int j = sum - i;
                    if (j >= 0 && j < n)
                    {
                        // Проверка, что элемент находится на побочной диагонали или ниже
                        if (i + j >= n - 1)
                        {
                            matrix[i, j] = value++;
                        }
                        else
                        {
                            matrix[i, j] = 0;
                        }
                    }
                }
            }
        }

        //Найти подмассив 3*3 с максимальной суммой
        public int[,] FindMaxSubArrayWithMaxSum()
        {
            int maxSum = int.MinValue;
            int[,] maxSubArray = new int[3, 3];

            for (int i = 0; i < matrix.GetLength(0) - 2; i++)
            {
                for (int j = 0; j < matrix.GetLength(1) - 2; j++)
                {
                    int currentSum = 0;
                    int[,] currentSubArray = new int[3, 3];

                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            currentSubArray[k, l] = matrix[i + k, j + l];
                            currentSum += currentSubArray[k, l];
                        }
                    }

                    if (currentSum > maxSum)
                    {
                        maxSum = currentSum;
                        maxSubArray = currentSubArray;
                    }
                }
            }

            return maxSubArray;
        }

        // Переопределение метода ToString для вывода матрицы
        public override string ToString()
        {
            var output = new System.Text.StringBuilder();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    output.Append($"{matrix[i, j],5} ");
                }
                output.AppendLine();
            }
            return output.ToString();
        }

        // Метод для сложения двух матриц
        public static MatrixWorker operator +(MatrixWorker a, MatrixWorker b)
        {
            if (a.matrix.GetLength(0) != b.matrix.GetLength(0) || a.matrix.GetLength(1) != b.matrix.GetLength(1))
            {
                throw new InvalidOperationException("Матрицы должны быть одинакового размера для сложения.");
            }

            int rows = a.matrix.GetLength(0);
            int columns = a.matrix.GetLength(1);
            var result = new MatrixWorker(rows, columns, true);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    result.matrix[i, j] = a.matrix[i, j] + b.matrix[i, j];
                }
            }
            return result;
        }

        // Метод для умножения матрицы на число
        public static MatrixWorker operator *(MatrixWorker a, int scalar)
        {
            int rows = a.matrix.GetLength(0);
            int columns = a.matrix.GetLength(1);
            var result = new MatrixWorker(rows, columns, true);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    result.matrix[i, j] = a.matrix[i, j] * scalar;
                }
            }
            return result;
        }
        public static MatrixWorker operator *(int scalar, MatrixWorker a)
        {
            int rows = a.matrix.GetLength(0);
            int columns = a.matrix.GetLength(1);
            var result = new MatrixWorker(rows, columns, true);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    result.matrix[i, j] = a.matrix[i, j] * scalar;
                }
            }
            return result;
        }

        // Переопределение оператора ~ для транспонирования матрицы
        public static MatrixWorker operator ~(MatrixWorker a)
        {
            int rows = a.matrix.GetLength(0);
            int columns = a.matrix.GetLength(1);
            var result = new MatrixWorker(columns, rows, true);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    result.matrix[j, i] = a.matrix[i, j];
                }
            }
            return result;
        }

        // Конструктор для внутреннего использования
        private MatrixWorker(int rows, int columns, bool zero)
        {
            matrix = new int[rows, columns];
        }
    }

}
