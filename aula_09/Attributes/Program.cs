using System;
using System.Reflection;

namespace Attributes
{
    [AttributeUsage(AttributeTargets.Method|AttributeTargets.Class, AllowMultiple =true)]
    public class TestedByAttribute : Attribute
    {
        public String Date;
        public String Name { get; set; }
        public TestedByAttribute(String s)
        {
            Name = s;
        }
    }

    [TestedBy("Manel")]
    [TestedBy("Maria", Date = "31-03-2017")]
    class Program
    {
        [TestedBy("Maria")]
        public void M()
        {

        }
        static void Main(string[] args)
        {
            Type t = typeof(Program);
            MethodInfo mi = t.GetMethod("M");
            if (Attribute.IsDefined(mi, typeof(TestedByAttribute))) {

            }

            TestedByAttribute attr = (TestedByAttribute) Attribute.GetCustomAttribute(mi, typeof(TestedByAttribute));
            Console.WriteLine(attr.Name);
            attr.Name = "xpto";
            Console.WriteLine(attr.Name);

        }
    }
}
