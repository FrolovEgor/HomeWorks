using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace HomeWork_7
{   /// <summary>
    ///Хранит список записей PaymentNote и способы его обработки 
    /// </summary>
    class NoteBook
    {
        #region Конструкторы
        /// <summary>
        /// Инициализирует новый экземпляр класса NoteBook
        /// </summary>
        public NoteBook()
        {
            _ListOfNotes = new List<PaymentNote>();
        }
        #endregion

        #region Методы
        /// <summary>
        /// Метод для выбора файла на диске с помощью диалогового окна
        /// </summary>
        /// <returns>Полный путь до выбранного файла</returns>
        private string GetPathWithDialog()
        {
            string path = @"C:\";
            using (var fileDialog = new OpenFileDialog())       ///Инициализация диалогового окна
            {
                if (fileDialog.ShowDialog() == DialogResult.OK)     //Проверка выбран ли файл
                {
                    path = fileDialog.FileName;
                    return path;
                }
                return path;
            }
        }
        /// <summary>
        /// Метод для загрузки записей с диска
        /// </summary>
        /// <param name="sectionDateStart">НАчало диапазона для загрузки</param>
        /// <param name="sectionDateEnd">Конец диапазона для загрузки</param>
        /// <returns>Информация о результате работы метода</returns>
        public string LoadNotesFromFile(DateTime sectionDateStart, DateTime sectionDateEnd)
        {
            using (var LoadFromFileStream = new StreamReader(GetPathWithDialog()))  //Создание экзепляра потока для чтения из файла
            {

                LoadFromFileStream.ReadLine();                                      //Чтение первой строки, содержащей легенду
                while (!LoadFromFileStream.EndOfStream)                             //Цикл, контролирующий конец файла
                {
                    char[] separator = { ',' };                                     //Массив разделителей для Split
                    string currentReadedeLine = LoadFromFileStream.ReadLine();
                    string[] parsedReadedLine = currentReadedeLine.Split(separator, StringSplitOptions.RemoveEmptyEntries); //Разбиение прочитанной строки в массив

                    DateTime currentNoteDate = DateTime.Parse(parsedReadedLine[0]);                 //Копирование даты создания файла
                    if (currentNoteDate > sectionDateStart && currentNoteDate < sectionDateEnd)     //Фильтр по диапазону дат для загрузки
                    {
                                                                                                    //Чтение всех остальных переменных если проверка прошла
                        DateTime CreationTime = DateTime.Parse(parsedReadedLine[0]);
                        DateTime invoiceDate = DateTime.Parse(parsedReadedLine[1]);
                        string accountNumber = parsedReadedLine[2];
                        string company = parsedReadedLine[3];
                        string description = parsedReadedLine[4];
                        double totalPayment = double.Parse(parsedReadedLine[5]);
                        double nowPaid = double.Parse(parsedReadedLine[6]);
                        string comment = parsedReadedLine[8];
                        string pathToInvoiceFile = parsedReadedLine[9];

                        _ListOfNotes.Add(new PaymentNote(CreationTime, invoiceDate, accountNumber, company,     //Вызов конструктора и создание записи
                                         description, totalPayment, nowPaid, comment, pathToInvoiceFile));
                    }
                }
            }
            return "Записи загружены";
        }
        /// <summary>
        /// Метод сохраняющий данные на диск в папку "БазаДанных"
        /// </summary>
        /// <returns>Информация о результате работы метода</returns>
        public string SaveNotesInFile()
        {
            if (!Directory.Exists($@".\..\..\DataBase\"))               //Проверка существования директории
            {
                Directory.CreateDirectory($@".\..\..\DataBase\");       //Создание директории если ее не существует
            }

            using (var SaveFileStream = new StreamWriter($@".\..\..\DataBase\Base.csv"))        //Создание экземпляра потока для записи в файл
            {
                SaveFileStream.WriteLine(PaymentNote.PrintTop());       //Запись легенды
                for (int i = 0; i < _ListOfNotes.Count; i++)            //Цикл для записи всех данных
                {
                    SaveFileStream.WriteLine(_ListOfNotes[i].ToString());
                }
            }
            return "Записи сохранены";
        }
        /// <summary>
        /// Метод для добавление новой записи
        /// </summary>
        /// <param name="noteFieldValues">Массив, содержащий значения полей для записи</param>
        /// <returns>Информация о результате работы метода</returns>
        public string AddNewNote(params string[] noteFieldValues)
        {
            if (noteFieldValues.Length != 7)                    //Проверка количества аргументов
            {
                return "Запись не добавлена, неверные аргументы";
            }

            _ListOfNotes.Add(new PaymentNote(DateTime.Parse(noteFieldValues[0]),    //Вызов констуктора
                                             noteFieldValues[1],
                                             noteFieldValues[2], 
                                             noteFieldValues[3],
                                             double.Parse(noteFieldValues[4]),
                                             double.Parse(noteFieldValues[5]),
                                             noteFieldValues[6]));
            return "Запись добавленна";
        }
        /// <summary>
        /// Метод добаляющий к выбранной записи файл счета (PDF или иной), и копирующий ее в папку "Документы о платежах"
        /// </summary>
        /// <param name="numberOfNote">Номер записи</param>
        /// <returns>Информация о результате работы метода</returns>
        public string AddInvoiceFile(int numberOfNote)
        {
            if (numberOfNote >= 0 && numberOfNote < _ListOfNotes.Count)             //Проверка, что введена правильная запись
            {
                _ListOfNotes[numberOfNote].AddInvoiceFile(GetPathWithDialog());
                return "Счет прикреплен";
            }
            return "Такой записи не существует";
        }
        /// <summary>
        ///  Добавления данных об оплате по счету к выбранной записи
        /// </summary>
        /// <param name="numberOfNote">Номер записи</param>
        /// <param name="NewPaymentValue">Сумма внесенной оплаты</param>
        /// <returns>Информация о результате работы метода</returns>
        public string AddNewPayment(int numberOfNote, double NewPaymentValue)
        {
            _ListOfNotes[numberOfNote].AddPayment(NewPaymentValue);
            return "Добавлено новая оплата";
        }
        /// <summary>
        /// Метод для изменение доступных полей у выбранной записи
        /// </summary>
        /// <param name="noteNumber">Номер записи</param>
        /// <param name="field">Номер поля, которое необходимо изменить</param>
        /// <param name="value">Новое значение дял поля</param>
        /// <returns>Информация о результате работы метода</returns>
        public string ChangeNote(int noteNumber, int field, string value) 
        {
            switch (field) 
            {
                case 2:
                    _ListOfNotes[noteNumber].InvoiceDate = DateTime.Parse(value);
                    break;
                case 3:
                    _ListOfNotes[noteNumber].AccountNumber = value;
                    break;
                case 4:
                    _ListOfNotes[noteNumber].Company = value;
                    break;
                case 5:
                    _ListOfNotes[noteNumber].Description = value;
                    break;
                case 6:
                    _ListOfNotes[noteNumber].TotalPayment = double.Parse(value);
                    break;
                case 9:
                    _ListOfNotes[noteNumber].Comment = value;
                    break;
                default:
                    return "Данное поле нельзя изменить";
            }
            return "Запись изменена";
        }
        /// <summary>
        /// Метод для удаления записи по номеру
        /// </summary>
        /// <param name="noteNumber">Номер записи</param>
        /// <returns>Информация о результате работы метода</returns>
        public string DeleteNoteByNumber(int noteNumber)
        {
            if (noteNumber >= 0 && noteNumber < _ListOfNotes.Count)     //Проверка, существует ли такая запись
            {
                _ListOfNotes.RemoveAt(noteNumber);
                return $"Запись {noteNumber + 1} удалена";
            }
            return "Такой записи не существует";

        }
        /// <summary>
        /// Удаление записи по значению выбранного поля
        /// </summary>
        /// <param name="fieldNumber">Поле где производиться поиск</param>
        /// <param name="fieldKey">Значение поля</param>
        /// <returns>Информация о результате работы метода</returns>
        public string DeleteNoteByField(int fieldNumber, string fieldKey)
        {
            int numberOfdelitedFields = 0;          //Пременная, хранящая информацию, сколько записей удалено

            switch (fieldNumber)
            {
                case 1:
                    numberOfdelitedFields = _ListOfNotes.RemoveAll(note => note.CreationTime == DateTime.Parse(fieldKey));
                    break;
                case 2:
                    numberOfdelitedFields = _ListOfNotes.RemoveAll(note => note.InvoiceDate == DateTime.Parse(fieldKey));
                    break;
                case 3:
                    numberOfdelitedFields = _ListOfNotes.RemoveAll(note => note.AccountNumber == fieldKey);
                    break;
                case 4:
                    numberOfdelitedFields = _ListOfNotes.RemoveAll(note => note.Company == fieldKey);
                    break;
                case 5:
                    numberOfdelitedFields = _ListOfNotes.RemoveAll(note => note.Description == fieldKey);
                    break;
                case 6:
                    numberOfdelitedFields = _ListOfNotes.RemoveAll(note => note.TotalPayment == double.Parse(fieldKey));
                    break;
                case 7:
                    numberOfdelitedFields = _ListOfNotes.RemoveAll(note => note.NowPaid == double.Parse(fieldKey));
                    break;
                case 8:
                    numberOfdelitedFields = _ListOfNotes.RemoveAll(note => note.LeaveToPay == double.Parse(fieldKey));
                    break;
                case 9:
                    numberOfdelitedFields = _ListOfNotes.RemoveAll(note => note.Comment == fieldKey);
                    break;
                case 10:
                    numberOfdelitedFields = _ListOfNotes.RemoveAll(note => note.PathToInvoiceFile == fieldKey);
                    break;
                default:
                    return "Записи не удалены. Неверно выбран номер поля";
            }
            if (numberOfdelitedFields < 1)
            {
                return $"Записи не удалены. Нет полей {fieldNumber} со значением {fieldKey}";
            }
            return $"Записи со значением {fieldKey} удалены. Количество удаленных записей {numberOfdelitedFields}";
        }
        /// <summary>
        /// Метод для печати всех записей в консоль
        /// </summary>
        public void PrintAllNotes()
        {
            int noteIndex = 1;                                          //Индес распечатаеваемой записи
            Console.WriteLine($"{"№",5}" + PaymentNote.PrintTop());     //Печать легенды
            foreach (PaymentNote note in _ListOfNotes)                  //Цикл для печати индексов и записей
            {
                Console.WriteLine($"{noteIndex,5}" + note.ToString());
                noteIndex += 1;
            }

        }
        /// <summary>
        /// Метод для сортировки записей по выбранному полю
        /// </summary>
        /// <param name="FieldToSort">Номер поля, по которому осущетсвляется сортировка</param>
        /// <returns>Информация о результате работы метода</returns>
        public string SortByField(int FieldToSort)
        {
            if (FieldToSort > 0 && FieldToSort < 11)                    //Проверка правильности ввода                        
            {
                _ListOfNotes.Sort(new PaymentNoteComparers(FieldToSort));   //Создание экзепляра наследника IComparer для выбранного поля и сортировка
                return "Данные отсортированы";
            }
            return "Такого поля не существует";
        }
        /// <summary>
        /// Сортировка по дате создания записей
        /// </summary>
        /// <returns>Информация о результате работы метода</returns>
        public string SortByField() 
        {
            return SortByField(0);
        }
        /// <summary>
        /// Открывает файл счета, прикрепленный к выбранной записи
        /// </summary>
        /// <param name="numberOfNote">Номер записи</param>
        /// <returns>Информация о результате работы метода</returns>
        public string OpenInvoiceFile(int numberOfNote)
        {
            Process.Start(_ListOfNotes[numberOfNote].PathToInvoiceFile);
            return "Счет открыт";
        }
        #endregion

        #region Поля 
        private List<PaymentNote> _ListOfNotes;
        #endregion
    }
}
