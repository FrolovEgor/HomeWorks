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
            Console.WriteLine("При помощи рекурсии можно вычислить следующие значения функции аккермана:");
            int[,] mass = new int[18, 5];               //Создание массива чисел для хранения вычисленных значений

            for (int i = 0; i < 11; i++)                //Цикл для перебора строк
            {
                for (int j = 0; j < 5; j++)             //Цикл для перебора столбцов
                {
                    mass[i, j] = akkermanFunction(j, i);    //Вызов и вычисление функции аккермана
                    Console.Write($"{mass[i, j],15}");      //Вывод на экран
                }
                Console.WriteLine();                        //Перевод на новую строку
            }

            Console.WriteLine("\nПри помощи стека можно вычислить больше... (если есть время):");
            for (int i = 0; i < 17; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    mass[i, j] = StackAkkerman(j, i);    //Вызов и вычисление функции аккермана
                    Console.Write($"{mass[i, j],15}");   //Вывод на экран
                }
                Console.WriteLine();                     //Перевод на новую строку
            }
            Console.ReadKey();
        }
    }
}