using System;
using System.Reflection;

namespace ObjectLogger
{
    public class W
    {
        public int PP1 { get; set; }
        public int method() { return 123; }
    }

    public class T
    {
        public int P1 { get; set; }
        private int P2 { get; set; }
        public static int P3 { get; set; }
        public W P4
        {
            get
            {
                return _w;
            }
            set
            {
                _w = value;
            }
        }
        public String P5 { get; set; }
        W _w;
    }

    class Program
    {
        private static string MakeIdent(int ident)
        {
            string r = "";
            for(int i=0; i<ident; ++i)
            {
                r += '\t';
            }
            return r;
        }
        private static void show(MemberInfo mi, object val, int ident)
        {
            Type tVal = val.GetType();

            if (tVal.IsClass && tVal != typeof(String))
            {
                Console.WriteLine(mi.Name);
                Log(val, ident+1);
            }
            else
            {
                Console.WriteLine("{0}{1} = {2}", MakeIdent(ident), mi.Name, val);
            }
        }
        static void Log(object o, int ident)
        {
            Type t = o.GetType();
            foreach (
                PropertyInfo pi
                in
                t.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                object val = pi.GetValue(o);
                show(pi, val, ident);
            }

            foreach (
                MethodInfo mi in
                t.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                if (mi.ReturnType != typeof(void) &&
                    mi.GetParameters().Length == 0)
                {
                    object val = mi.Invoke(o, null);
                    show(mi, val, ident);
                }
            }

        }

        public static void Main(String[] args)
        {
            T t = new T();
            t.P1 = 1;
            t.P4 = new W();
            t.P5 = "AVE";

            Log(t, 0);
        }
    }
        
}
