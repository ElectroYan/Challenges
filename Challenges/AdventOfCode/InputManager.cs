using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace Challenges.AdventOfCode {
    internal static class InputManager {
        public static string GetInputByDay(int year, int day) {
            if (!Directory.Exists("Input"))
                Directory.CreateDirectory("Input");

            if (File.Exists($"Input/{year}-{day}.txt"))
                return File.ReadAllText($"Input/{year}-{day}.txt").Trim('\n');
            
            var client = GetClientWithCookies();
            var data = client.DownloadData($"https://adventofcode.com/{year}/day/{day}/input");
            var input = Encoding.UTF8.GetString(data).Trim('\n');
            File.WriteAllText($"Input/{year}-{day}.txt", input);
            return input.Trim('\n');
        }

        public static string SubmitAnswer(int year, int day, int level, string answer) {
            var client = GetClientWithCookies();
            var reqParams = new NameValueCollection
            {
                { "level", level.ToString() },
                { "answer", answer.ToString() }
            };
            var data = client.UploadValues($"https://adventofcode.com/{year}/day/{day}/answer", reqParams);
            var result = Encoding.UTF8.GetString(data);
            return result;
        }
        private static WebClient GetClientWithCookies() {
            var session = "";
            if (File.Exists("session.txt"))
                session = File.ReadAllText("session.txt");
            else {
                Console.WriteLine("Please add your session cookie to a 'session.txt' file!");
                Console.ReadKey();
                throw new Exception("No Session File Found");
            }
            var client = new WebClient();
            client.Headers.Add($"cookie: session={session}");
            return client;
        }
        
        
    }
}
