using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Lab6Sharp
{
    internal class Task4
    {
        class Triangle : IEnumerable, ICloneable
        {
            protected int a;
            protected int b;
            protected int c;
            protected int color;
            protected bool isValid;

            public Triangle() : this(3, 3, 3, 0) { }

            public Triangle(int a, int b, int c, int color = 0)
            {
                if (IsValidTriangle(a, b, c))
                {
                    this.a = a;
                    this.b = b;
                    this.c = c;
                    this.color = color;
                    this.isValid = true;
                }
                else
                {
                    Console.WriteLine("Помилка: задані сторони не утворюють трикутник!");
                    this.a = 3;
                    this.b = 3;
                    this.c = 3;
                    this.color = 0;
                    this.isValid = false;
                }
            }

            public IEnumerator GetEnumerator()
            {
                yield return a;
                yield return b;
                yield return c;
            }

            public IEnumerable GetSidesInfo()
            {
                yield return $"Сторона a = {a}";
                yield return $"Сторона b = {b}";
                yield return $"Сторона c = {c}";
                yield return $"Периметр = {GetPerimeter()}";
                yield return $"Площа = {GetArea():F2}";
            }

            public object Clone()
            {
                return this.MemberwiseClone();
            }

            public int this[int index]
            {
                get
                {
                    switch (index)
                    {
                        case 0: return this.a;
                        case 1: return this.b;
                        case 2: return this.c;
                        case 3: return this.color;
                        default:
                            Console.WriteLine($"Помилка індексатора (get): індекс [{index}] недопустимий!");
                            return -1;
                    }
                }
                set
                {
                    switch (index)
                    {
                        case 0:
                            if (IsValidTriangle(value, this.b, this.c)) this.a = value;
                            else Console.WriteLine($"Помилка: значення {value} не утворює трикутник (a)");
                            break;
                        case 1:
                            if (IsValidTriangle(this.a, value, this.c)) this.b = value;
                            else Console.WriteLine($"Помилка: значення {value} не утворює трикутник (b)");
                            break;
                        case 2:
                            if (IsValidTriangle(this.a, this.b, value)) this.c = value;
                            else Console.WriteLine($"Помилка: значення {value} не утворює трикутник (c)");
                            break;
                        case 3:
                            this.color = value;
                            break;
                        default:
                            Console.WriteLine($"Помилка індексатора (set): індекс [{index}] недопустимий!");
                            break;
                    }
                }
            }

            public static Triangle? operator ++(Triangle t)
            {
                if (t != null)
                {
                    t.a++; t.b++; t.c++;
                    t.UpdateValidity();
                }
                return t;
            }

            public static Triangle? operator --(Triangle t)
            {
                if (t != null)
                {
                    t.a--; t.b--; t.c--;
                    t.UpdateValidity();
                }
                return t;
            }

            public static bool operator true(Triangle t) => t?.isValid == true;
            public static bool operator false(Triangle t) => t?.isValid != true;

            public static Triangle operator *(Triangle t, int scalar)
            {
                if (t == null) throw new ArgumentNullException(nameof(t), "Трикутник не може бути null");
                return new Triangle(t.a * scalar, t.b * scalar, t.c * scalar, t.color);
            }

            public static Triangle operator *(int scalar, Triangle t) => t * scalar;

            public static explicit operator string(Triangle t)
            {
                if (t == null) return "Triangle[null]";
                return t.ToString()!;
            }

            public static explicit operator Triangle(string s)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(s)) return new Triangle();
                    string[] parts = s.Split(new[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length < 3)
                    {
                        Console.WriteLine("Попередження: недостатньо даних для створення трикутника");
                        return new Triangle();
                    }
                    int a = int.Parse(parts[0].Trim());
                    int b = int.Parse(parts[1].Trim());
                    int c = int.Parse(parts[2].Trim());
                    int color = (parts.Length > 3) ? int.Parse(parts[3].Trim()) : 0;
                    return new Triangle(a, b, c, color);
                }
                catch
                {
                    Console.WriteLine("Помилка формату при перетворенні рядка в Triangle");
                    return new Triangle();
                }
            }

            public int GetPerimeter() => this.a + this.b + this.c;

            public double GetArea()
            {
                if (!this.isValid) return 0.0;
                double p = GetPerimeter() / 2.0;
                return Math.Sqrt(p * (p - this.a) * (p - this.b) * (p - this.c));
            }

            public override string ToString()
            {
                return string.Format(
                    "Triangle[a={0}, b={1}, c={2}] | P={3}, S={4:F2} | Color={5} | Valid={6}",
                    this.a, this.b, this.c,
                    GetPerimeter(), GetArea(),
                    this.color, this.isValid ? "Так" : "Ні");
            }

            private void UpdateValidity()
            {
                this.isValid = IsValidTriangle(this.a, this.b, this.c);
            }

            private static bool IsValidTriangle(int x, int y, int z)
            {
                return (x > 0) && (y > 0) && (z > 0) &&
                       (x + y > z) && (x + z > y) && (y + z > x);
            }
        }

        public static void Run()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("=== ЛАБОРАТОРНА РОБОТА №6 — ЗАВДАННЯ 4 ===");
            Console.WriteLine("Клас Triangle реалізує: IEnumerable + ICloneable\n");

            Triangle original = new Triangle(6, 8, 10, 15);
            Console.WriteLine("Оригінал: " + original);

            Triangle clone = (Triangle)original.Clone();
            Console.WriteLine("Клон:     " + clone);

            clone[0] = 7;
            Console.WriteLine("\nПісля зміни клону (clone[0] = 7):");
            Console.WriteLine("Оригінал: " + original);
            Console.WriteLine("Клон:     " + clone);

            Console.WriteLine("\n=== Перебір сторін через foreach ===");
            foreach (int side in original)
                Console.WriteLine($"   → Сторона = {side}");

            Console.WriteLine("\nЗавдання 4 виконано! Використані стандартні інтерфейси:");
            Console.WriteLine("   • IEnumerable  (через yield return)");
            Console.WriteLine("   • ICloneable   (метод Clone())");
        }
    }
}