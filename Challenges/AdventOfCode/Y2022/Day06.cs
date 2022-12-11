namespace Challenges.AdventOfCode.Y2022 {
    internal class Day06 : Day {
        public Day06() {
        }

        public Day06(string input) : base(input) {
        }

        public override string Level1() {
            for (int i = 0; i < Input.Length - 4; i++)
                if (Input.Skip(i).Take(4).Distinct().Count() == 4)
                    return (i + 4) + "";
            return "";
        }

        public int LevelX(int take) =>
        Enumerable
            .Range(0, Input.Length - take)
            .First(x => Input.Skip(x).Take(take).Distinct().Count() == take)
            + take;

        public override string Level2() {
            for (int i = 0; i < Input.Length - 14; i++)
                if (Input.Skip(i).Take(14).Distinct().Count() == 14)
                    return (i + 14) + "";
            return "";
        }
    }
}
