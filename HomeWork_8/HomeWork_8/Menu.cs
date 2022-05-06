using System;
using System.Linq;
using System.IO;
using System.Xml.Linq;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Text;


namespace HomeWork_8
{
    class Menu
    {
        #region Constructors
        /// <summary>
        /// Меню для управления департаментом через консоль
        /// </summary>
        /// <param name="OperateDepartment">Экземпляр Department, для управления</param>
        public Menu(Department OperateDepartment) 
        { 
            Dep = OperateDepartment;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Печать текста основного меню, и считывание команды пользователя
        /// </summary>
        /// <param name="MainFlag">Признак главного меню</param>
        /// <returns>Выбор пункта меню, сделанный пользователем</returns>
        private int MainmenuText(bool MainFlag)
        {
            Console.Clear();
            Console.WriteLine(Dep.DepartmentName);
            Console.Write("Выберите действие:");
            
            if (MainFlag)       //Печать пунктов "Сохранения" и "Загрузки", если вызванный экземпляр является главным меню
            {
                Console.Write("\nФайл:" +
                              "\n1 - Загрузить данные с диска" +
                              "\n2 - Сохранить данные на диск");
            }
                Console.Write("\nПравка:" +                         //Печать общих для всех меню пунктов
                              "\n3 - Добавить работника" +
                              "\n4 - Удалить работника" +
                              "\n5 - Отсортировать работников по полю" +
                              "\n6 - Добавить департамент" +
                              "\n7 - Удалить департамент" +
                              "\nВид:" +
                              "\n8 - Показать работников департамента и вложенные департаменты" +
                              "\n9 - Войти во вложенный департамент" +
                              "\n10- Назад" +
                              "\n11- Выход" +
                              "\nВаше действие: ");

            int.TryParse(Console.ReadLine(), out int UserChoise);   //Обработка выбора пользователя
            return UserChoise;
        }

        /// <summary>
        /// Меню для десерилизации данных
        /// </summary>
        private void DeserelizeMenu() 
        {
            var Dialog = new OpenFileDialog();      //Вызов диалогового меню для выбора файла
            string path=null;

            if (Dialog.ShowDialog() == DialogResult.OK) path = Dialog.FileName;     //Проверка, что файл был выбран

            FileInfo DeserelizeFile = new FileInfo(path);   //Создание информации о файле, для определения расширения 
            string DeserelizeText = File.ReadAllText(path); //Считывание текста из выбранного файла

            if (DeserelizeFile.Extension == ".xml")         //Если выбран XML файл десериализация методом для XML
            {
                Dep.XMLDeserelize(XDocument.Parse(DeserelizeText).Element("Department")); //Получение первого XML элемента основного департамента
            }

            if(DeserelizeFile.Extension == ".json")         //Если выбран Json файл десериализация методом для Json
            {
                Dep.JsonDeserelize(JObject.Parse(DeserelizeText));  //Получение первого Json-объекта основного департамента
            }
        }

        /// <summary>
        /// Меню для серилизации данных
        /// </summary>
        private void SerelizeMenu() 
        {
            Console.Clear();
            Console.WriteLine("В каком формате вы хотите сохранить данные?" +
                              "\n1. xml" +
                              "\n2. json");
            
            int.TryParse(Console.ReadLine(), out int FormatChoise);         //обработка выбора пользователя

            if (FormatChoise == 1) Dep.XMLSerelize().Save(@$".\..\{Dep.DepartmentName}.xml");   //вызов XML сериализации и сохранение данных на диск
            if (FormatChoise == 2)  //вызов Json сериализации и сохранение данных на диск
            {
                FileInfo file = new FileInfo(@$".\..\{Dep.DepartmentName}.json");  //Создание информации о файле, для сохранения
                if (file.Exists) file.Delete();                                    //Удаление файла, если существует ранее созданый файл

                var stream = new FileStream(file.FullName, FileMode.CreateNew, FileAccess.Write);   //Открытие потока для записи

                byte[] buffer = Encoding.Default.GetBytes(Dep.JsonSerelize().ToString());  //Создание массива байтов для записи
                stream.Write(buffer, 0, buffer.Length);                                    //Запись в файл
                stream.Close();                                                            //Закрытие потока
            }
        }

        /// <summary>
        /// Вызов меню для взаимодействия с департаментом
        /// </summary>
        /// <param name="MainFlag">Признак основного меню</param>
        /// <param name="ExitFlag">Флаг для выхода из программы из любой вложенности</param>
        /// <returns>Измененный департамент, с которым происходило взаимодействие</returns>
        public void StartMenu(bool MainFlag, out bool ExitFlag)
        {
            bool Exit=false;                //Создание флага для выхода из вложенных меню
            bool flag =false;               //Создание флага для выхода из меню
            while (flag == false)           //Основной цикл работы меню
            {
                Console.WriteLine(Dep.DepartmentName);
                int selector = MainmenuText(MainFlag);      //Вызов текста меню и запись результата выбора для ветвления

                if (MainFlag != true && selector < 3)       //Обработка результатов выбора пользователя для не основного меню
                {
                    Console.WriteLine("Неверная команда");
                    Console.ReadLine();
                    continue;
                }

                switch (selector)                           //Ветвление основного меню 
                {
                    case 1:                                 //Вызов меню для загрузки записей
                        DeserelizeMenu();
                        Console.ReadKey();
                        break;
                    case 2:                                 //Вызов меню для сохранения записей
                        SerelizeMenu();
                        Console.ReadKey();
                        break;
                    case 3:                                 //Вызов меню для добавления работника
                        Console.Clear();
                        Worker TempWorker = new Worker(Dep.CountOfWorkers + 1, Dep.DepartmentName);
                        Console.WriteLine(Dep.AddNewWorker(TempWorker));
                        Console.ReadKey();
                        break;
                    case 4:                                 //Вызов меню для удаления работника
                        Console.WriteLine(Dep.RemoveWorker());
                        Console.ReadKey();
                        break;
                    case 5:                                 //Вызов меню для сортировки работников 
                        Console.Clear();
                        Console.Write("Введите Наименование номер поля, по которому хотите отсортировать сотрудников: ");
                        int.TryParse(Console.ReadLine(), out int FieldIndex);
                        Console.WriteLine(Dep.SortWorkers(FieldIndex)); 
                        Console.ReadKey();
                        break;
                    case 6:                                 //Вызов меню для добавления департамента
                        Console.Clear();
                        Console.Write("Введите Наименование департамента: ");
                        Console.WriteLine(Dep.AddNewDepartment(Dep.DepartmentName+"_"+Console.ReadLine()));
                        Console.ReadKey();
                        break;
                    case 7:                                 //Вызов меню для удаления департамента
                        Dep.PrintDepartments();
                        Console.WriteLine(Dep.RemoveDepartment());
                        Console.ReadKey();
                        break;
                    case 8:                                 //Печать информации о департаменте 
                        Console.Clear();
                        Dep.PrintDepartments();
                        Console.WriteLine("\n");
                        Dep.PrintWorkers();
                        Console.ReadLine();
                        break;
                    case 9:                                 //Меню входа во вложенный департамент
                        Console.Clear();
                        Dep.PrintDepartments();
                        int.TryParse(Console.ReadLine(), out int index);
                        if (index >=0 && index < Dep.CountOfDepartments)          //Проверка индекса вложенного департамента
                        {
                            new Menu(Dep.GetSubDep(index)).StartMenu(false, out Exit);  //Вызов меню для вложенного департамента
                            flag = Exit;
                        }
                        break;
                    case 10:                 //Выход в предыдущий департамент 
                        flag = true;
                        break;
                    case 11:                 //Выход из программы
                        flag=Exit = true;
                        break;
                }
            }

            ExitFlag = Exit;
        }
        #endregion

        #region Fields
        private Department Dep;
        #endregion
    }
}
