using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BeKi
{
    static class ABKezelo
    {
        static SqlConnection connection;
        static SqlCommand command;

        #region ABKapcsolat
        public static void Csatlakozas()
        {
            try
            {
                connection = new SqlConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["KonyvelesDBConn"].ConnectionString;
                connection.Open();
                command = new SqlCommand();
                command.Connection = connection;
            }
            catch (Exception ex)
            {
                throw new ABKivetel("Sikertelen csatlakozas", ex);
            }
        }
        public static void KapcsolatBontas()
        {
            try
            {
                connection.Close();
                command.Dispose();
            }
            catch (Exception ex)
            {
                throw new ABKivetel("A kapcsolat bontása sikertelen.", ex);
            }
        }
        #endregion

        #region Szerviz
        public static void BiztonsagiMentes(string utvonal)
        {
            try
            {
                command.Parameters.Clear();
                command.CommandText = @"BACKUP DATABASE [KonyvelesAB] TO DISK = @utvonal";
                command.Parameters.AddWithValue("@utvonal", utvonal);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ABKivetel("Az adatbázis biztonsági mentése sikertelen.", ex);
            }

        }
        public static void BiztonsagiMentesVisszaallitasa(string utvonal)
        {
            //https://stackoverflow.com/questions/4218960/how-to-restore-sql-server-database-through-c-sharp-code
            try
            {
                command.Parameters.Clear();
                command.CommandText = @"USE master RESTORE DATABASE [KonyvelesAB] FROM DISK = @utvonal WITH REPLACE";
                command.Parameters.AddWithValue("@utvonal", utvonal);
                command.ExecuteNonQuery();
                KapcsolatBontas();
                Csatlakozas();
            }
            catch (Exception ex)
            {
                try
                {
                    command.Transaction.Rollback();
                }
                catch (Exception ex2)
                {
                    throw new ABKivetel("A visszaállítás nem sikerült!!!", ex2);
                }
                throw new ABKivetel("Az adatbázis visszaállítása sikertelen.", ex);
            }
        }
        public static void PenzmozgasErtesitesBeallitas(bool beallit)
        {
            try
            {
                command.Parameters.Clear();
                command.CommandText = "UPDATE [Beallitasok] SET [Bekapcsolva] = @beallit WHERE [Beallitas] = 'NemRendezettPenzmozgasErtesites'";
                command.Parameters.AddWithValue("@beallit", beallit);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ABKivetel("A beállítás nem sikerült.", ex);
            }
        }
        public static bool PenzmozgasErtesitesBeallitasLekerdezes()
        {
            try
            {
                command.Parameters.Clear();
                command.CommandText = "SELECT * FROM [Beallitasok] WHERE [Beallitas] = 'NemRendezettPenzmozgasErtesites'";
                bool beallitva = false;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Beallitas"].ToString() == "NemRendezettPenzmozgasErtesites")
                        {
                            beallitva = (bool)reader["Bekapcsolva"];
                        }
                    }
                    reader.Close();
                }
                return beallitva;
            }
            catch (Exception ex)
            {
                throw new ABKivetel("Egyes beállítások betöltése nem sikerült.", ex);
            }
        }
        #endregion

        #region Kategoriak
        public static List<BevetelKategoria> BevetelKategoriaListazas()
        {
            try
            {
                command.Parameters.Clear();
                command.CommandText = "SELECT * FROM [BevetelKategoria]";
                List<BevetelKategoria> lista = new List<BevetelKategoria>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new BevetelKategoria((int)reader["Id"], reader["Megnevezes"].ToString()));
                    }
                    reader.Close();
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new ABKivetel("A bevétel gyűjtőtípusok betöltése sikertelen.", ex);
            }
        }
        public static List<KiadasKategoria> KiadasKategoriaListazas()
        {
            try
            {
                command.Parameters.Clear();
                command.CommandText = "SELECT * FROM [KiadasKategoria]";
                List<KiadasKategoria> lista = new List<KiadasKategoria>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new KiadasKategoria((int)reader["Id"], reader["Megnevezes"].ToString()));
                    }
                    reader.Close();
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new ABKivetel("A kiadás kategóriák betöltése sikertelen.", ex);
            }
        }
        public static void BevetelKategoriaLetrehozasa(BevetelKategoria uj)
        {
            try
            {
                command.Parameters.Clear();
                command.Transaction = connection.BeginTransaction();
                command.CommandText = "INSERT INTO [BevetelKategoria]([Megnevezes]) OUTPUT INSERTED.Id VALUES (@megnevezes)";
                command.Parameters.AddWithValue("@megnevezes", uj.Megnevezes);
                uj.Id = (int)command.ExecuteScalar();
                command.Transaction.Commit();
            }
            catch (Exception ex)
            {
                throw new ABKivetel("A bevétel kategória felvitele nem sikerült.", ex);
            }
        }
        public static void KiadasKategoriaLetrehozasa(KiadasKategoria uj)
        {
            try
            {
                command.Parameters.Clear();
                command.Transaction = connection.BeginTransaction();
                command.CommandText = "INSERT INTO [KiadasKategoria]([Megnevezes]) OUTPUT INSERTED.Id VALUES (@megnevezes)";
                command.Parameters.AddWithValue("@megnevezes", uj.Megnevezes);
                uj.Id = (int)command.ExecuteScalar();
                command.Transaction.Commit();
            }
            catch (Exception ex)
            {
                throw new ABKivetel("A kiadás kategória felvitel nem sikerült.", ex);
            }
        }
        public static void BevetelKiadasKategoriaModositas(BevetelKategoria modosit)
        {
            try
            {
                command.Parameters.Clear();
                command.CommandText = "UPDATE [BevetelKategoria] SET [Megnevezes] = @megnevezes WHERE [Id] = @id";
                command.Parameters.AddWithValue("@megnevezes", modosit.Megnevezes);
                command.Parameters.AddWithValue("@id", modosit.Id);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ABKivetel("A módosítás sikertelen", ex);
            }
        }
        public static void KiadasKategoriaModositas(KiadasKategoria modosit)
        {
            try
            {
                command.Parameters.Clear();
                command.CommandText = "UPDATE [KiadasKategoria] SET [Megnevezes] = @megnevezes WHERE [Id] = @id";
                command.Parameters.AddWithValue("@megnevezes", modosit.Megnevezes);
                command.Parameters.AddWithValue("@id", modosit.Id);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ABKivetel("A módosítás sikertelen", ex);
            }
        }
        public static void BevetelKategoriaTorles(BevetelKategoria torol)
        {
            try
            {
                command.Parameters.Clear();
                command.CommandText = "DELETE FROM [BevetelKategoria] WHERE [Megnevezes] = @megnevezes";
                command.Parameters.AddWithValue("@megnevezes", torol.Megnevezes);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ABKivetel("A törlés sikertelen.\nLehet, hogy a kategória hozzá van rendelve tételekhez?\nHa törölni szeretné a kategóriát, előbb törölje a hozzá rendelt tételeket.\n\n Tipp: Ha nem akarja a tételeket törölni, társítsa másik kategóriához azokat!", ex);
            }
        }
        public static void KiadasKategoriaTorles(KiadasKategoria torol)
        {
            try
            {
                command.Parameters.Clear();
                command.CommandText = "DELETE FROM [KiadasKategoria] WHERE [Megnevezes] = @megnevezes";
                command.Parameters.AddWithValue("@megnevezes", torol.Megnevezes);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ABKivetel("A törlés sikertelen.\nLehet, hogy a kategória hozzá van rendelve tételekhez?\nHa törölni szeretné a kategóriát, előbb törölje a hozzá rendelt tételeket.\n\n Tipp: Ha nem akarja a tételeket törölni, társítsa másik kategóriához azokat!", ex);
            }
        }
        #endregion

        #region Penzmozgasok
        public static List<Bevetel> Bevetel_ElmultEvListazasa(DateTime meddig)
        {
            //DateTime mettol = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, 1);
            DateTime mettol = new DateTime(meddig.Year - 1, meddig.Month, 1);
            return BevetelListazas(mettol, meddig);
        }
        public static List<Kiadas> Kiadas_ElmultEvListazasa(DateTime meddig)
        {
            //DateTime mettol = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, 1);
            DateTime mettol = new DateTime(meddig.Year - 1, meddig.Month, 1);
            return KiadasListazas(mettol, meddig);
        }
        public static List<Bevetel> BevetelListazas(DateTime mettol, DateTime meddig)
        {
            try
            {
                List<BevetelKategoria> bevKat = BevetelKategoriaListazas();
                List<Bevetel> bevetelLista = new List<Bevetel>();
                command.Parameters.Clear();
                command.CommandText = "SELECT [Bevetel].[Id], [BevetelKategoria].[Id] AS [KatId], [BevetelKategoria].[Megnevezes] AS [KatMegn], [Bevetel].[Megnevezes],[Osszeg],[TeljesitesDatuma],[Esedekesseg],[Rendszeresseg] FROM [Bevetel] LEFT JOIN [BevetelKategoria] ON [Bevetel].[KategoriaId] = [BevetelKategoria].[Id] WHERE [Esedekesseg] >= @mettol AND [Esedekesseg] <= @meddig";
                command.Parameters.AddWithValue("@mettol", mettol);
                command.Parameters.AddWithValue("@meddig", meddig);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Rendszeresseg"].ToString() == "" && reader["TeljesitesDatuma"].ToString() == "") //Nem rendszeres bevétel.
                        {
                            bevetelLista.Add(new Bevetel(
                                (int)reader["Id"],
                                new BevetelKategoria((int)reader["KatId"], reader["KatMegn"].ToString()),
                                reader["Megnevezes"].ToString(),
                                (int)reader["Osszeg"],
                                (DateTime)reader["Esedekesseg"]
                                ));
                        }
                        else if (reader["Rendszeresseg"].ToString() == "" && reader["TeljesitesDatuma"].ToString() != "") //Nem rendszeres bevétel ami ez esetben már pénzügyileg rendezett.
                        {
                            bevetelLista.Add(new Bevetel(
                                (int)reader["Id"],
                                new BevetelKategoria((int)reader["KatId"], reader["KatMegn"].ToString()),
                                reader["Megnevezes"].ToString(),
                                (int)reader["Osszeg"],
                                (DateTime)reader["Esedekesseg"],
                                (DateTime)reader["TeljesitesDatuma"]
                                ));
                        }
                        else if (reader["Rendszeresseg"].ToString() != "" && reader["TeljesitesDatuma"].ToString() == "") // Rendszeres bevétel.
                        {
                            bevetelLista.Add(new RendszeresBevetel(
                                (int)reader["Id"],
                                new BevetelKategoria((int)reader["KatId"], reader["KatMegn"].ToString()),
                                reader["Megnevezes"].ToString(),
                                (int)reader["Osszeg"],
                                (DateTime)reader["Esedekesseg"],
                                (Rendszeresseg)reader["Rendszeresseg"]
                                ));
                        }
                        else if (reader["Rendszeresseg"].ToString() != "" && reader["TeljesitesDatuma"].ToString() != "") // Rendszeres bevétel ami ez esetben már pénzügyileg rendezett.
                        {
                            bevetelLista.Add(new RendszeresBevetel(
                                (int)reader["Id"],
                                new BevetelKategoria((int)reader["KatId"], reader["KatMegn"].ToString()),
                                reader["Megnevezes"].ToString(),
                                (int)reader["Osszeg"],
                                (DateTime)reader["Esedekesseg"],
                                (Rendszeresseg)reader["Rendszeresseg"],
                                (DateTime)reader["TeljesitesDatuma"]
                                ));
                        }
                    }
                    reader.Close();
                }
                return bevetelLista;
            }
            catch (Exception ex)
            {
                throw new ABKivetel("A bevételek listázása sikertelen.", ex);
            }
        }
        public static List<Kiadas> KiadasListazas(DateTime mettol, DateTime meddig)
        {
            try
            {
                List<KiadasKategoria> kiadKat = KiadasKategoriaListazas();
                List<Kiadas> KiadasLista = new List<Kiadas>();
                command.Parameters.Clear();
                command.CommandText = "SELECT [Kiadas].[Id], [KiadasKategoria].[Id] AS [KatId], [KiadasKategoria].[Megnevezes] AS [KatMegn], [Kiadas].[Megnevezes],[Osszeg],[TeljesitesDatuma],[Esedekesseg],[Rendszeresseg] FROM [Kiadas] LEFT JOIN [KiadasKategoria] ON [Kiadas].[KategoriaId] = [KiadasKategoria].[Id] WHERE [Esedekesseg] >= @mettol AND [Esedekesseg] <= @meddig";
                command.Parameters.AddWithValue("@mettol", mettol);
                command.Parameters.AddWithValue("@meddig", meddig);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Rendszeresseg"].ToString() == "" && reader["TeljesitesDatuma"].ToString() == "") //Nem rendszeres kiadás.
                        {
                            KiadasLista.Add(new Kiadas(
                                (int)reader["Id"],
                                new KiadasKategoria((int)reader["KatId"], reader["KatMegn"].ToString()),
                                reader["Megnevezes"].ToString(),
                                (int)reader["Osszeg"],
                                (DateTime)reader["Esedekesseg"]
                                ));
                        }
                        else if (reader["Rendszeresseg"].ToString() == "" && reader["TeljesitesDatuma"].ToString() != "") //Nem rendszeres kiadás ami ez esetben már pénzügyileg rendezett.
                        {
                            KiadasLista.Add(new Kiadas(
                                (int)reader["Id"],
                                new KiadasKategoria((int)reader["KatId"], reader["KatMegn"].ToString()),
                                reader["Megnevezes"].ToString(),
                                (int)reader["Osszeg"],
                                (DateTime)reader["Esedekesseg"],
                                (DateTime)reader["TeljesitesDatuma"]
                                ));
                        }
                        else if (reader["Rendszeresseg"].ToString() != "" && reader["TeljesitesDatuma"].ToString() == "") // Rendszeres kiadás.
                        {
                            KiadasLista.Add(new RendszeresKiadas(
                                (int)reader["Id"],
                                new KiadasKategoria((int)reader["KatId"], reader["KatMegn"].ToString()),
                                reader["Megnevezes"].ToString(),
                                (int)reader["Osszeg"],
                                (DateTime)reader["Esedekesseg"],
                                (Rendszeresseg)reader["Rendszeresseg"]
                                ));
                        }
                        else if (reader["Rendszeresseg"].ToString() != "" && reader["TeljesitesDatuma"].ToString() != "") // Rendszeres kiadás ami ez esetben már pénzügyileg rendezett.
                        {
                            KiadasLista.Add(new RendszeresKiadas(
                                (int)reader["Id"],
                                new KiadasKategoria((int)reader["KatId"], reader["KatMegn"].ToString()),
                                reader["Megnevezes"].ToString(),
                                (int)reader["Osszeg"],
                                (DateTime)reader["Esedekesseg"],
                                (Rendszeresseg)reader["Rendszeresseg"],
                                (DateTime)reader["TeljesitesDatuma"]
                                ));
                        }
                    }
                    reader.Close();
                }
                return KiadasLista;
            }
            catch (Exception ex)
            {
                throw new ABKivetel("A kiadások listázása sikertelen.", ex);
            }
        }
        public static void BevetelFelvitel(Bevetel uj, bool teljesitve, int id)
        {
            try
            {
                command.Parameters.Clear();
                command.Transaction = connection.BeginTransaction();
                command.Parameters.AddWithValue("@megnevezes", uj.Megnevezes);
                command.Parameters.AddWithValue("@osszeg", uj.Osszeg);
                command.Parameters.AddWithValue("@kategoriaId", uj.Kategoria.Id);
                command.Parameters.AddWithValue("@esedekesseg", uj.Esedekesseg);
                if (id == 0)
                {
                    if (teljesitve) //Már teljesült bevétel felvitel.
                    {
                        command.Parameters.AddWithValue("@teljesites", uj.TeljesitesDatuma);
                        command.CommandText = "INSERT INTO [Bevetel] ([Megnevezes], [Osszeg], [KategoriaId], [Esedekesseg], [TeljesitesDatuma]) OUTPUT INSERTED.Id VALUES(@megnevezes, @osszeg, @kategoriaId, @esedekesseg, @teljesites)";
                    }
                    else //Bevétel felvitel.
                    {
                        command.CommandText = "INSERT INTO [Bevetel] ([Megnevezes], [Osszeg], [KategoriaId], [Esedekesseg]) OUTPUT INSERTED.Id VALUES(@megnevezes, @osszeg, @kategoriaId, @esedekesseg)";
                    }
                    command.ExecuteNonQuery();
                }
                else
                {
                    command.Parameters.AddWithValue("@id", id);
                    if (teljesitve)  //Már teljesült bevétel módosítás.
                    {
                        command.Parameters.AddWithValue("@teljesites", uj.TeljesitesDatuma);
                        command.CommandText = "UPDATE [Bevetel] SET [Megnevezes] = @megnevezes, [Osszeg] = @osszeg, [KategoriaId] = @kategoriaId, [Esedekesseg] = @esedekesseg, [TeljesitesDatuma] = @teljesites WHERE [Id] = @id";
                        command.ExecuteNonQuery();
                    }
                    else //Bevétel módosítás.
                    {
                        command.CommandText = "UPDATE [Bevetel] SET [Megnevezes] = @megnevezes, [Osszeg] = @osszeg, [KategoriaId] = @kategoriaId, [Esedekesseg] = @esedekesseg WHERE [Id] = @id";
                        command.ExecuteNonQuery();
                        command.CommandText = "UPDATE [Bevetel] SET [TeljesitesDatuma] = NULL WHERE [Id] = @id";
                        command.ExecuteNonQuery();
                    }
                    //Ha módosítás van, törölni kell a táblából a rendszeresség bejegyzést, mert lehet, hogy rendszeres volt.
                    command.CommandText = "UPDATE [Bevetel] SET [Rendszeresseg] = NULL WHERE [Id] = @id";
                    command.ExecuteNonQuery();
                }
                command.Transaction.Commit();
            }
            catch (Exception ex)
            {
                try
                {
                    if (command.Transaction != null)
                    {
                        command.Transaction.Rollback();
                    }
                }
                catch (Exception ex2)
                {
                    throw new ABKivetel("Végzetes hiba. További műveletek nem végezhetők.", ex2);
                }
                throw new ABKivetel("A művelet nem sikerült!", ex);
            }
        }
        public static void RendszeresBevetelFelvitel(RendszeresBevetel uj, bool teljesitve, int id)
        {
            try
            {
                command.Parameters.Clear();
                command.Transaction = connection.BeginTransaction();
                command.Parameters.AddWithValue("@megnevezes", uj.Megnevezes);
                command.Parameters.AddWithValue("@osszeg", uj.Osszeg);
                command.Parameters.AddWithValue("@kategoriaId", uj.Kategoria.Id);
                command.Parameters.AddWithValue("@esedekesseg", uj.Esedekesseg);
                command.Parameters.AddWithValue("@rendszeresseg", uj.Rendszeresseg);
                if (id == 0)
                {
                    if (teljesitve) //Már teljesült, rendszeres bevétel felvitel.
                    {
                        command.Parameters.AddWithValue("@teljesites", uj.TeljesitesDatuma);
                        command.CommandText = "INSERT INTO [Bevetel] ([Megnevezes], [Osszeg], [KategoriaId], [Esedekesseg], [TeljesitesDatuma], [Rendszeresseg]) OUTPUT INSERTED.Id VALUES(@megnevezes, @osszeg, @kategoriaId, @esedekesseg, @teljesites, @rendszeresseg)";
                    }
                    else //Rendszeres bevétel felvitel.
                    {
                        command.CommandText = "INSERT INTO [Bevetel] ([Megnevezes], [Osszeg], [KategoriaId], [Esedekesseg], [Rendszeresseg]) OUTPUT INSERTED.Id VALUES(@megnevezes, @osszeg, @kategoriaId, @esedekesseg, @rendszeresseg)";
                    }
                    command.ExecuteNonQuery();
                }
                else
                {
                    command.Parameters.AddWithValue("@id", id);
                    if (teljesitve) //Már teljesült, rendszeres bevétel módosítás.
                    {
                        command.Parameters.AddWithValue("@teljesites", uj.TeljesitesDatuma);
                        command.CommandText = "UPDATE [Bevetel] SET [Megnevezes] = @megnevezes, [Osszeg] = @osszeg, [KategoriaId] = @kategoriaId, [Esedekesseg] = @esedekesseg, [TeljesitesDatuma] = @teljesites, [Rendszeresseg] = @rendszeresseg WHERE [Id] = @id";
                        command.ExecuteNonQuery();

                    }
                    else //Rendszeres bevétel módosítás.
                    {
                        command.CommandText = "UPDATE [Bevetel] SET [Megnevezes] = @megnevezes, [Osszeg] = @osszeg, [KategoriaId] = @kategoriaId, [Esedekesseg] = @esedekesseg, [Rendszeresseg] = @rendszeresseg WHERE [Id] = @id";
                        command.ExecuteNonQuery();
                        command.CommandText = "UPDATE [Bevetel] SET [TeljesitesDatuma] = NULL WHERE [Id] = @id";
                        command.ExecuteNonQuery();
                    }
                }
                command.Transaction.Commit();

            }
            catch (Exception ex)
            {
                try
                {
                    if (command.Transaction != null)
                    {
                        command.Transaction.Rollback();
                    }
                }
                catch (Exception ex2)
                {
                    throw new ABKivetel("Végzetes hiba. További műveletek nem végezhetők.", ex2);
                }
                throw new ABKivetel("A művelet nem sikerült!", ex);
            }
        }
        public static void KiadasFelvitel(Kiadas uj, bool teljesitve, int id)
        {
            try
            {
                command.Parameters.Clear();
                command.Transaction = connection.BeginTransaction();
                command.Parameters.AddWithValue("@megnevezes", uj.Megnevezes);
                command.Parameters.AddWithValue("@osszeg", uj.Osszeg);
                command.Parameters.AddWithValue("@kategoriaId", uj.Kategoria.Id);
                command.Parameters.AddWithValue("@esedekesseg", uj.Esedekesseg);
                if (id == 0)
                {
                    if (teljesitve) //Már teljesült kiadás felvitel.
                    {
                        command.Parameters.AddWithValue("@teljesites", uj.TeljesitesDatuma);
                        command.CommandText = "INSERT INTO [Kiadas] ([Megnevezes], [Osszeg], [KategoriaId], [Esedekesseg], [TeljesitesDatuma]) OUTPUT INSERTED.Id VALUES(@megnevezes, @osszeg, @kategoriaId, @esedekesseg, @teljesites)";
                    }
                    else //Kiadás felvitel.
                    {
                        command.CommandText = "INSERT INTO [Kiadas] ([Megnevezes], [Osszeg], [KategoriaId], [Esedekesseg]) OUTPUT INSERTED.Id VALUES(@megnevezes, @osszeg, @kategoriaId, @esedekesseg)";
                    }
                    command.ExecuteNonQuery();
                }
                else
                {
                    command.Parameters.AddWithValue("@id", id);
                    if (teljesitve)  //Már teljesült kiadás módosítás.
                    {
                        command.Parameters.AddWithValue("@teljesites", uj.TeljesitesDatuma);
                        command.CommandText = "UPDATE [Kiadas] SET [Megnevezes] = @megnevezes, [Osszeg] = @osszeg, [KategoriaId] = @kategoriaId, [Esedekesseg] = @esedekesseg, [TeljesitesDatuma] = @teljesites WHERE [Id] = @id";
                        command.ExecuteNonQuery();
                    }
                    else //Kiadás módosítás.
                    {
                        command.CommandText = "UPDATE [Kiadas] SET [Megnevezes] = @megnevezes, [Osszeg] = @osszeg, [KategoriaId] = @kategoriaId, [Esedekesseg] = @esedekesseg WHERE [Id] = @id";
                        command.ExecuteNonQuery();
                        command.CommandText = "UPDATE [Kiadas] SET [TeljesitesDatuma] = NULL WHERE [Id] = @id";
                        command.ExecuteNonQuery();
                    }
                    //Ha módosítás van, törölni kell a táblából a rendszeresség bejegyzést, mert lehet, hogy rendszeres volt.
                    command.CommandText = "UPDATE [Kiadas] SET [Rendszeresseg] = NULL WHERE [Id] = @id";
                    command.ExecuteNonQuery();
                }
                command.Transaction.Commit();
            }
            catch (Exception ex)
            {
                try
                {
                    if (command.Transaction != null)
                    {
                        command.Transaction.Rollback();
                    }
                }
                catch (Exception ex2)
                {
                    throw new ABKivetel("Végzetes hiba. További műveletek nem végezhetők.", ex2);
                }
                throw new ABKivetel("A művelet nem sikerült!", ex);
            }
        }
        public static void RendszeresKiadasFelvitel(RendszeresKiadas uj, bool teljesitve, int id)
        {
            try
            {
                command.Parameters.Clear();
                command.Transaction = connection.BeginTransaction();
                command.Parameters.AddWithValue("@megnevezes", uj.Megnevezes);
                command.Parameters.AddWithValue("@osszeg", uj.Osszeg);
                command.Parameters.AddWithValue("@kategoriaId", uj.Kategoria.Id);
                command.Parameters.AddWithValue("@esedekesseg", uj.Esedekesseg);
                command.Parameters.AddWithValue("@rendszeresseg", uj.Rendszeresseg);
                if (id == 0)
                {
                    if (teljesitve) //Már teljesült, rendszeres kiadás felvitel.
                    {
                        command.Parameters.AddWithValue("@teljesites", uj.TeljesitesDatuma);
                        command.CommandText = "INSERT INTO [Kiadas] ([Megnevezes], [Osszeg], [KategoriaId], [Esedekesseg], [TeljesitesDatuma], [Rendszeresseg]) OUTPUT INSERTED.Id VALUES(@megnevezes, @osszeg, @kategoriaId, @esedekesseg, @teljesites, @rendszeresseg)";
                    }
                    else //Rendszeres kiadás felvitel.
                    {
                        command.CommandText = "INSERT INTO [Kiadas] ([Megnevezes], [Osszeg], [KategoriaId], [Esedekesseg], [Rendszeresseg]) OUTPUT INSERTED.Id VALUES(@megnevezes, @osszeg, @kategoriaId, @esedekesseg, @rendszeresseg)";
                    }
                    command.ExecuteNonQuery();
                }
                else
                {
                    command.Parameters.AddWithValue("@id", id);
                    if (teljesitve) //Már teljesült, rendszeres kiadás módosítás.
                    {
                        command.Parameters.AddWithValue("@teljesites", uj.TeljesitesDatuma);
                        command.CommandText = "UPDATE [Kiadas] SET [Megnevezes] = @megnevezes, [Osszeg] = @osszeg, [KategoriaId] = @kategoriaId, [Esedekesseg] = @esedekesseg, [TeljesitesDatuma] = @teljesites, [Rendszeresseg] = @rendszeresseg WHERE [Id] = @id";
                        command.ExecuteNonQuery();

                    }
                    else //Rendszeres kiadás módosítás.
                    {
                        command.CommandText = "UPDATE [Kiadas] SET [Megnevezes] = @megnevezes, [Osszeg] = @osszeg, [KategoriaId] = @kategoriaId, [Esedekesseg] = @esedekesseg, [Rendszeresseg] = @rendszeresseg WHERE [Id] = @id";
                        command.ExecuteNonQuery();
                        command.CommandText = "UPDATE [Kiadas] SET [TeljesitesDatuma] = NULL WHERE [Id] = @id";
                        command.ExecuteNonQuery();
                    }
                }
                command.Transaction.Commit();
            }
            catch (Exception ex)
            {
                try
                {
                    if (command.Transaction != null)
                    {
                        command.Transaction.Rollback();
                    }
                }
                catch (Exception ex2)
                {
                    throw new ABKivetel("Végzetes hiba. További műveletek nem végezhetők.", ex2);
                }
                throw new ABKivetel("A művelet nem sikerült!", ex);
            }
        }
        public static void PenzmozgasTorles(Penzmozgas torol)
        {
            try
            {
                command.Parameters.Clear();
                command.Transaction = connection.BeginTransaction();
                command.Parameters.AddWithValue("@id", torol.Id);
                if (torol is Bevetel || torol is RendszeresBevetel)
                {
                    command.CommandText = "DELETE FROM [BevetelBizonylat] WHERE [BevetelId] = @id";
                    command.ExecuteNonQuery();
                    command.CommandText = "DELETE FROM [Bevetel] WHERE [Id] = @id";
                }
                else if (torol is Kiadas || torol is RendszeresKiadas)
                {
                    command.CommandText = "DELETE FROM [KiadasBizonylat] WHERE [KiadasId] = @id";
                    command.ExecuteNonQuery();
                    command.CommandText = "DELETE FROM [Kiadas] WHERE [Id] = @id";
                }
                command.ExecuteNonQuery();
                command.Transaction.Commit();
            }
            catch (Exception ex)
            {
                try
                {
                    if (command.Transaction != null)
                    {
                        command.Transaction.Rollback();
                    }
                }
                catch (Exception ex2)
                {
                    throw new ABKivetel("Végzetes hiba. További műveletek nem végezhetők.", ex2);
                }
                throw new ABKivetel("A törlés sikertelen volt", ex);
            }
        }
        public static void PenzmozgasTeljesites(Penzmozgas penzmozgas, DateTime teljesitesNapja)
        {
            try
            {
                command.Parameters.Clear();
                if (penzmozgas is Bevetel || penzmozgas is RendszeresBevetel)
                {
                    command.CommandText = "UPDATE [Bevetel] SET [TeljesitesDatuma] = @teljDatum WHERE [Id] = @id";
                }
                else if (penzmozgas is Kiadas || penzmozgas is RendszeresKiadas)
                {
                    command.CommandText = "UPDATE [Kiadas] SET [TeljesitesDatuma] = @teljDatum WHERE [Id] = @id";
                }
                command.Parameters.AddWithValue("@teljdatum", teljesitesNapja);
                command.Parameters.AddWithValue("@id", penzmozgas.Id);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ABKivetel("A Teljesítés beállítása nem sikerült", ex);
            }
        }
        #endregion

        #region Bizonylatok
        public static void BizonylatFelvitel(Bizonylat uj, Penzmozgas mihez)
        {
            try
            {
                command.Parameters.Clear();
                command.Transaction = connection.BeginTransaction();
                if (uj is BevetelBizonylat)
                {
                    command.CommandText = "INSERT INTO [BevetelBizonylat] ([BevetelId], [Fajlnev], [Fajl]) OUTPUT INSERTED.Id VALUES(@bevetelId, @fajlnev, @fajl)";
                    command.Parameters.AddWithValue("@bevetelId", mihez.Id);
                }
                else if (uj is KiadasBizonylat)
                {
                    command.CommandText = "INSERT INTO [KiadasBizonylat] ([KiadasId], [Fajlnev], [Fajl]) OUTPUT INSERTED.Id VALUES(@kiadasId, @fajlnev, @fajl)";
                    command.Parameters.AddWithValue("@kiadasId", mihez.Id);
                }
                command.Parameters.AddWithValue("@fajlnev", uj.Fajlnev);
                command.Parameters.AddWithValue("@fajl", uj.Fajl);
                command.ExecuteNonQuery();
                command.Transaction.Commit();
            }
            catch (Exception ex)
            {
                try
                {
                    if (command.Transaction != null)
                    {
                        command.Transaction.Rollback();
                    }
                }
                catch (Exception ex2)
                {
                    throw new ABKivetel("Végzetes hiba. További műveletek nem végezhetők.", ex2);
                }
                throw new ABKivetel("A bizonylat felvitele nem sikerült.", ex);
            }
        }
        public static List<Bizonylat> BizonylatListazas(Penzmozgas mihez)
        {
            try
            {
                command.Parameters.Clear();
                if (mihez is Bevetel)
                {
                    command.CommandText = "SELECT * FROM [BevetelBizonylat] WHERE [BevetelId] = @bevId";
                    command.Parameters.AddWithValue("@bevId", mihez.Id);
                }
                else if (mihez is Kiadas)
                {
                    command.CommandText = "SELECT * FROM [KiadasBizonylat] WHERE [KiadasId] = @kiId";
                    command.Parameters.AddWithValue("@kiId", mihez.Id);
                }
                List<Bizonylat> bizonylatLista = new List<Bizonylat>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (mihez is Bevetel)
                        {
                            bizonylatLista.Add(new BevetelBizonylat(
                                (int)reader["Id"],
                                reader["Fajlnev"].ToString(),
                                (byte[])reader["Fajl"]));
                        }
                        else if (mihez is Kiadas)
                        {
                            bizonylatLista.Add(new KiadasBizonylat(
                                (int)reader["Id"],
                                reader["Fajlnev"].ToString(),
                                (byte[])reader["Fajl"]));
                        }
                    }
                    reader.Close();
                }
                return bizonylatLista;
            }
            catch (Exception ex)
            {
                throw new ABKivetel("A bizonylatok listázása sikertelen.", ex);
            }
        }
        public static void BizonylatTorles(Bizonylat torles, Penzmozgas honnan)
        {
            try
            {
                command.Transaction = connection.BeginTransaction();
                command.Parameters.Clear();
                if (honnan is Bevetel) command.CommandText = "DELETE FROM [BevetelBizonylat] WHERE [Id] = @id";
                else if (honnan is Kiadas) command.CommandText = "DELETE FROM [KiadasBizonylat] WHERE [Id] = @id";
                command.Parameters.AddWithValue("@id", torles.Id);
                command.ExecuteNonQuery();
                command.Transaction.Commit();
            }
            catch (Exception ex)
            {
                try
                {
                    if (command.Transaction != null)
                    {
                        command.Transaction.Rollback();
                    }
                }
                catch (Exception ex2)
                {
                    throw new ABKivetel("Végzetes hiba. További műveletek nem végezhetők.", ex2);
                }

                throw new ABKivetel("A törlés sikertelen.", ex);
            }
        }
        public static bool VanBizonylatEhhezAPenzmozgashoz(Penzmozgas kerdesesPenzmozgas)
        {
            try
            {
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", kerdesesPenzmozgas.Id);
                if (kerdesesPenzmozgas is Bevetel)
                {
                    command.CommandText = "SELECT TOP(1) * FROM [BevetelBizonylat] WHERE [BevetelId] = @id";
                }
                else if (kerdesesPenzmozgas is Kiadas)
                {
                    command.CommandText = "SELECT TOP(1) * FROM [KiadasBizonylat] WHERE [KiadasId] = @id";
                }
                SqlDataReader reader = command.ExecuteReader();
                bool visszateres = false;
                reader.Read();
                if (reader.HasRows)
                {
                    visszateres = true;
                }
                reader.Close();
                return visszateres;
            }
            catch (Exception ex)
            {
                throw new ABKivetel("Hiba", ex);
            }
        }
        #endregion
    }
}