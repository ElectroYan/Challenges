namespace Challenges.AdventOfCode.Y2022 {
    internal class Day07 : Day {
        public Day07() {
        }

        public Day07(string input) : base(input) {
        }

        public class DirectoryTree {
            public string Name { get; set; }
            public DirectoryTree Parent { get; set; }
            public DirectoryTree(string name, DirectoryTree parent) {
                Name = name.Trim();
                Parent = parent;
            }

            public long GetSize() {
                var size = (long)Files.Sum(x => x.Item2);
                size += Children.Sum(x => x.Value.GetSize());
                return size;
            }

            public Dictionary<string, DirectoryTree> Children { get; set; } = new Dictionary<string, DirectoryTree>();
            public List<(string, double)> Files = new List<(string, double)>();
        }

        public DirectoryTree BuildTree() {
            DirectoryTree basis = new(" / ", parent: null);
            DirectoryTree current = basis;
            foreach (var command in InputList) {
                var cmd = command.Trim('\r');
                if (cmd.StartsWith("$ cd")) {
                    var dirName = cmd[2..].Split(' ')[1];
                    if (dirName == " / ")
                        current = basis;
                    else if (dirName == "..") {
                        current = current.Parent;
                    } else {
                        if (!current.Children.ContainsKey(dirName))
                            current.Children.Add(dirName, new DirectoryTree(dirName, current));
                        current = current.Children[dirName];
                    }
                } else if (cmd.StartsWith("$ ls"))
                    continue;
                else if (cmd.StartsWith("dir")) {
                    var dirName = cmd.Split(' ')[1];
                    if (!current.Children.ContainsKey(dirName))
                        current.Children.Add(dirName, new DirectoryTree(dirName, current));
                } else {
                    var fileSize = long.Parse(cmd.Split(' ')[0]);
                    var fileName = cmd.Split(' ')[1];
                    current.Files.Add((fileName, fileSize));
                }
            }
            return basis;
        }

        public List<long> Sizes = new List<long>();
        public override string Level1() {
            var basis = BuildTree();
            CalcSizes(basis, 100000);

            return "" + Sizes.Sum();
        }
        public override string Level2() {
            var basis = BuildTree();
            CalcSizes(basis, 10000000000);
            var max = 70000000;
            var current = basis.GetSize();
            var required = 30000000;
            var remove = Sizes.OrderBy(x => x).First(x => max - current + x > required);
            return "" + remove;
        }

        public void CalcSizes(DirectoryTree tree, long max) {
            var size = tree.GetSize();
            if (size <= max) Sizes.Add(size);
            foreach (var item in tree.Children) {
                CalcSizes(item.Value, max);
            }
        }
    }
}
