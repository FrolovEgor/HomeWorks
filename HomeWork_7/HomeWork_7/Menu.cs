using System;


namespace HomeWork_7
{   /// <summary>
    ///Меню для взаимодействия со списком записей NoteBook через консоль 
    /// </summary>
    class Menu
    {
        #region Конструкторы
        /// <summary>
        /// Инициализирует новый экземпляр класса Menu
        /// </summary>
        /// <param name="note">Ссылка на NoteBook</param>
        public Menu(ref NoteBook note) 
        {
            _note = note;
        }
        #endregion

        #region Методы
        /// <summary>
        /// Печать текста главного меню и обработка выбора пользователя
        /// </summary>
        /// <returns>Номер действия выбранного пользователем</returns>
        private int MainmenuText()
        {
            Console.Clear();
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

            return int.Parse(Console.ReadLine());
        }
        /// <summary>
        /// Печать текста меню загрузки базы NoteBook и обработка выбора пользователя
        /// </summary>
        /// <returns>Результат работы программы</returns>
        private string LoadNoteText() 
        {
            Console.Clear();
            Console.Write("Выберите способ загрузки:" +
                          "\n1 - Загрузить всю базу" +
                          "\n2 - Загрузить по диапазону баз" +
                          "\nВаше действие: ");
            int loadDecision = int.Parse(Console.ReadLine());

            if (loadDecision == 2)          //Если базу данных необходимо загрузить частично вводится диапазон дат для загрузки
            {
                Console.Write("Введите дату начала диапазона в формате дд/мм/гггг: ");
                DateTime sectionStartDate = DateTime.Parse(Console.ReadLine());
                Console.Write("Введите дату начала диапазона в формате дд/мм/гггг: ");
                DateTime sectionEndDate = DateTime.Parse(Console.ReadLine());

                return _note.LoadNotesFromFile(sectionStartDate, sectionEndDate);
            }
            return _note.LoadNotesFromFile(DateTime.MinValue, DateTime.MaxValue);   //Загрузка базы по всему диапазону дат
        }
        /// <summary>
        /// Печать текста меню сохранения базы NoteBook и обработка выбора пользователя
        /// </summary>
        /// <returns>Результат работы программы</returns>
        private string SaveNoteText() 
        {
            Console.Clear();
            Console.Write($"Данные будут сохранены, вы уверенны? y/n: ");
            string saveDecision = Console.ReadLine();

            saveDecision = saveDecision.ToLower();      //Приведение ответа пользователя к нижнему регистру

            if (saveDecision == "y") 
            {
                return _note.SaveNotesInFile();
            }
            return "Данные не сохранены";
        }
        /// <summary>
        /// Печать текста меню добавления новой записи и обработка выбора пользователя
        /// </summary>
        /// <returns>Результат работы программы</returns>
        private string AddNoteText() 
        {
            Console.Clear();

            

            string[] NoteValues = new string[7];                //Создание массива для хранения значений введеных пользователем

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

            return _note.AddNewNote(NoteValues);                //Создание нововй записи
        }
        /// <summary>
        /// Печать текста меню добавления файла счета и обработка выбора пользователя
        /// </summary>
        /// <returns>Результат работы программы</returns>
        private string AddInvoceFileText() 
        {
            Console.Clear();
            Console.Write("Введите номер записи для добавления файла:");
            int noteNumber = int.Parse(Console.ReadLine());

            return _note.AddInvoiceFile(noteNumber-1);          //Добавление к записи noteNumber-1 т.к. в консоли записи пронумерованы от 1 а не от 0
        }
        /// <summary>
        /// Печать текста меню добавления оплаты по счету и обработка выбора пользователя
        /// </summary>
        /// <returns>Результат работы программы</returns>
        private string AddNewPaymentText() 
        {
            Console.Clear();
            Console.Write("Введите номер записи: ");
            int noteIndex = int.Parse(Console.ReadLine());

            Console.Write("Введите оплаченную сумму: ");
            double paymentValue = double.Parse(Console.ReadLine());

            return _note.AddNewPayment(noteIndex - 1, paymentValue); //Добавление к записи noteNumber-1 т.к. в консоли записи пронумерованы от 1 а не от 0
        }
        /// <summary>
        /// Печать текста меню изменения выбранного поля записи и обработка выбора пользователя
        /// </summary>
        /// <returns>Результат работы программы</returns>
        private string ChangeNoteText() 
        {
            Console.Clear();

            Console.Write("Введите номер записи: ");
            int noteIndex = int.Parse(Console.ReadLine());

            Console.Write("Введите номер поля, которое нужно изменить: ");
            int fieldIndex = int.Parse(Console.ReadLine());

            Console.Write("Введите новое значение выбранного поля: ");
            string newValue = Console.ReadLine();

            return _note.ChangeNote(noteIndex - 1, fieldIndex, newValue);  //Изменение записи noteNumber-1 т.к. в консоли записи пронумерованы от 1 а не от 0
        }
        /// <summary>
        /// Печать текста меню удаления записи и обработка выбора пользователя
        /// </summary>
        /// <returns>Результат работы программы</returns>
        private string DeleteNoteText() 
        {
            Console.Clear();

            Console.Write("Выберите способ удаления:" +
                          "\n1 - Удалить по номеру записи" +
                          "\n2 - Удалить по номеру поля и его значению" +
                          "\nВаше действие: ");

            int loadDecision = int.Parse(Console.ReadLine());

            if (loadDecision == 1)                      //Текст и ввод для удаления по номеру записи
            {
                Console.Write("Введите номер записи: ");
                int noteIndex = int.Parse(Console.ReadLine());

                Console.Write($"Запись {noteIndex} будет удалена. Вы уверенны? y/n: ");       //Приведение ответа пользователя к нижнему регистру
                string deliteDecision = Console.ReadLine();
                
                deliteDecision = deliteDecision.ToLower();

                if (deliteDecision == "y")
                {
                    return _note.DeleteNoteByNumber(noteIndex - 1);
                }
                return "Операция отменена";
            }
            
            if(loadDecision == 2)                   //Если требуется удаление по значению поля вызов другого меню
            {
                return DeleteNoteByField();
            }

            return "Неверный ввод";
         }
        /// <summary>
        /// Печать текста меню удаления записи по значению поля и обработка выбора пользователя
        /// </summary>
        /// <returns>Результат работы программы</returns>
        private string DeleteNoteByField() 
        {
            Console.Write("Введите номер поля: ");
            int fieldIndex = int.Parse(Console.ReadLine());

            Console.Write("Введите значения поля: ");
            string fieldKey = Console.ReadLine();

            Console.Write($"Записи со значением {fieldKey} присвоенным полям {fieldIndex} будут удалена. Вы уверенны? y/n: ");       //Приведение ответа пользователя к нижнему регистру
            string deliteDecision = Console.ReadLine();
            
            deliteDecision = deliteDecision.ToLower();
            
            if (deliteDecision == "y")
            {
                return _note.DeleteNoteByField(fieldIndex,fieldKey);
            }
            return "Операция отменена";
        }
        /// <summary>
        /// Метод для печати всех записей
        /// </summary>
        /// <returns>Результат работы программы</returns>
        private string PrintNotesText() 
        {
            Console.Clear();
            _note.PrintAllNotes();
            return "";    
        }
        /// <summary>
        /// Печать текста меню сортировки и обработка выбора пользователя
        /// </summary>
        /// <returns>Результат работы программы</returns>
        private string SortNotesText() 
        {
            Console.Clear();
            Console.Write("Введите номер поля: ");
            int fieldIndex = int.Parse(Console.ReadLine());

            return _note.SortByField(fieldIndex);
        }
        /// <summary>
        /// Печать текста меню открытия файла счета и обработка выбора пользователя
        /// </summary>
        /// <returns>Результат работы программы</returns>
        private string OpenInvoiceFileText()
        {
            Console.Clear();
            Console.Write("Введите номер записи: ");
            int noteIndex = int.Parse(Console.ReadLine());

            return _note.OpenInvoiceFile(noteIndex - 1);    //Запись noteNumber-1 т.к. в консоли записи пронумерованы от 1 а не от 0
        }
        /// <summary>
        /// Запуск меню для работы с NoteBook
        /// </summary>
        public void StartMenu()
        {
            bool flag = false;              //Создание флага для выхода из программы
            while (flag == false)           //Основной цикл работы меню
            {
                int selector = MainmenuText();  //Вызов текста меню и запись результата ввода в Selector для Switch
                switch (selector)               //Ветвление основного меню 
                {
                    case 1:                 //Вызов меню для загрузки записей
                        Console.WriteLine(LoadNoteText());
                        Console.ReadKey();
                        break;
                    case 2:                 //Вызов меню для сохранения записей
                        Console.WriteLine(SaveNoteText());
                        Console.ReadKey();
                        break;
                    case 3:                 //Вызов меню для добавления записей
                        Console.WriteLine(AddNoteText());
                        Console.ReadKey();
                        break;
                    case 4:                 //Вызов меню для добавлеия файлаов счетов записей
                        Console.WriteLine(AddInvoceFileText());
                        Console.ReadKey();
                        break;
                    case 5:                 //Вызов меню для добавления оплаты к записям 
                        Console.WriteLine(AddNewPaymentText());
                        Console.ReadKey();
                        break;
                    case 6:                 //Вызов меню для изменения записей
                        Console.WriteLine(ChangeNoteText());
                        Console.ReadKey();
                        break;
                    case 7:                 //Вызов меню для удаления записей
                        Console.WriteLine(DeleteNoteText());
                        Console.ReadKey();
                        break;
                    case 8:                 //Вызов меню для печати записей 
                        Console.WriteLine(PrintNotesText());
                        Console.ReadKey();
                        break;
                    case 9:                 //Вызов меню для сортировки записей 
                        Console.WriteLine(SortNotesText());
                        Console.ReadKey();
                        break;
                    case 10:                 //Вызов меню для открытия файлов счетов записей 
                        Console.WriteLine(OpenInvoiceFileText());
                        Console.ReadKey();
                        break;
                    case 11:                 //Выход из меню
                        flag = true;
                        break;
                }
            }
        }
        #endregion

        #region Поля
        private NoteBook _note;
    #endregion
}
}
