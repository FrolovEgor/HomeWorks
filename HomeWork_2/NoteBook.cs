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
        //Создание списка студентов
        private List<Student> Notes;

       //Конструктор, выделящий память для хранения списка студентов
        public NoteBook()
        {
            Notes = new List<Student>();
        }

        /// <summary>
        /// Метод для создания новой заметки
        /// </summary>
        /// <returns>Возвращает строковое значение об успешном/неуспешном создании новой записи</returns>
        public void NewNote()
        {
            //Очистка окна консоли
            Console.Clear();

            //Обработка ошибок ввода с консоли
            try
            {
                //Вызов конструктора для добавления новго студента
                Notes.Add(new Student());
                Console.WriteLine("\nЗапись успешно добавлена!");
            }

            //Блок вывода в консоль информации об ошибке ввода
            catch
            {
               Console.WriteLine ("\nЗапись не добавлена, вы ошиблись!(");
            }
        }
        
        /// <summary>
        /// Метод сообщающий о количестве записей
        /// </summary>
        /// <returns>Возвращает строковое значение количества записей</returns>
        public string GetNotesCount()
        {
            return Notes.Count.ToString();
        }

        /// <summary>
        /// Метод для печати элементов из списка
        /// </summary>
        /// <param name="Width">ширина текущего окна консоли</param>
        /// <param name="Height">Высота текущего окна консоли</param>
        /// <param name="Number">Число записей, которые необходимо напечатать (если "<0" печатает все запис) </param>
        public void PrintNotes(int Width, int Height, int Number)
        {
            //Очистка окна консоли
            Console.Clear();

            //Вычисление горизонтальной позиции курсора для вывода печати по центру
            int horizontalPrintPosition = (Width - 87) / 2;

            //Вычисление вертикальной позиции курсора для вывода печати по центру
            int verticalPrintPosition = (Height - Notes.Count - 1) / 2;

            //Устранение ошибок, вызванных слишком маленьким горизонтальным размером окна консоли
            if (horizontalPrintPosition < 0)
            {
                horizontalPrintPosition = 0;
            }

            //Устранение ошибок, вызванных слишком маленьким горизонтальным размером окна консоли
            if (verticalPrintPosition < 0)
            {
                verticalPrintPosition = 0;
            }

            //Позиционирование курсора по центру экрана с учетом ширины строк и количества записей
            Console.SetCursorPosition(horizontalPrintPosition, verticalPrintPosition);

            //Изменение цвета печати, для печати шапки таблицы вывода и ее печать
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(Notes[0].PrintTop());

            //Изменение цвета печати, для печати данных
            Console.ForegroundColor = ConsoleColor.White;

            //Проверка, печатать все элементы или только один
            if (Number < 0)
            {

                //Цикл для печати всех элементов со сдвигом курсора в зависимости от строки
                for (int i = 0; i < Notes.Count; i++)
                {
                    Console.SetCursorPosition(horizontalPrintPosition, verticalPrintPosition + i + 1);
                    Console.WriteLine(Notes[i].ToString());
                }
            }

            //Печать одного элемента с номер Number
            else
            {
                Console.WriteLine(Notes[Number].ToString());
            }
        }



        /// <summary>
        /// Метод для печати одного элемента с разными типами печати (не форматированный, форматированный и с интерполяцией)
        /// </summary>
        /// 
        public void PrintOneNote()
        {

            //Очистка окна консоли
            Console.Clear();

            //Обработка ошибок ввода с консоли
            try
            {
                //Запрос номера элемента для печати
                Console.Write("Введите номер записи для отображения: ");
                int noteNumber = int.Parse(Console.ReadLine()) - 1;

                //Запрос способа форматирования для печати выбранного элемента
                Console.Write("\n Введите способ вывода(1 - без форматирования, 2 - с форматированием, 3 - С интерполяцией): ");
                int typeOfPrint = int.Parse(Console.ReadLine());

               //Ветвление, в зависимости от выбранного типа форматирования
                switch (typeOfPrint)
                {

                    //Печать элемента без форматирования
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

                    //Печать с форматированием
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

                    //Печать с интерполяцией
                    case 3:
                        PrintNotes(0, 0, noteNumber);
                        break;
                }    
            }

            //Вывод сообщения на экран при ошибке ввода
            catch 
            {
                Console.WriteLine("\nЧто-то пошло не так. Вы ошиблись!("); ;
            }            
        }
    }
}
