using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    public delegate void Action(int x);
    public delegate void OtherAction(int x);

    class TargetMethods
    {
        public static void DoAction(Action a, int i) { a(i); }
        public static void SomeAction(int i) { Console.WriteLine(i); }

        public static void Main(String[] args)
        {
            // 1.
            OtherAction o = new OtherAction(SomeAction);
            DoAction(new Action(SomeAction), 1024);
            DoAction(new Action(SomeAction), 10);

            // 2.
            Action a0_1 = new Action(Beep);
            // OU
            Action a0_2 = Beep;
            // OU
            Action a0_3 = delegate (int x) { Console.Beep(1500, x); };
            // OU
            int a = 10;
            Action a0_4 = (x) => { Console.Beep(1500, x + a); };

            Console.WriteLine("==== First time ==== ");
            Action a1 = SomeAction;
            Action a2 = (Action)Delegate.Combine(a1, a0_1, a0_1);
            a2(750);

            Console.WriteLine("==== Second time ==== ");
            a2 -= new Action(SomeAction);
            a2(750);
        }
        static void Beep(int duration)
        {
            Console.Beep(1500, duration);
        }
    }
}
