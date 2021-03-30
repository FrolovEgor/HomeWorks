using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_5_3
{
    class Program
    {   
        /// <summary>
        /// Возвращает строчку, где удалены все повторяющиеся друг за другом символы
        /// </summary>
        /// <param name="originalString">Входная строка для преобразования</param>
        /// <returns>Строчка без повторяющихся символов</returns>
        static string ThrowSameLetters(string originalString)
        {
            char[] massCharFromString = originalString.ToLower().ToCharArray();     //Преобразование входной строчки в массив char

            int massLenth = massCharFromString.Length;          //Определение длинны входной строки
            int verifiableLetter = 0, nextLetterCounter = 1;    //verifiableLetter-индекс проверяемой буквы; nextLetterCounter-индекс следующих за проверяемой одинаковых букв
            string result = null;                               //строковая переменная для хранения результата 

            while (true)                                        //Цикл для сравнения каждого символа со следующим
            {
                if (verifiableLetter + nextLetterCounter == massLenth)              //Проверка является ли проверяемая буква последней
                {
                    result += massCharFromString[verifiableLetter].ToString();      //Добавление последней буквы в result
                    break;
                }
                if (massCharFromString[verifiableLetter] == massCharFromString[verifiableLetter + nextLetterCounter]) //Проверка равенства проверяемой буквы следующей
                {
                    nextLetterCounter += 1;                                         //Увелечение счетчика при равенсве 
                    continue;
                }
                else
                {
                    result += massCharFromString[verifiableLetter].ToString();      //Прибавление 
                    verifiableLetter += nextLetterCounter;
                    nextLetterCounter = 1;
                }
            }
            return result;
        }
        static void Main(string[] args)
        {
            string inpuntString = "Ххххоооорррооошшшиий деееннннь";
            string result = ThrowSameLetters(inpuntString);

            Console.WriteLine("Введенная строка: {0}\n" +
                "Без повторяющихся символов выглядит лучше: {1}", inpuntString, result);
            
            Console.ReadKey();
        }
    }
}
