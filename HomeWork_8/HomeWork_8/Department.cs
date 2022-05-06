using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;



namespace HomeWork_8
{
    class Department
    {
        #region Сonstructor
        /// <summary>
        /// Конструктор для создания департамента с заданием полей "наименование департамента" и "время создания"
        /// </summary>
        /// <param name="depName">Название департамента</param>
        /// <param name="Time">Время создания департамента</param>
        public Department(string depName,DateTime Time)
        {
            _baseOfWorkers = new List<Worker>();
            _baseOfDepartments = new List<Department>();
            _creationTime = Time;
            DepartmentName = depName;
        }

        /// <summary>
        /// Конструктор для создания департамента с заданием полей "наименование департамента"
        /// </summary>
        /// <param name="depName">Название департамента</param>
        public Department(string depName):this(depName, DateTime.Now)
        {}
        #endregion

        #region Metods
        /// <summary>
        /// Добавление работника в коллекцию данного департамента
        /// </summary>
        /// <param name="TempWorker">Работник, которого необходимо добавить</param>
        /// <returns>Информация о результате работы метода в строке</returns>
        public string AddNewWorker(Worker TempWorker)
        {

            if (_baseOfWorkers.Contains(TempWorker))        //Проверка наличия анологичного работника в коллекции
            {
                return "Такой работник уже существует";
            }
            else
            {
                _baseOfWorkers.Add(TempWorker);             //Добавление работника в коллекцию
                return "Работник добавлен";
            }
        }

        /// <summary>
        /// Добавление нового вложенного департамента
        /// </summary>
        /// <param name="DepName">Название нового департамента</param>
        /// <returns>Информация о результате работы метода в строке</returns>
        public string AddNewDepartment(string DepName)
        {
            Department TempDep = new Department (DepName);
            if (_baseOfDepartments.Contains(TempDep))       //Проверка наличия анологичного работника в коллекции
            {
                return "Такой департамент уже существует";
            }
            else
            {
                _baseOfDepartments.Add(TempDep);            //Добавление вложенного департамента в коллекцию
                return "Департамент добавлен";
            }
        }

        /// <summary>
        /// Удаление вложенного департамента
        /// </summary>
        /// <returns>Информация о результате работы метода в строке</returns>
        public string RemoveDepartment()
        {
            Console.Clear();
            PrintDepartments();                         //Печать информации о вложенных департаментах перед удалением

            Console.Write("Введите номер департамента, который хотите удалить: ");

            int.TryParse(Console.ReadLine(),out int depName);
            if (depName > _baseOfDepartments.Count-1 && depName !< 0)   //Провера, что введенные данные корректны 
            {
                return "Ошибка";
            }
            else 
            {
                _baseOfDepartments.Remove(_baseOfDepartments[depName]); //Удаление вложенного департамента
                return "Департамент удален";
            } 
        }

        /// <summary>
        /// Удаление работника из коллекции работников
        /// </summary>
        /// <returns>Информация о результате работы метода в строке</returns>
        public string RemoveWorker()
        {
            Console.Clear();
            this.PrintWorkers();
            Console.Write("Введите ID работника, которого хотите удалить: ");
            int DeleteID = int.Parse(Console.ReadLine());
            return _baseOfWorkers.Remove(_baseOfWorkers.Find(x => x.Worker_ID == DeleteID))?"Работник удален":"Ошибка";
        }

        /// <summary>
        /// Сортировка работников в коллекции данного департамента
        /// </summary>
        /// <param name="SortField">Номер поля для сортировки</param>
        /// <returns>Информация о результате работы метода в строке</returns>
        public string SortWorkers(int SortField)
        {
            _baseOfWorkers.Sort(new WorkerComparer(SortField));
            return "Сотрудники отсортирован";
        }

        /// <summary>
        /// Метод для печати иформации о работниках в консоль
        /// </summary>
        public void PrintWorkers() 
        {
            Worker.PrintTop();                                      //Печать шапки таблицы  в консоль
            foreach (Worker CurrentWorker in _baseOfWorkers)
            {
                Console.WriteLine(CurrentWorker.ToString());
            }
        }

        /// <summary>
        /// Метод для вывода данных о коллекции департаментов в консоль
        /// </summary>
        public void PrintDepartments()
        {
            Console.WriteLine($"{"#",10}{"Наименование",50}{"Дата создания",20}{"работники",15}");
            foreach (Department dep in _baseOfDepartments)
            {
                Console.WriteLine($"{_baseOfDepartments.IndexOf(dep),10}" +
                                  $"{dep.DepartmentName,50}" +
                                  $"{dep.CreationTime.ToString("dd/MM/yyyy"),20}" +
                                  $"{dep.CountOfWorkers,15}");
            }
        }

        /// <summary>
        /// Переопределение метода для сравнения объектов Departmen с любым объектом
        /// </summary>
        /// <param name="obj">Сравниваемый объект</param>
        /// <returns>true если все наименования Department одинаковы, false если есть различия</returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as Department);
        }

