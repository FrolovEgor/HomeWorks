using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_4_3_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите число строк матриц: ");                  //Запрос на ввод числа строк матриц
            int rows = int.Parse(Console.ReadLine());                       //Ввод числа строк матриц
            Console.Write("Введите число столбцов матриц: ");               //Запрос на ввод числа столбцов матриц
            int columns = int.Parse(Console.ReadLine());                    //Ввод числа столбцов матриц

            Console.WriteLine("Сгенерированная матрицы A и B:");            //Вывод шапки для матриц

            Random randomizer = new Random();                               //Инициализация генерации псевдослучайных чисел

            int[,] matrixOne = new int[rows, columns];                      //Инициализация массива для матрицы A
            int[,] matrixTwo = new int[rows, columns];                      //Инициализация массива для матрицы B

            if (rows == columns)                                            //Проверка правильности введенных данных
            {
                for (int i = 0; i < rows; i++)                              //Цикл для перебора строк матриц A и B
                {
                    for (int j = 0; j < columns; j++)                       //Цикл для перебора столбцов матрицы A
                    {
                        matrixOne[i, j] = randomizer.Next(-999, 1000);      //Заполнение элемента матрицы А(i,j) случайным числом
                        Console.Write($"{matrixOne[i, j],6}");              //Вывод элемента матрицы A(i,j)
                    }
                    Console.Write("     ");                                 //Пробел для разделения вывода матрицы A и B
                    for (int j = 0; j < columns; j++)                       //Цикл для перебора строк матрицы B
                    {
                        matrixTwo[i, j] = randomizer.Next(-999, 1000);      //Заполнение элемента матрицы B(i,j) случайным числом
                        Console.Write($"{matrixTwo[i, j],6}");              //Вывод элемента матрицы B(i,j)
                    }
                    Console.WriteLine();                                    //Переход на следующую строку при выводе
                }

                Console.Write("\nВыберите действие которое необходимо сделать с матрицами(+/-): ");     //Запрос действия от пользователя
                string action = Console.ReadLine();                         //Ввод действия
                Console.WriteLine("Матрица A"+action+"B равна:");

                switch (action)                                             //Ветвление в зависимости от введенного дествия
                {
                    case "+":                                               //Ветка для сложения матриц
                        for (int i = 0; i < rows; i++)                      //Цикл для перебора строк матриц A и B
                        {
                            for (int j = 0; j < columns; j++)               //Цикл для перебора столбцов матриц A и B
                            {
                                matrixOne[i, j] += matrixTwo[i, j];         //Прибавление к элементу A(i,j) элемента B(i,j)
                                Console.Write($"{matrixOne[i, j],6}");      //Печать элемента A(i,j)
                            }
                            Console.WriteLine();                            //Переход на следующую строчку
                        }
                        break;
                    case "-":                                               //Ветка для вычитания матриц
                        for (int i = 0; i < rows; i++)                      //Цикл для перебора строк матриц A и B
                        {
                            for (int j = 0; j < columns; j++)               //Цикл для перебора столбцов матриц A и B
                            {
                                matrixOne[i, j] -= matrixTwo[i, j];         //Вычитание из элемента A(i,j) элемента B(i,j)
                                Console.Write($"{matrixOne[i, j],6}");      //Печать элемента A(i,j)
                            }
                            Console.WriteLine();                            //Переход на следующую строчку
                        }
                        break;
                    default:
                        Console.WriteLine("Вывели неверное действие!");     //Сообщение при неправильном вводе действия
                        break;
                }
            }
            else 
            {
                Console.WriteLine("Матрицы должны быть квадратными для осуществления операций");    //Сообщение при неправильном вводе размера матриц
            }
            Console.ReadKey();
        }
    }
}
