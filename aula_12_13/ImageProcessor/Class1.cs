using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalProcessors
{
    public class ImageProcessors
    {
        int value = 10;
        public void M1(double[] v)
        {
            //Console.WriteLine("@M1");
            v[0] = 10;
        }

        public static void M2(double[] v)
        {
            //Console.WriteLine("@M2");
        }

        public int M3(double[] d, bool b)
        {
            return 0;
        }

    }
}
