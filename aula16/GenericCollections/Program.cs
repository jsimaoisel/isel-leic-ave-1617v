using System;
using System.Collections;
using System.Collections.Generic;

namespace GenericCollections
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> arrayList = new List<string>();

            LinkedList<SortedSet<string>> linkedList = new LinkedList<SortedSet<string>>();

            Dictionary<int, SortedSet<string>> ht = new Dictionary<int, SortedSet<string>>();

            foreach(string v in arrayList)
            {
                // print v
            }

            IEnumerable<int> seq = MakeSequence(10);
            foreach (int i in seq)
            {
                Console.WriteLine(i);
            }

            IEnumerator<int> it = MakeSequence(10).GetEnumerator();
            while (it.MoveNext())
            {
                int i = it.Current;
                Console.WriteLine(i);
            }
            
        }

        private static IEnumerable<int> MakeSequence(int v)
        {
            return new NIEnumerable(v);
        }
    }

    class NIEnumerable : IEnumerable<int>
    {
        int limit;
        public NIEnumerable(int limit)
        {
            this.limit = limit;
        }
        public IEnumerator<int> GetEnumerator()
        {
            return new NIEnumerator(limit);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new NIEnumerator(limit);
        }
    }

    class NIEnumerator : IEnumerator<int>
    {
        int limit;
        int current = -1;
        public NIEnumerator(int limit)
        {
            this.limit = limit;
        }
        public int Current
        {
            get
            {
                return current;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return current;
            }
        }

        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            current++;
            return current <= limit;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
