using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace events
{
    class Button
    {
        public event Action<String> OnClick;
        /*
         * private Action<String> OnClick;
         * 
         * public void add_OnClick(Action<String> a) {
         *      OnClick += a; //  OnClick = (Action<String>) Delgate.Combine(OnClick, a);
         * }
         * 
         * public void remove_OnClick(Action<String> a)
         *      OnClick -= a;
         * }
         */
        public void m()
        {
            if (OnClick != null)
            {
                OnClick.Invoke("AVE");
            }
        }
    }


    class Program
    {
        int x;



        static void Main(string[] args)
        {
            Button btn = new Button();
            btn.OnClick += x => Console.WriteLine("OnClick");
        }
    }
}
