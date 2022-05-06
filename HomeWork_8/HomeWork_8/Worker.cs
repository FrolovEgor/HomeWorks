using System;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;



namespace HomeWork_8
{
    class Worker
    {
        #region Сonstructor
        /// <summary>
        /// Создание нового экземпляра работника с вводом всех данных из программы
        /// </summary>
        /// <param name="worker_ID">Идентификационный номер работника</param>
        /// <param name="firstName">Фамилия</param>
        /// <param name="lastName">Имя</param>
        /// <param name="age">Возраст</param>
        /// <param name="department">Департамент</param>
        /// <param name="salary">Зарплата</param>
        public Worker(int worker_ID, string firstName, string lastName, int age, string department, int salary)
        {
            Worker_ID = worker_ID;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Department = department;
            Salary = salary;
        }

        /// <summary>
        /// Создание нового экземпляра работника с вводом данных из консоли
        /// </summary>
        /// <param name="worker_ID">Идентификационный номер работника</param>
        /// <param name="department">Департамент</param>
        public Worker(int worker_ID, string department)
        {
            Worker_ID = worker_ID;
            
            Console.Write("Введите имя работника: ");
            FirstName = Console.ReadLine();
            
            Console.Write("Введите фамилию работника: ");
            LastName = Console.ReadLine();
            
            Console.Write("Введите возраст работника: ");
            int.TryParse(Console.ReadLine(), out int TempAge);
            Age = TempAge;

            Department = department;

            Console.Write("Введите зарплату работника: ");
            int.TryParse(Console.ReadLine(), out int TempSalary);
            Salary = TempSalary;
        }
        #endregion
        
        #region Methotds
        /// <summary>
        /// Получение данных из экземпляра в виде строки
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Worker_ID,10}{FirstName,20}{LastName,30}{Age,10}{Department,40}{Salary,20}";
        }
        /// <summary>
        /// Печатает шапку вывода работников
        /// </summary>
        public static void PrintTop() 
        {
            Console.WriteLine($"{"1. ID",10}" +
                              $"{"2. Имя",20}" +
                              $"{"3. Фамилия",30}" +
                              $"{"4. Возраст",10}" +
                              $"{"5. Департамент",40}" +
                              $"{"6. Зарплата",20}");
        }
        /// <summary>
        /// Переопределение метода для сравнения объектов Worker с любым объектом
        /// </summary>
        /// <param name="obj">Сравниваемый объект</param>
        /// <returns>true если все данные Worker одинаковы, кроме Worker_ID, false если есть различия</returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as Worker);      //Приведение сравниваемого объекта к типу Worker
        }
        /// <summary>
        /// Метод сравнения объектов типа Worker
        /// </summary>
        /// <param name="that">Сравниваемый объект</param>
        /// <returns>true если все данные Worker одинаковы, кроме Worker_ID, false если есть различия</returns>
        private bool Equals(Worker that)
        {
            if (that == null)                       //проверка на null, если объект не приведен к типу Worker
            {
                return false;
            }
            return this.FirstName == that.FirstName && this.LastName == that.LastName
                && this.Age == that.Age && this.Department == that.Department && this.Salary == that.Salary;
        }
        /// <summary>
        /// Сериализация объекта Worker в формат XML
        /// </summary>
        /// <returns>XML объект XElement с данными о работнике</returns>
        public XElement XMLSerelize()
        {
            XElement XLMWorker = new XElement("Worker");
            XElement xWorker_ID = new XElement("Worker_ID", Worker_ID);
            XElement xFirstName = new XElement("FirstName", FirstName);
            XElement xLastdName = new XElement("LastdName", LastName);
            XElement xAge = new XElement("Age", Age);
            XElement xDepartment = new XElement("Department", Department);
            XElement xSalary = new XElement("Salary", Salary);

            XLMWorker.Add(xWorker_ID, xFirstName, xLastdName, xAge, xDepartment, xSalary);

            return XLMWorker;
        }
        /// <summary>
        /// Десериализует XML объект в текущий экзепляр Worker
        /// </summary>
        /// <param name="XMLWorker">XML объект для сериализации</param>
        public void XMLDeserelize(XElement XMLWorker) 
        {
            Worker_ID = int.Parse(XMLWorker.Element("Worker_ID").Value);
            FirstName = XMLWorker.Element("FirstName").Value.ToString();
            LastName  = XMLWorker.Element("LastdName").Value.ToString();
            Age = int.Parse(XMLWorker.Element("Age").Value);
            Department = XMLWorker.Element("Department").Value.ToString();
            Salary = int.Parse(XMLWorker.Element("Salary").Value);
        }
        /// <summary>
        /// Сериализация объекта Worker в формат Json
        /// </summary>
        /// <returns>json объект JObject с данными о работнике</returns>
        public JObject JsonSerelize()
        {
            JObject JWorker = new JObject();

            JWorker["Worker_ID"] = Worker_ID;
            JWorker["FirstName"] = FirstName;
            JWorker["LastdName"] = LastName;
            JWorker["Age"] = Age;
            JWorker["Department"] = Department;
            JWorker["Salary"] = Salary;

            return JWorker;
        }
        /// <summary>
        /// Десериализует Json объект в текущий экзепляр Worker
        /// </summary>
        /// <param name="JsonWorker">Json объект для сериализации</param>
        public void JsonDeserelize(JToken JsonWorker) 
        {
            Worker_ID = int.Parse(JsonWorker["Worker_ID"].ToString());
            FirstName = JsonWorker["FirstName"].ToString();
            LastName = JsonWorker["LastdName"].ToString();
            Age = int.Parse(JsonWorker["Age"].ToString());
            Department = JsonWorker["Department"].ToString();
            Salary = int.Parse(JsonWorker["Salary"].ToString());
        }
        #endregion

        #region Properties
        /// <summary>
        /// Табельный номер работника
        /// </summary>
        public int Worker_ID { get; set; }
        /// <summary>
        /// Имя работника
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Фамилия работника
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Возраст работника
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// Департамент в котором числится работник
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// Зарплата работника
        /// </summary>
        public int Salary { get; set; }
        #endregion
    }
}