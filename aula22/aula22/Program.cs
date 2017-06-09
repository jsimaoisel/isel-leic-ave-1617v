using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace aula22
{
    class X {
        int[] a1 = new int[512];
        int[] a2  = new int[512];
        int[] a3 = new int[512];
    }
    class Program
    {
        static List<X> list = new List<X>();
        static void Main(string[] args)
        {
            int counter = 0;
            for (;;)
            {
                X p = new X();
                Thread.Sleep(10);
                if (++counter % 5 == 0)
                {
                    list.Add(p);
                }
            }
        }
    }
}
