// See https://aka.ms/new-console-template for more information

Console.WriteLine("[A]dvent of Code");
var input = Console.ReadKey();

if (input.Key == ConsoleKey.A || input.Key == ConsoleKey.Enter) Challenges.AdventOfCode.ChallengeManager.Run();
else if (input.Key == ConsoleKey.C) throw new NotImplementedException();
else Console.WriteLine("Invalid Input");