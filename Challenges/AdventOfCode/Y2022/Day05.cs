using System.Security.Principal;
using System.Text.RegularExpressions;

namespace Challenges.AdventOfCode.Y2022 {
    internal class Day05 : Day {
        public Day05() {
        }

        public Day05(string input) : base(input) {
        }

        public (Dictionary<int, Stack<string>>, List<(int, int, int)>) ParseInput() {
            Dictionary<int, Stack<string>> stacks = new();
            var stackRow = InputList.First(x => x.StartsWith(" 1"));
            var stackCount = 9;
            int stackHeight = InputList.IndexOf(stackRow);
            var regex = "";

            for (int i = 0; i < stackCount; i++) {
                stacks.Add(i + 1, new Stack<string>());
                regex += $@"(\[(?< Group{(i + 1)}>\w)\]|(?<Group{(i + 1)}>\s{{4}}))\s ? ";
            }

            for (int i = stackHeight - 1; i >= 0; i--) {
                stackRow = InputList[i];

                var stackParsed = Regex.Match(stackRow, regex);
                for (int j = 1; j <= stackCount; j++) {
                    var value = stackParsed.Groups["Group" + j].Value;
                    if (value.Trim() != "")
                        stacks[j].Push(value);
                }
            }

            List<(int, int, int)> commands = new();
            var commandList = InputList.Skip(stackCount + 1);
            foreach (var item in commandList) {
                var parsed = Regex.Match(item, @"move(?<Count>\d +) from(?<Source>\d) to(?<Dest>\d)");
                commands.Add((int.Parse(parsed.Groups["Count"].Value), int.Parse(parsed.Groups["Source"].Value), int.Parse(parsed.Groups["Dest"].Value)));
            }
            return (stacks, commands);
        }

        public override string Level1() {
            (var stacks, var instructions) = ParseInput();
            foreach (var inst in instructions) {
                (var count, var source, var dest) = inst;
                for (int i = 0; i < count; i++)
                    stacks[dest].Push(stacks[source].Pop());
            }
            var res = "";
            for (int i = 0; i < stacks.Count; i++)
                res += stacks[(i + 1)].Peek();
            return res;
        }
        public override string Level2() {
            (var stacks, var instructions) = ParseInput();

            foreach (var inst in instructions) {
                (var count, var source, var dest) = inst;
                var stackTmp = new Stack<string>();
                for (int i = 0; i < count; i++) {
                    stackTmp.Push(stacks[source].Pop());
                }
                for (int i = 0; i < count; i++) {
                    stacks[dest].Push(stackTmp.Pop());
                }
            }

            var res = "";
            for (int i = 0; i < stacks.Count; i++) {
                res += stacks[(i + 1)].Peek();
            }
            return res;
        }
    }
}
