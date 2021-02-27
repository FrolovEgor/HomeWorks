using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_4_3_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите число строк матрицы A: ");               //Запрос на ввод числа строк
            int rows = int.Parse(Console.ReadLine());                       //Ввод числа строк
            Console.Write("Введите число столбцов матрицы A: ");            //Запрос на ввод числа столбцов
            int columns = int.Parse(Console.ReadLine());                    //Ввод числа столбцов


            if (rows > 0 || columns > 0)                                    //Проверка корректности введенных данных
            {
                int[,] matrix = new int[rows, columns];                     //Создание массива для хранения матрицы

                Random randomizer = new Random();                           //Инициализация генератора псевдослучайных чисел

                Console.WriteLine("Исходная матрица A:\n");                 //Вывод шапки к сгенерированой матрице
                for (int i = 0; i < rows; i++)                              //Цикл для перебора строк матрицы
                {
                    for (int j = 0; j < columns; j++)                       //Цикл для перебора столбцов матрицы
                    {
                        matrix[i, j] = randomizer.Next(0, 101);             //Заполнение матрицы случайными числами
                        Console.Write($"{matrix[i, j],5}");                 //Вывод заполненного элемента
                    }
                    Console.WriteLine();                                    //Перевод каретки на следующую строку матрицы
                }
                Console.Write("\nВведите число, на которое нужно умножить матрицу A: ");    //Запрос на ввод множителя матрицы
                int multipleNumber = int.Parse(Console.ReadLine());                         //Ввод множителя

                Console.WriteLine($"Матрица A умноженная на {multipleNumber} равна:\n");    //Вывод шапки к результату решения

                for (int i = 0; i < rows; i++)                              //Цикл для перебора строк
                {
                    for (int j = 0; j < columns; j++)                       //Цикл ля перебора столбцов
                    {
                        matrix[i, j] *= multipleNumber;                     //Умножение каждого элемента матрицы на число
                        Console.Write($"{matrix[i, j],5}");                 //Вывод полученных элементов
                    }
                    Console.WriteLine();                                    //Перевод каретки на следующую строку матрицы
                }
                            }
            else Console.WriteLine("Вы ввели неправильные данные! попробуйте еще раз."); //Вывод сообщения в случаи неправильного ввода
            Console.ReadKey();
        }
    }
}
