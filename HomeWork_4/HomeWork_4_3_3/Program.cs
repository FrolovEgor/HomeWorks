using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_4_3_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите число строк матрицы A: ");               //Запрос на ввод числа строк матрицы А
            int rowsA = int.Parse(Console.ReadLine());                      //Ввод числа строк матриц А
            Console.Write("Введите число столбцов матрицы A: ");            //Запрос на ввод числа столбцов матриц B
            int columnsA = int.Parse(Console.ReadLine());                   //Ввод числа столбцов матриц B

            Console.Write("Введите число строк матрицы B: ");               //Запрос на ввод числа строк матрицы А
            int rowsB = int.Parse(Console.ReadLine());                      //Ввод числа строк матриц А
            Console.Write("Введите число столбцов матрицы B: ");            //Запрос на ввод числа столбцов матриц B
            int columnsB = int.Parse(Console.ReadLine());                   //Ввод числа столбцов матриц B



            Random randomizer = new Random();                               //Инициализация генерации псевдослучайных чисел

            int[,] matrixOne = new int[rowsA, columnsA];                    //Инициализация массива для матрицы A
            int[,] matrixTwo = new int[rowsB, columnsB];                    //Инициализация массива для матрицы B
            int[,] matrixThree = new int[rowsA, columnsB];                  //Инициализация массива для матрицы C

            if (rowsA == columnsB && rowsB == columnsA)                     //Проверка правильности введенных данных
            {
                Console.WriteLine("Сгенерированные матрицы A и B:");        //Вывод шапки для матриц
                for (int i = 0; i < matrixOne.GetLength(0); i++)            //Цикл для перебора строк матриц A и B
                {
                    for (int j = 0; j < matrixOne.GetLength(1); j++)        //Цикл для перебора столбцов матрицы A
                    {
                        matrixOne[i, j] = randomizer.Next(-999, 1000);      //Заполнение элемента матрицы А(i,j) случайным числом
                        Console.Write($"{matrixOne[i, j],6}");              //Вывод элемента матрицы A(i,j)
                    }
                    Console.Write("     ");                                 //Пробел для разделения вывода матрицы A и B

                    if (i < matrixTwo.GetLength(0))                         //Ограничение для правильного заполнения матрицы B
                    {

                        for (int j = 0; j < matrixTwo.GetLength(1); j++)        //Цикл для перебора строк матрицы B
                        {
                            matrixTwo[i, j] = randomizer.Next(-999, 1000);      //Заполнение элемента матрицы B(i,j) случайным числом
                            Console.Write($"{matrixTwo[i, j],6}");              //Вывод элемента матрицы B(i,j)
                        }
                    }
                    Console.WriteLine("\n");                                    //Переход на следующую строку при выводе
                }

                for (int i = 0; i < matrixThree.GetLength(0); i++)              //Цикл для перебора строк матрицы C
                {
                    for (int j = 0; j < matrixTwo.GetLength(1); j++)            //Цикл для перебора столбцов матрицы B, которые нужно умножить на строку матрицы А
                    {
                        for (int k = 0; k < matrixOne.GetLength(1); k++)        //Цикл для перебора элементов строки матрицы А
                        {
                            matrixThree[i, j] += matrixOne[i, k] * matrixTwo[k, j]; //Получение суммы произведений строки А и столбца B
                        }
                        Console.Write($"{matrixThree[i, j],10}");               //Вывод на экран
                    }
                    Console.WriteLine();                                        //Переход на следующую строчку при печати
                }

                //Сообщение при неправильном вводе размера матриц
            }else Console.WriteLine("Умножить матрицы невозможно! Количество " +
                "столбцов матрицы А должно быть\nравно количестеству строк матрицы B, " +
                "а количество строк матрицы А должно\nбыть равно столбцам матрицы B.");

            Console.ReadKey();
        }

    }
    
}
