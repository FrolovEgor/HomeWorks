using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_5_4
{
    class Program
    {
        /// <summary>
        /// Определяет Является ли заданный массив чисел арифметической или геометрической прогрессией
        /// </summary>
        /// <param name="initialMass">массив чисел</param>
        /// <returns>Результат анализа в виде строки текста</returns>
        static string SearchProgression(double[] initialMass) 
        {
            int MassSize = initialMass.Length;      //определение длинны массива
            double Summ = 0.0;                      //инициализация переменной
            for (int i = 0; i < MassSize; i++)      //цикл для прямого вычисления суммы массива
            {
                Summ += initialMass[i];            
            }
            
            double likeArephmiticSumm = (initialMass[0] + initialMass[MassSize - 1]) * MassSize / 2 ; //вычисление суммы массива по правилам для арифметической прогресии

            double denominator = 0;         //инициализация переменных
            double likeGeometrySumm = 0;
            if (MassSize > 0 && initialMass[0] != 0) denominator = initialMass[1] / initialMass[0];     //Вычисление возможного знаменателя геометрической прогресии
            if (denominator != 1) likeGeometrySumm = (initialMass[0] * (Math.Pow(denominator, MassSize) - 1)) / (denominator - 1); //Вычисление суммы массива по 
                                                                                                                                   //правилам для геометрической прогресии
            string result = null;          //инициализация переменных
            if (Summ == initialMass[0]*MassSize) return result = "Данный массив состоит из одинаковых чисел";           //Проверка, что массив состоит не из одинаковых чисел
            if (Summ == likeArephmiticSumm) return result = "Данный массив чисел содержит арифметическую прогрессию";   //Проверка равна ли сумма масива сумме арифметической прогресии
            if (Summ == likeGeometrySumm) return result = "Данный массив чисел содержит геометрическую прогрессию";     //Проверка равна ли сумма масива сумме геометрической прогресии
            return result = "Данный массив не содержит прогрессий";
        }
        static void Main(string[] args)
        {
            double[] startMass= { 1, 2, 3, 4, 5, 6, 7};             //Инициализация массива содержащего последовательность

            Console.WriteLine("Дана последовательность чисел:");    
            foreach (double numb in startMass)                      //Вывод последовательности на экран
            {
                Console.Write(" {0};",numb);
            }

            Console.WriteLine("\n"+SearchProgression(startMass));   //Вызов метода и вывод результата работы
            Console.ReadKey();

        }
    }
}
