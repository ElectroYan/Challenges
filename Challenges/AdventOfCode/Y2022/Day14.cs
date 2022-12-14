namespace Challenges.AdventOfCode.Y2022
{
    internal class Day14 : Day
    {
        public Day14()
        {
        }

        public Day14(string input) : base(input)
        {
        }

        enum Type {
            Free,
            Rock,
            Sand
        }
        class Particle {
            public Particle(Point position, Type type) {
                Position = position;
                Type = type;
            }

            public Point Position { get; set; }
            public Type Type { get; set; }
        }
        class Field {
            public int SandsDropped = 0;
            public Point Spawn { get; set; }
            public Particle[,] FieldA;
            private int minX;
            private int minY;
            private int width;
            private int height;
            public bool ReachedAbyss = false;
            public bool ReachedSpawn = false;
            public Field(List<string> input, Point spawn) {
                Spawn = spawn;
                var cords = input.SelectMany(x => x.Replace(" -> ", ":").Split(':')).ToList();
                width = cords.Select(x => int.Parse(x.Split(',')[0])).Max() + 200;
                minX = cords.Select(x => int.Parse(x.Split(',')[0])).Min() - 1;
                height = cords.Select(x => int.Parse(x.Split(',')[1])).Max() + 3;
                minY = cords.Select(x => int.Parse(x.Split(',')[1])).Min() - 1;
                FieldA = new Particle[width, height];
                for (int i = 0; i < width; i++) {
                    for (int j = 0; j < height; j++) {
                        FieldA[i, j] = new Particle(new Point(i, j), Type.Free);
                    }
                }

                for (int i = 0; i < width; i++) {
                    FieldA[i, height - 1].Type = Type.Rock;
                }

                foreach (var item in input) {
                    var parts = item.Replace(" -> ", ":").Split(':');
                    var prev = parts[0];
                    foreach (var c in parts.Skip(1)) {
                        var prevX = int.Parse(prev.Split(',')[0]);
                        var prevY = int.Parse(prev.Split(',')[1]);
                        var currX = int.Parse(c.Split(',')[0]);
                        var currY = int.Parse(c.Split(',')[1]);
                        if (prevX == currX)
                            for (int i = Math.Min(prevY, currY); i <= Math.Max(prevY, currY); i++)
                                FieldA[prevX, i].Type = Type.Rock;
                        if (prevY == currY)
                            for (int i = Math.Min(prevX, currX); i <= Math.Max(prevX, currX); i++)
                                FieldA[i, prevY].Type = Type.Rock;
                        prev = c;
                    }
                }
            }

            public void Print() {
                for (int i = 0; i < height; i++) {
                    for (int j = minX; j < width; j++) {
                        if (FieldA[j, i].Position == Spawn)
                            Console.Write("X");
                        else
                            Console.Write(FieldA[j, i].Type == Type.Free ? "." : (FieldA[j, i].Type == Type.Rock ? "#" : "O"));
                    }
                    Console.WriteLine();
                }
            }

            public void DropSand() {
                SandsDropped++;
                var sand = new Particle(Spawn, Type.Sand);
                bool falling = true;
                while (falling) {
                    if (sand.Position.y + 3 >= height)
                        ReachedAbyss = true;
                    if (FieldA[sand.Position.x, sand.Position.y + 1].Type == Type.Free)
                        sand.Position += new Point(0, 1);
                    else if (FieldA[sand.Position.x - 1, sand.Position.y + 1].Type == Type.Free)
                        sand.Position += new Point(-1, 1);
                    else if (FieldA[sand.Position.x + 1, sand.Position.y + 1].Type == Type.Free)
                        sand.Position += new Point(1, 1);
                    else {
                        FieldA[sand.Position.x, sand.Position.y] = sand;
                        falling = false;
                    }
                }
                if (sand.Position == Spawn)
                    ReachedSpawn = true;
            }

        }


        public override string Level1() {
            Field f = new Field(InputList, new Point(500, 0));
            while (!f.ReachedAbyss) {
                f.DropSand();
            }
            f.Print();
            Console.WriteLine(f.SandsDropped - 1);

            return f.SandsDropped - 1 + "";
        }
        public override string Level2() {
            Field f = new Field(InputList, new Point(500, 0));
            while (!f.ReachedSpawn) {
                f.DropSand();
            }
            f.Print();
            Console.WriteLine(f.SandsDropped);

            return f.SandsDropped + "";
        }
    }
}
