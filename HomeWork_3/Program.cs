using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_3
{
    class Program
    {

        #region Решение без усложнений

        /// <summary>
        /// Функция для создания новых игроков
        /// </summary>
        /// <param name="playersRef">Ссылка на список игроков</param>
        /// <param name="n">Количество игроков</param>
        static void MakeNewPlayers(ref List<string> playersRef, int n)
        {
            //Цикл для создания n новых пользователей
            for (int i = 0; i < n; i++)
            {
                Console.Write($"Введите имя {i + 1} игрока: ");
                playersRef.Add(Console.ReadLine());

                //Если пользователь не ввел имя присвоить ему имя по умолчанию
                if (playersRef[i] == "") playersRef[i] = $"Player {i + 1}";
            }
        }

        /// <summary>
        /// Функция игры
        /// </summary>
        /// <param name="numberOfPlayers">Количество игроков</param>
        /// <param name="gameNumber">Загаданное число</param>
        /// <param name="playersRef">Ссылка на список игроков</param>
        static void Game(int numberOfPlayers, int gameNumber, ref List<string> playersRef)
        {
            //Флаг продолжения игры
            bool winFlag = true;

            //Основной цикл игры
            while (winFlag)
            {
                //Цикл для опроса игроков и проверки их ввода
                for (int i = 0; i < numberOfPlayers; i++)
                {
                    //Печать запроса в консоль и прием числа от игрока
                    Console.WriteLine($"\nЧисло: {gameNumber}");
                    Console.Write($"Ход {playersRef[i]} :");
                    int userTry = int.Parse(Console.ReadLine());

                    //Проверка, что пользователь ввел число нужного диапазона
                    if (userTry < 1 || userTry > 4) userTry = 0;

                    //Проверка, что число введеное пользователем не больше оставшегося для игры числа
                    if (userTry <= gameNumber) gameNumber -= userTry;

                    //Определение победителя
                    if (gameNumber == 0)
                    {
                        Console.WriteLine($"Ура! {playersRef[i]} победил!");

                        //Сброс флага, для завершения игры
                        winFlag = false;
                        break;
                    }
                }
            }
        }


        static void Main(string[] args)
        {
            //Инициализация списка игроков
            List<string> Players = new List<string>();

            //Вызов функции для заполнения списка игроков
            MakeNewPlayers(ref Players, 2);

            //Инициализация генератора псевдослучайных чисел и генерация случайного числа
            Random randomizer = new Random();
            int gameNumber = randomizer.Next(12, 121);

            //Вызов функции игры
            Game(2, gameNumber, ref Players);
            Console.ReadKey();
        }
        #endregion

        #region Решение *

        #endregion
       
    }
}
