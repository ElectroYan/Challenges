using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenges.AdventOfCode.Y2022 {
    public struct Point {
        public int x;
        public int y;
        public static bool operator ==(Point c1, Point c2) {
            return c1.x == c2.x && c1.y == c2.y;
        }
        public static bool operator !=(Point c1, Point c2) {
            return c1.x != c2.x || c1.y != c2.y;
        }
        public static Point operator +(Point c1, Point c2) {
            return new Point(c1.x + c2.x, c1.y + c2.y);
        }
        public static Point operator -(Point c1, Point c2) {
            return new Point(c1.x - c2.x, c1.y - c2.y);
        }
        public Point(int x, int y) {
            this.x = x;
            this.y = y;
        }
        public override string ToString() {
            return $"({x},{y})";
        }
    }
}
