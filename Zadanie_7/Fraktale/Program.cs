using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fraktale
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Okno okno = new Okno(920, 760))
            {
                okno.Run(30.0);
            }
        }
    }
}
