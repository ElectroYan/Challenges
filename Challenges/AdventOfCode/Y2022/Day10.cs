using System.Security.Cryptography;

namespace Challenges.AdventOfCode.Y2022 {
    internal class Day10 : Day {
        public Day10() {
        }

        public Day10(string input) : base(input) {
        }

        class ClockRegister {
            public int Clock { get; private set; } = 1;
            public long Register { get; private set; } = 1;
            public Queue<long> Queue { get; } = new();
            public void AdvanceClock() {
                if (Queue.Count > 0)
                    Register += Queue.Dequeue();
                Clock++;
            }
        }

        public override string Level1() {
            ClockRegister r = new();
            var sum = 0L;
            for (int i = 0; i < 230; i++) {
                if (i < InputList.Count) {
                    var instruction = InputList[i];
                    if (instruction.StartsWith("addx")) {
                        r.Queue.Enqueue(0);
                        r.Queue.Enqueue(int.Parse(instruction.Split(' ')[1]));
                    } else {
                        r.Queue.Enqueue(0);
                    }
                }

                r.AdvanceClock();

                if ((r.Clock - 20) % 40 == 0)
                    sum += r.Clock * r.Register;
            }

            return sum + "";
        }

        public override string Level2() {
            bool[,] screen = new bool[6, 40];
            ClockRegister r = new();
            for (int i = 0; i < 240; i++) {
                if (i < InputList.Count) {
                    var instruction = InputList[i];
                    if (instruction.StartsWith("addx")) {
                        r.Queue.Enqueue(0);
                        r.Queue.Enqueue(int.Parse(instruction.Split(' ')[1]));
                    } else {
                        r.Queue.Enqueue(0);
                    }
                }

                var clock = r.Clock - 1;
                var xPosition = clock % 40;
                screen[(clock / 40), xPosition] = Math.Abs(r.Register - xPosition) <= 1;
                r.AdvanceClock();
            }
            for (int i = 0; i < screen.GetLength(0); i++) {
                for (int j = 0; j < screen.GetLength(1); j++) {
                    Console.Write(screen[i, j] ? "##" : "..");
                }
                Console.WriteLine();
            }

            Console.Write("READ SCREEN: ");
            var result = Console.ReadLine() ?? "";
            return result;
        }
    }
}
