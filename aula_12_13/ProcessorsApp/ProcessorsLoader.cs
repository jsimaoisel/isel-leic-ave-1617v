using DoubleProcessorsLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;


namespace ProcessorsApp
{
    class ProcessorsLoader
    {

        public static List<Processor> ProcessorsWithReflection(String name)
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
                object target = null;
                foreach (MethodInfo mi in t.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
                {
                    if (IsProcessor(mi))
                    {
                        if (!mi.IsStatic && target == null)
                        {
                            // método de instancia mas ainda não foi
                            // criada um object do tipo representado por 't'
                            target = Activator.CreateInstance(t);
                        }
                        Type processorType =
                            CreateDynamicProcessor(mb, mi);
                        Processor p = (Processor)
                            Activator.CreateInstance(processorType, target);
                        list.Add(p);
                    }
                }
            }
            ab.Save(aName.Name + ".dll");
            return list;
        }

        private static Type CreateDynamicProcessor(ModuleBuilder mb, MethodInfo mi)
        {
            TypeBuilder tb = mb.DefineType(
                "ProcessorFor" + mi.Name,
            TypeAttributes.Public);

            tb.AddInterfaceImplementation(typeof(Processor));

            FieldBuilder fb = tb.DefineField(
                "target",
                mi.DeclaringType,
                FieldAttributes.Private);

            ConstructorBuilder cb = tb.DefineConstructor(
                MethodAttributes.Public,
                CallingConventions.ExplicitThis,
                new Type[] { mi.DeclaringType });

            ILGenerator ctor1IL = cb.GetILGenerator();
            // call object ctor
            ctor1IL.Emit(OpCodes.Ldarg_0);
            ctor1IL.Emit(OpCodes.Call,
                typeof(object).GetConstructor(Type.EmptyTypes));
            // load this
            ctor1IL.Emit(OpCodes.Ldarg_0);
            // load ctor parameter
            ctor1IL.Emit(OpCodes.Ldarg_1);
            // store field
            ctor1IL.Emit(OpCodes.Stfld, fb);
            ctor1IL.Emit(OpCodes.Ret);

            MethodBuilder meth = tb.DefineMethod(
                "Process",
                MethodAttributes.Public | MethodAttributes.Virtual,
                typeof(void),
                new Type[] { typeof(double[]) });

            ILGenerator methIL = meth.GetILGenerator();
            if (!mi.IsStatic)
            {
                // load "target" to eval stack
                // load this
                methIL.Emit(OpCodes.Ldarg_0);
                // load field "target"
                methIL.Emit(OpCodes.Ldfld, fb);
            }
            // load double[] to eval stack
            methIL.Emit(OpCodes.Ldarg_1);
            // call method
            methIL.Emit(OpCodes.Call, mi);
            methIL.Emit(OpCodes.Ret);   
            return tb.CreateType();
        }
    }
}
