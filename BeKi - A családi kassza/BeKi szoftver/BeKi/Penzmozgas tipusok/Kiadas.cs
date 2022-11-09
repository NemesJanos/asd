using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeKi
{

    class Kiadas : Penzmozgas
    {
        KiadasKategoria kategoria;

        internal KiadasKategoria Kategoria
        {
            get => kategoria;
            set
            {
                if (value != null)
                {
                    kategoria = value;
                }
                else
                {
                    throw new ArgumentException("A kategóriát kötelező megadni.");
                }
            }
        }
        /// <summary>
        /// Nem rendszeres kiadás. (Autoincrement Id.) LÉTREHOZÁS
        /// </summary>
        /// <param name="kategoria"></param>
        /// <param name="megnevezes"></param>
        /// <param name="osszeg"></param>
        /// <param name="esedekesseg"></param>
        public Kiadas(KiadasKategoria kategoria, string megnevezes, int osszeg, DateTime esedekesseg) : base(megnevezes, osszeg, esedekesseg)
        {
            Kategoria = kategoria;
        }
        /// <summary>
        /// Nem rendszeres kiadás. (Már van Id. Visszaolvasás műveletvégzéshez.)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="kategoria"></param>
        /// <param name="megnevezes"></param>
        /// <param name="osszeg"></param>
        /// <param name="esedekesseg"></param>
        public Kiadas(int id, KiadasKategoria kategoria, string megnevezes, int osszeg, DateTime esedekesseg) : base(id, megnevezes, osszeg, esedekesseg)
        {
            Kategoria = kategoria;
        }
        /// <summary>
        /// Nem rendszeres kiadás ami ez esetben már pénzügyileg rendezett. (Autoincrement Id.) LÉTREHOZÁS
        /// </summary>
        /// <param name="kategoria"></param>
        /// <param name="megnevezes"></param>
        /// <param name="osszeg"></param>
        /// <param name="esedekesseg"></param>
        /// <param name="teljesitesDatuma"></param>
        public Kiadas(KiadasKategoria kategoria, string megnevezes, int osszeg, DateTime esedekesseg, DateTime teljesitesDatuma) : base(megnevezes, osszeg, esedekesseg, teljesitesDatuma)
        {
            Kategoria = kategoria;
        }
        /// <summary>
        /// Nem rendszeres kiadás ami ez esetben már pénzügyileg rendezett. (Már van Id. Visszaolvasás műveletvégzéshez.)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="kategoria"></param>
        /// <param name="megnevezes"></param>
        /// <param name="osszeg"></param>
        /// <param name="esedekesseg"></param>
        /// <param name="teljesitesDatuma"></param>
        public Kiadas(int id, KiadasKategoria kategoria, string megnevezes, int osszeg, DateTime esedekesseg, DateTime teljesitesDatuma) : base(id, megnevezes, osszeg, esedekesseg, teljesitesDatuma)
        {
            Kategoria = kategoria;
        }

        public override string ToString()
        {
            return Megnevezes;
        }
    }
}
