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

        public static IEnumerable<Point> ConvertLazzy(
            this IEnumerable<String> origin)
        {
            foreach (String s in origin)
            {
                String[] parts = s.Split(';');
                yield return
                    new Point(
                        Int32.Parse(parts[0]),
                        Int32.Parse(parts[1])
                    );
            }
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


        public static IEnumerable<T> FilterLazzy<T>(
            this IEnumerable<T> origin,
            Func<T, bool> criteria)
        {
            foreach (T p in origin)
            {
                if (criteria(p))
                {
                    yield return p;
                }
            }
        }

        public static Point FirstPoint(this List<Point> origin)
        {
            return origin[0];
        }

        public static Point FirstPoint(
            this IEnumerable<Point> origin)
        {
            IEnumerator<Point> it = origin.GetEnumerator();
            it.MoveNext();
            return it.Current;
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
            Point point = 
               points
                .Convert()
                .FilterPoints(p => p.y % 2 == 0)
                .FilterPoints(p => p.x % 2 != 0)
                .FirstPoint();

            points
                .ConvertLazzy()
                .FilterLazzy(p => p.y % 2 == 0)
                .FirstPoint();

            IEnumerable<Point> seqPoints =
                points
                    .Select(s => MakePoint(s))
                    .Where(p => p.x % 2 == 0)
                    .Take(10);

            IEnumerable<Point> seq =
                from s in points
                select MakePoint(s);

            IEnumerable<Point> finalSeq =
                from p in seq
                where p.x % 2 == 0
                select p;            

        }

        static public Point MakePoint(String s)
        {
            String[] parts = s.Split(';');
            int x = Int32.Parse(parts[0]);
            int y = Int32.Parse(parts[1]);
            return new Point(x, y);
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
