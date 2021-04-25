using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using Ionic.Zip;



namespace HomeWork_6
{
    class Program
    {
        /// <summary>
        /// Расчитывает количество групп чисел, которые не делятся друг на друга
        /// </summary>
        /// <param name="directory">Путь до папки с файлом, содержащим исходное число</param>
        /// <param name="chekNumber">Возвращает число, прочитаное из файла</param>
        /// <returns>Возвращает количество групп для исходного числа</returns>
        static int GetCountOfGroups(string directory, out int chekNumber) 
        {
            Console.Clear();                                                        //Очистка консоли
            double number = double.Parse(File.ReadAllText(directory));              //Чтение исходного числа из файла
            chekNumber = (int)number;                                               //Приведение к int для возврата групп из функции
            return (int)Math.Truncate(Math.Log(number, 2.0) + 1);                   //Возвращает число групп
        }
        /// <summary>
        /// Разбивает все числа от 1 до иходного числа на группы и записывает их на диск
        /// </summary>
        /// <param name="directory">Путь до папки с файлом, содержащим исходное число</param>
        /// <returns>Возвращает строку с информацией о выполнении функции</returns>
        static string WriteGroups(string directory) 
        {
            int countOfGroups = GetCountOfGroups(directory, out int number);            //Вызов для считывания исходного чила и расчета кол-ва групп

            Console.Clear();                                                            //Очистка консоли
            Console.WriteLine("Идет процесс расчета групп и их запись на диск...");     //Оповещение о том что операция выполняется

            if (number < 1 || number > 1_000_000_000) return "Исходное число некорректно. Введите корректные данные";   //Проверка числа на соответствие диапазону
                        
            FileInfo[] massAnswers = new FileInfo[countOfGroups];                       //Создание массива инфо о файлах с ответами, равное количеству групп
            StreamWriter[] writeMassAnswers = new StreamWriter[countOfGroups];          //Создание массива потоков для вывода групп в отдельные файлы

            if (!Directory.Exists(@".\..\..\Answers")) Directory.CreateDirectory(@".\..\..\Answers");   //Создание папки с ответами, если ее не существует 

            for (int i = 0; i < countOfGroups; i++)     //Заполнение массивов инфо о файлах и массива потоков вывода
            {
                massAnswers[i] = new FileInfo($@".\..\..\Answers\Answer{i + 1}.txt");
                writeMassAnswers[i] = new StreamWriter(massAnswers[i].FullName);
            }

            int indexOfAnswer = 0;      //Индекс, соответсвующий текущей заполняемой группе чисел

            writeMassAnswers[indexOfAnswer].WriteLine("1"); //Запись первой единичной группы
            writeMassAnswers[indexOfAnswer].Close();        //Закрытие потока вывода

            if (number <= 1) return "На диск записана одна группа.";    //Завершение метода, если группа всего одна
                                    
            double nextPowOfTwo = 2;    //Следующая за текущим числом степень двойки
            double indexOfPow = 0;      //Показатель степени для вычисления SuperPow

            for (int i = 2; i < number + 1; i++)    //Цикл для перебора всех чисел от 2 до исходнго числа
            {
                if (i == nextPowOfTwo)              //Переход к следующей группе, если текущее число равно степени двойки
                {
                    writeMassAnswers[indexOfAnswer].Close();        //Закрытие используемого для предыдущей группы потока
                    indexOfAnswer++;                                //Прирост индекса для файлов ответов и потоков вывода
                    writeMassAnswers[indexOfAnswer].Write(i + " "); //Запись текущего числа в файл
                    indexOfPow += 1;                                //Увеличение показателя степени
                    nextPowOfTwo = Math.Pow(2.0, indexOfPow + 1);   //Расчет следующей степении двойки
                    continue;                                       
                }

                writeMassAnswers[indexOfAnswer].Write(i + " ");     //Запись текущего числа в текущий файл
            }
            return $"На диск записано {countOfGroups} групп.";      //Возврат сообщения с результатами работы метода
        }
        /// <summary>
        /// Архивирует данные в указанной дирректории, если в ней нет других архивов, и удаляет исходные файлы
        /// </summary>
        /// <param name="directory">Путь до дирректории с фалами</param>
        /// <returns>Возвращает строку с информацией о выполнении функции</returns>
        static string ArchiveFiles(string directory) 
        {
            Console.Clear();            //Очистка консоли

            DirectoryInfo archiveDirectory = new DirectoryInfo(directory);          //Инициализациия информации о папке
            if (!archiveDirectory.Exists) return "Папки с данными не существует.";  //Завершение метода если папки не существует

            FileInfo[] ArchiveFiles = archiveDirectory.GetFiles();                  //Считывание всех файлов из дтректории в массив
            FileInfo answerZip = new FileInfo(directory + "\\Answer.Zip");          //Создание Инфо о архивном файле
            
            foreach (FileInfo chekedFile in ArchiveFiles)        //Проверка, если ли в папке существующий архив
            {
                if (chekedFile.Extension == ".Zip") return "Данные уже архивированы."; // Завершение метода если архив уже существует
            }

            if (ArchiveFiles.Length == 0)       //Проверка, если ли в папке файлы 
            {
                return "В данной папке нет данных для архивации";   // Завершение метода если папка пустая
            }

            Console.WriteLine("Идет процесс архивации данных...");  //Оповещение о том что операция выполняется
            using (ZipFile zip = new ZipFile())                     //Создание потока для архивации
            {
                zip.AddDirectory(directory);                        //Добавление папки в архив
                zip.UseZip64WhenSaving = Zip64Option.AsNecessary;   //Разрешает использовать ZIP64
                zip.Save(answerZip.FullName);                       //Сохранение архива на диск
            }

            long FileSizeBeforeZip = 0;                             //Создание переменной для хранения размера исходных файлов
            foreach (FileInfo existingFile in ArchiveFiles)         //Цикл для вычисления суммы размеров всех файлов и удаления после архивации
            {
                FileSizeBeforeZip += existingFile.Length;
                existingFile.Delete();
            }

            //Сообщение о результате работы метода
            return $"Даныне заархивированы." +
                $"\nВ папке {archiveDirectory.FullName} cоздан файл {answerZip.Name} размером {answerZip.Length} байт." +
                $"\nРазмер файлов до архивации {FileSizeBeforeZip} байт.";
        }
        /// <summary>
        /// Разархивирует все файлы из архива и удаляет его
        /// </summary>
        /// <param name="directory">Путь до дирректории с архивом</param>
        /// <returns>Возвращает строку с информацией о выполнении функции</returns>
        static string UnArchiveFiles(string directory) 
        {
            Console.Clear();            //Очистка консоли

            FileInfo UnArchiveFiles = new FileInfo(directory + "\\Answer.Zip");         //Пути до исходного архива
            if (!UnArchiveFiles.Exists) return "Архивированных данных не существует.";  //Если архива не существует, вернуть информацию об этом

            Console.WriteLine("Идет процесс разархивации данных...");       //Оповещение о том что операция выполняется  
            using (ZipFile unzip = new ZipFile(UnArchiveFiles.FullName))    //Создание потока для разархивации
            {
                unzip.UseZip64WhenSaving = Zip64Option.AsNecessary;         //Разрешает использовать ZIP64
                unzip.ExtractAll(directory);                                //Разархивирование всех файлов в указаную дирректорию
            }
            UnArchiveFiles.Delete();                                        //Удаление исходного массива после разархивации
            return $"Данные из архива {UnArchiveFiles.Name} извлечены.";    //Возврат сообщения о результате работы метода
        }
        /// <summary>
        /// Метод для удаления данных с ответами, записанными на диск (Включая архивные)
        /// </summary>
        /// <param name="directory">Путь до дирректории</param>
        /// <returns>Возвращает строку с информацией о выполнении функции</returns>
        static string DeleteAnswers(string directory) 
        {
            Console.Clear();            //Очистка консоли

            DirectoryInfo answersDir = new DirectoryInfo(directory);            //Создание инфо о дирректории с ответами

            if (!answersDir.Exists) return "Данные уже удалены или не созданы"; //Завершение метода, если дирректории не существует

            FileInfo[] deletingFiles = answersDir.GetFiles();                   //Создание массива файлов содержащихся в дирректории с ответами

            foreach (FileInfo del in deletingFiles)         //Цикл для удаления всех элементов массива deletingFiles
            {
                del.Delete();
            }

            answersDir.Delete();        //Удаление папки с ответами                          

            return "Данные удалены.";   //Возврат сообщения о результате работы метода
        } 
        /// <summary>
        /// Выводит на экран основное меню и обрабатывает ввод
        /// </summary>
        /// <returns>Значение выбранного пункта меню</returns>
        static int menuText() 
        {
            Console.Clear();                     //Очистка консоли и вывод меню
            Console.Write("Выберите действие:\n1 - Вывести количество только количество групп" +
                                            "\n2 - Расчитать группы, с записью на диск" +
                                            "\n3 - Архивировать даные" +
                                            "\n4 - Разархивировать даныне" +
                                            "\n5 - Удалить данные" +
                                            "\n6 - Выход" +
                                            "\nВаше действие: ");

            return int.Parse(Console.ReadLine()); //Обработка ввода пользователя

        }
        /// <summary>
        /// Логика работы основного меню программы
        /// </summary>
        /// <param name="path">Путь к дирректории для в которой будет работать программа</param>
        static void mainMenu(string path) 
        {
            bool flag = false;          //Создание флага для выхода из программы
            while (flag == false)       //Основной цикл работы меню
            {
                int selector = menuText();  //Выхзов текста меню и запись результата ввода в Selector для Switch
                switch (selector)           //Ветвление основного меню 
                {
                    case 1:                 //Вызов метода для расчета групп
                        int Groups = GetCountOfGroups(path, out int readedNumber);
                        Console.WriteLine($"Для числа {readedNumber} количество рассчитаных групп равно: {Groups}");
                        Console.ReadKey();
                        break;
                    case 2:                 //Вызов метода для расчета групп и их записи на диск с вычислением скорости работы
                        DateTime startTime = DateTime.Now;
                        string ResultText = WriteGroups(path);
                        TimeSpan timeOfExecution = DateTime.Now - startTime;
                        Console.WriteLine($"{ResultText}\nНа это потребовалось {timeOfExecution}");
                        Console.ReadKey();
                        break;
                    case 3:                 //Вызов метода архивации данных
                        Console.WriteLine(ArchiveFiles(@".\..\..\Answers"));
                        Console.ReadKey();
                        break;
                    case 4:                 //Вызов метода для разархивации данных
                        Console.WriteLine(UnArchiveFiles(@".\..\..\Answers"));
                        Console.ReadKey();
                        break;
                    case 5:                 //Вызов метода для удаленния данных 
                        Console.WriteLine(DeleteAnswers(@".\..\..\Answers"));
                        Console.ReadKey();
                        break;
                    case 6:                 //Выход из меню
                        flag = true;
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            string pathToFile = @".\..\..\test_n.txt";
            mainMenu(pathToFile);
        }
    }
}



//Что нужно сделать
//Разработайте программу, которая будет разбивать числа от 1 до N на группы, при этом числа в каждой отдельно взятой группе 
//не делятся друг на друга. Число N хранится в файле, поэтому его необходимо сначала оттуда прочитать. 
//Это число может изменяться от единицы до одного миллиарда.

//После получения числа N необходимо начать поиск групп неделящихся друг на друга чисел. Сделать это можно различными
//способами. Например, для N = 50 группы могут получиться такими:

//Группа 1: 1.
//Группа 2: 2 3 5 7 11 13 17 19 23 29 31 37 41 43 47.
//Группа 3: 4 6 9 10 14 15 21 22 25 26 33 34 35 38 39 46 49.
//Группа 4: 8 12 18 20 27 28 30 42 44 45 50.
//Группа 5: 16 24 36 40.
//Группа 6: 32 48.


//А для N = 10, такими:

//Группа 1: 1.
//Группа 2: 2 7 9.
//Группа 3: 3 4 10.
//Группа 4: 5 6 8.
//Кроме распределения чисел реализуйте возможность получить только количество групп. После получения групп их 
//надо записать на диск. Если пользователь захотел рассчитать только их количество, записывать его на диск не нужно, 
//достаточно вывести на экран.

//После записи групп на диск необходимо спросить пользователя, хочет ли он поместить файл с группами в архив.
//В случае положительного ответа заархивируйте этот файл и выведите информацию о его размере до и после архивации.



//Советы и рекомендации
//Заметьте, что группы можно рассчитать совершенно разными способами, при этом неважно, какой способ расчёта 
//вы выбрали ― до тех пор, пока числа в группах не делятся друг на друга, задача считается решённой.
//Для расчёта только количества групп необязательно проходить все числа. Попробуйте сделать этот расчёт с помощью формулы.
//При N равному миллиарду вам может не хватить оперативной памяти для хранения всех групп. Тогда стоит отказаться от их хранения.
//Не стоит использовать цикл тройной вложенности, так как это решение будет слишком медленным.
//После архивации расширение исходного файла может потеряться. Стоит предусмотреть этот момент.
//Если расчёт для миллиарда чисел идёт более 20 минут (с поправкой на слабое оборудование), вам стоит поменять алгоритм их поиска.
//Задачу можно решить с помощью одного цикла и без использования массивов.


//Что оценивается
//Число N прочитано из файла. Если данного числа там нет, а также если оно выходит за рамки заданного диапазона или не может быть прочитано, пользователю выводится сообщение об ошибке.
//Группы чисел рассчитаны, при этом в каждой группе находятся только те числа, которые не делятся друг на друга.
//Пользователю предлагается выбрать: рассчитать все группы или только посмотреть их количество для заданного N.
//После расчётов группы чисел записываются в файл, по строке на группу.
//Пользователю предлагается поместить файл с рассчитанными группами в архив. При его положительном ответе архив сформирован, а статистика по размеру обоих файлов выведена на экран.
//Расчёт групп для N = 1_000_000_000 не должен превышать 20 минут, какое бы оборудование ни использовалось.