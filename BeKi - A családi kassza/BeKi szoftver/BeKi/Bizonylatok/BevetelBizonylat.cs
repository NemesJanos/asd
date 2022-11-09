using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeKi
{
    class BevetelBizonylat : Bizonylat
    {
        public BevetelBizonylat(string fajlnev, byte[] fajl) : base(fajlnev, fajl)
        {
        }

        public BevetelBizonylat(int id, string fajlnev, byte[] fajl) : base(id, fajlnev, fajl)
        {
        }
    }
}
