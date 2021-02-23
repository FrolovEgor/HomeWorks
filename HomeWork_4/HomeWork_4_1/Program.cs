using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_4
{
    class Program
    {
        struct numeratedProfit     //Создание структуры для хранения данных о прибыли вместе с номером месяца
        {
            public int index;
            public int value;
        }
        /// <summary>
        /// Функция для заполения и печати данных по месяцам
        /// </summary>
        /// <param name="inFunds">Массив с данными о "Доходах"</param>
        /// <param name="outFunds">Массив с данными о "Расходах"</param>
        /// <param name="profit">Массив с данными о "Прибыли" и месяцах</param>
        static void FillAndPrintData(ref int[] inFunds,ref int[] outFunds,ref numeratedProfit[] profit)
        {
            Random randomizer = new Random();                           //Созданиеэкземпляра класса Random
            
            for (int i = 0; i < inFunds.Length; i++)                    //Цикл для заполениея данных в таблице 
            {
                inFunds[i] = randomizer.Next(70_000, 120_000);          //Заполнение столбца "Доходы"
                outFunds[i] = randomizer.Next(45_000, 90_000);          //Заполнение стоблца "Расходы"
                profit[i].value = inFunds[i] - outFunds[i];             //Заполнение стоблца "Прибыль"
                profit[i].index = i + 1;                                //Заполнение стоблца "Месяц"

                //Печать данных
                Console.WriteLine($"{profit[i].index,10}{inFunds[i],20}{outFunds[i],20}{profit[i].value,20}");
            }
        }
        /// <summary>
        /// Функция для определения и печати самых не прибыльных месяцев
        /// </summary>
        /// <param name="Month">Массив с данными о "Прибыли" и месяцах</param>
        /// <param name="numberOfMonth">Количество худших месяцев, которые необходимо выявить</param>
        static void getWorstMonths(numeratedProfit[] Month, int numberOfMonth)
        {
            for (int i = 0; i < Month.Length; i++)                      //Перый цикл для сортировки 
            {
                for (int j = 0; j < (Month.Length - 1); j++)            //Второй цикл для сортировки
                {
                    if (Month[i].value < Month[j].value)                //Условие сортировки
                    {
                        numeratedProfit Buffer;                         //Создание буфера для сортировки
                        Buffer = Month[i];
                        Month[i] = Month[j];
                        Month[j] = Buffer;
                    }
                }
            }

            int Counter2 = 0;                                           //Создание счетчика одинаковых по прибыли худших месяцев
            for (int i = 0; i < (Month.Length - 1); i++)                //Цикл для определения одинаковых худших месяцев
            {
                int Counter1 = 0;                                       //Создание счетчика для ограничения поиска одинаковых месяцев
                if (Month[i].value == Month[i + 1].value) Counter2++;   //Увеличение счетчкика одинаковых месяцев, если прибыль текущего
                                                                        //месяца равна прибыли следующего
                else Counter1++;                                        //Увеличение счетчичка ограничения при неравенстве прибылей
                if (Counter1 > numberOfMonth) break;                    //Выход их цикла, если найдено требуемое кол-во месяцев
            }

            Console.Write("Самые убыточные месяцы: ");                  //Печать примечания к данным
            if (Counter2 + numberOfMonth < Month.Length)                //Проверка, для исключения выхода за границы, при кол-ве
                                                                        //худших месяцев равных 12
            {
                for (int i = 0; i < numberOfMonth + Counter2; i++)      //Цикл для печати данных всех месяцев
                {
                    Console.Write($"{Month[i].index,4}");               //Печать данных
                }
            }
            else
            {
                for (int i = 0; i < Month.Length; i++)                  // Цикл для печати данных худших месяцев чило которых равно
                                                                        //"numberOfMonth" + число одинаковых месяцев "Counter2"
                {
                    Console.Write($"{Month[i].index,4}");               //Печать данных
                }
            }
            Console.Write("\n");
        }
        /// <summary>
        /// Выяление и печать кол-ва прибыльных месяцев
        /// </summary>
        /// <param name="profit">Массив с данными о "Прибыли" и месяцах</param>
        static void getNumberOfProfitMonth(numeratedProfit[] profit) 
        {
            int profitMonths = 0;                                       //Создание счетчика прибыльных месяцев
            for (int i = 0; i < profit.Length; i++)                     //Цикл для вычисления кол-ва прибыльных месяцев
            {
                if (profit[i].value > 0) profitMonths++;                //Увеличение счетчика при положительной прибыли
            }

            Console.WriteLine($"Количество прибыльных месяцев: {profitMonths}");    //Печать результата
        }
        static void Main(string[] args)
        {
            int[] incomingFunds = new int[12];                          //Создание массива для хранения "Доходов"
            int[] spentFunds = new int[12];                             //Создание массива для хранения "Расходов"
            numeratedProfit[] monthProfit = new numeratedProfit[12];    //Создание массива для хранения "Прибыли" с номером месяца
                       
            Console.ForegroundColor = ConsoleColor.Black;               //Изменение цвета текста консоли
            Console.BackgroundColor = ConsoleColor.Blue;                //Изменение цвета фона консоли для печати шапки
            //Печать шапки
            Console.WriteLine($"{"Месяц",10}{"Доход, тыс. руб.",20}{"Расход, тыс. руб.",20}{"Прибыль, тыс. руб.",20}");

            Console.BackgroundColor = ConsoleColor.Magenta;                         //Изменение Фона консоли для печати данных
            FillAndPrintData(ref incomingFunds,ref spentFunds,ref monthProfit);     //Заполнение и печать данных

            //Печать меню
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write("Что необхдимо сделать?\n" +
                     "1 - показать сколько было прибыльных месяцев\n" +
                     "2 - показать какие месяцы были худшими\n" +
                     "3- выход  ");

            bool menuFlag = true;                                       //Создание флага, для выхода из меню
            while (menuFlag)                                            //Цикл меню
            {
                int menuSelector = int.Parse(Console.ReadLine());       //Переменная для организации меню

                switch (menuSelector)                                   //Организация меню
                {
                    case 1:
                        Console.BackgroundColor = ConsoleColor.Green;   //Изменение Фона консоли для печати плохих месяцев
                        getNumberOfProfitMonth(monthProfit);            //Выявление и печать кол-ва прибыльных месяцев
                        break;
                    case 2:
                        Console.BackgroundColor = ConsoleColor.Red;     //Изменение Фона консоли для печати худших месяцев
                        getWorstMonths(monthProfit, 3);                 //Выявление и печать номеров худших месяцев
                        break;
                    case 3:
                        menuFlag = false;
                        break;
                }
            }
            
            Console.ReadKey();
        }
    }
}
