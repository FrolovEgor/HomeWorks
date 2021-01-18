using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_2
{
    class Program
    {
       
        static void Main(string[] args)
        {
            NoteBook Book = new NoteBook();

            int MenuSelector;

             do
            {
                Console.Clear();
                Console.Write("Меню управления записной книжкой. Для выполнения действия, введите его номер из списка\n" +
                                  "\n   Всего записей в базе: " + Book.GetNotesCount() +
                                  "\n\n   1. Создать новую запись" +
                                  "\n   2. Напечатать информацию об одной записи "+ 
                                  "\n   3. Напечатать все записи" +
                                  "\n   4. Напечатать все записи по центру экрана" +
                                  "\n   5. Выйти из программы" +
                                  "\n\nВаша выбор - ");

                try
                {
                    MenuSelector = int.Parse(Console.ReadLine());
                }

                catch {

                    MenuSelector = 0;
                }

                switch (MenuSelector)
                {
                    case 1:
                        Book.NewNote();
                        Console.ReadKey();
                        break;
                    case 2:
                        Book.PrintOneNote();
                        Console.ReadKey();
                        break;
                    case 3:
                        //Book.PrintAllNotes();
                        Book.PrintNotes(0, 0, -1);
                        Console.ReadKey();
                        break;
                    case 4:

                        int width = Console.WindowWidth;
                        int height = Console.WindowHeight;
                        Book.PrintNotes(width, height, -1);
                        Console.ReadKey();
                        break;
                }
                                
            } while (MenuSelector != 5);
           

            #region Home Work Tasks
            // Заказчик просит написать программу «Записная книжка». Оплата фиксированная - $ 120.

            // В данной программе, должна быть возможность изменения значений нескольких переменных для того,
            // чтобы персонифецировать вывод данных, под конкретного пользователя.

            // Для этого нужно: 
            // 1. Создать несколько переменных разных типов, в которых могут храниться данные
            //    - имя;
            //    - возраст;
            //    - рост;
            //    - баллы по трем предметам: история, математика, русский язык;

            // 2. Реализовать в системе автоматический подсчёт среднего балла по трем предметам, 
            //    указанным в пункте 1.

            // 3. Реализовать возможность печатки информации на консоли при помощи 
            //    - обычного вывода;
            //    - форматированного вывода;
            //    - использования интерполяции строк;

            // 4. Весь код должен быть откомментирован с использованием обычных и хml-комментариев

            // **
            // 5. В качестве бонусной части, за дополнительную оплату $50, заказчик просит реализовать 
            //    возможность вывода данных в центре консоли.
            #endregion
        }
    }
}