        /// <summary>
        /// Метод сравнения объектов типа Department
        /// </summary>
        /// <param name="that">Сравниваемый объект</param>
        /// <returns>true если все наименования Department одинаковы, false если есть различия</returns>
        private bool Equals(Department that)
        {
            if (that == null)
            {
                return false;
            }
            return this.DepartmentName == that.DepartmentName;
        }

        /// <summary>
        /// Сериализует все данные департамента в формате XML 
        /// </summary>
        /// <returns>XML объект с серриализованным экземпляром Department</returns>
        public XElement XMLSerelize()
        {                                                                               //Создание элементов XML для:
            XElement XLMDep = new XElement("Department");                               //<Department> все данные департамента 
            XElement XbaseOfWorkers = new XElement("baseOfWorkers");                    //<baseOfWorkers> Коллекцию работников
            XElement XbaseOfDepartment = new XElement("baseOfDepartment");              //<baseOfDepartment> Коллекцию вложенных департаментов

            XElement xDepName = new XElement("DepartmentName", DepartmentName);         //<DepartmentName> Наименование департамента
            XElement xDepCreationTime = new XElement("CreationTime", _creationTime);    //<CreationTime> Время создания департамента

            if (_baseOfWorkers.Count > 0)                             //Проверка наличия элементов в коллекции работников
            {
                foreach (Worker currentWorker in _baseOfWorkers)      //Заполнение элемента <baseOfWorkers>
                {                                                     //элементами <Worker>, если коллекция не пустая
                    XbaseOfWorkers.Add(currentWorker.XMLSerelize());
                }
                XLMDep.Add(XbaseOfWorkers);                           //Добавление элемента <baseOfWorkers> в этот <Department>
            }

            if (_baseOfDepartments.Count > 0)                         //Проверка наличия элементов в коллекции департаментов
            {
                foreach (Department currentDep in _baseOfDepartments) //Заполнение элемента <baseOfDepartment>
                {                                                     //элементами <Department>, если коллекция не пустая
                    XbaseOfDepartment.Add(currentDep.XMLSerelize());  //через рекурсивный вызов этого метода
                }
                XLMDep.Add(XbaseOfDepartment);                        //Добавление элемента <baseOfWorkers> в этот <Department>
            }

            XLMDep.Add(xDepName, xDepCreationTime);                   //Добавление элементов <DepartmentName>, <CreationTime> в этот <Department> 

            return XLMDep;                                            //Возвращает этот <Department> как XElement
        }

        /// <summary>
        /// Десериализует XML объект в текущий экзепляр Department
        /// </summary>
        /// <param name="XML_obj">XML объект для сериализации</param>
        public void XMLDeserelize(XElement XML_obj)
        {
            _baseOfDepartments.Clear();             //Очитска коллекции департаментов и работников                                    
            _baseOfWorkers.Clear();
                                                                                                //Получение значений из элементов:
            DepartmentName = XML_obj.Element("DepartmentName").Value.ToString();                //DepartmentName из <DepartmentName>
            _creationTime = DateTime.Parse(XML_obj.Element("CreationTime").Value.ToString());   //_creationTime из <CreationTime>

                                                                                                  //Получение коллекций элементов:
            var XMLWorkerList = XML_obj.Element("baseOfWorkers").Elements("Worker").ToList();     //<Worker> из <baseOfWorkers>
            var XMLDepList = XML_obj.Elements("baseOfDepartment").Elements("Department").ToList(); //<Department> из <baseOfDepartment>

            foreach (var XMLWorker in XMLWorkerList)            //Обработка коллекци XML элементов <Worker> 
            {
                Worker tempWorker = new Worker(0,"","",0,"",0); //Создание временного работника и 
                tempWorker.XMLDeserelize(XMLWorker);            //десериализация отдельного <Worker>
                
                this.AddNewWorker(tempWorker);        //Добавление временного работника в коллекцию
            }                                         //работников текущего департамента    

            foreach (var item in XMLDepList)          //Обработка коллекци XML элементов <Department> 
            {
                var tempDep = new Department("");     //Создание временного департамента и десериализация отдельного
                tempDep.XMLDeserelize(item);          //<Department> через рекурсивный вызов данного метода
  
                _baseOfDepartments.Add(tempDep);      //Добавление временного департамента в коллекцию   
            }                                         //департаментов текущего департамента 
        }

