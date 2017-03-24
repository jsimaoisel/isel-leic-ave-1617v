using System;
using System.Reflection;

namespace AttributeExample
{
    class AttributeExample : System.Attribute
    {

    }

    class Program
    {
        [AttributeExample]
        public int f1;
        [AttributeExample]
        public int P1 { get; set; }
        public int P2 { get; set; }
        [AttributeExample]
        public void M() { }

        static void Main(string[] args)
        {
            Type t = typeof(Program);
            PropertyInfo pi = t.GetProperty("P1");
            Console.WriteLine(pi.GetCustomAttributes(false).Length == 0);

            pi = t.GetProperty("P2");
            Console.WriteLine(pi.GetCustomAttributes(false).Length != 0);

        }
    }
}
