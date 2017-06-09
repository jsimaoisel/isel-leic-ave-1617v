using System;

namespace _10_WeakReferences
{
    class Student
    {
        public string Name { get; set; }
        public int Number { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create a strong reference to a new object.
            Student s = new Student();
            // Create a strong reference to a short WeakReference object.
            // The WeakReference object tracks the Object's lifetime.
            WeakReference<Student> wr = new WeakReference<Student>(s);
            s = null; // Remove the strong reference to the object.
            if (wr.TryGetTarget(out s))
            {
                // A garbage collection occurred and Object's was reclaimed
            }
            else {
                // A garbage collection did not occur and I can successfully access
                // the object using o.
            }
        }
    }
}
