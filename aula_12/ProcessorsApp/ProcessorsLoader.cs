using DoubleProcessorsLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;


namespace ProcessorsApp
{
    class ProcessorsLoader
    {

        public static List<Processor> ProcessorsFromAssembly(String name)
        {
            List<Processor> list = new List<Processor>();
            Assembly asm = Assembly.LoadFrom(name);
            Type[] types = asm.GetTypes();
            foreach (Type t in types)
            {
                object _this = null;
                foreach (MethodInfo mi in t.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
                {
                    if (IsProcessor(mi))
                    {
                        if (!mi.IsStatic && _this == null)
                        {
                            // método de instancia mas ainda não foi
                            // criada um object do tipo representado por 't'
                            _this = Activator.CreateInstance(t);
                        }
                        Processor p =
                            new ProcessorWithMethod(_this, mi);
                        list.Add(p);
                    }
                }
            }
            return list;
        }

        private static bool IsProcessor(MethodInfo mi)
        {
            ParameterInfo[] pi = mi.GetParameters();
            return mi.ReturnType == typeof(void) &&
                   pi.Length == 1 &&
                   pi[0].ParameterType == typeof(double[]);
        }

        public static List<Processor> ProcessorsWithEmit(string name)
        {
            AssemblyName aName = new AssemblyName("DynamicProcessors");
            AssemblyBuilder ab =
                AppDomain.CurrentDomain.DefineDynamicAssembly(
                    aName,
                    AssemblyBuilderAccess.RunAndSave);

            // For a single-module assembly, the module name is usually
            // the assembly name plus an extension.
            ModuleBuilder mb =
                ab.DefineDynamicModule(aName.Name, aName.Name + ".dll");

            List<Processor> list = new List<Processor>();
            Assembly asm = Assembly.LoadFrom(name);
            Type[] types = asm.GetTypes();
            foreach (Type t in types)
            {
                object _this = null;
                foreach (MethodInfo mi in t.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
                {
                    if (IsProcessor(mi))
                    {
                        Type processorType =
                            CreateDynamicProcessor(mb, _this, mi);
                        Processor p = (Processor)
                            Activator.CreateInstance(processorType);
                        list.Add(p);
                    }
                }
            }
            ab.Save(aName.Name + ".dll");
            return list;
        }

        private static Type CreateDynamicProcessor(ModuleBuilder mb, object _this, MethodInfo mi)
        {
            TypeBuilder tb = mb.DefineType(
                "ProcessorFor" + mi.Name,
            TypeAttributes.Public);

            tb.AddInterfaceImplementation(typeof(Processor));

            MethodBuilder meth = tb.DefineMethod(
                "Process",
                MethodAttributes.Public | MethodAttributes.Virtual,
                typeof(void),
                new Type[] { typeof(double[]) });

            ILGenerator methIL = meth.GetILGenerator();
            methIL.Emit(OpCodes.Ldstr, "Hello Emit!");
            methIL.Emit(OpCodes.Call,
                typeof(System.Console).
                GetMethod("WriteLine", new Type[] { typeof(String) }));
            methIL.Emit(OpCodes.Ret);

            return tb.CreateType();
        }
    }
}
