using System;


namespace Properties
{
    class Program
    {

        public int Number;

        private String _Name;
        public String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (value.Length > 15) throw new InvalidOperationException("Name must have less than 15 characters");
                _Name = value;
            }
        }
        public int Year
        {
            get { return DateTime.Now.Year; }
            set { }
        }

        public int Day
        {
            get;
            set;
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Name = "AVE";
            Console.WriteLine(p.Name);

            p.Name = "aaaa";

            p.Year = 2017;
            Console.WriteLine("Year = {0}", p.Year);
        }
    }
}
