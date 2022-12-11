namespace Challenges.AdventOfCode.Y2022 {
    internal class Day08 : Day {
        public Day08() {
        }

        public Day08(string input) : base(input) {
        }

        public enum Visible {
            Visible,
            Invisible,
            Unchecked
        }
        public enum Direction {
            Up,
            Down,
            Left,
            Right
        }

        int[,] GenField() {
            var field = new int[InputList.Count, InputList[0].Length];
            for (int row = 0; row < InputList.Count; row++)
                for (int col = 0; col < InputList[0].Length; col++)
                    field[row, col] = InputList[row][col] - '0';
            return field;
        }

        Visible[,] GenFieldVisible() {
            var field = new Visible[InputList.Count, InputList[0].Length];
            for (int row = 0; row < InputList.Count; row++)
                for (int col = 0; col < InputList[0].Length; col++)
                    if (row == 0 || row == InputList.Count - 1 || col == 0 || col == InputList[0].Length - 1)
                        field[row, col] = Visible.Visible;
                    else
                        field[row, col] = Visible.Unchecked;

            return field;
        }
        long[,] GenFieldScenic {
            get {
                var field = new long[InputList.Count, InputList[0].Length];
                for (int row = 0; row < InputList.Count; row++)
                    for (int col = 0; col < InputList[0].Length; col++)
                        if (row == 0 || row == InputList.Count - 1 || col == 0 || col == InputList[0].Length - 1)
                            field[row, col] = 0;
                        else
                            field[row, col] = 0;

                return field;
            }
        }

        static long CountField<T>(T[,] field, Func<T, bool> func) {
            long cnt = 0;
            for (int row = 0; row < field.GetLength(0); row++) {
                for (int col = 0; col < field.GetLength(1); col++) {
                    if (func.Invoke(field[row, col])) cnt++;
                }
            }
            return cnt;
        }

        private static long MaxField<T>(T[,] field, Func<T, long> func) {
            long cnt = 0;
            for (int row = 0; row < field.GetLength(0); row++) {
                for (int col = 0; col < field.GetLength(1); col++) {
                    cnt = Math.Max(func.Invoke(field[row, col]), cnt);
                }
            }
            return cnt;
        }

        int[,] field = new int[0, 0];
        Visible[,] fieldVisible = new Visible[0, 0];
        public override string Level1() {
            field = GenField();
            fieldVisible = GenFieldVisible();
            var count = field.GetLength(0) * 2 + (field.GetLength(1) - 2) * 2;
            for (int row = 1; row < field.GetLength(0) - 1; row++) {
                for (int col = 1; col < field.GetLength(1) - 1; col++) {
                    if (fieldVisible[row, col] == Visible.Unchecked) {
                        CheckCell(row, col, Direction.Up);
                        CheckCell(row, col, Direction.Left);
                        CheckCell(row, col, Direction.Down);
                        CheckCell(row, col, Direction.Right);
                    }
                }
            }

            return CountField(fieldVisible, x => x == Visible.Visible) + "";
        }


        public void CheckCell(int row, int col, Direction dir) {
            if (fieldVisible[row, col] == Visible.Invisible || fieldVisible[row, col] == Visible.Unchecked) {
                if (dir == Direction.Up) {
                    for (int i = row - 1; i >= 0; i--) {
                        if (field[row, col] <= field[i, col]) {
                            fieldVisible[row, col] = Visible.Invisible;
                            return;
                        }

                    }
                    fieldVisible[row, col] = Visible.Visible;
                }
                if (dir == Direction.Left) {
                    for (int i = col - 1; i >= 0; i--) {
                        if (field[row, col] <= field[row, i]) {
                            fieldVisible[row, col] = Visible.Invisible;
                            return;
                        }

                    }
                    fieldVisible[row, col] = Visible.Visible;
                }
                if (dir == Direction.Down) {
                    for (int i = row + 1; i < field.GetLength(0); i++) {
                        if (field[row, col] <= field[i, col]) {
                            fieldVisible[row, col] = Visible.Invisible;
                            return;
                        }
                    }
                    fieldVisible[row, col] = Visible.Visible;
                }
                if (dir == Direction.Right) {
                    for (int i = col + 1; i < field.GetLength(1); i++) {
                        if (field[row, col] <= field[row, i]) {
                            fieldVisible[row, col] = Visible.Invisible;
                            return;
                        }

                    }
                    fieldVisible[row, col] = Visible.Visible;
                }


            }
        }

        public long CheckCell2(int row, int col, Direction dir) {
            var cnt = 0;
            if (dir == Direction.Up) {
                for (int i = row - 1; i >= 0; i--) {
                    if (field[row, col] <= field[i, col]) {
                        fieldVisible[row, col] = Visible.Invisible;
                        return cnt + 1;
                    } else {
                        cnt++;
                    }

                }
                return cnt;
            }
            if (dir == Direction.Left) {
                for (int i = col - 1; i >= 0; i--) {
                    if (field[row, col] <= field[row, i]) {
                        fieldVisible[row, col] = Visible.Invisible;
                        return cnt + 1;
                    } else {
                        cnt++;
                    }
                }
                return cnt;
            }
            if (dir == Direction.Down) {
                for (int i = row + 1; i < field.GetLength(0); i++) {
                    if (field[row, col] <= field[i, col]) {
                        fieldVisible[row, col] = Visible.Invisible;
                        return cnt + 1;
                    } else {
                        cnt++;
                    }
                }
                return cnt;
            }
            if (dir == Direction.Right) {
                for (int i = col + 1; i < field.GetLength(1); i++) {
                    if (field[row, col] <= field[row, i]) {
                        fieldVisible[row, col] = Visible.Invisible;
                        return cnt + 1;
                    } else {
                        cnt++;
                    }
                }
                return cnt;
            }
            return 0;
        }

        long[,] fieldScenic = new long[0, 0];
        public override string Level2() {
            field = GenField();
            fieldScenic = GenFieldScenic;
            for (int row = 1; row < field.GetLength(0) - 1; row++) {
                for (int col = 1; col < field.GetLength(1) - 1; col++) {
                    var val1 = CheckCell2(row, col, Direction.Up);
                    var val2 = CheckCell2(row, col, Direction.Left);
                    var val3 = CheckCell2(row, col, Direction.Down);
                    var val4 = CheckCell2(row, col, Direction.Right);
                    fieldScenic[col, row] = val1 * val2 * val3 * val4;
                }
            }

            return MaxField(fieldScenic, x => x) + "";
        }
    }
}
