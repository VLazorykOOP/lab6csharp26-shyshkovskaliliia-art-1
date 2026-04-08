using System;

namespace Lab6Sharp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== ЛАБОРАТОРНА РОБОТА №6 (C#) ===\n");
                Console.WriteLine("1. Завдання 1");
                Console.WriteLine("2. Завдання 2");
                Console.WriteLine("3. Завдання 3");
                Console.WriteLine("4. Завдання 4");  
                Console.WriteLine("0. Вихід з програми");

                Console.Write("\n Введіть номер завдання (0–4): ");

                string input = Console.ReadLine()?.Trim() ?? "";

                Console.WriteLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("Запуск Завдання 1\n");
                        Task1.Run();
                        BreakToMenu();
                        break;

                    case "2":
                        Console.WriteLine("Запуск Завдання 2\n");
                        Task2.Run();
                        BreakToMenu();
                        break;

                    case "3":
                        Console.WriteLine("Запуск Завдання 3\n");
                        Task3.Run();
                        BreakToMenu();
                        break;

                    case "4":
                        Console.WriteLine("Запуск Завдання 4\n");
                        Task4.Run();
                        BreakToMenu();
                        break;

                    case "0":
                        Console.WriteLine("\nДо побачення! Програма завершила роботу.");
                        Console.WriteLine("   Натисніть Enter для виходу...");
                        Console.ReadLine();  
                        return;

                    default:
                        Console.WriteLine(" Невірний вибір! Натисніть будь-яку клавішу...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void BreakToMenu()
        {
            Console.WriteLine("\n" + new string('─', 50));
            Console.WriteLine("Натисніть Enter, щоб повернутися в меню...");
            Console.ReadLine();
        }
    }
}
