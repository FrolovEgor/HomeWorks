using System;
using System.IO;

namespace HomeWork_7
{
    class PaymentNote
    {
        public PaymentNote(DateTime creationTime, DateTime invoiceDate, string accountNumber, string company, string description,
                           string comment, string pathToInvoiceFile, double totalPayment, double nowPaid)
        {
            CreationTime = creationTime;
            InvoiceDate = invoiceDate;
            AccountNumber = accountNumber;
            Company = company;
            Description = description;
            Comment = comment;
            _pathToInvoiceFile = pathToInvoiceFile;
            _totalPayment = totalPayment;
            _nowPaid = nowPaid;
            _leaveToPay = _totalPayment - _nowPaid;
        }

        public PaymentNote(DateTime invoiceDate, string accountNumber, string company, string description,
                   string comment,  double totalPayment, double nowPaid):this(DateTime.Now, invoiceDate,
                       accountNumber, comment, description, comment, @"C:\", totalPayment, nowPaid)
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
        public override string ToString() 
        {
            return $"{CreationTime.ToString(),15}, {InvoiceDate.ToString(),15}," +
                                       $" {AccountNumber,15}, {Company,15}, {Description,15}, {Comment,15}," +
                                       $" {_pathToInvoiceFile,15}, {_totalPayment.ToString(),15}," +
                                       $"{_nowPaid.ToString(),15},{_leaveToPay.ToString(),15}";      
        }

        public DateTime CreationTime { get; }
        public DateTime InvoiceDate { get; set; }
        public string Company { get; set; }
        public string AccountNumber { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public string PathToInvoiceFile 
        {
            get 
            {
                return _pathToInvoiceFile;
            } 
                
        }
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

        private string _pathToInvoiceFile;
        private double _totalPayment;
        private double _nowPaid;
        private double _leaveToPay;

    }
}
