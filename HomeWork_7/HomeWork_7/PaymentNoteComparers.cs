using System;
using System.Collections.Generic;




namespace HomeWork_7
{
    static class PaymentNoteComparers
    {

        public static int DateTimeFieldCompare(DateTime x, DateTime y)
        {
            if (x > y)
                return 1;
            else if (x < y)
                return -1;
            else
                return 0;
        }
        public static int StringFieldCompare(string x, string y, DateTime x1, DateTime y2)
        {
            if (x.CompareTo(y) > 0)
                return 1;
            else if (x.CompareTo(y) < 0)
                return -1;
            else
                return DateTimeFieldCompare(x1, y2);
        }
        public static int DoubleFieldCompare(double x, double y, DateTime x1, DateTime y2)
        {
            if (x > y)
                return 1;
            else if (x < y)
                return -1;
            else
                return DateTimeFieldCompare(x1, y2);
        }
    }


    class NotesComaparereByCreationTime : IComparer<PaymentNote>
    {
        private int _selector;
        public NotesComaparereByCreationTime(int selector)
        {
            _selector = selector;
        }
        public NotesComaparereByCreationTime() :this(0)
        {
        }

        public int Compare(PaymentNote x, PaymentNote y)
        {
            switch (_selector) 
            {
                case 2:
                    return PaymentNoteComparers.DateTimeFieldCompare(x.InvoiceDate, y.InvoiceDate);
                    break;
                case 3:
                    return PaymentNoteComparers.StringFieldCompare(x.AccountNumber, y.AccountNumber, x.CreationTime, y.CreationTime);
                case 4:
                    return PaymentNoteComparers.StringFieldCompare(x.Company, y.Company, x.CreationTime, y.CreationTime);
                case 5:
                    return PaymentNoteComparers.StringFieldCompare(x.Description, y.Description, x.CreationTime, y.CreationTime);
                case 6:
                    return PaymentNoteComparers.StringFieldCompare(x.Comment, y.Comment, x.CreationTime, y.CreationTime);
                case 7:
                    return PaymentNoteComparers.DoubleFieldCompare(x.TotalPayment, y.TotalPayment, x.CreationTime, y.CreationTime);
                case 8:
                    return PaymentNoteComparers.DoubleFieldCompare(x.NowPaid, y.NowPaid, x.CreationTime, y.CreationTime);
                case 9:
                    return PaymentNoteComparers.DoubleFieldCompare(x.LeaveToPay, y.LeaveToPay, x.CreationTime, y.CreationTime);
                default:
                    return PaymentNoteComparers.DateTimeFieldCompare(x.CreationTime, y.CreationTime);
                    break;
            }
        }
    }
}
