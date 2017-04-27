using System;

namespace ValueTypes_review
{
    struct V
    {
        int i, j;
        public void Inc()
        {
            ++i;
            ++j;
        }
        public override string ToString()
        {
            return "i=" + i + " j=" + j;
        }
    }

    class Program
    {
        public static void Main()
        {
            V v = new V();
            object o = v;

            ((V)o).Inc();

            v.Inc();

            Console.WriteLine(v.ToString());

            v.ToString();
        }
    }
}
