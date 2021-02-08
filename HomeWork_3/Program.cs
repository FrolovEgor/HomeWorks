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

        ///// <summary>
        ///// Функция для создания новых игроков
        ///// </summary>
        ///// <param name="playersRef">Ссылка на список игроков</param>
        ///// <param name="n">Количество игроков</param>
        //static void MakeNewPlayers(ref List<string> playersRef, int n)
        //{
        //    //Цикл для создания n новых пользователей
        //    for (int i = 0; i < n; i++)
        //    {
        //        Console.Write($"Введите имя {i + 1} игрока: ");
        //        playersRef.Add(Console.ReadLine());

        //        //Если пользователь не ввел имя присвоить ему имя по умолчанию
        //        if (playersRef[i] == "") playersRef[i] = $"Player {i + 1}";
        //    }
        //}

        ///// <summary>
        ///// Функция игры
        ///// </summary>
        ///// <param name="numberOfPlayers">Количество игроков</param>
        ///// <param name="gameNumber">Загаданное число</param>
        ///// <param name="playersRef">Ссылка на список игроков</param>
        //static void Game(int numberOfPlayers, int gameNumber, ref List<string> playersRef)
        //{
        //    //Флаг продолжения игры
        //    bool winFlag = true;

        //    //Основной цикл игры
        //    while (winFlag)
        //    {
        //        //Цикл для опроса игроков и проверки их ввода
        //        for (int i = 0; i < numberOfPlayers; i++)
        //        {
        //            //Печать запроса в консоль и прием числа от игрока
        //            Console.WriteLine($"\nЧисло: {gameNumber}");
        //            Console.Write($"Ход {playersRef[i]} :");
        //            int userTry = int.Parse(Console.ReadLine());

        //            //Проверка, что пользователь ввел число нужного диапазона
        //            if (userTry < 1 || userTry > 4) userTry = 0;

        //            //Проверка, что число введеное пользователем не больше оставшегося для игры числа
        //            if (userTry <= gameNumber) gameNumber -= userTry;

        //            //Определение победителя
        //            if (gameNumber == 0)
        //            {
        //                Console.WriteLine($"Ура! {playersRef[i]} победил!");

        //                //Сброс флага, для завершения игры
        //                winFlag = false;
        //                break;
        //            }
        //        }
        //    }
        //}


        //static void Main(string[] args)
        //{
        //    //Инициализация списка игроков
        //    List<string> Players = new List<string>();

        //    //Вызов функции для заполнения списка игроков
        //    MakeNewPlayers(ref Players, 2);

        //    //Инициализация генератора псевдослучайных чисел и генерация случайного числа
        //    Random randomizer = new Random();
        //    int gameNumber = randomizer.Next(12, 121);

        //    //Вызов функции игры
        //    Game(2, gameNumber, ref Players);
        //    Console.ReadKey();
        //}
        #endregion

        #region Решение *
        ///// <summary>
        ///// Функция для создания новых игроков
        ///// </summary>
        ///// <param name="playersRef">Ссылка на список игроков</param>
        ///// <param name="n">Количество игроков</param>
        //static void MakeNewPlayers(ref List<string> playersRef, int n)
        //{
        //    //Цикл для создания n новых пользователей
        //    for (int i = 0; i < n; i++)
        //    {
        //        Console.Write($"Введите имя {i + 1} игрока: ");
        //        playersRef.Add(Console.ReadLine());

        //        //Если пользователь не ввел имя присвоить ему имя по умолчанию
        //        if (playersRef[i] == "") playersRef[i] = $"Player {i + 1}";
        //    }
        //}

        ///// <summary>
        ///// Функция игры
        ///// </summary>
        ///// <param name="numberOfPlayers">Количество игроков</param>
        ///// <param name="userNumbersLimit">максимальное число для userTry</param>
        ///// <param name="gameNumber">Загаданное число</param>
        ///// <param name="playersRef">Ссылка на список игроков</param>
        //static void Game(int numberOfPlayers, int userNumbersLimit, int gameNumber, ref List<string> playersRef)
        //{
        //    //Флаг продолжения игры
        //    bool winFlag = true;

        //    //Основной цикл игры
        //    while (winFlag)
        //    {
        //        //Цикл для опроса игроков и проверки их ввода
        //        for (int i = 0; i < numberOfPlayers; i++)
        //        {
        //            //Печать запроса в консоль и прием числа от игрока
        //            Console.WriteLine($"\nЧисло: {gameNumber}");
        //            Console.Write($"Ход {playersRef[i]} :");
        //            int userTry = int.Parse(Console.ReadLine());

        //            //Проверка, что пользователь ввел число нужного диапазона
        //            if (userTry < 1 || userTry > userNumbersLimit) userTry = 0;

        //            //Проверка, что число введеное пользователем не больше оставшегося для игры числа
        //            if (userTry <= gameNumber) gameNumber -= userTry;

        //            //Определение победителя
        //            if (gameNumber == 0)
        //            {
        //                Console.WriteLine($"Ура! {playersRef[i]} победил!");

        //                //Сброс флага, для завершения игры
        //                winFlag = false;
        //                break;
        //            }
        //        }
        //    }
        //}


        //static void Main(string[] args)
        //{
        //    //Инициализация списка игроков
        //    List<string> Players = new List<string>();

        //    //Запрос количества игроков
        //    Console.Write("Введите количество игроков: ");
        //    int countOfPlayers = int.Parse(Console.ReadLine());

        //    //Вызов функции для заполнения списка игроков
        //    MakeNewPlayers(ref Players, countOfPlayers);

        //    //Запрос нижней границы случайных чисел
        //    Console.Write("Введите наименьшее случайное число: ");
        //    int minRand = int.Parse(Console.ReadLine());

        //    //Запрос верхней границы 
        //    Console.Write("Введите наибольшее случайное число: ");
        //    int maxRand = int.Parse(Console.ReadLine());

        //    Console.Write("Какое максимальное число можно использовать игрокам?: ");
        //    int userNumbersLimit = int.Parse(Console.ReadLine());


        //    //Инициализация генератора псевдослучайных чисел и генерация случайного числа
        //    Random randomizer = new Random();
        //    int gameNumber = randomizer.Next(minRand, maxRand+1);

        //    //Вызов функции игры
        //    Game(countOfPlayers, userNumbersLimit, gameNumber, ref Players);
        //    Console.ReadKey();
        //}
        #endregion

        #region Одиночная игра

        /// <summary>
        /// Функция для запроса имени игрока
        /// </summary>
        /// <returns>Возвращает строку с именем игрока</returns>
        static string MakeNewPlayer()
        {
                //Запрос и ввод имени игрока
                Console.Write($"Введите имя игрока: ");
                string name= Console.ReadLine();

                //Если игрок не ввел имя, присвоить ему стандартное
                if (name == "") name = "Player 1";

            return name;
        }

        /// <summary>
        /// Функция игры 
        /// </summary>
        /// <param name="gameNumber">Случайное число</param>
        /// <param name="playerName">Имя игрока</param>
        /// <param name="randomizerRef">Ссылка на объект рандом</param>
        static void Game(int gameNumber, string playerName, ref Random randomizerRef)
        {
            //Флаг продолжения игры
            bool winFlag = true;

            //Инициализация переменной для хранения хода игроков
            int userTry=0;

            //Основной цикл игры
            while (winFlag)
            {
                //Цикл для одного раунда
                for (int i=0; i <2; i++) 
                {
        
                if (i == 0) //Действия для хода игрока
                    {
                        //Печать запроса в консоль и прием числа от игрока
                        Console.Write($"\nЧисло: {gameNumber}\nХод {playerName} :");
                        userTry = int.Parse(Console.ReadLine());

                        //Проверка, что пользователь ввел число нужного диапазона
                        if (userTry < 1 || userTry > 4) userTry = 1;

                        //Проверка, что число введеное пользователем не больше оставшегося для игры числа
                        if (userTry <= gameNumber) gameNumber -= userTry;
                    }
                    else //Действия для хода компьютера
                    {
                        //логика компьютера:
                        //Пока число большое компьютер снижает на рандомное число от 1 до 4
                        //Когда число близко к границе победы компьютер ждет ошибки игрока и снижает число на 1
                        //Если игрок ошибся компьютер побеждает
                        if (gameNumber >= 9) userTry = randomizerRef.Next(1,5);
                        else if (gameNumber > 4) userTry = 1;
                        else if (gameNumber <= 4) userTry = gameNumber;

                        Console.WriteLine($"\nЧисло: {gameNumber}\nХод компьютера: {userTry}");
                        gameNumber -= userTry;
                    }

                    //Определение победы
                    if (gameNumber == 0 )
                    {
                        //Вывод сообщения о победе игрока или компьютера
                        if (i == 0) Console.WriteLine($"Ура! {playerName} победил!");
                        else Console.WriteLine($"Увы! компьютер победил!");

                        //Сброс флага, для завершения игры
                        winFlag = false;
                        break;
                    }
                }
            }
        }


        static void Main(string[] args)
        {
            //Инициализация игрока
            string playerName = MakeNewPlayer();

            
            //Инициализация генератора псевдослучайных чисел и генерация случайного числа
            Random randomizer = new Random();
            int gameNumber = randomizer.Next(12, 121);

            //Вызов функции игры
            Game(gameNumber, playerName, ref randomizer);
            Console.ReadKey();
        }
        #endregion
    }
}
