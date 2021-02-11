using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_4
{
    class Program
    {
        struct numeratedProfit
        {
            public int index;
            public int value;
        }

        static void getWorstMonths(ref numeratedProfit[] Month, int numberOfMonth)
        { 
            for (int i = 0; i<Month.Length; i++)
            {
                for (int j = 0; j < (Month.Length-1); j++) 
                {
                    if (Month[i].value < Month[j].value) 
                    {
                        numeratedProfit Buffer;
                        Buffer = Month[i];
                        Month[i] = Month[j];
                        Month[j] = Buffer;
                    }
                }
            }
          
            
            int Counter2 = 0;
            for (int i = 0; i < (Month.Length - 1); i++)
            {
                int Counter1 = 0;
                if (Month[i].value == Month[i + 1].value) Counter2++;
                else Counter1++;
                if (Counter1 > numberOfMonth) break;
            }

            Console.Write("Самые убыточные месяцы: ");

            if (Counter2 + numberOfMonth < Month.Length)
            {
                for (int i = 0; i < numberOfMonth + Counter2; i++)
                {
                    Console.Write($"{Month[i].index,4}");
                }
            }
            else
            {
                for (int i = 0; i < Month.Length; i++)
                {
                    Console.Write($"{Month[i].index,4}");
                }

            }

        }


        static void Main(string[] args)
        {
            int[] incomingFunds = new int[12];
            int[] spentFunds = new int[12];
            numeratedProfit[] profit = new numeratedProfit[12];
                       
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"{"Месяц",10}{"Доход, тыс. руб.",20}{"Расход, тыс. руб.",20}{"Прибыль, тыс. руб.",20}");

            Random randomizer = new Random();

            for (int i = 0; i < incomingFunds.Length; i++) 
            {
                incomingFunds[i] = randomizer.Next(70_000, 120_000);
                spentFunds[i] = randomizer.Next(45_000, 90_000);

                profit[i].value = incomingFunds[i] - spentFunds[i];
                profit[i].index = i+1;

                Console.BackgroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"{profit[i].index,10}{incomingFunds[i],20}{spentFunds[i],20}{profit[i].value,20}");
            }

            Console.BackgroundColor = ConsoleColor.Red;
            getWorstMonths(ref profit, 3);

            int profitMonths = 0;

            for (int i = 0; i < profit.Length; i++) 
            {
                if (profit[i].value > 0) profitMonths++;
            }
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nКоличество прибыльных месяцев: {profitMonths}");




            Console.ReadKey();
        }
    }
}
