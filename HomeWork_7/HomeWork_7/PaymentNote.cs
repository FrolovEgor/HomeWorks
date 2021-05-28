using System;
using System.IO;

namespace HomeWork_7
{
    class PaymentNote
    {
        public PaymentNote(DateTime creationTime, DateTime invoiceDate, string accountNumber, string company, string description,
                           double totalPayment, double nowPaid, string comment, string pathToInvoiceFile)
        {
            CreationTime = creationTime;
            InvoiceDate = invoiceDate;
            AccountNumber = accountNumber;
            Company = company;
            Description = description;
            _totalPayment = totalPayment;
            _nowPaid = nowPaid;
            _leaveToPay = _totalPayment - _nowPaid;
            Comment = comment;
            _pathToInvoiceFile = pathToInvoiceFile;
        }

        public PaymentNote(DateTime invoiceDate, string accountNumber, string company, string description,
                    double totalPayment, double nowPaid, string comment) :  this(DateTime.Now, invoiceDate,
                    accountNumber, company, description, totalPayment, nowPaid, comment, @"")
        {

        }

        public void ChangePayment(double newPayment)
        {
            _nowPaid += newPayment;
            _leaveToPay = _totalPayment - _nowPaid;
        }
        public void AddInvoiceFile(string pathToInvoiceFile)
        {
            FileInfo path = new FileInfo(pathToInvoiceFile);

            if (path.Exists)
            {
                if (!Directory.Exists($@".\..\..\PaymentDocuments\{Company}\"))
                {
                    Directory.CreateDirectory($@".\..\..\PaymentDocuments\{Company}\");
                }
                _pathToInvoiceFile = ($@".\..\..\PaymentDocuments\{Company}\{AccountNumber}{path.Extension}");
                path.CopyTo(_pathToInvoiceFile, true);
            }

        }
        public static string PrintTop()
        {
            return ($"{"1.Дата создания",15},{"2.Дата счета",15},{"3.Номер счета",15},{"4.Название компании",25}," +
                    $"{"5.Назначение",20},{"6.Сумма счета",15},{"7.Оплачено",15},{"8.Неоплачено",15}," +
                    $"{"9.Коментарий",20},{"10.Файл счета",20}");
        }

        public override string ToString() 
        {
            return $"{CreationTime.ToString("dd/MM/yyyy"),15},{InvoiceDate.ToString("dd/MM/yyyy"),15},{AccountNumber,15},{Company,25}," +
                   $"{Description,20},{_totalPayment.ToString(),15},{_nowPaid.ToString(),15},{_leaveToPay.ToString(),15}," +
                   $"{ Comment,20},{ _pathToInvoiceFile,20}";
            
        }

        public DateTime CreationTime { get; }
        public DateTime InvoiceDate { get; set; }
        public string Company { get; set; }
        public string AccountNumber { get; set; }
        public string Description { get; set; }
        public double TotalPayment 
        { 
            get
            {
                return _totalPayment;
            }
            set 
            {
                if (value > 0)
                {
                    _totalPayment = value;
                    _leaveToPay = _totalPayment - _nowPaid;
                }
            }       
        }
        public double NowPaid
        {
            get
            {
                return _nowPaid;
            }
        }
        public double LeaveToPay
        {
            get
            {
                return _leaveToPay;
            }
        }
        public string Comment { get; set; }
        public string PathToInvoiceFile
        {
            get
            {
                return _pathToInvoiceFile;
            }

        }

        private string _pathToInvoiceFile;
        private double _totalPayment;
        private double _nowPaid;
        private double _leaveToPay;

    }
}
