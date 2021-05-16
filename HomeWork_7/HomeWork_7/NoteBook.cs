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

        public string AddNewNote()
        {
            Console.Write("Введите дату поступления счета: ");
            DateTime invoiceDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Введите номер счета: ");
            string accountNumber = Console.ReadLine();
            Console.Write("Введите наименование компании: ");
            string company = Console.ReadLine();
            Console.Write("Введите описание счета: ");
            string description = Console.ReadLine();
            Console.Write("Комментарий: ");
            string comment = Console.ReadLine();
            Console.Write("Введите полную сумму счета: ");
            double totalPayment = double.Parse(Console.ReadLine());
            Console.Write("Сколько оплачено на данный момент: ");
            double nowPaid = double.Parse(Console.ReadLine());

            _ListOfNotes.Add(new PaymentNote(invoiceDate, accountNumber, company, description, comment, totalPayment, nowPaid));
            return "Запись добавленна";
        }

        public void AddInvoiceFile(int numberOfNote)
        {
            _ListOfNotes[numberOfNote].AddInvoiceFile(GetPathWithDialog());
        }
        public void OpenInvoiceFile(int numberOfNote)
        {
            Process.Start(_ListOfNotes[numberOfNote].PathToInvoiceFile);
        }

        public void AddNewPayment(int numberOfNote, int NewPaymentNumber)
        {
            _ListOfNotes[0].ChangePayment(NewPaymentNumber);
        }

        public void LoadNotesFromFile()
        {
            using (var LoadFromFileStream = new StreamReader(GetPathWithDialog()))
            {

                LoadFromFileStream.ReadLine();
                while (!LoadFromFileStream.EndOfStream)
                {
                    char[] separator = { ',' };
                    string currentReadedeLine = LoadFromFileStream.ReadLine();
                    string[] parsedReadedLine = currentReadedeLine.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                    DateTime CreationTime = DateTime.Parse(parsedReadedLine[0]);
                    DateTime invoiceDate = DateTime.Parse(parsedReadedLine[1]);
                    string accountNumber = parsedReadedLine[2];
                    string company = parsedReadedLine[3];
                    string description = parsedReadedLine[4];
                    string comment = parsedReadedLine[5];
                    string pathToInvoiceFile = parsedReadedLine[6];
                    double totalPayment = double.Parse(parsedReadedLine[7]);
                    double nowPaid = double.Parse(parsedReadedLine[8]);

                    _ListOfNotes.Add(new PaymentNote(CreationTime, invoiceDate, accountNumber, company,
                                     description, comment, pathToInvoiceFile, totalPayment, nowPaid));
                }
            }
        }
        public void SaveNotesInFile()
        {
            if (!Directory.Exists($@".\..\..\DataBase\"))
            {
                Directory.CreateDirectory($@".\..\..\DataBase\");
            }

            using (var SaveFileStream = new StreamWriter($@".\..\..\DataBase\Base.CSV"))
            {
                for (int i = 0; i < _ListOfNotes.Count; i++)
                {
                    SaveFileStream.WriteLine(_ListOfNotes[i].ToString());
                }
            }
        }
        public void Sort() 
        {
            _ListOfNotes.Sort(new NotesComaparereByCreationTime(2));
        }
        
    }
}
