using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_5_1_1
{
    class Program
    {
        /// <summary>
        /// Метод для заполнения любой матрицы рандомными числами
        /// </summary>
        /// <param name="rand">Ссылка на объект класса Random</param>
        /// <param name="fillingMatrix">Ссылка на заполняемую матрицу</param>
        static void FillRandomMatrix(ref Random rand, ref int[,] fillingMatrix)
        {
            for (int i = 0; i < fillingMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < fillingMatrix.GetLength(1); j++)
                {
                    fillingMatrix[i, j] = rand.Next(-999, 1000);
                }
            }
        }
        /// <summary>
        /// Метод реализующий печать матрицы
        /// </summary>
        /// <param name="printingMatrix">Ссылка на матрицу, которую необходимо распечатать</param>
        static void PrintMatrix(ref int[,] printingMatrix)
        {
            for (int i = 0; i < printingMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < printingMatrix.GetLength(1); j++)
                {
                    Console.Write($"{printingMatrix[i, j],10}");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        /// <summary>
        /// Метод умножающий матрицу на число
        /// </summary>
        /// <param name="matrix">Ссылка на матрицу, которую нужно умножить</param>
        /// <param name="number">Число на которое надо умножить</param>
        static void MulMatrix(ref int[,] matrix, int number) 
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] *= number;
                }
            }
        }
        /// <summary>
        /// Метод возвращающий произведение одной матрицы на другую
        /// </summary>
        /// <param name="firstMatrix">Ссылка на первую матрицу</param>
        /// <param name="secondMatrix">Массив содержащий вторую матрицу</param>
        /// <returns>Возвращает массив содержащий результат умножения матриц</returns>
        static int[,] MulMatrix(ref int[,] firstMatrix, ref int[,] secondMatrix)
        {
            if (firstMatrix.GetLength(0) == secondMatrix.GetLength(1) &&
                firstMatrix.GetLength(1) == secondMatrix.GetLength(0))
            {
                int[,] matrixThree = new int[firstMatrix.GetLength(0), secondMatrix.GetLength(1)];
                for (int i = 0; i < matrixThree.GetLength(0); i++)
                {
                    for (int j = 0; j < secondMatrix.GetLength(1); j++)
                    {
                        for (int k = 0; k < firstMatrix.GetLength(1); k++)
                        {
                            matrixThree[i, j] += firstMatrix[i, k] * secondMatrix[k, j];
                        }
                    }
                }
                return matrixThree;
            }
            else 
            {
                Console.WriteLine("Операция умножения данных матриц невозможна");
                return null;
            }
        }
        /// <summary>
        /// Метод прибавляющий к первой матрице вторую
        /// </summary>
        /// <param name="firstMatrix">Массив содержащий первую матрицу</param>
        /// <param name="secondMatrix">Массив содержащий вторую матрицу</param>
        /// <param name="operation">Операция сложения или вычитания</param>
        /// /// <returns>Возвращает флаг успешности операции</returns>
        static bool SumMatrix(ref int[,] firstMatrix, int[,] secondMatrix, char operation)
        {
            if (firstMatrix.GetLength(0) == secondMatrix.GetLength(0) &&
                firstMatrix.GetLength(1) == secondMatrix.GetLength(1) &&
                (operation == '+' || operation == '-'))
            {
                if (operation == '-') MulMatrix(ref secondMatrix, -1);
                for (int i = 0; i < firstMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < firstMatrix.GetLength(1); j++)
                    {
                        firstMatrix[i, j] += secondMatrix[i, j];
                    }
                }
                return true;
            }
            else
            {
                Console.WriteLine("Операция сложения/вычитания с данными матрицами невозможна");
                return false;
            }
        }

        static void Main(string[] args)
        {
            //Организация ввода параметров матрицы A
            Console.Write("Введите число строк матрицы A: ");              
            int rowsA = int.Parse(Console.ReadLine());                      
            Console.Write("Введите число столбцов матрицы A: ");            
            int columnsA = int.Parse(Console.ReadLine());

            //Организация ввода параметров матрицы B
            Console.Write("Введите число строк матрицы B: ");               
            int rowsB = int.Parse(Console.ReadLine());                      
            Console.Write("Введите число столбцов матрицы B: ");
            int columnsB = int.Parse(Console.ReadLine());

            //Иницыиализация генератора псевдослучайных чисел 
            Random randomizer = new Random();

            //Заполнение и печать матрицы А
            Console.WriteLine("\nМатрица А:");
            int[,] matrixOne = new int[rowsA, columnsA];
            FillRandomMatrix(ref randomizer,ref matrixOne);
            PrintMatrix(ref matrixOne);

            //Заполнение и печать матрицы B
            Console.WriteLine("Матрица B:");
            int[,] matrixTwo = new int[rowsB, columnsB];
            FillRandomMatrix(ref randomizer, ref matrixTwo);
            PrintMatrix(ref matrixTwo);

            //Сложение матрицы А и B и печать результата
            Console.WriteLine("\nМатрица А + матрица B равно:");
            bool sumFlag = SumMatrix(ref matrixOne, matrixTwo, '+');
            if (sumFlag) PrintMatrix(ref matrixOne);

            //Вычитание из матрицы А матрицы B и печать результата
            Console.WriteLine("\nМатрица А - матрица B равно:");
            bool divFlag = SumMatrix(ref matrixOne, matrixTwo, '-');
            if (divFlag) PrintMatrix(ref matrixOne);

            //Умножение матрицы B на число
            Console.WriteLine("\nМатрица B умноженная на 2 равна:");
            MulMatrix(ref matrixTwo, 2);
            PrintMatrix(ref matrixTwo);

            //Проверка возможности умножения матриц
            if (matrixOne.GetLength(0) == matrixTwo.GetLength(1) &&
                matrixOne.GetLength(1) == matrixTwo.GetLength(0))
            {
                //Умножение матрицы A на матрицу B и вывод результата
                Console.WriteLine("\nМатрица А умноженная на матрицу B равна:");
                int[,] matrixThree = MulMatrix(ref matrixOne, ref matrixTwo);
                PrintMatrix(ref matrixThree);
            }
            else Console.WriteLine("\nДанные матрицы невозможно умножить друг на друга");

            Console.ReadKey();
        }
    }
}
