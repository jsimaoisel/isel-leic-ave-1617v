using System;
using ClassLibrary;

namespace ConsoleApplication
{
    public class Program
    {
        public static void M() {
            Console.WriteLine("1+2={0}", SimpleMath.Add(1,2));
        }
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World! Press ENTER...");
            Console.ReadLine();
            M();
        }
    }
}
