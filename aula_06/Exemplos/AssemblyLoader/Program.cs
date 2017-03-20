using System;
using System.Reflection;

namespace AssemblyLoader
{
    class Program {
        static void Main(string[] args)
        {
            Assembly asm = Assembly.
                LoadFrom(@"C:\<put your dir here>\Members.exe");
                foreach (Type t in asm.GetTypes())
            {
                Console.WriteLine(t.Name);
                ShowMembers(t);
            }
        }

        private static void ShowMembers(Type t)
        {
            foreach(MemberInfo mi in t.GetMembers())
            {
                Console.WriteLine(mi);
            }
        }
    }
}
