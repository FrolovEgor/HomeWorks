using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_4_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите количество строк треугольника паскаля: "); //Запрос ввод количества строк треугольника
            int numberOfLines = int.Parse(Console.ReadLine());                //Ввод количества строк треугольника

            int[][] mass = new int[numberOfLines][];                          //Создание массива массивов с количеством 
                                                                              //массивов равным numberOfLines

            for (int i = 0 ; i < numberOfLines; i++)                          //Цикл для перебора основного массива
            {
                mass[i] = new int[i+1];                                       //Инициализация элементов как массивов

                Console.CursorLeft = (mass.Length-mass.Length/2)*8-(i+1)*4;   //Сдвиг курсора для организации печати по центру

                for (int j = 0; j <= i; j++)                                  //Цикл для перебора вложенного массива (элемента)
                {                    
                    if (j == 0 || j == (mass[i].Length-1))                    //Условие для заполнения крайних элементов =1
                    {
                        mass[i][j] = 1;                                       //Заполнение элемента
                        Console.Write($"{mass[i][j],8}");                     //Печать элемента
                    }
                    else 
                    {
                        mass[i][j] = mass[i-1][j-1]+ mass[i - 1][j];          //Заполнение не крайних элементов суммой 
                                                                              //элементов с такимже и на 1 меньше номером 
                                                                              //из предыдущего массива (строчки)
                        Console.Write($"{mass[i][j],8}");                     //Печать элемента
                    }
                }
                Console.Write("\n");                                          //Переход на следующую строчку
            }
            Console.ReadKey();
        }
    }
}
