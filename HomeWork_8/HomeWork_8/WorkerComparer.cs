using System;
using System.Collections.Generic;


namespace HomeWork_8
{
    class WorkerComparer : IComparer<Worker>
    {
        #region Constructors
        /// <summary>
        /// Создание объекта для определения правил сортировки в коллекциях, с реализацией интерфейса IComparer с выбором правила
        /// </summary>
        /// <param name="Selector">Номер поля для сортировки</param>
        public WorkerComparer(int Selector)
        {
            _selector = Selector;
        }
        /// <summary>
        /// Создание объекта для определения правил сортировки в коллекциях, с реализацией интерфейса IComparer с базоым правилом
        /// </summary>
        public WorkerComparer() : this(0) { }
        #endregion

        #region Methods
        /// <summary>
        /// Метод для сравнения int полей
        /// </summary>
        /// <param name="x">первое поле</param>
        /// <param name="y">второе поле</param>
        /// <returns>1 если x>y, -1 если x<y, 0 если поля равны</returns>
        private int IntCompare(int x, int y)
        {
            if (x > y)
                return 1;
            else if (x < y)
                return -1;
            else
                return 0;
        }
        /// <summary>
        /// Метод для сравнения int полей с доп проверкой по второму int полю
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <returns>1 если x>y или х1>y1, -1 если x<y или x1<y1, 0 если поля равны</returns>
        private int IntCompare(int x, int y, int x1, int y1)
        {
            if (x > y)
                return 1;
            else if (x < y)
                return -1;
            else
                return IntCompare(x1, y1);
        }
        /// <summary>
        /// Метод для сравнения string полей с доп проверкой по второму int полю
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <returns>1 если x>y или х1>y1, -1 если x<y или x1<y1, 0 если поля равны</returns>
        private int StringCompare(string x, string y, int x1, int y1)
        {
            if (x.CompareTo(y) > 0)
                return 1;
            else if (x.CompareTo(y) < 0)
                return -1;
            else
                return IntCompare(x1, y1);
        }
        /// <summary>
        /// Метод сравнения двух объектов типа Worker для сравнения в коллекциях
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <returns>1 если x1>x2, -1 если x1<x2, 0 если объекты равны</returns>
        public int Compare(Worker x1, Worker x2)
        {
            switch (_selector)              //Ветвление для реализации сравнения по разным полям
            {
                case 2:
                    return StringCompare(x1.FirstName, x2.FirstName, x1.Worker_ID, x2.Worker_ID);
                    break;
                case 3:
                    return StringCompare(x1.LastName, x2.LastName, x1.Worker_ID, x2.Worker_ID);
                    break;
                case 4:
                    return IntCompare(x1.Age, x2.Age, x1.Worker_ID, x2.Worker_ID);
                    break;
                case 5:
                    return StringCompare(x1.Department, x2.Department, x1.Worker_ID, x2.Worker_ID);
                    break;
                case 6:
                    return IntCompare(x1.Salary, x2.Salary, x1.Worker_ID, x2.Worker_ID);
                    break;
                default:
                    return IntCompare(x1.Worker_ID, x2.Worker_ID);
                    break;
            }
        }
        #endregion

        #region Fiels
        /// <summary>
        /// Номер поля для сортировки
        /// </summary>
        private int  _selector;
        #endregion
    }
}
