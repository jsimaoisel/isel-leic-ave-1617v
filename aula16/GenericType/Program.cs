using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericType
{
    class AGenericType<T, U> //where U : class where T : IComparable<U>
    {
        public T P { get; set; }

        public W M<W>(T t, U u) where W : new()
        {
            /*...*/
            return new W();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Type open = typeof(AGenericType<,>);
            Type closed = typeof(AGenericType<string, int>);
        }
    }
}
