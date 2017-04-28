using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDelegate
{
    class GenericType<T>
    {
        public T P { get; set; }
    }

    public delegate void Action<T>(T x);

    class Program
    {
        static void M(int a) { }
        static void M(String s) { }

        static void Main(string[] args)
        {
            Action<int> a_int = M;
            Action<String> a_str = M;
            Action<Program> p;
        }
    }
}
