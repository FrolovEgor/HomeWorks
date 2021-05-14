using System;
using System.IO;

namespace HomeWork_7
{
    struct PaymentNote
    {
        public PaymentNote(DateTime invoiceDate, string accountNumber, string company, string description,
                           string comment, double totalPayment, double nowPaid)
        {
            CreationTime = DateTime.Now;
            InvoiceDate = invoiceDate;
            AccountNumber = accountNumber;
            Company = company;
            Description = description;
            Comment = comment;
            PathToInvoiceFile = null;
            _totalPayment = totalPayment;
            _nowPaid = nowPaid;
            _leaveToPay = _totalPayment - _nowPaid;
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
                PathToInvoiceFile = new FileInfo($@".\..\..\PaymentDocuments\{Company}\{AccountNumber}{path.Extension}");
                path.CopyTo(PathToInvoiceFile.FullName, true);
            }

        }

        public DateTime CreationTime { get; }
        public DateTime InvoiceDate { get; set; }
        public string Company { get; set; }
        public string AccountNumber { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public FileInfo PathToInvoiceFile { get;  set; }
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


        private double _totalPayment;
        private double _nowPaid;
        private double _leaveToPay;

    }
}
