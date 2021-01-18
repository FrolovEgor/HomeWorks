using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_2
{
    /// <summary>
    /// Класс описывающий модель студента
    /// </summary>
    class Student
    {
        /// <summary>
        /// Имя студента
        /// </summary>
        public string firstName;

        /// <summary>
        /// Фамилия студента
        /// </summary>
        public string lastName;

        /// <summary>
        /// Возраст студента
        /// </summary>
        public byte age;

        /// <summary>
        /// рост студента
        /// </summary>
        public float heiht;
        
        /// <summary>
        /// Баллы полученные за математику
        /// </summary>
        public float mathsScore;

        /// <summary>
        /// Баллы полученные за физику
        /// </summary>
        public float physicsScore;

        /// <summary>
        /// Баллы полученные за язык
        /// </summary>
        public float langScore;

        /// <summary>
        /// Средний бал за три предмета
        /// </summary>
        public float averageValue;

        
        /// <summary>
        /// Конструктор заполняющий данные о студенте через консоль
        /// </summary>
        public Student()
        {
            //Приглашение ввести имя и ввод имени
            Console.Write("Введите имя: ");
            firstName = Console.ReadLine();

            //Приглашение ввести фамилию и ввод фамилии
            Console.Write("Введите Фамилию: ");
            lastName = Console.ReadLine();

            //Приглашение ввести возраст и ввод возраста
            Console.Write("Введите возраст: ");
            age = byte.Parse(Console.ReadLine());

            //Приглашение ввести рост и ввод роста
            Console.Write("Введите рост: ");
            heiht = float.Parse(Console.ReadLine());

            //Приглашение ввести балы за математику и ввод данных балов
            Console.Write("Введите Балы за математику: ");
            mathsScore = Convert.ToSingle(Console.ReadLine());

            //Приглашение ввести балы за физику и ввод данных балов
            Console.Write("Введите Балы за физику: ");
            physicsScore = float.Parse(Console.ReadLine());

            //Приглашение ввести балы за язык и ввод данных балов
            Console.Write("Введите Балы за язык: ");
            langScore = float.Parse(Console.ReadLine());

            //Расчет среднего бала студента
            averageValue = (mathsScore + physicsScore + langScore) / 3;

        }

        /// <summary>
        /// Метод для печати шапки таблицы
        /// </summary>
        /// <returns>Возвращает строкой шапку вывода данных о студенте</returns>
        public string PrintTop()
        {
            return $"{"Имя",10}{"Фамилия",15}{"Возраст",10}{"Рост",8}{"Математика",12}{"Физика",10}{"Язык",8}{"Средний балл",14}";
        }

         
        /// <summary>
        /// Метод вывода данных о студенте
        /// </summary>
        /// <returns>Возвращает строкой данные о студенте</returns>
        public override string ToString()
        {
            return ($"{firstName,10}{lastName,15}{age,10}{heiht,8}{mathsScore,12}{physicsScore,10}{langScore,8}{averageValue.ToString("##.##"),14}");
        }
    }
}
