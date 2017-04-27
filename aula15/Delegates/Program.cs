using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    class TargetMethods
    {
        public delegate void Action(int x);
        public delegate void OtherAction(int x);

        public static void DoAction(Action a, int i) { a(i); }
        public static void SomeAction(int i) { Console.WriteLine(i); }

        public static void Main(String[] args)
        {
            // 1.
            OtherAction o = SomeAction;
            DoAction(SomeAction, 1024);
            DoAction(o, 10);

            // 2.
            Action a0 = delegate (int x) { Console.Beep(1500, x); };
            Action a1 = SomeAction;
            Action a2 = (Action)Delegate.Combine(a1, a0, a0);
            a2(750);

            a2 -= new Action(SomeAction);
            a2(750);
        }
    }
}
