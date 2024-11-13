using System;
using System.Diagnostics;
using System.Globalization;

namespace DataStructures
{
    public class Program
    {
        private static List<MatrixWorker> matrices = new List<MatrixWorker>();
        public static void Main()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Добро пожаловать в систему работы с матрицами!");
                Console.WriteLine("1. Создать матрицу n*m (ввод с клавиатуры).");
                Console.WriteLine("2. Создать матрицу n*n (случайные числа относительно побочной диагонали).");
                Console.WriteLine("3. Создать матрицу n*n (лесенка).");
                Console.WriteLine("4. Найти максимальный подмассив 3*3 в матрице.");
                Console.WriteLine("5. Найти значение выражения Ат+2*В+Ст.");
                Console.WriteLine("6. Найти все компоненты исходного бинарного файла, которые делятся на m и не делятся на n.");
                Console.WriteLine("7. Найти разность максимального и минимального элементов бинарного файла.");
                Console.WriteLine("8. Найти минимальный элемент файла.");
                Console.WriteLine("9. Переписать в другой файл строки, начинающиеся с заданного символа.");
                Console.WriteLine("10. Показать все матрицы.");
                Console.WriteLine("11. Выйти");
                Console.Write("Выберите действие: ");

                // Считывание выбора пользователя
                string choice = Console.ReadLine();

                // Выполнение действия в зависимости от выбора пользователя
                switch (choice)
                {
                    case "1":
                        CreateMatrixNxMKeyboardInput();
                        break;
                    case "2":
                        CreateMatrixNxNRandom();
                        break;
                    case "3":
                        CreateMatrixNxNStairs();
                        break;
                    case "4":
                        FindMaxSubArrayThreeWithMaxSum();
                        break;
                    case "5":
                        FindValueOfExpression();
                        break;
                    case "6":
                        FindInBinaryFile();
                        break;
                    case "7":
                        FindDiferenceMaxMinTxt();
                        break;
                    case "8":
                        FindMinElementInTxt();
                        break;
                    case "9":
                        FindStrings();
                        break;
                    case "10":
                        ShowAllMatrices();
                        break;
                    case "11":
                        Console.WriteLine("Выход...");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }

                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }

        //Ввод строк в файл
        public static void CreateFileTxt(string path)
        {
            StreamWriter sw = new StreamWriter(path);
            string s = "";
            while (s != "0")
            {
                s = Console.ReadLine();
                sw.WriteLine(s);
            }
            sw.Close();
        }

        //Переписать в другой файл строки, начинающиеся с заданного символа. 
        public static void FindStrings()
        {
            string InputFile = "InputString.txt";
            string OutputFile = "OutputString.txt";
            Console.WriteLine("Введите файл");
            CreateFileTxt(InputFile);
            var m = ReadString("Введите символ m: ");
            char zz;
            try
            {
                zz = Convert.ToChar(m);
            }
            catch
            {
                Console.WriteLine("m Не является символом");
                throw;
            }
            FileWorker.FindStrings(InputFile, OutputFile, zz);
            PrintTxtFile(OutputFile);
        }

        //Найти минимальный элемент файла
        public static void FindMinElementInTxt()
        {
            string originalTxt = "Original.txt";
            Console.WriteLine("Найти минимальный элемент файла:");
            Console.WriteLine("Рандомный исходный файл:");
            GenerateRandomData(originalTxt);
            PrintTxtFile(originalTxt);
            int min = FileWorker.FindMinValueInFile(originalTxt);
            Console.WriteLine($"Минимальный элемент: {min}");
        }

