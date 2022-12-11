using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenges.AdventOfCode.Y2022 {
    internal class Day01 : Day {
        public override string Level1() {
            var input = Input.Replace("\n\n", "-");
            var max = input.Split('-').Select(x => x.Split('\n').Select(y => int.Parse(y)).Sum()).Max();
            return max + "";
        }

        public override string Level2() {
            var input = Input.Replace("\n\n", "-");
            var top3 = input.Split('-').Select(x => x.Split('\n').Select(y => int.Parse(y)).Sum())
                .OrderByDescending(x => x).Take(3).Sum();
            return top3 + "";
        }
    }
}
