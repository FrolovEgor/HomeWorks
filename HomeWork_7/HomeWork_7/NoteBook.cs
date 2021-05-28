using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace HomeWork_7
{
    class NoteBook
    {
        private List<PaymentNote> _ListOfNotes;
        public NoteBook()
        {
            _ListOfNotes = new List<PaymentNote>();
        }

        private string GetPathWithDialog()
        {
            string path = @"C:\";
            using (var fileDialog = new OpenFileDialog())
            {
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    path = fileDialog.FileName;
                    return path;
                }
                return path;
            }
        }

        public string AddNewNote(params string[] noteFieldValues)
        {
            if (noteFieldValues.Length != 7)
            {
                return "Запись не добавлена, неверные аргументы";
            }

            _ListOfNotes.Add(new PaymentNote(DateTime.Parse(noteFieldValues[0]), 
                                             noteFieldValues[1],
                                             noteFieldValues[2], 
                                             noteFieldValues[3],
                                             double.Parse(noteFieldValues[4]),
                                             double.Parse(noteFieldValues[5]),
                                             noteFieldValues[6]));
            return "Запись добавленна";
        }


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

        public string AddNewPayment(int numberOfNote, double NewPaymentValue)
        {
            _ListOfNotes[numberOfNote].ChangePayment(NewPaymentValue);
            return "Добавлено новая оплата";
        }

        public string AddInvoiceFile(int numberOfNote)
        {
            if (numberOfNote >= 0 && numberOfNote < _ListOfNotes.Count) 
            {
                _ListOfNotes[numberOfNote].AddInvoiceFile(GetPathWithDialog());
                return "Счет прикреплен";
            }
            return "Такой записи не существует";
        }
        public string OpenInvoiceFile(int numberOfNote)
        {
            Process.Start(_ListOfNotes[numberOfNote].PathToInvoiceFile);
            return "Счет открыт";
        }

        public string DeleteNoteByNumber(int noteNumber) 
        {
            if (noteNumber >= 0 && noteNumber < _ListOfNotes.Count) 
            {
                _ListOfNotes.RemoveAt(noteNumber);
                return $"Запись {noteNumber + 1} удалена";
            }
            return "Такой записи не существует";

        }

        public string DeleteNoteByField(int fieldNumber, string fieldKey)
        {
            int numberOfdelitedFields = 0;

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
            if (numberOfdelitedFields <1 ) 
            {
                return $"Записи не удалены. Нет полей {fieldNumber} со значением {fieldKey}";
            }
            return $"Записи со значением {fieldKey} удалены. Количество удаленных записей {numberOfdelitedFields}";
        }
        
        public string LoadNotesFromFile(DateTime sectionDateStart, DateTime sectionDateEnd)
        {
            using (var LoadFromFileStream = new StreamReader(GetPathWithDialog()))
            {

                LoadFromFileStream.ReadLine();
                while (!LoadFromFileStream.EndOfStream)
                {
                    char[] separator = { ',' };
                    string currentReadedeLine = LoadFromFileStream.ReadLine();
                    string[] parsedReadedLine = currentReadedeLine.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                    DateTime currentNoteDate = DateTime.Parse(parsedReadedLine[0]);
                    if (currentNoteDate > sectionDateStart && currentNoteDate < sectionDateEnd)
                    {

                        DateTime CreationTime = DateTime.Parse(parsedReadedLine[0]);
                        DateTime invoiceDate = DateTime.Parse(parsedReadedLine[1]);
                        string accountNumber = parsedReadedLine[2];
                        string company = parsedReadedLine[3];
                        string description = parsedReadedLine[4];
                        double totalPayment = double.Parse(parsedReadedLine[5]);
                        double nowPaid = double.Parse(parsedReadedLine[6]);
                        string comment = parsedReadedLine[8];
                        string pathToInvoiceFile = parsedReadedLine[9];

                        _ListOfNotes.Add(new PaymentNote(CreationTime, invoiceDate, accountNumber, company,
                                         description, totalPayment, nowPaid, comment, pathToInvoiceFile));
                    }
                }
            }
            return "Записи загружены";
        }
        public string SaveNotesInFile()
        {
            if (!Directory.Exists($@".\..\..\DataBase\"))
            {
                Directory.CreateDirectory($@".\..\..\DataBase\");
            }

            using (var SaveFileStream = new StreamWriter($@".\..\..\DataBase\Base.csv"))
            {
                SaveFileStream.WriteLine(PaymentNote.PrintTop());
               for (int i = 0; i < _ListOfNotes.Count; i++)
                {
                    SaveFileStream.WriteLine(_ListOfNotes[i].ToString());
                }
            }
            return "Записи сохранены";
        }
        public string SortByField(int FieldToSort)
        {
            if (FieldToSort > 0 && FieldToSort < 11)
            {
                _ListOfNotes.Sort(new PaymentNoteComparers(FieldToSort));
                return "Данные отсортированы";
            }
            return "Такой записи не существует";
        }
        public string SortByField() 
        {
            return SortByField(0);
        }
        public void PrintAllNotes() 
        {
            int noteIndex = 1;
            Console.WriteLine($"{"№", 5}"+PaymentNote.PrintTop());
            foreach (PaymentNote note in _ListOfNotes) 
            {
                Console.WriteLine($"{noteIndex,5}"+note.ToString());
                noteIndex += 1;
            }
        
        }
    }
}
