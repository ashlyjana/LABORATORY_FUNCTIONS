using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace function3
{
    internal class Program
    {
        // static void Main(string[] args)
        static bool IsAllLetters(string s)
        {
            foreach (char c in s)
                if (!char.IsLetter(c)) return false;
            return true;
        }

        static bool IsValidSku(string s, out string reason)
        {
            reason = "";
            string[] parts = s.ToUpper().Split('-');

            if (parts.Length != 4)
            {
                reason = "Wrong format";
                return false;
            }

            string category = parts[0];
            string year = parts[1];
            string batch = parts[2];
            string size = parts[3];

            if (category.Length < 3 || category.Length > 6 || !IsAllLetters(category))
            {
                reason = "CATEGORY invalid";
                return false;
            }

            if (!(year.Length == 4 && int.TryParse(year, out int y) && y >= 2020))
            {
                reason = "YEAR invalid";
                return false;
            }

            if (!(int.TryParse(batch, out int b) && batch.Length >= 2 && batch.Length <= 4))
            {
                reason = "BATCH invalid";
                return false;
            }

            string[] validSizes = { "S", "M", "L", "XL", "XXL" };
            bool ok = false;
            foreach (string v in validSizes)
                if (size == v) { ok = true; break; }

            if (!ok)
            {
                reason = "SIZE invalid";
                return false;
            }

            return true;
        }

        static void Main()
        {
            Console.Write("How many SKUs? ");
            int k = int.Parse(Console.ReadLine());

            string[] skus = new string[k];
            for (int i = 0; i < k; i++)
            {
                Console.Write($"Enter SKU #{i + 1}: ");
                skus[i] = Console.ReadLine().Trim();
            }

            int countELEC = 0, countFOOD = 0, countTOY = 0;
            int countS = 0, countM = 0, countL = 0, countXL = 0, countXXL = 0;

            Console.WriteLine("\nInvalid SKUs:");
            foreach (string sku in skus)
            {
                if (IsValidSku(sku, out string reason))
                {
                    string[] parts = sku.ToUpper().Split('-');
                    string category = parts[0];
                    string size = parts[3];

                    if (category == "ELEC") countELEC++;
                    else if (category == "FOOD") countFOOD++;
                    else if (category == "TOY") countTOY++;

              
                    if (size == "S") countS++;
                    else if (size == "M") countM++;
                    else if (size == "L") countL++;
                    else if (size == "XL") countXL++;
                    else if (size == "XXL") countXXL++;
                }
                else
                {
                    Console.WriteLine($" {sku} - {reason}");
                }
            }

            Console.WriteLine("\nCategory Counts:");
            Console.WriteLine($" ELEC: {countELEC}");
            Console.WriteLine($" FOOD: {countFOOD}");
            Console.WriteLine($" TOY : {countTOY}");

            int[] sizeCounts = { countS, countM, countL, countXL, countXXL };
            string[] sizeNames = { "S", "M", "L", "XL", "XXL" };

            int max = 0;
            string topSize = "";
            for (int i = 0; i < sizeCounts.Length; i++)
            {
                if (sizeCounts[i] > max)
                {
                    max = sizeCounts[i];
                    topSize = sizeNames[i];
                }
            }

            Console.WriteLine($"\nMost Frequent SIZE: {topSize} ({max})");
        }
    }
}
