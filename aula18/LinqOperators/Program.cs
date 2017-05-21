using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentAPI
{
    struct Point
    {
        public int x, y;
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public override string ToString()
        {
            return "x=" + x + " y=" + y;
        }
    }

    static class PointExtensionMethods
    {
        public static List<Point> Convert(this List<String> origin)
        {
            List<Point> r = new List<Point>();
            foreach (String s in origin)
            {
                String[] parts = s.Split(';');
                r.Add(
                    new Point(
                        Int32.Parse(parts[0]),
                        Int32.Parse(parts[1])
                    )
                );
            }
            return r;
        }

        public static List<Point> FilterPoints(
            this List<Point> origin,
            Func<Point, bool> criteria)
        {
            List<Point> r = new List<Point>();
            foreach (Point p in origin)
            {
                if (criteria(p))
                {
                    r.Add(p);
                }
            }
            return r;
        }

        public static Point FirstPoint(this List<Point> origin)
        {
            return origin[0];
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<String> points = new List<string>();
            points.Add("1;2");
            points.Add("13;25");
            points.Add("12;15");
            points.Add("10;12");

            // processing operations
            List<Point> realPoints = Convert(points);
            List<Point> filteredPoints = FilterPoints(
                realPoints,
                p => p.y % 2 == 0
            );
            Point first = First(filteredPoints);

            // same operations but with extensions methods, using a fluent API
            Point point = points
                .Convert()
                .FilterPoints(p => p.y % 2 == 0)
                .FirstPoint();

            foreach(Point p in points.Convert())
            {
                Console.WriteLine(p);
            }
        }


        static Point First(List<Point> points)
        {
            return points[0];
        }


        static List<Point> Convert(List<String> points)
        {
            List<Point> r = new List<Point>();
            foreach (String s in points)
            {
                String[] parts = s.Split(';');
                r.Add(
                    new Point(
                        Int32.Parse(parts[0]),
                        Int32.Parse(parts[1])
                    )
                );
            }
            return r;
        }

        static List<Point> FilterPoints(
            List<Point> points,
            Func<Point, bool> criteria)
        {

            List<Point> r = new List<Point>();
            foreach (Point p in points)
            {
                if (criteria(p))
                {
                    r.Add(p);
                }
            }
            return r;
        }
    }
}
