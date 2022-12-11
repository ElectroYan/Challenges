using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenges.AdventOfCode.Y2022 {
    internal class Day03 : Day {
        public Day03() : base() {
        }

        public Day03(string input) : base(input) {
        }

        public override string Level1() {
            return InputList.Sum(line => {
                var half1 = line[..(line.Length / 2 + 1)];
                var half2 = line[(line.Length / 2)..];
                var duplicate = half1.FirstOrDefault(x => half2.Contains(x));
                return char.IsLower(duplicate) ? duplicate - 'a' + 1 : duplicate - 'A' + 27;
            }) + "";
        }

        public override string Level2() {
            List<List<string>> groups = new();
            for (int i = 0; i < InputList.Count / 3; i++)
                groups.Add(InputList.Skip(i * 3).Take((i + 1) * 3).ToList());
            return groups.Sum(group => {
                var duplicate = group[0].First(x => group[1].Contains(x) && group[2].Contains(x));
                return char.IsLower(duplicate) ? duplicate - 'a' + 1 : duplicate - 'A' + 27;
            }) + "";
        }
    }
}
