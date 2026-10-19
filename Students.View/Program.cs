using System;
using System.Windows.Forms;
using Students.BusinessLogic; // Единственная внешняя зависимость View

namespace Students.View
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Инициализируем БД через бизнес-логику (View не знает про AppDbContext)
            var service = new StudentService();
            service.InitializeDatabase();

            Application.Run(new Form1());
        }
    }
}