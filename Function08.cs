using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace function8
{
    internal class Program
    {
        class Record
        {
            public string Phone;
            public string Plan;
            public int Minutes;
            public int DataMB;
            public decimal Bill;
        }

        static decimal ComputeMinutesCost(string plan, int minutes)
        {
            switch (plan.ToUpper())
            {
                case "BASIC": return minutes * 1.0m;
                case "PLUS": return minutes * 0.80m;
                case "ULTRA": return minutes * 0.70m;
                default: return 0;
            }
        }

        static decimal ComputeDataCost(string plan, int dataMB)
        {
            switch (plan.ToUpper())
            {
                case "BASIC": return dataMB * 0.50m;
                case "PLUS": return dataMB * 0.30m;
                case "ULTRA":
                    if (dataMB <= 500) return 0;
                    else return (dataMB - 500) * 0.20m;
                default: return 0;
            }
        }

        static void Main()
        {
            List<Record> records = new List<Record>();

            while (true)
            {
                Console.Write("Enter phone number (11 digits, END to stop): ");
                string phone = Console.ReadLine();
                if (phone.ToUpper() == "END") break;

                if (phone.Length != 11 || !long.TryParse(phone, out _))
                {
                    Console.WriteLine("Invalid phone number.");
                    continue;
                }

                Console.Write("Enter plan code (BASIC/PLUS/ULTRA): ");
                string plan = Console.ReadLine().ToUpper();
                if (plan != "BASIC" && plan != "PLUS" && plan != "ULTRA")
                {
                    Console.WriteLine("Invalid plan code.");
                    continue;
                }

                Console.Write("Enter minutes: ");
                int minutes = Convert.ToInt32(Console.ReadLine());
                if (minutes < 0)
                {
                    Console.WriteLine("Minutes cannot be negative.");
                    continue;
                }

                Console.Write("Enter data MB: ");
                int data = Convert.ToInt32(Console.ReadLine());
                if (data < 0)
                {
                    Console.WriteLine("Data cannot be negative.");
                    continue;
                }

                decimal cost = ComputeMinutesCost(plan, minutes) + ComputeDataCost(plan, data);
                cost = Math.Round(cost, 2);

                records.Add(new Record { Phone = phone, Plan = plan, Minutes = minutes, DataMB = data, Bill = cost });
                Console.WriteLine($"Bill for {phone}: ₱{cost:F2}\n");
            }

            Console.WriteLine("\n--- Billing Summary ---");
            if (records.Count == 0)
            {
                Console.WriteLine("No records.");
                return;
            }

            decimal total = 0, maxBill = 0;
            string maxPhone = "";
            foreach (var r in records)
            {
                Console.WriteLine($"{r.Phone} ({r.Plan}) → ₱{r.Bill:F2}");
                total += r.Bill;
                if (r.Bill > maxBill)
                {
                    maxBill = r.Bill;
                    maxPhone = r.Phone;
                }
            }

            decimal avg = total / records.Count;
            Console.WriteLine($"\nHighest bill: {maxPhone} = ₱{maxBill:F2}");
            Console.WriteLine($"Average bill: ₱{avg:F2}");
        }
    }
}