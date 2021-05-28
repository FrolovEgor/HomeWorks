using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_7
{
    class Menu
    {
        private NoteBook _note;
        public Menu(ref NoteBook note) 
        {
            _note = note;
        }
        private int MainmenuText()
        {
            Console.Clear();                    //Очистка консоли и вывод меню
            Console.Write("Выберите действие:\nФайл:" +
                                            "\n1 - Загрузить данные с диска" +
                                            "\n2 - Сохранить данные на диск" +
                                            "\nПравка:" +
                                            "\n3 - Добавить новую запись" +
                                            "\n4 - Добавить файл счета к записи" +
                                            "\n5 - Добавить данные об оплате" +
                                            "\n6 - Изменить запись" +
                                            "\n7 - Удалить запись" +
                                            "\nВид:" +
                                            "\n8 - Показать все записи" +
                                            "\n9 - Отсортировать записи" +
                                            "\n10- Открыть файл счета записи" +
                                            "\n11- Выход" +
                                            "\nВаше действие: ");

            return int.Parse(Console.ReadLine()); //Обработка ввода пользователя
        }

        private string LoadNoteText() 
        {
            Console.Clear();                    //Очистка консоли и вывод меню
            Console.Write("Выберите способ загрузки:" +
                          "\n1 - Загрузить всю базу" +
                          "\n2 - Загрузить по диапазону баз" +
                          "\nВаше действие: ");
            int loadDecision = int.Parse(Console.ReadLine());      //Обработка ввода пользователя
            if (loadDecision == 2)
            {
                Console.Write("Введите дату начала диапазона в формате дд/мм/гггг: ");
                DateTime sectionStartDate = DateTime.Parse(Console.ReadLine());
                Console.Write("Введите дату начала диапазона в формате дд/мм/гггг: ");
                DateTime sectionEndDate = DateTime.Parse(Console.ReadLine());
                return _note.LoadNotesFromFile(sectionStartDate, sectionEndDate);
            }
            return _note.LoadNotesFromFile(DateTime.MinValue, DateTime.MaxValue);
        }
        private string SaveNoteText() 
        {
            Console.Clear();                    //Очистка консоли и вывод меню
            Console.Write($"Данные будут сохранены в дирректорию {$@".\..\..\DataBase\"}. Вы уверенны? y/n: ");
            string saveDecision = Console.ReadLine();
            saveDecision = saveDecision.ToLower();
            if (saveDecision == "y") 
            {
                return _note.SaveNotesInFile();
            }
            return "Данные не сохранены";
        }
        private string AddNoteText() 
        {
            Console.Clear();                    //Очистка консоли и вывод меню

            string[] NoteValues = new string[7];

            Console.Write("Введите дату поступления счета: ");
            NoteValues[0] = Console.ReadLine();

            Console.Write("Введите номер счета: ");
            NoteValues[1] = Console.ReadLine();

            Console.Write("Введите наименование компании: ");
            NoteValues[2] = Console.ReadLine();

            Console.Write("Введите описание счета: ");
            NoteValues[3] = Console.ReadLine();

            Console.Write("Введите полную сумму счета: ");
            NoteValues[4] = Console.ReadLine();

            Console.Write("Сколько оплачено на данный момент: ");
            NoteValues[5] = Console.ReadLine();

            Console.Write("Комментарий: ");
            NoteValues[6] = Console.ReadLine();

            return _note.AddNewNote(NoteValues);
        }

        private string AddInvoceFileText() 
        {
            Console.Clear();                    //Очистка консоли и вывод меню
            Console.Write("Введите номер записи для добавления файла:");
            int noteNumber = int.Parse(Console.ReadLine());
            return _note.AddInvoiceFile(noteNumber-1);
        }

        private string AddNewPaymentText() 
        {
            Console.Clear();                    //Очистка консоли
            Console.Write("Введите номер записи: ");
            int noteIndex = int.Parse(Console.ReadLine());

            Console.Write("Введите оплаченную сумму: ");
            double paymentValue = double.Parse(Console.ReadLine());

            return _note.AddNewPayment(noteIndex - 1, paymentValue);
        }

        private string ChangeNoteText() 
        {
            Console.Clear();                    //Очистка консоли и вывод меню

            Console.Write("Введите номер записи: ");
            int noteIndex = int.Parse(Console.ReadLine());

            Console.Write("Введите номер поля, которое нужно изменить: ");
            int fieldIndex = int.Parse(Console.ReadLine());

            Console.Write("Введите новое значение выбранного поля: ");
            string newValue = Console.ReadLine();

            return _note.ChangeNote(noteIndex - 1, fieldIndex, newValue);
        }

        private string DeleteNoteText() 
        {
            Console.Clear();                    //Очистка консоли и вывод меню
            Console.Write("Выберите способ удаления:" +
                          "\n1 - Удалить по номеру записи" +
                          "\n2 - Удалить по номеру поля и его значению" +
                          "\nВаше действие: ");

            int loadDecision = int.Parse(Console.ReadLine());      //Обработка ввода пользователя

            if (loadDecision == 1)
            {
                Console.Write("Введите номер записи: ");
                int noteIndex = int.Parse(Console.ReadLine());

                Console.Write($"Запись {noteIndex} будет удалена. Вы уверенны? y/n: ");
                string deliteDecision = Console.ReadLine();
                deliteDecision = deliteDecision.ToLower();
                if (deliteDecision == "y")
                {
                    return _note.DeleteNoteByNumber(noteIndex - 1);
                }
                return "Операция отменена";
            }
            
            if(loadDecision == 2) 
            {
                return DeleteNoteByField();
            }

            return "Неверный ввод";
         }

        private string DeleteNoteByField() 
        {
            Console.Write("Введите номер поля: ");
            int fieldIndex = int.Parse(Console.ReadLine());

            Console.Write("Введите значения поля: ");
            string fieldKey = Console.ReadLine();

            Console.Write($"Записи со значением {fieldKey} присвоенным полям {fieldIndex} будут удалена. Вы уверенны? y/n: ");
            string deliteDecision = Console.ReadLine();
            deliteDecision = deliteDecision.ToLower();
            if (deliteDecision == "y")
            {
                return _note.DeleteNoteByField(fieldIndex,fieldKey);
            }
            return "Операция отменена";
        }

        private string PrintNotesText() 
        {
            Console.Clear();                    //Очистка консоли и вывод меню
            _note.PrintAllNotes();
            return "";    
        }

        private string SortNotesText() 
        {
            Console.Clear();                    //Очистка консоли и вывод меню
            Console.Write("Введите номер поля: ");
            int fieldIndex = int.Parse(Console.ReadLine());
            return _note.SortByField(fieldIndex);
        }

        private string OpenInvoiceFileText()
        {
            Console.Clear();                    //Очистка консоли и вывод меню
            Console.Write("Введите номер записи: ");
            int noteIndex = int.Parse(Console.ReadLine());
            return _note.OpenInvoiceFile(noteIndex - 1);
        }




        public void StartMenu()
        {
            bool flag = false;              //Создание флага для выхода из программы
            while (flag == false)           //Основной цикл работы меню
            {
                int selector = MainmenuText();  //Вызов текста меню и запись результата ввода в Selector для Switch
                switch (selector)               //Ветвление основного меню 
                {
                    case 1:                     //Вызов метода для расчета групп
                        Console.WriteLine(LoadNoteText());
                        Console.ReadKey();
                        break;
                    case 2:                 //Вызов метода для расчета групп и их записи на диск с вычислением скорости работы
                        Console.WriteLine(SaveNoteText());
                        Console.ReadKey();
                        break;
                    case 3:                 //Вызов метода архивации данных
                        Console.WriteLine(AddNoteText());
                        Console.ReadKey();
                        break;
                    case 4:                 //Вызов метода для разархивации данных
                        Console.WriteLine(AddInvoceFileText());
                        Console.ReadKey();
                        break;
                    case 5:                 //Вызов метода для удаленния данных 
                        Console.WriteLine(AddNewPaymentText());
                        Console.ReadKey();
                        break;
                    case 6:                 //Вызов метода для удаленния данных 
                        Console.WriteLine(ChangeNoteText());
                        Console.ReadKey();
                        break;
                    case 7:                 //Вызов метода для удаленния данных 
                        Console.WriteLine(DeleteNoteText());
                        Console.ReadKey();
                        break;
                    case 8:                 //Вызов метода для удаленния данных 
                        Console.WriteLine(PrintNotesText());
                        Console.ReadKey();
                        break;
                    case 9:                 //Вызов метода для удаленния данных 
                        Console.WriteLine(SortNotesText());
                        Console.ReadKey();
                        break;
                    case 10:                 //Вызов метода для удаленния данных 
                        Console.WriteLine(OpenInvoiceFileText());
                        Console.ReadKey();
                        break;
                    case 11:                 //Выход из меню
                        flag = true;
                        break;
                }
            }
        }


    }
}
