using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace function5
{
    internal class Program
    {
        //  static void Main(string[] args)
        static bool IsValid(string u, out string reason)
        {
            reason = "";

            if (u.Length < 6 || u.Length > 15)
            {
                reason = "Length must be 6–15 characters";
                return false;
            }

            if (!char.IsLetter(u[0]))
            {
                reason = "Must start with a letter";
                return false;
            }

            bool hasDigit = false;
            foreach (char c in u)
            {
                if (!(char.IsLetterOrDigit(c) || c == '_'))
                {
                    reason = "Contains invalid character(s)";
                    return false;
                }
                if (char.IsDigit(c)) hasDigit = true;
            }

            if (!hasDigit)
            {
                reason = "Must contain at least one digit";
                return false;
            }

            return true;
        }

        static List<string> Suggest(string u)
        {
            List<string> suggestions = new List<string>();

            string fixedU = u;

            char[] arr = fixedU.ToCharArray();
            List<char> cleaned = new List<char>();
            foreach (char c in arr)
            {
                if (c == ' ') cleaned.Add('_');
                else if (char.IsLetterOrDigit(c) || c == '_')
                    cleaned.Add(c);
            }
            fixedU = new string(cleaned.ToArray());

            if (fixedU.Length > 0 && !char.IsLetter(fixedU[0]))
            {
                fixedU = "U" + fixedU;
            }

            if (fixedU.Length < 6)
            {
                suggestions.Add(fixedU + "123");
                suggestions.Add(fixedU + "99");
            }
            else
            {

                bool hasDigit = false;
                foreach (char c in fixedU)
                    if (char.IsDigit(c)) { hasDigit = true; break; }

                if (!hasDigit)
                {
                    suggestions.Add(fixedU + "1");
                    suggestions.Add(fixedU + "2025");
                }
            }

            HashSet<string> unique = new HashSet<string>(suggestions);
            return new List<string>(unique);
        }

        static void Main()
        {
            Console.Write("How many usernames? ");
            int t = int.Parse(Console.ReadLine());

            for (int i = 0; i < t; i++)
            {
                Console.Write($"Enter username number{i + 1}: ");
                string u = Console.ReadLine().Trim();

                if (IsValid(u, out string reason))
                {
                    Console.WriteLine("VALID");
                }
                else
                {
                    Console.WriteLine($"INVALID: {reason}");
                    List<string> sug = Suggest(u);
                    if (sug.Count > 0)
                    {
                        Console.WriteLine("Suggestions:");
                        foreach (string s in sug)
                            Console.WriteLine(" - " + s);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}



