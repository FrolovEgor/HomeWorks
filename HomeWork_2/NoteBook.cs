using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_2
{
    /// <summary>
    /// Класс для хранения всех заметок о студентах и вывода заметок в консоль
    /// </summary>
    class NoteBook
    {
        private List<Student> Notes;

       
        public NoteBook()
        {
            Notes = new List<Student>();
        }

        /// <summary>
        /// Метод для создания новой заметки
        /// </summary>
        /// <returns>Строковое значение об успешном/неуспешном создании новой записи</returns>
        public void NewNote()
        {
            Console.Clear();

            try
            {
                Notes.Add(new Student());
                Console.WriteLine("\nЗапись успешно добавлена!");
            }

            catch
            {
               Console.WriteLine ("\nЗапись не добавлена, вы ошиблись!(");
            }
        }
        
        public string GetNotesCount()
        {
            return Notes.Count.ToString();
        }


        public void PrintNotes(int Width, int Height, int Number)
        {
            Console.Clear();
            int horizontalPrintPosition = (Width - 87) / 2;
            int verticalPrintPosition = (Height - Notes.Count - 1) / 2;
            if (horizontalPrintPosition < 0)
            {
                horizontalPrintPosition = 0;
            }

            if (verticalPrintPosition < 0)
            {
                verticalPrintPosition = 0;
            }
            Console.SetCursorPosition(horizontalPrintPosition, verticalPrintPosition);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(Notes[0].PrintTop());
            Console.ForegroundColor = ConsoleColor.White;

            if (Number < 0)
            {

                for (int i = 0; i < Notes.Count; i++)
                {
                    Console.SetCursorPosition(horizontalPrintPosition, verticalPrintPosition + i + 1);
                    Console.WriteLine(Notes[i].ToString());
                }
            }
            else
            {
                Console.WriteLine(Notes[Number].ToString());
            }
        }



        /// <summary>
        /// Метод для печати одной заметки
        /// </summary>
        /// 
        public void PrintOneNote()
        {
            Console.Clear();
            try
            {
                Console.Write("Введите номер записи для отображения: ");
                int noteNumber = int.Parse(Console.ReadLine()) - 1;
                Console.Write("\n Введите способ вывода(1 - без форматирования, 2 - с форматированием, 3 - С интерполяцией): ");
                int typeOfPrint = int.Parse(Console.ReadLine());

                switch (typeOfPrint)
                {
                    case 1:
                        Console.Write("Имя: {0}\n", Notes[noteNumber].firstName);
                        Console.Write("Фамилия: {0}\n", Notes[noteNumber].lastName);
                        Console.Write("Возраст: {0}\n", Notes[noteNumber].age);
                        Console.Write("Рост: {0}\n", Notes[noteNumber].heiht);
                        Console.Write("Баллы за математику:{0}\n", Notes[noteNumber].mathsScore);
                        Console.Write("Баллы за физику: {0}\n", Notes[noteNumber].physicsScore);
                        Console.Write("Баллы за язык: {0}\n", Notes[noteNumber].langScore);
                        Console.Write("Средний балл: {0}\n", Notes[noteNumber].averageValue);
                        break;
                    case 2:
                        Console.Write("Имя: {0, 25}\n", Notes[noteNumber].firstName);
                        Console.Write("Фамилия: {0, 21}\n", Notes[noteNumber].lastName);
                        Console.Write("Возраст: {0, 21}\n", Notes[noteNumber].age);
                        Console.Write("Рост: {0, 24}\n", Notes[noteNumber].heiht);
                        Console.Write("Баллы за математику: {0, 9}\n", Notes[noteNumber].mathsScore);
                        Console.Write("Баллы за физику: {0, 13}\n", Notes[noteNumber].physicsScore);
                        Console.Write("Баллы за язык: {0, 15}\n", Notes[noteNumber].langScore);
                        Console.Write("Средний балл: {0:00.00}\n", Notes[noteNumber].averageValue);
                        break;
                    case 3:
                        PrintNotes(0, 0, noteNumber);
                        break;
                }    
            }
            catch 
            {
                Console.WriteLine("\nЧто-то пошло не так. Вы ошиблись!("); ;
            }            
        }



    }
}
