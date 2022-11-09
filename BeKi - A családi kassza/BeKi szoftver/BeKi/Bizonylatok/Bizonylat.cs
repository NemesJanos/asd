using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeKi
{
    abstract class Bizonylat
    {
        int id;
        string fajlnev;
        byte[] fajl;

        public int Id
        {
            get => id;
            set
            {
                if (id == default(int))
                {
                    id = value;
                }
                else
                {
                    throw new InvalidOperationException("Az Id értékét a rendszer állítja be és nem módosítható.");
                }
            }
        }
        public string Fajlnev
        {
            get => fajlnev;
            set
            {
                if (value.Length > 0 && value.Length <= 255)
                {
                    fajlnev = value;
                }
                else
                {
                    throw new ArgumentException("A file neve nem lehet üres és maximum 255 karakteres lehet.");
                }
            }
        }
        public byte[] Fajl { get => fajl; set => fajl = value; }

        protected Bizonylat(string fajlnev, byte[] fajl)
        {
            Fajlnev = fajlnev;
            Fajl = fajl;
        }

        protected Bizonylat(int id, string fajlnev, byte[] fajl) : this(fajlnev, fajl)
        {
            Id = id;
        }

        public override string ToString()
        {
            return fajlnev.Substring(fajlnev.LastIndexOf(@"\")+1);
        }
    }
}
