using System;
using System.Collections.Generic;




namespace HomeWork_7
{   
    /// <summary>
    /// Реализация интерфейса IComparer для сортировки элементов типа PaymentNote в зависимости от поля, по которому проиходит сортировка
    /// </summary>
    class PaymentNoteComparers : IComparer<PaymentNote>
    {
        #region Конструкторы
        /// <summary>
        /// Инициализирует новый экземпляр класса PaymentNoteComparers. Сортировка по выбранному полю.
        /// </summary>
        /// <param name="selector">Номер поля, по которому необхомо произвести сортировку</param>
        public PaymentNoteComparers(int selector)
        {
            _selector = selector;
        }
        /// <summary>
        /// Инициализирует новый экземпляр класса PaymentNoteComparers. Сортировка по дате создания.
        /// </summary>
        public PaymentNoteComparers() :this(0)
        {
        }
        #endregion

        #region Методы
        /// <summary>
        /// Метот для сравнения полей типа DateTime
        /// </summary>
        /// <param name="x">Первое поле для сравнения</param>
        /// <param name="y">Второе поле для сравнения</param>
        /// <returns>Резульат сравнения</returns>
        private int DateTimeFieldCompare(DateTime x, DateTime y)
        {
            if (x > y)
                return 1;
            else if (x < y)
                return -1;
            else
                return 0;
        }
        /// <summary>
        /// Метот для сравнения полей типа string. При равенстве сортировка происходит по доп. полю DateTime
        /// </summary>
        /// <param name="x">Первое поле для сравнения</param>
        /// <param name="y">Второек поле для сравнения</param>
        /// <param name="x1">Время создания записи первого поля</param>
        /// <param name="y2">Время создания записи второго поля</param>
        /// <returns>Резульат сравнения</returns>
        private int StringFieldCompare(string x, string y, DateTime x1, DateTime y2)
        {
            if (x.CompareTo(y) > 0)
                return 1;
            else if (x.CompareTo(y) < 0)
                return -1;
            else
               return DateTimeFieldCompare(x1, y2);
        }
        /// <summary>
        /// Метот для сравнения полей типа double. При равенстве сортировка происходит по доп. полю DateTime
        /// </summary>
        /// <param name="x">Первое поле для сравнения</param>
        /// <param name="y">Второек поле для сравнения</param>
        /// <param name="x1">Время создания записи первого поля</param>
        /// <param name="y2">Время создания записи второго поля</param>
        /// <returns>Резульат сравнения</returns>
        private int DoubleFieldCompare(double x, double y, DateTime x1, DateTime y2)
        {
            if (x > y)
                return 1;
            else if (x < y)
                return -1;
            else
                return DateTimeFieldCompare(x1, y2);
        }
        /// <summary>
        /// Реализация метода Compare из IComparer для типа PaymentNote в зависимости от выбранного поля
        /// </summary>
        /// <param name="x">Первый экземпляр PaymentNote</param>
        /// <param name="y">Второй экземпляр PaymentNote</param>
        /// <returns>Резульат сравнения</returns>
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
        #endregion

        #region Поля
        private int _selector;
        #endregion
    }
}
