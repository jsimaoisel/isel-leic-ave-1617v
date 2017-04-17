using System;
using System.Reflection;
using DoubleProcessorsLib;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ProcessorsApp
{
    class SumN : Processor
    {
        private double n;
        public SumN(double d) { n = d; }
        public void Process(double[] values)
        {
            for(int i=0; i<values.Length; ++i)
            {
                values[i] += n;
            }
        }
    }

    class ProcessorWithMethod : Processor
    {
        MethodInfo mi;
        object _this;
        public ProcessorWithMethod(
            object _this,
            MethodInfo mi)
        {
            this.mi = mi;
            this._this = _this;
        }
        public void Process(double[] values)
        {
            mi.Invoke(
                _this, 
                new object[] { values });
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Processors processors =
                new Processors();
            processors.Add(new SumN(1));
            processors.Add(ProcessorsLoader.ProcessorsWithReflection(
                @"c:\assemblies\ExternalProcessors.dll"));
            processors.Add(ProcessorsLoader.ProcessorsWithEmit(
                @"c:\assemblies\ExternalProcessors.dll"));

            double[] values = { 0.0, 0.0, 0.0 };
            processors.Run(values);

            foreach(double d in values)
            {
                Console.Write("{0} ", d);
            }
        }



        
    }
}
