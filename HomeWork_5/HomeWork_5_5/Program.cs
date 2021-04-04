using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_5_5
{
    class Program
    {
        /// <summary>
        /// Реккурсивный метод для вычисления функции аккермана
        /// </summary>
        /// <param name="m">Исходынй параметр m</param>
        /// <param name="n">Исходный параметр n</param>
        /// <returns>Вычисленное значение функции</returns>
        static int akkermanFunction(int m, int n)
        {
            if (m > 0 && n > 0) return akkermanFunction(m - 1, akkermanFunction(m, n - 1));
            if (m > 0 && n == 0) return akkermanFunction(m - 1, 1);
            return n + 1;
        }
        /// <summary>
        /// Метод для вычисления функции аккермана через стек
        /// </summary>
        /// <param name="m">Исходынй параметр m</param>
        /// <param name="n">Исходный параметр n</param>
        /// <returns>Вычисленное значение функции</returns>
        static int StackAkkerman(int m, int n) 
        { 
            Stack<int> myStack = new Stack<int>();     //Инициализация стека
            while (true)                               //Цикл для замены рекурсии функци
            {
                if (m > 0)
                {
                    if (n > 0)
                    {
                        n -= 1;                         //Уменьшение n пока n !=0
                        myStack.Push(m - 1);            //Помещение в стек значения m-1, при котором уменьшается n
                        continue;
                    }
                    else
                    {
                        m -= 1;                         //Уменьшение m при n=0
                        n = 1;
                        continue;
                    }
                }
                else if (myStack.Count > 0)
                {
                    m = myStack.Peek();                 //Вытаскиваем из стека запомненное значение
                    myStack.Pop();                      //Удаление последнего запомненого m
                    n += 1;                         
                    continue;
                }
                else break;
            }
            return n += 1;                              //Последнее прибавление n
        }

        static void Main(string[] args)
        {
            //Создание массива чисел
            //int[] numbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17};
            int[,] mass = new int[18, 5];

            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    mass[i, j] = akkermanFunction(j, i);
                    Console.Write($"{mass[i, j],15}");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            for (int i = 0; i < 17; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    mass[i, j] = StackAkkerman(j, i);
                    Console.Write($"{mass[i, j],15}");
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}