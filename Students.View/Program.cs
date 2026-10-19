using System;
using System.Windows.Forms;
using Students.BusinessLogic; // ≈динственна€ внешн€€ зависимость View

namespace Students.View
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

          
            var service = new StudentService();
            service.InitializeDatabase();

            Application.Run(new Form1());
        }
    }
}