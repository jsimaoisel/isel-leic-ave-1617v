using System;
using System.Reflection;
using DoubleProcessorsLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Diagnostics;

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
        object target;
        public ProcessorWithMethod(
            object target,
            MethodInfo mi)
        {
            this.mi = mi;
            this.target = target;
        }
        public void Process(double[] values)
        {
            mi.Invoke(
                target, 
                new object[] { values });
        }
    }

    class Program
    {
        public readonly static int RUNS = 1000;
        public readonly static int WARM_UP = 10;
        static void Main(string[] args)
        {
            Processors procEmit =
                new Processors();
            procEmit.Add(ProcessorsLoader.ProcessorsWithEmit(
                "ExternalProcessors.dll"));

            Processors procReflection =
                new Processors();
            procReflection.Add(ProcessorsLoader.ProcessorsWithReflection(
                "ExternalProcessors.dll"));

            double[] values = { 0.0, 0.0, 0.0 };
            Stopwatch sw = new Stopwatch();

            // warmup
            for (int i = 0; i < WARM_UP; ++i)
            {
                procReflection.Run(values);
            }
            for (int i=0; i< WARM_UP; ++i)
            {
                procEmit.Run(values);
            }

            // benchmark
            sw.Start();
            for (int i = 0; i < RUNS; ++i)
            {
                procReflection.Run(values);
            }
            sw.Stop();
            long reflection = sw.Elapsed.Ticks;

            sw.Restart();
            for (int i = 0; i < RUNS; ++i)
            {
                procEmit.Run(values);
            }
            sw.Stop();
            long emit = sw.Elapsed.Ticks;

            Console.WriteLine("Emit {0} ticks", emit / (double)RUNS);
            Console.WriteLine("Reflection {0} ticks", reflection / (double)RUNS);
        }
    }
}
