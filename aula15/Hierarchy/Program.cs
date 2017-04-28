using System;

using System.Threading.Tasks;

namespace Hierarchy
{
    interface I
    {
        void M1();
    }
    class A : I
    {
        public virtual void M1() { Console.WriteLine("A::M1"); }
        public virtual void M2() { Console.WriteLine("A::M2"); }
    }
    class B : A
    {
        public override void M1() { Console.WriteLine("B::M1"); }
        public override void M2() { Console.WriteLine("B::M2"); }
    }
    class C : B
    {
        public new void M1() { Console.WriteLine("C::M1"); }
        public override void M2() { Console.WriteLine("C::M2"); }
    }

    class Program
    {
        static void Main(string[] args)
        {
            I i = new C();
            i.M1();

            A a = ((A)i);
            a.M2();
            a.M1();

            C c = ((C)a);
            c.M1();
        }
    }
}