        /// <summary>
        /// Сериализует все данные департамента в формате Json
        /// </summary>
        /// <returns>Json объект с серриализованным экземпляром Department</returns>
        public JObject JsonSerelize() 
        {
            JObject JsonDepartment = new JObject();              //Создание Json-объекта для текущего экземпляра департамента
            JArray JsonBaseOfWorkers = new JArray();             //Создание Json-массива для коллекции работников
            JArray JsonBaseOfDepartment = new JArray();          //Создание Json-массива для коллекции вложенных департаментов

            foreach (Worker worker in _baseOfWorkers)            //Заполнение Json-массива работников серриализованными 
            {                                                    //в Json-объект работниками из коллекции работников
                JsonBaseOfWorkers.Add(worker.JsonSerelize());
            }

            foreach (Department dep in _baseOfDepartments)      //Заполнение Json-массива департаментов серриализованными
            {                                                   //в Json-объект вложенными департаментами из коллекции депратаментов
                JsonBaseOfDepartment.Add(dep.JsonSerelize());
            }
                                                                        //Присвоение ключам Json-объекта департамента значений:
            JsonDepartment["BaseOfWorkers"] = JsonBaseOfWorkers;        //Ключу "BaseOfWorkers" - Json-массива работников
            JsonDepartment["BaseOfDepartments"] = JsonBaseOfDepartment; //Ключу "BaseOfDepartments" - Json-массива вложенных департаментов
            JsonDepartment["DepartmentName"] = DepartmentName;          //Ключу "DepartmentName" - значение названия департамента
            JsonDepartment["CreationTime"] = _creationTime.ToString();  //Ключу "CreationTime" - значение времени создания департамента

            return JsonDepartment;              //Возвращает Json-объект с данными о текущем департаментие как JObject
        }

        /// <summary>
        /// Десериализует Json объект в текущий экзепляр Department
        /// </summary>
        /// <param name="Json_obj">Json объект для сериализации</param>
        /// <returns></returns>
        public void JsonDeserelize(JToken Json_obj)
        {
            _baseOfDepartments.Clear();             //Очитска коллекции департаментов и работников                                    
            _baseOfWorkers.Clear();
                                                                                    //Получение значений из ключей:
            DepartmentName = Json_obj["DepartmentName"].ToString();                 //DepartmentName из [DepartmentName]
            _creationTime = DateTime.Parse(Json_obj["CreationTime"].ToString());    //_creationTime из [CreationTime]

            var JsonWorkerArray = Json_obj["BaseOfWorkers"].ToArray();  //Получение Json-массива из ключа [BaseOfWorkers]

            foreach (var JsonWorker in JsonWorkerArray)                 //Обработка Json-массива работников 
            {
                Worker tempWorker = new Worker(0, "", "", 0, "", 0);    //Создание временного работника и 
                tempWorker.JsonDeserelize(JsonWorker);                  //десериализация отдельного Json-объекта Worker
                
                this.AddNewWorker(tempWorker);                          //Добавление временного работника в коллекцию
            }                                                           //работников текущего департамента

            var JsonDepArray = Json_obj["BaseOfDepartments"].ToArray(); //Получение Json-массива из ключа [BaseOfDepartments]

            foreach (var JsonDep in JsonDepArray)                       //Обработка Json-массива вложенных департаментов 
            {
                var tempDep = new Department("");        //Создание временного департамента и десериализация отдельного
                tempDep.JsonDeserelize(JsonDep);         //Json-объекта департамента через рекурсивный вызов данного метода

                _baseOfDepartments.Add(tempDep);         //Добавление временного департамента в коллекцию  
            }                                            //департаментов текущего департамента 
        }

        /// <summary>
        /// Получение вложенного департамента
        /// </summary>
        /// <param name="index">Индекс вложенного департамета</param>
        /// <returns>Вложенный департамент</returns>
        public Department GetSubDep(int index) 
        {
            return _baseOfDepartments[index];
        }

        #endregion

        #region Properties
        /// <summary>
        /// Название департамента
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// Дата создания департамента
        /// </summary>
        public DateTime CreationTime 
        { 
            get 
            {
                return _creationTime;
            } 
        }
        /// <summary>
        /// Колличество работников департамента
        /// </summary>
        public int CountOfWorkers 
        {
            get { return _baseOfWorkers.Count(); } 
        }
        /// <summary>
        /// Колличество вложенных департаментов
        /// </summary>
        public int CountOfDepartments 
        { 
            get { return _baseOfDepartments.Count(); }
        }
        #endregion

        #region Fields
        /// <summary>
        /// Коллекция работников внутри департамента
        /// </summary>
        private List<Worker> _baseOfWorkers;
        /// <summary>
        /// Коллекция департаментов внутри департамента
        /// </summary>
        private List<Department> _baseOfDepartments;
        /// <summary>
        /// Дата создания департамента
        /// </summary>
        private DateTime _creationTime;
        #endregion
    }    
}
