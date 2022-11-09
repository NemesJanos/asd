using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeKi
{
    abstract class Penzmozgas
    {
        int id;
        string megnevezes;
        int osszeg;
        DateTime teljesitesDatuma;
        DateTime esedekesseg;

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
        public string Megnevezes 
        {
            get => megnevezes;
            set
            {
                if (value.Length > 0 && value.Length <= 64)
                {
                    megnevezes = value;
                }
                else
                {
                    throw new ArgumentException("A megnevezés nem lehet üres és maximum 64 karakteres lehet.");
                }
            }
        }
        public int Osszeg 
        {
            get => osszeg;
            set
            {
                if (value > 0 && value < int.MaxValue)
                {
                    osszeg = value;
                }
                else
                {
                    if (value < 0)
                    {
                        throw new ArgumentException("Az összeg nullánál nagyobb egész szám kell legyen és költség esetén sem lehet negatív.");
                    }
                    else if (value > int.MaxValue)
                    {
                        throw new ArgumentException("Túl nagy számot adott meg.");
                    }
                }
            }
        }

        public DateTime TeljesitesDatuma { get => teljesitesDatuma; set => teljesitesDatuma = value; }
        
        public DateTime Esedekesseg { get => esedekesseg; set => esedekesseg = value; }


        public Penzmozgas(string megnevezes, int osszeg, DateTime esedekesseg)
        {
            Megnevezes = megnevezes;
            Osszeg = osszeg;
            Esedekesseg = esedekesseg;
        }

        public Penzmozgas(int id, string megnevezes, int osszeg, DateTime esedekesseg) : this(megnevezes, osszeg, esedekesseg)
        {
            Id = id;
        }

        public Penzmozgas(string megnevezes, int osszeg, DateTime esedekesseg, DateTime teljesitesDatuma) : this(megnevezes, osszeg, esedekesseg)
        {
            TeljesitesDatuma = teljesitesDatuma;
        }

        public Penzmozgas(int id, string megnevezes, int osszeg, DateTime esedekesseg, DateTime teljesitesDatuma) : this(id, megnevezes, osszeg, esedekesseg)
        {
            TeljesitesDatuma = teljesitesDatuma;
        }
    }
}
