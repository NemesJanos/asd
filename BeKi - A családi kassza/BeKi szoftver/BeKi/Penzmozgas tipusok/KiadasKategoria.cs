using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeKi
{
    class KiadasKategoria : IComparable
    {
        int id;
        string megnevezes;

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

        public KiadasKategoria(string megnevezes)
        {
            Id = id;
            Megnevezes = megnevezes;
        }

        public KiadasKategoria(int id, string megnevezes) : this(megnevezes)
        {
            Id = id;
        }

        public override string ToString()
        {
            return megnevezes;
        }

        public int CompareTo(object obj)
        {
            return Megnevezes.CompareTo(obj.ToString());
        }
    }
}
