using System;

namespace HomeWork_7
{
    
    class Program
    {
      
        [STAThread]
        static void Main(string[] args)
        {
            NoteBook bookOfCompanyPayments = new NoteBook();    //Создание экземпляра базы для хранения записей об оплатах

            Menu Main = new Menu(ref bookOfCompanyPayments);    //Создание экземпляра меню
            
            Main.StartMenu();                                   //Вызов главного меню

        }
    }
}
