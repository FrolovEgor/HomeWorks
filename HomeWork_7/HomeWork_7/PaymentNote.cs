using System;
using System.IO;

namespace HomeWork_7
{/// <summary>
/// Класс для храенения информации о счете
/// </summary>
    class PaymentNote
    {
        #region Конструкторы
        /// <summary>
        /// Инициализирует новый экземпляр класса PaymentNote. Доступна запись всех полей. Используется при загрузке данных из файлов.
        /// </summary>
        /// <param name="creationTime">Дата создания записи</param>
        /// <param name="invoiceDate">Дата поступления счета</param>
        /// <param name="accountNumber">Номер счета</param>
        /// <param name="company">Компания от которой поступил счет</param>
        /// <param name="description">Назначение счета</param>
        /// <param name="totalPayment">Полнная сумма по счету</param>
        /// <param name="nowPaid">Сумма оплаченая на данный момент</param>
        /// <param name="comment">Комментарий</param>
        /// <param name="pathToInvoiceFile">Путь до файла (PDF или иной) где лежит выставленный счет</param>
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
        /// <summary>
        /// Инициализирует новый экземпляр класса PaymentNote. Доступна только часть полей, вводимых пользователем.
        /// </summary>
        /// <param name="invoiceDate"></param>
        /// <param name="accountNumber"></param>
        /// <param name="company"></param>
        /// <param name="description"></param>
        /// <param name="totalPayment"></param>
        /// <param name="nowPaid"></param>
        /// <param name="comment"></param>
        public PaymentNote(DateTime invoiceDate, string accountNumber, string company, string description,
                    double totalPayment, double nowPaid, string comment) :  this(DateTime.Now, invoiceDate,
                    accountNumber, company, description, totalPayment, nowPaid, comment, @"")
        {

        }
        #endregion

        #region Методы
        /// <summary>
        /// Метод для добавления данных об оплате по счету
        /// </summary>
        /// <param name="newPayment">Сумма внесенной оплаты</param>
        public void AddPayment(double newPayment)
        {
            _nowPaid += newPayment;                     //Добавление новой оплаты к текущей
            _leaveToPay = _totalPayment - _nowPaid;     //Изменение поля "Сколько осталось оплатить"
        }

        /// <summary>
        ///Метод для добавления файла для выставленного счета, и копирования его в папку "Документы о платежах"
        /// </summary>
        /// <param name="pathToInvoiceFile">Полный адресс до файла</param>
        public void AddInvoiceFile(string pathToInvoiceFile)
        {
            FileInfo path = new FileInfo(pathToInvoiceFile);    //Создание экзепляра для работы с файлом

            if (path.Exists)                                    //Проверка, существует ли файл
            {
                if (!Directory.Exists($@".\..\..\PaymentDocuments\{Company.Trim(' ')}\"))    //Проверка существует ли дирректоря для копирования
                {
                    Directory.CreateDirectory($@".\..\..\PaymentDocuments\{Company.Trim(' ')}\");         //Создаем дирректорию если ее не существует
                }
                _pathToInvoiceFile = ($@".\..\..\PaymentDocuments\{Company.Trim(' ')}\{AccountNumber.Trim(' ')}{path.Extension}");   //Создание нового адреса для копии файла
                path.CopyTo(_pathToInvoiceFile, true);          //Копирование файла
            }
        }
        //Статический метод для печати легенды к записи
        public static string PrintTop()
        {
            
            return ($"{"1.Дата создания",15},{"2.Дата счета",15},{"3.Номер счета",15},{"4.Название компании",25}," +
                    $"{"5.Назначение",20},{"6.Сумма счета",15},{"7.Оплачено",15},{"8.Неоплачено",15}," +
                    $"{"9.Коментарий",20},{"10.Файл счета",20}");
        }
        /// <summary>
        /// Метод для перевода данных в строковое представление с форматированием и разделителями
        /// </summary>
        /// <returns>Возвращает записанные данные в строковом формате</returns>
        public override string ToString() 
        {
            return $"{CreationTime.ToString("dd/MM/yyyy"),15},{InvoiceDate.ToString("dd/MM/yyyy"),15},{AccountNumber,15},{Company,25}," +
                   $"{Description,20},{_totalPayment.ToString(),15},{_nowPaid.ToString(),15},{_leaveToPay.ToString(),15}," +
                   $"{ Comment,20},{ _pathToInvoiceFile,20}";

        }
        #endregion

        #region Свойства
        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime CreationTime { get; }

        /// <summary>
        /// Дата поступления счета
        /// </summary>
        public DateTime InvoiceDate { get; set; }

        /// <summary>
        /// Номер счета
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Компания от которой поступил счет
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// Назначение счета
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Полнная сумма по счету
        /// </summary>
        public double TotalPayment 
        { 
            get
            {
                return _totalPayment;
            }
            set 
            {
                if (value > 0)                      //Проверка и пересчет связанных полей
                {
                    _totalPayment = value;
                    _leaveToPay = _totalPayment - _nowPaid;
                }
            }       
        }

        /// <summary>
        /// Сумма оплаченая на данный момент
        /// </summary>
        public double NowPaid
        {
            get
            {
                return _nowPaid;
            }
        }

        /// <summary>
        /// Не оплаченая сумма
        /// </summary>
        public double LeaveToPay
        {
            get
            {
                return _leaveToPay;
            }
        }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Путь до файла (PDF или иной) где лежит выставленный счет
        /// </summary>
        public string PathToInvoiceFile
        {
            get
            {
                return _pathToInvoiceFile;
            }

        }
        #endregion

        #region Поля
        private string _pathToInvoiceFile;
        private double _totalPayment;
        private double _nowPaid;
        private double _leaveToPay;
        #endregion
    }
}
