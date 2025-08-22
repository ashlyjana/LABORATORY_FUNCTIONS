using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace function4
{
    internal class Program
    {
        
            static int[] MakeChange(decimal change)
            {
               
                int cents = Convert.ToInt32(change * 100);

                int[] denomCents = {
            100000, 50000, 20000, 10000, 5000,
            2000, 1000, 500, 100,
            25, 10, 5  
        };

                int[] counts = new int[denomCents.Length];

                for (int i = 0; i < denomCents.Length; i++)
                {
                    counts[i] = cents / denomCents[i];
                    cents = cents % denomCents[i];
                }

                return counts;
            }

            static void Main()
            {
                Console.Write("Enter amount due: ");
                decimal due = Convert.ToDecimal(Console.ReadLine());

                Console.Write("Enter amount paid: ");
                decimal paid = Convert.ToDecimal(Console.ReadLine());

                if (paid < due)
                {
                    Console.WriteLine("Error: Paid amount must be greater than or equal to due.");
                    return;
                }

                decimal change = Math.Round(paid - due, 2);

                if (change == 0)
                {
                    Console.WriteLine("Exact payment — no change.");
                    return;
                }

                int[] counts = MakeChange(change);

                decimal[] denom = {
            1000m, 500m, 200m, 100m, 50m,
            20m, 10m, 5m, 1m,
            0.25m, 0.10m, 0.05m
        };

                Console.WriteLine($"\nChange to return: {change:C}");
                Console.WriteLine("Denomination\tCount");

                int totalPieces = 0;
                for (int i = 0; i < denom.Length; i++)
                {
                    if (counts[i] > 0)
                    {
                        Console.WriteLine($"{denom[i],10:C}\t{counts[i]}");
                        totalPieces += counts[i];
                    }
                }

                Console.WriteLine($"\nTotal pieces: {totalPieces}");
            }
        }
    }


