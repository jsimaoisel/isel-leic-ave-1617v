using System;
using System.Reflection;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello System.Reflection!");
            Type t1 = typeof(Program);
            Program p = new Program();
            Type t2 = p.GetType();
            Console.WriteLine("Are the same? {0}", Object.ReferenceEquals(t1, t2));

            AssemblyName name = new AssemblyName(@"C:\work\isel\ensino\AVE\1617v\code\aula_05\lib\bin\Debug\netstandard1.6\lid.dll");
            Assembly asm = Assembly.Load(name);
            foreach(Type t in asm.GetTypes()) {
                foreach(MethodInfo mi in t.GetMethods()) {

                }
            }

        }
    }
}
