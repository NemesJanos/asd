using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeKi
{
    class RendszeresKiadas : Kiadas, IComparable<RendszeresKiadas>
    {
        Rendszeresseg rendszeresseg;

        
        public Rendszeresseg Rendszeresseg { get => rendszeresseg; set => rendszeresseg = value; }
        
        /// <summary>
        /// Rendszeres kiadás. (Autoincrement Id.) LÉTREHOZÁS
        /// </summary>
        /// <param name="kategoria"></param>
        /// <param name="megnevezes"></param>
        /// <param name="osszeg"></param>
        /// <param name="esedekesseg"></param>
        /// <param name="rendszeresseg"></param>
        public RendszeresKiadas(KiadasKategoria kategoria, string megnevezes, int osszeg, DateTime esedekesseg, Rendszeresseg rendszeresseg) : base(kategoria, megnevezes, osszeg, esedekesseg)
        {
            Rendszeresseg = rendszeresseg;
        }
        /// <summary>
        /// Rendszeres kiadás. (Már van Id. Visszaolvasás műveletvégzéshez.)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="kategoria"></param>
        /// <param name="megnevezes"></param>
        /// <param name="osszeg"></param>
        /// <param name="esedekesseg"></param>
        /// <param name="rendszeresseg"></param>
        public RendszeresKiadas(int id, KiadasKategoria kategoria, string megnevezes, int osszeg, DateTime esedekesseg, Rendszeresseg rendszeresseg) : base(id, kategoria, megnevezes, osszeg, esedekesseg)
        {
            Rendszeresseg = rendszeresseg;
        }
        /// <summary>
        /// Rendszeres kiadás ami ez esetben már pénzügyileg rendezett. (Autoincrement Id.) LÉTREHOZÁS
        /// </summary>
        /// <param name="kategoria"></param>
        /// <param name="megnevezes"></param>
        /// <param name="osszeg"></param>
        /// <param name="esedekesseg"></param>
        /// <param name="rendszeresseg"></param>
        /// <param name="teljesitesDatuma"></param>
        public RendszeresKiadas(KiadasKategoria kategoria, string megnevezes, int osszeg, DateTime esedekesseg, Rendszeresseg rendszeresseg, DateTime teljesitesDatuma) : base(kategoria, megnevezes, osszeg, esedekesseg, teljesitesDatuma)
        {
            Rendszeresseg = rendszeresseg;
        }
        /// <summary>
        /// Rendszeres kiadás ami ez esetben már pénzügyileg rendezett. (Már van Id. Visszaolvasás műveletvégzéshez.)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="kategoria"></param>
        /// <param name="megnevezes"></param>
        /// <param name="osszeg"></param>
        /// <param name="esedekesseg"></param>
        /// <param name="rendszeresseg"></param>
        /// <param name="teljesitesDatuma"></param>
        public RendszeresKiadas(int id, KiadasKategoria kategoria, string megnevezes, int osszeg, DateTime esedekesseg, Rendszeresseg rendszeresseg, DateTime teljesitesDatuma) : base(id, kategoria, megnevezes, osszeg, esedekesseg, teljesitesDatuma)
        {
            Rendszeresseg = rendszeresseg;
        }

        public new string ToString()
        {
            return base.ToString() + $" [{rendszeresseg}]";
        }

        public int CompareTo(RendszeresKiadas obj)
        {
            if (obj == null)
            {
                return 1;
            }
            else if (this.Megnevezes == obj.Megnevezes)
            {
                if (this.Kategoria.Id == obj.Kategoria.Id)
                {
                    if (this.Osszeg == obj.Osszeg)
                    {
                        return this.Rendszeresseg.CompareTo(obj.Rendszeresseg);
                    }
                }
            }
            return -1;
        }
    }
}
