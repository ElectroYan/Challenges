using Challenges.AdventOfCode.Y2022;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Challenges.AdventOfCode {
    internal class ChallengeManager {
        public static void Run() {
            Console.Write($"Advent of Code [{DateTime.Now.Year}]: ");
            var year = Console.ReadLine() ?? "";
            if (year == "") year = DateTime.Now.Year.ToString();
            Console.Write($"Day [{DateTime.Now.Day}]: ");
            var day = Console.ReadLine() ?? "";
            if (day == "") day = DateTime.Now.Day.ToString();
            Console.Write("Test [y/N]: ");
            var test = Console.ReadKey();
            var input = "";
            if (test.Key == ConsoleKey.Y) {
                Console.WriteLine("Test Input (press backspace to confirm):");
                input = GetLongInput();
            }
            Console.Clear();
            var className = "Challenges.AdventOfCode.Y" + year + ".Day" + day.PadLeft(2, '0');
            var type = Type.GetType(className) ?? typeof(Day01);
            Day dayy;
            if (input == "")
                dayy = Activator.CreateInstance(type) as Day;
            else
                dayy = Activator.CreateInstance(type, input) as Day;
            dayy = dayy ?? new Day01();
            Console.Write("Calculate Level 1: ");
            Stopwatch w = new();
            w.Start();
            string level1 = dayy.Level1(); 
            Console.Write($"{w.ElapsedMilliseconds}ms\nResult: {level1}\nCalculate Level 2: ");
            string level2 = dayy.Level2(); 
            Console.WriteLine($"{w.ElapsedMilliseconds}ms\nResult: {level2}");
            w.Stop();

            Console.Write("Submit Answer: ");
            var answer = Console.ReadLine() ?? "";
            var response = "";
            if (answer.ToLower() == "1")
                response = InputManager.SubmitAnswer(int.Parse(year), int.Parse(day), 1, level1);
            else if (answer.ToLower() == "2")
                response = InputManager.SubmitAnswer(int.Parse(year), int.Parse(day), 2, level2);
            else
                Console.WriteLine("No Answer Submitted");

            if (response != "") {
                var main = Regex.Match(response, @"<main>(.*?)</main>", RegexOptions.Singleline).Groups[1].Value;
                Console.WriteLine(main);
            }
        }

        private static string GetLongInput() {
            Console.WriteLine();
            var input = "";
            var key = Console.ReadKey();
            while (key.Key != ConsoleKey.Backspace) {
                if (key.Key == ConsoleKey.Enter)
                    input += "\n";
                else
                    input += key.KeyChar;
                key = Console.ReadKey();
            }
            Console.WriteLine();
            return input;
        }
    }
}
