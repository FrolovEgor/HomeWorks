using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_5_2
{
    /// <summary>
    /// Класс для задания правила сортировки строк в массиве по возрастанию длины строки 
    /// </summary>
    class ShortLengthStringCopmparer : IComparer<String>
    { 
        public int Compare(String p1, String p2) 
        {
            if (p1.Length > p2.Length)
                return 1;
            else if (p1.Length < p2.Length)
                return -1;
            else
                return 0;    
        }
    }
    /// <summary>
    /// Класс для задания правила сортировки строк в массиве по убыванию длины строки 
    /// </summary>
    class LongLengthStringCopmparer : IComparer<String>
    {
        public int Compare(String p1, String p2)
        {
            if (p1.Length < p2.Length)
                return 1;
            else if (p1.Length > p2.Length)
                return -1;
            else
                return 0;
        }
    }
    class Program
    {
        /// <summary>
        /// Метод для определения заданного количества наименьших/наибольших слов в строке
        /// </summary>
        /// <param name="startString">Исходная строка</param>
        /// <param name="mode">Выбор режима работы метода 0 - выбор наименьшего слова 1 - выбор наибольшего</param>
        /// <param name="numberOfWords">Количество строк с наибольшей/наименьшей длинной возвращаемых методом</param>
        /// <returns>Масиив строк, содержащий отдельные слова, возвращаемый функцией по резуьтатам работы</returns>
        static string[] GetWordFromString(string startString, int mode, int numberOfWords) 
        {
            char[] filter = { ' ', '.', ',', '!', '-' };        //Массив разделителей, для разбиения исходной строки на отдельные слова
            string[] separeted = startString.Split(filter, StringSplitOptions.RemoveEmptyEntries);       //Разбиение исходной строки по разделителям filter в массив строк
            if (mode == 0) Array.Sort(separeted, 0, separeted.Length, new ShortLengthStringCopmparer()); //Сортировка массива по возрастанию длины строк
            else Array.Sort(separeted, 0, separeted.Length, new LongLengthStringCopmparer());            //Сортировка массива по убыванию длины строк
            numberOfWords = numberOfWords < separeted.Length ? numberOfWords : separeted.Length;         //Проверка условия для предотвращения выхода за границу массива
            int count = 1;                                                                               //Счетчик числа одинаковых по длинне элементов
            for (int i = 0; i < numberOfWords-1; i++)                                                    //Цикл для определения числа возвращаемых элементов
            {
                if (separeted[i].Length == separeted[i + 1].Length) //Условие определения одинаковых по длинне строк, подлежащих выводу
                {
                    count++;
                }
            }
            string[] result = new string[count];                    //Создание массива строк для вывода результатов
            Array.Copy(separeted, result, count);                   //Копирование нужного числа строк, удовлетворяющих результату в result
            return result;
        }
             
        static void Main(string[] args)
        {
            Console.Write("Введите исходную строку: ");           //Запрос ввода
            string Test = Console.ReadLine();                     //Ввод исходной строки

            string[] result = GetWordFromString(Test, 0, 1);     //Вызов метода для выделения одного наименьшего слова в строке
            Console.Write("\nНаименьшее слово в строке: ");     
            Console.Write($"{result[0]}");

            result = GetWordFromString(Test, 1, 2);             //Вызов метода для выделения двух наибольших слов в строке
            Console.Write("\nДва самое большое слово/слова в строке: ");
            foreach (var word in result)
            {
                Console.Write($" {word}; ");
            }
            Console.ReadKey();
        }
    }
}