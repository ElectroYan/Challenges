namespace Challenges.AdventOfCode {
    internal abstract class Day {
        public Day() {
            Input = InputManager.GetInputByDay(
                int.Parse(GetType()?.Namespace?.Split('.')?.Last()?[1..] ?? "2022"),
                int.Parse(GetType().Name.Replace("Day", ""))
            );
        }
        public Day(string input) {
            Input = input.Replace("\r", "").Trim('\n');
        }
        public string Input { get; private set; }
        public List<string> InputList => Input.Split('\n').ToList();
        public abstract string Level1();
        public abstract string Level2();
    }
}