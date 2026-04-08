using System;
using System.Collections.Generic;
using System.Text;

namespace Lab6Sharp
{
    public interface IProduct : IComparable, ICloneable
    {
        void DisplayInfo();
        bool MatchesType(string type);

        string Name { get; }
        double Price { get; }
    }

    public class Toy : IProduct
    {
        public string Name { get; private set; }
        public double Price { get; private set; }
        public string Manufacturer { get; private set; }
        public string Material { get; private set; }
        public int Age { get; private set; }

        public void Play()
        {
            Console.WriteLine($"   Граємо в іграшку \"{Name}\" (вік {Age}+)");
        }

        public Toy(string name, double price, string manufacturer, string material, int age)
        {
            Name = name;
            Price = price;
            Manufacturer = manufacturer;
            Material = material;
            Age = age;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Іграшка: \"{Name}\", ціна: {Price} грн, виробник: {Manufacturer},");
            Console.WriteLine($"          матеріал: {Material}, вік: {Age}+");
        }

        public bool MatchesType(string type)
        {
            string t = type.ToLower();
            return t == "toy" || t == "іграшка";
        }

        public int CompareTo(object? obj)
        {
            if (obj is null) return 1;
            if (obj is IProduct other)
                return Price.CompareTo(other.Price);
            return 0;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class Book : IProduct
    {
        public string Name { get; private set; }
        public double Price { get; private set; }
        public string Author { get; private set; }
        public string Publisher { get; private set; }
        public int Age { get; private set; }

        public void Read()
        {
            Console.WriteLine($"   Читаємо книгу \"{Name}\" автора {Author}");
        }

        public Book(string name, string author, double price, string publisher, int age)
        {
            Name = name;
            Author = author;
            Price = price;
            Publisher = publisher;
            Age = age;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Книга: \"{Name}\", автор: {Author}, ціна: {Price} грн,");
            Console.WriteLine($"       видавництво: {Publisher}, вік: {Age}+");
        }

        public bool MatchesType(string type)
        {
            string t = type.ToLower();
            return t == "book" || t == "книга";
        }

        public int CompareTo(object? obj)
        {
            if (obj is null) return 1;
            if (obj is IProduct other)
                return Price.CompareTo(other.Price);
            return 0;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class SportsInventory : IProduct
    {
        public string Name { get; private set; }
        public double Price { get; private set; }
        public string Manufacturer { get; private set; }
        public int Age { get; private set; }

        public void Train()
        {
            Console.WriteLine($"   Тренуємося зі спорт-інвентарем \"{Name}\" (вік {Age}+)");
        }

        public SportsInventory(string name, double price, string manufacturer, int age)
        {
            Name = name;
            Price = price;
            Manufacturer = manufacturer;
            Age = age;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Спорт-інвентар: \"{Name}\", ціна: {Price} грн,");
            Console.WriteLine($"                виробник: {Manufacturer}, вік: {Age}+");
        }

        public bool MatchesType(string type)
        {
            string t = type.ToLower();
            return t == "sports" || t == "спорт-інвентар" || t == "sportsinventory";
        }

        public int CompareTo(object? obj)
        {
            if (obj is null) return 1;
            if (obj is IProduct other)
                return Price.CompareTo(other.Price);
            return 0;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    internal class Task2
    {
        public static void Run()
        {
            Console.WriteLine("Лабораторна робота №6. Варіант 2.7 (Товар)\n");

            IProduct[] products = new IProduct[6];
            products[0] = new Toy("Лего конструктор", 499.99, "Lego", "Пластик", 6);
            products[1] = new Book("Гаррі Поттер і філософський камінь", "Дж. К. Роулінг", 299.50, "А-БА-БА-ГА-ЛА-МА-ГА", 10);
            products[2] = new SportsInventory("Футбольний м'яч", 199.99, "Nike", 8);
            products[3] = new Toy("Барбі лялька", 249.99, "Mattel", "Пластик+тканина", 5);
            products[4] = new Book("Казки Андерсена", "Ганс Крістіан Андерсен", 149.99, "Видавництво Старого Лева", 4);
            products[5] = new SportsInventory("Тенісна ракетка", 399.99, "Wilson", 12);

            Console.WriteLine("Сортуємо товари за ціною (реалізація IComparable):");
            Array.Sort(products);
            Console.WriteLine("Сортування завершено.\n");

            Console.WriteLine("Повна інформація з бази товарів:");
            foreach (IProduct p in products)
            {
                p.DisplayInfo();

                if (p is Toy toy)
                    toy.Play();
                else if (p is Book book)
                    book.Read();
                else if (p is SportsInventory sport)
                    sport.Train();

                Toy? t = p as Toy;
                if (t != null)
                {
                    Console.WriteLine("   (перевірено через as: це іграшка)");
                }

                Console.WriteLine();
            }

            Console.Write("Введіть тип товару для пошуку (toy / книга / sports): ");
            string searchType = Console.ReadLine() ?? "";

            Console.WriteLine($"\nРезультати пошуку за типом \"{searchType}\":");
            bool found = false;
            foreach (IProduct p in products)
            {
                if (p.MatchesType(searchType))
                {
                    p.DisplayInfo();
                    found = true;

                    if (p is Toy toy) toy.Play();
                    else if (p is Book book) book.Read();
                    else if (p is SportsInventory sport) sport.Train();

                    Console.WriteLine();
                }
            }
            if (!found)
            {
                Console.WriteLine("Товарів даного типу не знайдено.");
            }

            Console.WriteLine("\nДемонстрація клонування (ICloneable):");
            IProduct original = products[0];
            IProduct clone = (IProduct)((ICloneable)original).Clone()!;

            Console.WriteLine("Оригінал:");
            original.DisplayInfo();
            Console.WriteLine("Клон:");
            clone.DisplayInfo();
            Console.WriteLine("(об'єкти незалежні)");

            Console.WriteLine("\nНатисніть Enter для завершення...");
            Console.ReadLine();
        }
    }
}