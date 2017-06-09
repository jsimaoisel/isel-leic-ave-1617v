using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Revisoes
{
    class Program
    {
        static Func<T, T> ChainMethods<T>(string path)
        {
            Func<T, T> chain = null;
            Type retType = typeof(T);
            Assembly source = Assembly.LoadFrom(path);
            foreach (Type t in source.GetTypes())
            {
                foreach(MethodInfo mi in t.GetMethods())
                {
                    ParameterInfo[] paramsInfo = mi.GetParameters();
                    if (mi.IsStatic && /* TODO */
                        mi.ReturnType == retType &&
                        paramsInfo.Length == 1 &&
                        paramsInfo[0].ParameterType == retType)
                    {
                        /*Func<T, T> f =
                            a => (T)mi.Invoke(
                                null,
                                new object[] { a });
                        */
                        Func<T, T> f = 
                            (Func<T,T>) Delegate.CreateDelegate(typeof(Func<T, T>), mi);
                        Func<T, T> validator = BuildValidator<T>(mi);
                        if (chain == null)
                        {
                            chain = validator.AndThen(f);
                        } else
                        {
                            chain = chain.AndThen(validator).AndThen(f);
                        }
                    }
                }
            }
            return chain;
        }

        static Func<T,T> BuildValidator<T>(MethodInfo mi)
        {
            Attribute[] attrs = 
                Attribute.GetCustomAttributes(
                    mi, 
                    typeof(Validator));

            return t =>
            {
                /* TODO: 
                 * 1. check if validators exist
                 * 2. call all validators, not just the first
                 */
                Type val = ((Validator)attrs[0]).ValidatorType;
                object obj = Activator.CreateInstance(val);
                if (!((IValidator<T>)obj).validate(t))
                {
                    throw new Exception();
                }
                return t;
            };
        }

        interface IValidator<T> { bool validate(T arg); }

        class Validator : Attribute
        {
            public Type ValidatorType;
            public Validator(Type t) { ValidatorType = t; }
        }

        static void Main(string[] args)
        {
        }
    }
}
