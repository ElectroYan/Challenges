namespace Challenges.AdventOfCode.Y2022 {
    internal class Day09 : Day {
        public Day09() {
        }

        public Day09(string input) : base(input) {
        }

        class Rope {
            public enum Direction {
                U, D, L, R
            }
            Dictionary<Direction, Point> directions = new Dictionary<Direction, Point> {
                {Direction.U, new Point(0,1) },
                {Direction.D, new Point(0,-1) },
                {Direction.L, new Point(-1,0) },
                {Direction.R, new Point(1,0) },
            };



            //public Point Head = new Point(0, 0);
            //public Point Tail = new Point(0, 0);

            public HashSet<Point> Positions = new HashSet<Point>();

            public List<Point> Knots = new List<Point>();
            public Rope(int knots) {
                Positions.Add(new Point(0, 0));
                for (int i = 0; i < knots; i++) {
                    Knots.Add(new Point(0, 0));
                }
            }

            public void Move(Direction direction) {
                Knots[0] = Knots[0] + directions[direction];

                for (int i = 1; i < Knots.Count; i++) {
                    var diff = Knots[i - 1] - Knots[i];
                    if (Math.Abs(diff.x) > 1 && Math.Abs(diff.y) > 1) {
                        Knots[i] = Knots[i] + new Point(Math.Sign(diff.x) * (Math.Abs(diff.x) - 1), Math.Sign(diff.y) * (Math.Abs(diff.y) - 1));
                    } else if (Math.Abs(diff.x) > 1) {
                        Knots[i] = Knots[i] + new Point(Math.Sign(diff.x) * (Math.Abs(diff.x) - 1), diff.y);
                    } else if (Math.Abs(diff.y) > 1) {
                        Knots[i] = Knots[i] + new Point(diff.x, Math.Sign(diff.y) * (Math.Abs(diff.y) - 1));
                    }

                }
                Positions.Add(Knots.Last());
            }
        }


        public override string Level1() {
            return LevelN(2) + "";
        }
        public override string Level2() {
            return LevelN(10) + "";
        }

        public int LevelN(int knots) {
            Rope r = new Rope(knots);
            foreach (var item in InputList) {
                var direction = (Rope.Direction)Enum.Parse(typeof(Rope.Direction), item[0] + "");
                var count = int.Parse(item.Split(' ')[1]);
                for (int i = 0; i < count; i++) {
                    r.Move(direction);
                }
            }
            return r.Positions.Count;
        }

    }
}
