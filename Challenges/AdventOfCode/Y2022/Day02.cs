using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenges.AdventOfCode.Y2022 {
    internal class Day02 : Day {
        enum Type {
            Rock = 1, Paper = 2, Scissors = 3
        }
        enum State {
            Lose = 0, Draw = 3, Win = 6
        }
        public override string Level1() {
            Dictionary<string, Type> mapping = new Dictionary<string, Type>
            {
                {"A", Type.Rock },
                {"B", Type.Paper },
                {"C", Type.Scissors },
                {"X", Type.Rock },
                {"Y", Type.Paper },
                {"Z", Type.Scissors},
            };
            var dict = Input.Split('\n').Select(x => new { Player = x[2], Opponent = x[0] });
            var sum = 0;
            foreach (var game in dict) {
                var player = mapping[game.Player.ToString()];
                var opponent = mapping[game.Opponent.ToString()];
                var result = (int)player - (int)opponent;
                if (result == 0) sum += (int)State.Draw;
                else if (result == 1 || result == -2) sum += (int)State.Win;
                else sum += (int)State.Lose;
                sum += (int)player;
            }
            return sum + "";
        }

        public override string Level2() {
            Dictionary<string, Type> typeMapping = new Dictionary<string, Type>
            {
                {"A", Type.Rock },
                {"B", Type.Paper },
                {"C", Type.Scissors },
            };
            Dictionary<string, State> stateMapping = new Dictionary<string, State>
            {
                {"X", State.Lose },
                {"Y", State.Draw },
                {"Z", State.Win},
            };
            var dict = Input.Split('\n').Select(x => new { State = x[2], Opponent = x[0] });
            var sum = 0;
            foreach (var game in dict) {
                var opponent = typeMapping[game.Opponent.ToString()];
                var state = stateMapping[game.State.ToString()];
                if (state == State.Draw) sum += (int)opponent;
                else if (state == State.Win) sum += ((int)opponent % 3) + 1;
                else {
                    var a = ((int)opponent + 2) % 3;
                    sum += a == 0 ? 3 : a;
                }
                sum += (int)state;
            }
            return sum + "";
        }
    }
}
