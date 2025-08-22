using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace function1
{
    internal class Program
    {
        static int computePoints(decimal spent, string coupon)
        {
            int points = (int)Math.Floor(spent) * 5;
            if (!string.IsNullOrEmpty(coupon))
            {
                string c = coupon.ToUpper();
                if (c == "NEW50" || c == "VIP50")
                {
                    points += 50;
                }
            }
            return points;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Loyalty Points Normalizer");
            Console.WriteLine("This program normalizes loyalty points for customers.");

            Console.WriteLine("Enter number of lines:");
            int N = Convert.ToInt32(Console.ReadLine());

            List<string> ids = new List<string>();
            List<int> pts = new List<int>();

            for (int i = 0; i < N; i++)
            {
                Console.Write("Line " + (i + 1) + ": ");
                string line = Console.ReadLine();
                string[] parts = line.Split(',');

                if (parts.Length < 2)
                {
                    Console.WriteLine("Skipped: invalid format");
                    continue;
                }

                string customerId = parts[0].Trim();
                string totalSpentStr = parts[1].Trim();
                string coupon = parts.Length == 3 ? parts[2].Trim() : "";

            
                bool validId = true;
                foreach (char ch in customerId)
                {
                    if (!char.IsLetterOrDigit(ch))
                    {
                        validId = false;
                        break;
                    }
                }
                if (!validId)
                {
                    Console.WriteLine("Skipped: invalid customerId");
                    continue;
                }

                decimal spent;
                if (!decimal.TryParse(totalSpentStr, out spent) || spent < 0)
                {
                    Console.WriteLine("Skipped: invalid totalSpent");
                    continue;
                }

                int points = computePoints(spent, coupon);
                ids.Add(customerId);
                pts.Add(points);
            }

           
            for (int i = 0; i < ids.Count - 1; i++)
            {
                for (int j = i + 1; j < ids.Count; j++)
                {
                    if (pts[j] > pts[i] || (pts[j] == pts[i] && String.Compare(ids[j], ids[i]) < 0))
                    {
                        
                        int tempP = pts[i]; pts[i] = pts[j]; pts[j] = tempP;
                        string tempId = ids[i]; ids[i] = ids[j]; ids[j] = tempId;
                    }
                }
            }

            Console.WriteLine("\nTop Customers:");
            for (int i = 0; i < ids.Count && i < 3; i++)
            {
                Console.WriteLine(ids[i] + " => " + pts[i] + " points");
            }
        }
    }
}