using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Members
{
    class Program
    {
        public int c;           // FieldInfo

        public int P            // ProperyInfo
        {
            get; set;
        }

        public void M() { }     // MethodInfo

        public class C          // Type
        {

        }

        public interface I      // Type
        {

        }

        public struct S         // definição de um novo tipo valor (Type)
        {

        }

        static void Main(string[] args)
        {
            PropertyInfo pi = typeof(Program).GetProperty("P");
            Program a = new Program();
            pi.SetValue(a, 10);

            Console.WriteLine(a.P);
        }
    }
}
