using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleProcessorsLib
{
    public interface Processor
    {
        void Process(double[] values);
    }

    public class Processors
    {
        private List<Processor> processors;

        public Processors()
        {
            processors = new List<Processor>();
        }
        public void Add(Processor p)
        {
            processors.Add(p);
        }
        public void Add(List<Processor> p)
        {
            processors.AddRange(p);
        }
        public void Run(double[] values)
        {
            foreach(Processor p in processors)
            {
                p.Process(values);
            }
        }
    }
}
