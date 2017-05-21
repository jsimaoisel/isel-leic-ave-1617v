using System;
using System.Collections;
using System.Collections.Generic;

/*
 * 
 *** static IEnumerable<T> Concat<T>(IEnumerable<T> seq1, IEnumerable<T> seq2)
   
   que retorna a sequência que representa a concatenação das sequências seq1 e seq2. 
 * 
 *** IEnumerable<R> Zip<TA, TB, R>(IEnumerable<TA> a, IEnumerable<TB> b, Func<TA, TB, R> join), 

    que retorna uma sequência de elementos resultantes da
    aplicação da função join aos elementos de a e b. 
    A sequência resultante termina
    assim que uma das sequências não tenha mais elementos.
 *
*/


namespace GenericCollections
{
    class Program
    {
        static void Main(string[] args)
        {
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

            IEnumerable<int> s123 = Sequence123();
            foreach(int i in s123)
            {
                Console.WriteLine(i);
            }

            List<String> words = new List<string>();
            words.Add("A"); words.Add("B"); words.Add("C");
            IEnumerable<int> numbers = InfiniteSequence();

            IEnumerable<String> result =
                Zip(words, numbers, 
                   (a, b) => b + ": " + a
                /*
                <=>

                (a, b) =>
                {
                    return b + ": " + a;
                }
                */);
            foreach(String s in result)
            {
                Console.WriteLine(s);
            }
        }

        static IEnumerable<int> InfiniteSequence()
        {
            for(int i=0;;++i)
            {
                yield return i; 
            }
        }

        static IEnumerable<R> Zip<TA, TB, R>(
            IEnumerable<TA> a, 
            IEnumerable<TB> b, 
            Func<TA, TB, R> join)
        {
            IEnumerator<TA> it1 = a.GetEnumerator();
            IEnumerator<TB> it2 = b.GetEnumerator();
            while(it1.MoveNext() && it2.MoveNext())
            {
                yield return join(it1.Current, it2.Current);
            }
        }


        static IEnumerable<T> Concat<T>(IEnumerable<T> seq1, IEnumerable<T> seq2)
        {
            foreach(T t in seq1)
            {
                yield return t;
            }
            foreach (T t in seq2)
            {
                yield return t;
            }
        }

        private static IEnumerable<int> Sequence123()
        {
            yield return 0;
            yield return 1;
            yield return 2;
        }

        private static IEnumerable<int> MakeAutoSequence(int limit)
        {
            for (int i=0; i<limit; ++i)
            {
                yield return i;
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
