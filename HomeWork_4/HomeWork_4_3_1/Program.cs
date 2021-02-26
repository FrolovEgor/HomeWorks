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


            if (rows > 0 || columns > 0)
            {

                int[,] matrix = new int[rows, columns];

                Random randomizer = new Random();

                Console.WriteLine("Исходная матрица A:\n");
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        matrix[i, j] = randomizer.Next(0, 101);
                        Console.Write($"{matrix[i, j],5}");
                    }
                    Console.WriteLine();
                }
                Console.Write("\nВведите число, на которое нужно умножить матрицу A: ");
                int multipleNumber = int.Parse(Console.ReadLine());

                Console.WriteLine($"Матрица A умноженная на {multipleNumber} равна:\n");

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        matrix[i, j] *= multipleNumber;
                        Console.Write($"{matrix[i, j],5}");
                    }
                    Console.WriteLine();
                }


            }
            else Console.WriteLine("Вы ввели неправильные данные! попробуйте еще раз.");
            Console.ReadKey();
        }
    }
}
