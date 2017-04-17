using System;
using System.Reflection;

namespace ByRef
{
    class A
    {
        public int P { get; set; }
    }

    class Program
    {
        public static void M(ref int a)
        {
            a = 30;
        }
        public static int T(out int a)
        {
            a = 30;
            return 10;
        }
        public static void Z(ref A s)
        {
            s = new A();
        }

        static void Main(string[] args)
        {
            int b = 20;
            M(ref b);

            int c;
            b = T(out c);
            Console.WriteLine(c);

            A a = new A();
            Z(ref a);

        }

    }

}
