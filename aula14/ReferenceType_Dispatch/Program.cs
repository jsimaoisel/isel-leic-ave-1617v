using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceType_Dispatch
{
    public interface I
    {
        void M();
    }

    class A : I
    {
        public virtual void M() { }
        public virtual void M(int i) { }
        public void MyMethod(int a)
        {

        }
    }

    class B : A
    {
        public override void M()
        {
            base.M();
        }
        public new virtual void M(int i)
        {

        }
        public void X() { }
    }


    class Program
    {
        public static void CallM(I i)
        {
            i.M();
        }
        public static void CallM(A a)
        {
            a.M();
        }
        static void Main(string[] args)
        {
            I i = new B();
            i.M();

            A a = new B();
            a = null;
            a.M();
        }
    }
}
