using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace function7
{
    internal class Program
    {
        enum ClassType { YOGA, HIIT, DANCE }

        class Registration
        {
            public string Name;
            public int Age;
            public ClassType Class;
            public string TimeSlot;
        }

        static bool TryRegister(List<Registration> list, string name, int age, string classType, string timeSlot, out string error)
        {
            error = "";

            if (age < 12 || age > 80)
            {
                error = "Invalid age (must be 12–80).";
                return false;
            }

            ClassType cType;
            switch (classType.ToUpper())
            {
                case "YOGA": cType = ClassType.YOGA; break;
                case "HIIT": cType = ClassType.HIIT; break;
                case "DANCE": cType = ClassType.DANCE; break;
                default:
                    error = "Invalid class type.";
                    return false;
            }

            DateTime dt;
            if (!DateTime.TryParse(timeSlot, out dt))
            {
                error = "Invalid time format.";
                return false;
            }
            if (dt.Minute % 15 != 0)
            {
                error = "Time must be on quarter-hour (:00, :15, :30, :45).";
                return false;
            }
            string slot = dt.ToString("HH:mm");

            if (age < 16 && cType == ClassType.HIIT)
            {
                error = "Must be 16+ for HIIT.";
                return false;
            }

            int count = 0;
            foreach (var r in list)
                if (r.TimeSlot == slot) count++;
            if (count >= 10)
            {
                error = "Time slot full (max 10).";
                return false;
            }

            list.Add(new Registration { Name = name.ToUpper(), Age = age, Class = cType, TimeSlot = slot });
            return true;
        }

        static void Main()
        {
            List<Registration> regs = new List<Registration>();

            while (true)
            {
                Console.Write("Enter Name (or END): ");
                string name = Console.ReadLine();
                if (name.ToUpper() == "END") break;

                Console.Write("Enter Age: ");
                int age = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter ClassType (Yoga/HIIT/Dance): ");
                string classType = Console.ReadLine();

                Console.Write("Enter TimeSlot (HH:mm): ");
                string timeSlot = Console.ReadLine();

                string err;
                if (TryRegister(regs, name, age, classType, timeSlot, out err))
                    Console.WriteLine("Registered successfully.");
                else
                    Console.WriteLine("Error: " + err);
            }

            Console.WriteLine("\n--- Class Schedule ---");
            HashSet<string> slotsShown = new HashSet<string>();
            foreach (var r in regs)
            {
                if (!slotsShown.Contains(r.TimeSlot))
                {
                    Console.WriteLine($"\nSlot {r.TimeSlot}:");
                    foreach (var r2 in regs)
                        if (r2.TimeSlot == r.TimeSlot)
                            Console.WriteLine($" - {r2.Class} : {r2.Name}");
                    slotsShown.Add(r.TimeSlot);
                }
            }
        }
    }
}