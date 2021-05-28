using System;
using System.Collections.Generic;




namespace HomeWork_7
{    class PaymentNoteComparers : IComparer<PaymentNote>
    {
        private int _selector;
        public PaymentNoteComparers(int selector)
        {
            _selector = selector;
        }
        public PaymentNoteComparers() :this(0)
        {
        }

        private int DateTimeFieldCompare(DateTime x, DateTime y)
        {
            if (x > y)
                return 1;
            else if (x < y)
                return -1;
            else
                return 0;
        }
        private int StringFieldCompare(string x, string y, DateTime x1, DateTime y2)
        {
            if (x.CompareTo(y) > 0)
                return 1;
            else if (x.CompareTo(y) < 0)
                return -1;
            else
               return DateTimeFieldCompare(x1, y2);
        }
        private int DoubleFieldCompare(double x, double y, DateTime x1, DateTime y2)
        {
            if (x > y)
                return 1;
            else if (x < y)
                return -1;
            else
                return DateTimeFieldCompare(x1, y2);
        }

        public int Compare(PaymentNote x, PaymentNote y)
        {
            switch (_selector) 
            {
                case 2:
                    return DateTimeFieldCompare(x.InvoiceDate, y.InvoiceDate);
                case 3:
                    return StringFieldCompare(x.AccountNumber, y.AccountNumber, x.CreationTime, y.CreationTime);
                case 4:
                    return StringFieldCompare(x.Company, y.Company, x.CreationTime, y.CreationTime);
                case 5:
                    return StringFieldCompare(x.Description, y.Description, x.CreationTime, y.CreationTime);
                case 6:
                    return DoubleFieldCompare(x.TotalPayment, y.TotalPayment, x.CreationTime, y.CreationTime);
                case 7:
                    return DoubleFieldCompare(x.NowPaid, y.NowPaid, x.CreationTime, y.CreationTime);
                case 8:
                    return DoubleFieldCompare(x.LeaveToPay, y.LeaveToPay, x.CreationTime, y.CreationTime);
                case 9:
                    return StringFieldCompare(x.Comment, y.Comment, x.CreationTime, y.CreationTime);
                case 10:
                    return StringFieldCompare(y.PathToInvoiceFile, x.PathToInvoiceFile, x.CreationTime, y.CreationTime);
                default:
                    return DateTimeFieldCompare(x.CreationTime, y.CreationTime);
            }
        }
    }
}
