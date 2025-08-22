using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace function6
{
    internal class Program
    {
        class LogEntry
        {
            public string Timestamp;
            public string Level;
            public string Message;
        }

        static LogEntry Parse(string line)
        {
            string[] parts = line.Split(',');
            LogEntry entry = new LogEntry();
            if (parts.Length == 3)
            {
                entry.Timestamp = parts[0].Trim();
                entry.Level = parts[1].Trim().ToUpper();
                entry.Message = parts[2].Trim();
            }
            else
            {
                entry.Timestamp = "INVALID";
                entry.Level = "INVALID";
                entry.Message = "Parse error";
            }
            return entry;
        }

        static void Main()
        {
            Console.Write("Enter number of log lines: ");
            int n = int.Parse(Console.ReadLine());

            List<LogEntry> logs = new List<LogEntry>();

            for (int i = 0; i < n; i++)
            {
                Console.Write($"Enter log line NO.{i + 1} (timestamp,level,message): ");
                logs.Add(Parse(Console.ReadLine()));
            }

            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Count by level");
                Console.WriteLine("2. Filter by keyword");
                Console.WriteLine("3. Show last N ERRORs");
                Console.WriteLine("4. Exit");
                Console.Write("Choose option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        int info = 0, warn = 0, error = 0;
                        foreach (var log in logs)
                        {
                            if (log.Level == "INFO") info++;
                            else if (log.Level == "WARN") warn++;
                            else if (log.Level == "ERROR") error++;
                        }
                        Console.WriteLine($"INFO: {info}, WARN: {warn}, ERROR: {error}");
                        break;

                    case "2":
                        Console.Write("Enter keyword: ");
                        string key = Console.ReadLine().ToLower();
                        foreach (var log in logs)
                            if (log.Message.ToLower().Contains(key))
                                Console.WriteLine($"{log.Timestamp} [{log.Level}] {log.Message}");
                        break;

                    case "3":
                        Console.Write("How many ERRORs to show? ");
                        int num = int.Parse(Console.ReadLine());
                        List<LogEntry> errors = new List<LogEntry>();
                        foreach (var log in logs)
                            if (log.Level == "ERROR") errors.Add(log);

                        int start = Math.Max(0, errors.Count - num);
                        for (int i = start; i < errors.Count; i++)
                            Console.WriteLine($"{errors[i].Timestamp} [{errors[i].Level}] {errors[i].Message}");
                        break;

                    case "4":
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid option, try again.");
                        break;
                }
            }
        }
    }
}
