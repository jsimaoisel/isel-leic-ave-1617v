using System;
using System.Reflection;

namespace NewObjectByReflection
{
    public class Student
    {
        public String Name { get; set; }


        public int Nr { get; set; }
        public Student(String nm, int nr)
        {
            this.Name = nm;
            this.Nr = nr;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Student s = (Student)Activator.CreateInstance(
                typeof(Student), 
                "Ze", 11111 /*new object[]{"Ze",11111}*/);

            Console.WriteLine("{0} {1}", s.Name, s.Nr);

            Type ts = typeof(Student);
            ConstructorInfo ci = ts.GetConstructor(
                    new Type[] { typeof(String), typeof(int) }
                );
            s = (Student) ci.Invoke(new object[] { "Outro Ze", 22222 });

            Console.WriteLine("{0} {1}", s.Name, s.Nr);

            // lança InvalidClassException
            Program p = (Program) ci.Invoke(new object[] { "Outro Ze", 22222 });

        }
    }
}
