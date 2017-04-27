using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DelegatesIntro
{
    delegate void Processor(double[] d);

    class Program
    {
        public void P2(double[] v)
        {
            Console.WriteLine("@Program.P2 (instance)");
        }
        public static void P1(double[] d)
        {
            Console.WriteLine("@Program.P1 (static)");
        }
        public static void P3(double[] p)
        {

        }
        public static void Call(Processor p)
        {
            Console.WriteLine("Calling {0} using target = {1}", p.Method, p.Target);
            p(new double[] { 1, 2, 3 });
            // OU
            p.Invoke(new double[] { 1, 2, 3 });
        }

        public static Processor BuildProcessors()
        {
            Processor p = null;
            p += P1;
            // OU
            // p = (Processor)Delegate.Combine(p, new Processor(P1));

            Program prg = new Program();
            p = p + prg.P2;
            
            return p;
        }

        static void Main(string[] args)
        {
            
            Processor p1 = P1;
            // OU
            Processor p2 = new Processor(P1);

            Call(p2);

            Program prg = new Program();
            Processor p3 = prg.P2;
            // OU
            Processor p4 = new Processor(prg.P2);

            Call(p4);

            Processor p = BuildProcessors();
            Console.WriteLine("Calling multicast delegate");
            p(new double[] { 1, 2, 3 });

        }
    }
}
