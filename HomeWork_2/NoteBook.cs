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

        private void PrintTop()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{"Имя",15}{"Фамилия",15}{"Возраст",15}{"Рост",15}{"Математика",15}{"Физика",15}{"Язык",15}{"Средний балл",15}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public NoteBook()
        {
            Notes = new List<Student>();
        }

        /// <summary>
        /// Метод для создания новой заметки
        /// </summary>
        /// <returns>Строковое значение об успешном/неуспешном создании новой записи</returns>
        public string NewNote()
        {
            Console.Clear();

            try
            {
                Notes.Add(new Student());
            }

            catch
            {
                return "\nЗапись не добавлена, вы ошиблись!(";
            }

            return "\nЗапись успешно добавлена!";
        }
        
        public string GetNotesCount()
        {
            return Notes.Count.ToString();
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
                Console.Write("\n Введите способ вывода(1 - без форматирования, 2 - с форматированием, 3 - С интерполяцией: ");
                int typeOfPrint = int.Parse(Console.ReadLine());

                switch (typeOfPrint)
                {
                    case 1:
                        Console.WriteLine("{0}\n", Notes[noteNumber].firstName);
                        Console.WriteLine("{0}\n", Notes[noteNumber].lastName);
                        Console.WriteLine("{0}\n", Notes[noteNumber].age);
                        Console.WriteLine("{0}\n", Notes[noteNumber].heiht);
                        Console.WriteLine("{0}\n", Notes[noteNumber].mathsScore);
                        Console.WriteLine("{0}\n", Notes[noteNumber].physicsScore);
                        Console.WriteLine("{0}\n", Notes[noteNumber].langScore);
                        Console.WriteLine("{0}\n", Notes[noteNumber].averageValue);
                        break;
                    case 2:
                        Console.WriteLine("{0, 15}\n", Notes[noteNumber].firstName);
                        Console.WriteLine("{0, 15}\n", Notes[noteNumber].lastName);
                        Console.WriteLine("{0, 15}\n", Notes[noteNumber].age);
                        Console.WriteLine("{0, 15}\n", Notes[noteNumber].heiht);
                        Console.WriteLine("{0, 15}\n", Notes[noteNumber].mathsScore);
                        Console.WriteLine("{0, 15}\n", Notes[noteNumber].physicsScore);
                        Console.WriteLine("{0, 15}\n", Notes[noteNumber].langScore);
                        Console.WriteLine("{0:##.##, 15}\n", Notes[noteNumber].averageValue);
                        break;
                    case 3:
                        PrintTop();
                        Console.WriteLine(Notes[noteNumber].ToString());
                        break;
                }    
            }
            catch 
            {
                Console.WriteLine("\nЧто-то пошло не так. Вы ошиблись!("); ;
            }            
        }

        /// <summary>
        /// Метод для вывода всех записей
        /// </summary>
        /// <param name="n"></param>
        public void PrintAllNotes()
        {
            Console.Clear();

            PrintTop();
            for (int i = 0; i < Notes.Count; i++)
            {
                Console.WriteLine(Notes[i].ToString());
            }
        }
    }
}
