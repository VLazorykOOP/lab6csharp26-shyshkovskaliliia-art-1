using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Lab6Sharp
{
    public interface IAssessable
    {
        string Name { get; }
        int Score { get; }
        void Show();
    }

    public interface ITimeBound
    {
        int Duration { get; }
        string Subject { get; }
    }

    public interface IQuestionable
    {
        int QuestionsCount { get; }
    }

    public class Test : IAssessable, IQuestionable, IComparable, ICloneable, IEnumerable
    {
        public string Name { get; }
        public int Score { get; }
        public int QuestionsCount { get; }

        public Test(string name, int score, int questionsCount)
        {
            Name = name;
            Score = score;
            QuestionsCount = questionsCount;
        }

        public void Show()
        {
            Console.WriteLine($"[Тест] Назва: {Name} | Оцінка: {Score} | Кількість питань: {QuestionsCount}");
        }

        public void CalculateSuccessRate()
        {
            double rate = (double)Score / QuestionsCount * 100;
            Console.WriteLine($" -> Особливий метод Тесту: Продуктивність = {rate:F1}%");
        }

        public int CompareTo(object? obj)
        {
            if (obj is null) return 1; 
            if (obj is IAssessable other)
                return Score.CompareTo(other.Score);
            throw new ArgumentException("Об'єкт не є IAssessable");
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 1; i <= QuestionsCount; i++)
                yield return i;
        }
    }

    public class Exam : IAssessable, ITimeBound, IComparable, ICloneable
    {
        public string Name { get; }
        public int Score { get; }
        public int Duration { get; }
        public string Subject { get; }

        public Exam(string name, int score, string subject, int duration)
        {
            Name = name; Score = score; Subject = subject; Duration = duration;
        }

        public void Show()
        {
            Console.WriteLine($"[Іспит] Назва: {Name} | Предмет: {Subject} | Оцінка: {Score} | Тривалість: {Duration} хв.");
        }

        public void ScheduleRetake()
        {
            Console.WriteLine($" -> Особливий метод Іспиту: Призначено перездачу '{Subject}'");
        }

        public int CompareTo(object? obj)
        {
            if (obj is null) return 1;
            if (obj is IAssessable other)
                return Score.CompareTo(other.Score);
            throw new ArgumentException("Об'єкт не є IAssessable");
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class FinalExam : IAssessable, ITimeBound, IComparable, ICloneable
    {
        public string Name { get; }
        public int Score { get; }
        public int Duration { get; }
        public string Subject { get; }
        public bool IsStateExam { get; }

        public FinalExam(string name, int score, string subject, int duration, bool isStateExam)
        {
            Name = name; Score = score; Subject = subject; Duration = duration; IsStateExam = isStateExam;
        }

        public void Show()
        {
            string type = IsStateExam ? "Державний іспит" : "Звичайний випускний";
            Console.WriteLine($"[Випускний іспит] Назва: {Name} | Предмет: {Subject} | Оцінка: {Score} | Тип: {type}");
        }

        public void IssueDiploma()
        {
            string docType = IsStateExam ? "державного зразка" : "установи";
            Console.WriteLine($" -> Особливий метод: Підготовлено диплом {docType}");
        }

        public int CompareTo(object? obj)
        {
            if (obj is null) return 1;
            if (obj is IAssessable other)
                return Score.CompareTo(other.Score);
            throw new ArgumentException("Об'єкт не є IAssessable");
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class Trial : IAssessable, IComparable, ICloneable
    {
        public string Name { get; }
        public int Score { get; }
        public string TrialType { get; }

        public Trial(string name, int score, string trialType)
        {
            Name = name; Score = score; TrialType = trialType;
        }

        public void Show()
        {
            Console.WriteLine($"[Випробування] Назва: {Name} | Оцінка: {Score} | Тип: {TrialType}");
        }

        public void GeneratePsychologicalReport()
        {
            Console.WriteLine($" -> Особливий метод Випробування: Згенеровано психологічний звіт");
        }

        public int CompareTo(object? obj)
        {
            if (obj is null) return 1;
            if (obj is IAssessable other)
                return Score.CompareTo(other.Score);
            throw new ArgumentException("Об'єкт не є IAssessable");
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    internal class Task1
    {
        public static void Run()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("=== Лабораторна робота №6 — Завдання 1 (варіант 1.7) ===\n");

            var test = new Test("Тест з ООП", 88, 25);
            var exam = new Exam("Іспит з C#", 95, "Програмування", 150);
            var finalExam = new FinalExam("Випускний іспит", 97, "Інформатика", 180, true);
            var trial = new Trial("Практичне випробування", 75, "Виконання проекту");

            IAssessable[] assessments = { test, exam, finalExam, trial };

            Console.WriteLine("1. Show() через IAssessable[]");
            foreach (var item in assessments) item.Show();

            Array.Sort(assessments);
            Console.WriteLine("\n2. Відсортовано (IComparable)");

            foreach (var item in assessments) item.Show();

            Console.WriteLine("\n3. Клонування (ICloneable + MemberwiseClone())");
            Test clonedTest = (Test)test.Clone();
            Console.Write("Оригінал: "); test.Show();
            Console.Write("Клон:     "); clonedTest.Show();


            Console.WriteLine("\n4. Перебір питань тесту (IEnumerable + yield return)");
            foreach (int q in test)
                Console.WriteLine($"   → Питання №{q}");

            Console.WriteLine("\n5. Особливі методи (type pattern)");
            PrintSpecificMethods(assessments);
        }

        private static void PrintSpecificMethods(IAssessable[] array)
        {
            foreach (var item in array)
            {
                switch (item)
                {
                    case Test t: t.CalculateSuccessRate(); break;
                    case FinalExam fe: fe.IssueDiploma(); break;
                    case Exam e: e.ScheduleRetake(); break;
                    case Trial tr: tr.GeneratePsychologicalReport(); break;
                }
            }
        }
    }
}