using System;
using System.Reflection;

namespace ConsoleApplication
{
    public struct S {
        public int a;
        public void m() {}
    }
    public class Program
    {
        public static void InspectHierarchy(Object obj) {
            Type t = obj.GetType();
            Type ot = typeof(System.Object);
            Console.WriteLine(t);
            while (!Object.ReferenceEquals(t,ot)) {
                //t = t.Base;
                t = t.GetTypeInfo().BaseType;
                Console.WriteLine(t);
            }
        }

        public static void Main(string[] args)
        {
            S s = new S();
            s.a = 10;
            Console.WriteLine("Hello World!");

            Program p = new Program();
            Type t = p.GetType();
            MethodInfo[] ti = t.GetMethods();
            foreach (MethodInfo mi in ti) {
                Console.WriteLine("Name = {0}", mi.Name);
            }
            InspectHierarchy(p);
        }
    }
}
