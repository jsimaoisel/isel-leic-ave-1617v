using System;
using System.Diagnostics;
using System.Reflection;

namespace ReflectionPerformance
{

    public class A
    {
        public int fieldC;

        public void M() { }
    } 

    public class Tester
    {
        private const double CALLS = 10000.0;
        public static void CallMethodByReflection(MethodInfo mi, object _this)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i=0; i<CALLS; ++i)
            {
                mi.Invoke(_this, null);
            }
            sw.Stop();
            Console.WriteLine("[Reflection] Calling {0} took {1} ticks", mi.Name, sw.ElapsedTicks / CALLS);
        }

        public static void CallMethodRegular(A a)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < CALLS; ++i)
            {
                a.M();
            }
            sw.Stop();
            Console.WriteLine("[Regular] Calling A::M() took {0} ticks", sw.ElapsedTicks / CALLS);
        }

        public static void ReadFieldByReflection(FieldInfo fi, object _this)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < CALLS; ++i)
            {
                int c = (int) fi.GetValue(_this);
            }
            sw.Stop();
            Console.WriteLine("[Reflection] Reading {0} took {1} ticks", fi.Name, sw.ElapsedTicks / CALLS);
        }

        public static void ReadFieldRegular(A a)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < CALLS; ++i)
            {
                int c = a.fieldC;
            }
            sw.Stop();
            Console.WriteLine("[Regular] Reading fieldC took {0} ticks", sw.ElapsedTicks / CALLS);
        }

        static void Main(string[] args)
        {
            Type ta = typeof(A);
            A a = new A();
            MethodInfo mi = ta.GetMethod("M");
            FieldInfo fi = ta.GetField("fieldC");

            CallMethodByReflection(mi, a);
            CallMethodRegular(a);

            ReadFieldByReflection(fi, a);
            ReadFieldRegular(a);

        }
    }
}
