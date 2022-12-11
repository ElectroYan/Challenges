using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using static Challenges.AdventOfCode.Y2022.Day11;

namespace Challenges.AdventOfCode.Y2022 {
    internal class Day11 : Day {
        public Day11() {
        }

        public Day11(string input) : base(input) {
        }

        public class Monkey {
            public static List<Monkey> Monkeys = new();
            public List<long> Items { get; set; } = new();
            public Func<long, long> Operation { get; }
            public int Test { get; }
            public Monkey TestTrue { get; set; }
            public Monkey TestFalse { get; set; }
            public long Inspections { get; set; } = 0;
            public Monkey(Func<long, long> operation, int test, List<long> items) {
                Items.AddRange(items);
                Operation = operation;
                Test = test;
            }

            public static void PlayRound(Func<long, long> eval) {
                foreach (var monkey in Monkeys) {
                    monkey.Inspections += monkey.Items.Count;
                    monkey.Items = monkey.Items.Select(x => monkey.Operation(x)).Select(eval).ToList();
                    foreach (var item in monkey.Items) {
                        if (item % monkey.Test == 0)
                            monkey.TestTrue.Items.Add(item);
                        else
                            monkey.TestFalse.Items.Add(item);
                    }
                    monkey.Items.Clear();
                }
            }
        }

        // lazy
        public void GenerateMonkeysTest() {
            Monkey.Monkeys.Clear();
            Monkey monkey0 = new((x) => x * 19, 23, new List<long> { 79, 98 });
            Monkey monkey1 = new((x) => x + 6, 19, new List<long> { 54, 65, 75, 74 });
            Monkey monkey2 = new((x) => x * x, 13, new List<long> { 79, 60, 97 });
            Monkey monkey3 = new((x) => x + 3, 17, new List<long> { 74 });
            monkey0.TestTrue = monkey2;
            monkey0.TestFalse = monkey3;
            monkey1.TestTrue = monkey2;
            monkey1.TestFalse = monkey0;
            monkey2.TestTrue = monkey1;
            monkey2.TestFalse = monkey3;
            monkey3.TestTrue = monkey0;
            monkey3.TestFalse = monkey1;
            Monkey.Monkeys.Add(monkey0);
            Monkey.Monkeys.Add(monkey1);
            Monkey.Monkeys.Add(monkey2);
            Monkey.Monkeys.Add(monkey3);
        }
        
        // lazy
        public void GenerateMonkeys() {
            Monkey.Monkeys.Clear();
            Monkey monkey0 = new((x) => x * 11, 7, new List<long> { 63, 57 });
            Monkey monkey1 = new((x) => x + 1, 11, new List<long> { 82, 66, 87, 78, 77, 92, 83 });
            Monkey monkey2 = new((x) => x * 7, 13, new List<long> { 97, 53, 53, 85, 58, 54 });
            Monkey monkey3 = new((x) => x + 3, 3, new List<long> { 50 });
            Monkey monkey4 = new((x) => x + 6, 17, new List<long> { 64, 69, 52, 65, 73 });
            Monkey monkey5 = new((x) => x + 5, 2, new List<long> { 57, 91, 65 });
            Monkey monkey6 = new((x) => x * x, 5, new List<long> { 67, 91, 84, 78, 60, 69, 99, 83 });
            Monkey monkey7 = new((x) => x + 7, 19, new List<long> { 58, 78, 69, 65 });
            monkey0.TestTrue = monkey6;
            monkey0.TestFalse = monkey2;
            monkey1.TestTrue = monkey5;
            monkey1.TestFalse = monkey0;
            monkey2.TestTrue = monkey4;
            monkey2.TestFalse = monkey3;
            monkey3.TestTrue = monkey1;
            monkey3.TestFalse = monkey7;
            monkey4.TestTrue = monkey3;
            monkey4.TestFalse = monkey7;
            monkey5.TestTrue = monkey0;
            monkey5.TestFalse = monkey6;
            monkey6.TestTrue = monkey2;
            monkey6.TestFalse = monkey4;
            monkey7.TestTrue = monkey5;
            monkey7.TestFalse = monkey1;
            Monkey.Monkeys.Add(monkey0);
            Monkey.Monkeys.Add(monkey1);
            Monkey.Monkeys.Add(monkey2);
            Monkey.Monkeys.Add(monkey3);
            Monkey.Monkeys.Add(monkey4);
            Monkey.Monkeys.Add(monkey5);
            Monkey.Monkeys.Add(monkey6);
            Monkey.Monkeys.Add(monkey7);
        }

        public override string Level1() {
            GenerateMonkeys();
            for (int i = 0; i < 20; i++) {
                Monkey.PlayRound(x=>x/3);
            }
            var inspections = Monkey.Monkeys.Select(x => x.Inspections).OrderByDescending(x => x).ToList();

            return inspections[0] * inspections[1] + "";
        }

        public override string Level2() {
            GenerateMonkeys();
            var mod = Monkey.Monkeys.Select(x => x.Test).Aggregate((x, y) => x * y);
            for (int i = 0; i < 10000; i++) {
                Monkey.PlayRound(x => x % mod);
            }
            var inspections = Monkey.Monkeys.Select(x => x.Inspections).OrderByDescending(x => x).ToList();

            return inspections[0] * inspections[1] + "";
        }
    }
}
