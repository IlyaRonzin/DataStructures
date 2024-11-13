using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class FileWorker
    {
        //Заполнение бинарного файла рандомными числами
        public static void FillFileWithRandomData(string path, int numNumbers)
        {
            Random randomNumber = new Random();
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                for (int i = 0; i < numNumbers; i++)
                {
                    writer.Write(randomNumber.Next(1, 100)); // Случайное число от 1 до 99
                }
            }
        }

        //Фильтрация
        public static void FilterData(string sourceFile, string filteredFile, int m, int n)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(sourceFile, FileMode.Open)))
            using (BinaryWriter writer = new BinaryWriter(File.Open(filteredFile, FileMode.Create)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    int number = reader.ReadInt32();
                    if (number % m == 0 && number % n != 0)
                    {
                        writer.Write(number);
                    }
                }
            }
        }

        //Разность максимального и минимального
        public static int GetDifferenceMaxMin(string sourceFile)
        {
            int maxValue = int.MinValue;
            int minValue = int.MaxValue;

            using (var reader = new StreamReader(sourceFile))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (int.TryParse(line, out int value))
                    {
                        if (maxValue < value)
                            maxValue = value;
                        if (minValue > value)
                            minValue = value;
                    }
                }
            }

            return maxValue - minValue;
        }

        //Поиск минимального
        public static int FindMinValueInFile(string filePath)
        {
            // Чтение всех строк файла
            string[] lines = File.ReadAllLines(filePath);

            // Переводим все строки в массив чисел
            var numbers = lines
                .SelectMany(line => line.Split(' ')) // Разделяем строки по пробелам
                .Select(num => int.Parse(num)) // Преобразуем в целые числа
                .ToList(); // Собираем все числа в список

            // Находим минимальное число
            return numbers.Min();
        }

        public static void FindStrings(string In, string Out, char q)
        {
            StreamWriter sw = new StreamWriter(Out);
            StreamReader sr = new StreamReader(In);
            string line = sr.ReadLine();
            while (line != "0")
            {
                if (line[0] == q)
                {
                    sw.WriteLine(line);
                }
                line = sr.ReadLine();
            }
            sw.Close();
        }
    }
}
