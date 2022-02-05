using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzescianTK18
{
    class Program
    {
        static void Main(string[] args)
        {
            using (okno przykl = new okno(800, 600))
            {
                przykl.Run(30.0);
            }
        }
    }
}
