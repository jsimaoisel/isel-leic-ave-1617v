using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Dispose
{
    class Program
    {
        static void Main(string[] args)
        {
            using (FileStream fs = new FileStream("aaa", FileMode.Open))
            {
                fs.Write(new byte[] { 1, 2, 3 },0,3);
            }
        }
    }
}
