using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeKi
{
    class KiadasBizonylat : Bizonylat
    {
        public KiadasBizonylat(string fajlnev, byte[] fajl) : base(fajlnev, fajl)
        {
        }

        public KiadasBizonylat(int id, string fajlnev, byte[] fajl) : base(id, fajlnev, fajl)
        {
        }
    }
}
