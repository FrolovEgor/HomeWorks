using System;


namespace HomeWork_7
{
    
    class Program
    {
      
        [STAThread]
        static void Main(string[] args)
        {
            NoteBook bookOfCompanyPayments = new NoteBook();
            Menu Main = new Menu(ref bookOfCompanyPayments);
            Main.StartMenu();

        }
    }
}

//Что нужно сделать
//ШАПКА ВЫВОДА ДЛЯ СОХРАНЕНИЯ