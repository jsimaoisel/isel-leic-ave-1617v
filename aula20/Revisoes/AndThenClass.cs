using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revisoes
{
    static class AndThenClass
    {
        static public Func<T, Rafter>
            AndThen<T, Rself, Rafter>(
                    this Func<T, Rself> self,
                    Func<Rself, Rafter> after)
            {
                /*
                 * Func<T, Rafter> f =
                    t => after(self(t));
                return f;
                */

                return t => after(self(t));

                /* <=>
                 * 
                 * Context<T,Rself,Rafter> ctx = 
                    new Context<T, Rself, Rafter>(self, after);

                Func<T, Rafter> f = 
                    new Func<T, Rafter>(ctx.Op);

                return f;*/
            }
        /* -- Context class equivalent to the one created by the C# compiler
         * 
         * class Context<T,Rself,Rafter>
        {
            private Func<T, Rself> self;
            private Func<Rself, Rafter> after;
            public Context(
                Func<T, Rself> self,
                Func<Rself, Rafter> after)
            {
                this.self = self;
                this.after = after;
            }
            public Rafter Op(T t)
            {
                return after(self(t));
            }
        }*/


    }
}
