using System;
using Students.BusinessLogic;

namespace Students.View.ConsoleApp
{
    internal class Program
    {
        private static readonly StudentService _service = new StudentService();

        static void Main(string[] args)
        {
            // Инициализируем БД через бизнес-логику (та же БД, что и у WinForms!)
            _service.InitializeDatabase();

            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine($"   СТУДЕНТЫ | Режим: {_service.GetCurrentProviderName()}");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Показать всех студентов");
                Console.WriteLine("2. Добавить студента");
                Console.WriteLine("3. Удалить студента");
                Console.WriteLine("4. Переключить провайдер (EF Core <-> Dapper)");
                Console.WriteLine("5. Выход");
                Console.WriteLine("----------------------------------------");
                Console.Write("Выберите пункт: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            ShowAllStudents();
                            break;
                        case "2":
                            AddStudent();
                            break;
                        case "3":
                            DeleteStudent();
                            break;
                        case "4":
                            ToggleProvider();
                            break;
                        case "5":
                            exit = true;
                            Console.WriteLine("До свидания!");
                            break;
                        default:
                            Console.WriteLine("Неверный пункт меню. Попробуйте снова.");
                            Pause();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Ошибка: {ex.Message}");
                    Console.ResetColor();
                    Pause();
                }
            }
        }

        private static void ShowAllStudents()
        {
            Console.Clear();
            Console.WriteLine("\n--- Список студентов ---");
            var students = _service.GetAll();

            if (students.Count == 0)
            {
                Console.WriteLine("Список пуст.");
            }
            else
            {
                Console.WriteLine($"{"Id",-5} {"Имя",-15} {"Фамилия",-15} {"Группа",-10}");
                Console.WriteLine(new string('-', 50));
                foreach (var s in students)
                {
                    Console.WriteLine($"{s.Id,-5} {s.FirstName,-15} {s.LastName,-15} {s.Group,-10}");
                }
            }
            Console.WriteLine($"\nВсего записей: {students.Count}");
            Pause();
        }

        private static void AddStudent()
        {
            Console.Clear();
            Console.WriteLine("\n--- Добавление студента ---");

            Console.Write("Имя: ");
            string firstName = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Фамилия: ");
            string lastName = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Группа: ");
            string group = Console.ReadLine()?.Trim() ?? "";

            _service.Add(firstName, lastName, group);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✓ Студент добавлен (провайдер: {_service.GetCurrentProviderName()})");
            Console.ResetColor();
            Pause();
        }

        private static void DeleteStudent()
        {
            Console.Clear();
            Console.WriteLine("\n--- Удаление студента ---");
            Console.Write("Введите Id студента для удаления: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                _service.Remove(id);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n✓ Студент с Id={id} удалён (провайдер: {_service.GetCurrentProviderName()})");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nНеверный формат Id.");
                Console.ResetColor();
            }
            Pause();
        }

        private static void ToggleProvider()
        {
            _service.ToggleRepository();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n✓ Провайдер переключён на: {_service.GetCurrentProviderName()}");
            Console.ResetColor();
            Pause();
        }

        private static void Pause()
        {
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
}