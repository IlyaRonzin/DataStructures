using System;
using System.Collections.Generic;
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
    }
}
