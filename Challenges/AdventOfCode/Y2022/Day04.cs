namespace Challenges.AdventOfCode.Y2022 {
    internal class Day04 : Day {
        public override string Level1() {
            return InputList.Count(x => {
                var pair1 = x.Split(',')[0].Split('-').Select(x => int.Parse(x)).ToArray();
                var pair2 = x.Split(',')[1].Split('-').Select(x => int.Parse(x)).ToList();
                return pair1[0] <= pair2[0] && pair1[1] >= pair2[1]
                    || pair1[0] >= pair2[0] && pair1[1] <= pair2[1];
            }) + "";
        }

        public override string Level2() {
            return InputList.Count(x => {
                var pair1 = x.Split(',')[0].Split('-').Select(x => int.Parse(x)).ToArray();
                var pair2 = x.Split(',')[1].Split('-').Select(x => int.Parse(x)).ToList();
                return
                    pair2[0] <= pair1[0] && pair1[0] <= pair2[1]
                || pair1[0] <= pair2[0] && pair2[0] <= pair1[1]
                || pair2[0] <= pair1[1] && pair1[1] <= pair2[1]
                || pair1[0] <= pair2[1] && pair2[1] <= pair1[1];
            }) + "";
        }
        public Day04() { }
        public Day04(string input) : base(input) { }
    }
}
