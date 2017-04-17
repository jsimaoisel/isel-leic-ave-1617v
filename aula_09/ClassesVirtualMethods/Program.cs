using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesVirtualMethods
{
    interface ISomething
    {
        void M();
    }

    class A: ISomething
    {
        public virtual void M()
        {

        }
        public void OtherM()
        {

        }
        public virtual void Operation()
        {

        }
    }

    sealed class B: A
    {
        public override void M() { }

        public override void Operation()
        {
            Console.WriteLine("@B");
        }
    }


    class Program
    {
        
        static void Main(string[] args)
        {
            A a = new B();
            a.OtherM();
            a.Operation();
        }
    }
}
