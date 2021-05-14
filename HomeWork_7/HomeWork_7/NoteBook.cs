using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            _ListOfNotes.Add(new PaymentNote(invoiceDate, accountNumber,company,description,comment,totalPayment,nowPaid));
            return "Запись добавленна";        
        }

        public void AddInvoiceFile(int numberOfNote) 
        {

            string path = @"C:\";
            using (var fileDialog = new OpenFileDialog())
            {
                if (fileDialog.ShowDialog() == DialogResult.OK)
                    path = fileDialog.FileName;
            }
            _ListOfNotes[numberOfNote].AddInvoiceFile(path);
            Console.ReadLine();

        }
        public void OpenInvoiceFile(int numberOfNote)
        {
            Console.ReadLine();
            Process.Start(_ListOfNotes[numberOfNote].PathToInvoiceFile.FullName);           
        }


        
    }
}
