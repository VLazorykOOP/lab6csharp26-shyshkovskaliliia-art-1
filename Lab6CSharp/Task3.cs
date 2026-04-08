using System;
using System.Text;

namespace Lab6Sharp
{
    [Serializable]
    public class MyDivisionException : ApplicationException
    {
        public MyDivisionException() { }
        public MyDivisionException(string message) : base(message) { }
        public MyDivisionException(string message, Exception ex) : base(message, ex) { }
    }

    internal class Task3
    {
        private static double CalculateZ(int x, int y)
        {
            if (y == 0)
            {
                MyDivisionException exc = new MyDivisionException("ПОМИЛКА: ділення на 0");
                exc.HelpLink = "https://moodle.chnu.edu.ua";
                throw exc;
            }
            return ((1.0 / (x * y)) + (1.0 / (x * x + 1))) * (x + y);
        }

        public static void Run()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Обробка помилок для формули з лабораторної №1:\n");

            try
            {
                Console.Write("Input x = ");
                int x = int.Parse(Console.ReadLine() ?? "0");

                Console.Write("Input y = ");
                int y = int.Parse(Console.ReadLine() ?? "0");

                double z = CalculateZ(x, y);

                Console.WriteLine($"Result = {z}");
            }
            catch (MyDivisionException ex)
            {
                Console.WriteLine($"\nКАСТОМНЕ ВИКЛЮЧЕННЯ (MyDivisionException): {ex.Message}");
                Console.WriteLine($"   HelpLink: {ex.HelpLink}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"\nСТАНДАРТНЕ ВИКЛЮЧЕННЯ DivideByZeroException: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"\nПомилка формату введення (FormatException): {ex.Message}");
                Console.WriteLine("   Потрібно вводити тільки цілі числа!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nІнший виняток: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("\n>>> Блок finally виконано (завершення обробки винятків).");
            }

            Console.WriteLine("\nНатисніть Enter для повернення в меню...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}