using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace function9
{
    internal class Program
    {
        class Book
        {
            public string Title;
            public string Author;
        }

        static string NormalizeSpaces(string s)
        {
            s = s.Trim();
            string result = "";
            bool lastSpace = false;

            foreach (char c in s)
            {
                if (char.IsWhiteSpace(c))
                {
                    if (!lastSpace)
                    {
                        result += " ";
                        lastSpace = true;
                    }
                }
                else
                {
                    result += c;
                    lastSpace = false;
                }
            }
            return result;
        }

        static string ToTitleCase(string s)
        {
            s = s.ToLower();
            string result = "";
            bool newWord = true;

            foreach (char c in s)
            {
                if (c == ' ')
                {
                    result += c;
                    newWord = true;
                }
                else
                {
                    if (newWord)
                        result += char.ToUpper(c);
                    else
                        result += c;
                    newWord = false;
                }
            }
            return result;
        }

        static void Main()
        {
            Console.Write("Enter number of books: ");
            int n = Convert.ToInt32(Console.ReadLine());

            List<Book> catalog = new List<Book>();

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\nBook #{i + 1}:");
                Console.Write("Enter title: ");
                string title = Console.ReadLine();
                Console.Write("Enter author: ");
                string author = Console.ReadLine();

                title = NormalizeSpaces(title);
                author = NormalizeSpaces(author);

                title = ToTitleCase(title);
                author = ToTitleCase(author);

                catalog.Add(new Book { Title = title, Author = author });
            }

            Console.WriteLine("\nCatalog saved. Start searching (type EXIT to stop).");

            while (true)
            {
                Console.Write("\nEnter title prefix: ");
                string prefix = Console.ReadLine();
                if (prefix.ToUpper() == "EXIT") break;

                prefix = prefix.Trim().ToLower();
                bool found = false;

                foreach (var book in catalog)
                {
                    if (book.Title.ToLower().StartsWith(prefix))
                    {
                        Console.WriteLine($"→ {book.Title} by {book.Author}");
                        found = true;
                    }
                }

                if (!found)
                    Console.WriteLine("No results.");
            }
        }
    }
}
