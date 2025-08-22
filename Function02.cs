using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace function2
{
    internal class Program
    {
       // static void Main(string[] args)
        
            static int Curve(int score)
            {
                if (score >= 60 && score <= 79) score += 5;
                else if (score >= 80 && score <= 89) score += 3;
                if (score > 100) score = 100;
                return score;
            }
            static string Bucket(int score)
            {
                switch (score)
                {
                    case int n when n >= 90: return "A";
                    case int n when n >= 80: return "B";
                    case int n when n >= 70: return "C";
                    case int n when n >= 60: return "D";
                    default: return "F";
                }
            }

            static void Main()
            {
                List<(string Name, int Score, string Grade)> list = new List<(string, int, string)>();

                while (true)
                {
                    Console.Write("Enter student name (END to stop): ");
                    string name = Console.ReadLine().Trim();

                    if (name.ToUpper() == "END") break;

                    int score;
                    while (true)
                    {
                        Console.Write("Enter score (0–100): ");
                        if (int.TryParse(Console.ReadLine(), out score) && score >= 0 && score <= 100)
                            break;
                        Console.WriteLine("Invalid score. Try again.");
                    }

                    int finalScore = Curve(score);
                    string grade = Bucket(finalScore);

                    list.Add((name.ToUpper(), finalScore, grade));
                }

                Console.WriteLine("\n--- Results by Bucket ---");
                string[] order = { "A", "B", "C", "D", "F" };

            foreach (string g in order)
            {
                int count = 0;
                foreach (var item in list)
                    if (item.Grade == g) count++;

                if (count > 0)
                {
                    Console.WriteLine($"\nBucket {g} ({count}):");
                    foreach (var item in list)
                        if (item.Grade == g)
                            Console.WriteLine($" - {item.Name} ({item.Score})");
                }
            }
                } 

    }
}