        //Запись рандомных чисел в несколько строк в файл
        public static void GenerateRandomData(string filePath)
        {
            Random rand = new Random();
            int numberOfLines = rand.Next(1, 11); // Генерация случайного количества строк (от 1 до 10)

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < numberOfLines; i++)
                {
                    int numbersInLine = rand.Next(1, 11); // Генерация случайного количества чисел в строке (от 1 до 10)
                    for (int j = 0; j < numbersInLine; j++)
                    {
                        writer.Write(rand.Next(1, 101)); // Записываем случайное целое число от 1 до 100
                        if (j < numbersInLine - 1) writer.Write(" "); // Добавляем пробел между числами
                    }
                    writer.WriteLine(); // Переход на новую строку
                }
            }
        }

        //Найти разность максимального и минимального элементов. 
        private static void FindDiferenceMaxMinTxt()
        {
            string originalFile = "data.txt";
            int diference;
            Console.WriteLine("Найти разность максимального и минимального элементов файла.");
            int num = ReadPositiveInt("Введите количество чисел в исходном файле:");
            FillFileWithRandomDataTxt(originalFile, num);
            diference = FileWorker.GetDifferenceMaxMin(originalFile);
            Console.WriteLine("Исходный файл:");
            PrintTxtFile(originalFile);
            Console.WriteLine($"Разница максимального и минимального элемента = {diference}");
        }

        //Заполнение txt рандомными числами
        public static void FillFileWithRandomDataTxt(string path, int numNumbers)
        {
            Random randomNumber = new Random();
            using (StreamWriter writer = new StreamWriter(path))
            {
                for (int i = 0; i < numNumbers; i++)
                {
                    writer.WriteLine(randomNumber.Next(1, 100)); // Случайное число от 1 до 99
                }
            }
        }


        // Найти все компоненты исходного бинарного файла, которые делятся на m и не делятся на n.
        private static void FindInBinaryFile()
        {
            string originalFile = "data.bin";
            string newFile = "filteredData.bin";
            Console.WriteLine("Найти все компоненты исходного бинарного файла, которые делятся на m и не делятся на n:");
            int m = ReadInt("Введите число m:");
            int n = ReadInt("Введите число n:");
            int num = ReadPositiveInt("Введите количество чисел в исходном файле:");
            FileWorker.FillFileWithRandomData(originalFile, num);
            FileWorker.FilterData(originalFile, newFile, m, n);

            Console.WriteLine("Фильтрация произведена.");
            Console.WriteLine("Исходный файл:");
            PrintBinFile(originalFile);
            Console.WriteLine("Результирующий файл");
            PrintBinFile(newFile);
        }

        //Вывод txt файла
        private static void PrintTxtFile(string txtFile)
        {
            using (var reader = new StreamReader(txtFile))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    // Попытка преобразовать строку в целое число, если это необходимо
                    if (int.TryParse(line, out int value))
                    {
                        Console.WriteLine(value);
                    }
                    else
                    {
                        // Если строка не является числом, выводим саму строку
                        Console.WriteLine(line);
                    }
                }
            }
        }


        //Вывод бинарного файла
        private static void PrintBinFile(string binFile)
        {
            using (var reader = new BinaryReader(File.Open(binFile, FileMode.Open)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    // Чтение данных. Например, если это целые числа
                    int value = reader.ReadInt32();
                    Console.WriteLine($"{value}");
                }
            }
        }

        //Найти значение выражения Ат+2*В+Ст
        private static void FindValueOfExpression()
        {
            Console.WriteLine("Найти значение выражения A^T + 2 * B + C:");
            var validMatrices = matrices.Where(m => m.matrix.GetLength(0) == m.matrix.GetLength(1)).ToList();
            if (validMatrices.Count < 3)
            {
                Console.WriteLine("Ошибка: недостаточно матриц одинакового размера для вычисления выражения.");
                return;
            }
            Console.WriteLine("Выберите 3 матрицы одинакового размера:");
            ShowAllMatrices();

            // Выбор матриц A, B и C
            int aIndex = ReadIndex("Введите номер матрицы A:") - 1;

            int bIndex = ReadIndex("Введите номер матрицы B:") - 1;

            int cIndex = ReadIndex("Введите номер матрицы C:") - 1;

            // Проверка на одинаковые размеры матриц
            var matrixA = matrices[aIndex];
            var matrixB = matrices[bIndex];
            var matrixC = matrices[cIndex];

            if (matrixA.matrix.GetLength(0) != matrixB.matrix.GetLength(0) || matrixA.matrix.GetLength(1) != matrixB.matrix.GetLength(1) ||
                matrixA.matrix.GetLength(0) != matrixC.matrix.GetLength(0) || matrixA.matrix.GetLength(1) != matrixC.matrix.GetLength(1))
            {
                Console.WriteLine("Ошибка: все матрицы должны иметь одинаковые размеры.");
                return;
            }

            // Вычисление выражения A^T + 2 * B + C
            var resultMatrix = ~matrixA + 2 * matrixB + ~matrixC;
            Console.WriteLine($"Ат+2*В+Ст =\n{resultMatrix}");
        }

        //Ввести индекс
        private static int ReadIndex(string message)
        {
            int result;
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out result) && result > 0 && result<=matrices.Count)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Ошибка: введите корректное число.");
                }
            }
            return result;
        }

        //Найти максимальный подмассив 3*3 в массиве n*m
        private static void FindMaxSubArrayThreeWithMaxSum()
        {
            // Проверка наличия подходящих матриц
            if (matrices.Count == 0 || !matrices.Any(m => m.matrix.GetLength(0) >= 3 && m.matrix.GetLength(1) >= 3))
            {
                Console.WriteLine("Нет подходящих матриц размером не меньше 3x3.");
                return;
            }
            Console.WriteLine("Найти матрицу 3*3 c максимальной суммой элементов в матрице n*m:");
            ShowAllMatrices();
            int num;
            while (true)
            {
                num = ReadPositiveInt("Выберите матрицу:") - 1;

                if (num < 0 || num >= matrices.Count)
                {
                    Console.WriteLine("Ошибка: выбранной матрицы не существует.");
                    continue;
                }

                int rows = matrices[num].matrix.GetLength(0);
                int columns = matrices[num].matrix.GetLength(1);

                if (rows < 3 || columns < 3)
                {
                    Console.WriteLine("Ошибка: размер матрицы должен быть не меньше 3x3 для поиска подмассива.");
                }
                else
                {
                    break;
                }
            }
            var foundMatrix = matrices[num].FindMaxSubArrayWithMaxSum();
            Console.WriteLine("Найденная матрица:");
            PrintMatrix(foundMatrix);
        }

        //Создание матрицыы n*n лесенкой
        private static void CreateMatrixNxNStairs()
        {
            Console.WriteLine("Создание матрицыы n*n лесенкой:");
            int n = ReadPositiveInt("Введите n:");
            var worker = new MatrixWorker(n, true);
            matrices.Add(worker);
            Console.WriteLine(worker);
        }

        //создание матрицы n*n рандомно
        private static void CreateMatrixNxNRandom()
        {
            Console.WriteLine("Cоздание матрицы n*n рандомно:");
            int n = ReadPositiveInt("Введите n:");
            var worker = new MatrixWorker(n);
            matrices.Add(worker);
            Console.WriteLine(worker);
        }

        //Создание матрицы n*m вводимую с клавиатуру
        private static void CreateMatrixNxMKeyboardInput()
        {
            Console.WriteLine("Создание матрицы n*m вводимую с клавиатуру:");
            int n = ReadPositiveInt("Введите n:");
            int m = ReadPositiveInt("Введите m:");
            var worker = new MatrixWorker(n, m);
            matrices.Add(worker);
            Console.WriteLine(worker);
        }

        // Безопасное считывание int(любого) с проверкой
        private static int ReadInt(string message)
        {
            int result;
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out result))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Ошибка: введите корректное число.");
                }
            }
            return result;
        }

        // Безопасное считывание int(положительного) с проверкой
        private static int ReadPositiveInt(string message)
        {
            int result;
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out result) && result > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Ошибка: введите корректное число.");
                }
            }
            return result;
        }

        // Чтение строки
        private static string ReadString(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }
        
        //Вывод матрицы
        public static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        //Вывод всех MatrixWorker
        private static void ShowAllMatrices()
        {
            if (matrices.Count == 0)
            {
                Console.WriteLine("Нет созданных матриц.");
                return;
            }

            for (int i = 0; i < matrices.Count; i++)
            {
                Console.WriteLine($"\nМатрица {i + 1}:");
                Console.WriteLine(matrices[i]);
            }
        }
    }
}
