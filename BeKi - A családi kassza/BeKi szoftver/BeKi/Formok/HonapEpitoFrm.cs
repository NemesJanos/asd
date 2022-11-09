using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeKi
{
    public partial class HonapEpitoFrm : Form
    {
        List<Penzmozgas> valogatottLista;
        List<Penzmozgas> valogatottListaUj;
        List<Penzmozgas> forrasLista;
        string tipus;
        DateTime valasztottHonap;
        bool modositasTortent = false;

        internal HonapEpitoFrm(List<Penzmozgas> lista, string tipus, DateTime valasztottHonap)
        {
            InitializeComponent();
            this.tipus = tipus;
            this.valasztottHonap = valasztottHonap;
            label6.Text = valasztottHonap.ToString("yyyy. MMMM");
            valogatottLista = new List<Penzmozgas>();
            valogatottListaUj = new List<Penzmozgas>();
            foreach (var item in lista)
            {
                valogatottLista.Add(item);
                valogatottListaUj.Add(item);
            }
            forrasLista = new List<Penzmozgas>();
            HonapepitoTablazatFrissites();
            dateTimePicker1.Value = new DateTime((int)DateTime.Now.Year, (int)DateTime.Now.Month, 1).AddMonths(-1);
            dateTimePicker2.Value = new DateTime((int)DateTime.Now.Year, (int)DateTime.Now.Month, DateTime.DaysInMonth((int)DateTime.Now.Year, (int)DateTime.Now.Month)).AddMonths(-1);
            ForrasListaFrissites(dateTimePicker1.Value, dateTimePicker2.Value);
        }

        public void HonapepitoTablazatFrissites()
        {
            try
            {
                listView2.Items.Clear();
                string[] sor = new string[6];
                if (tipus == "bevetel")
                {
                    for (int i = 0; i < valogatottListaUj.Count; i++)
                    {
                        sor[0] = valogatottListaUj[i].Id.ToString();
                        sor[1] = (valogatottListaUj[i] as Bevetel).Kategoria.ToString();
                        sor[2] = valogatottListaUj[i].Megnevezes.ToString();
                        sor[3] = valogatottListaUj[i].Osszeg.ToString();
                        if (valogatottListaUj[i] is RendszeresBevetel)
                        {
                            sor[4] = (valogatottListaUj[i] as RendszeresBevetel).Rendszeresseg.ToString();
                        }
                        else
                        {
                            sor[4] = null;
                        }
                        sor[5] = valogatottListaUj[i].Esedekesseg.Date.ToString("yyyy.MM.dd.");
                        listView2.Items.Add(new ListViewItem(sor));
                    }
                }
                else if (tipus == "kiadas")
                {
                    for (int i = 0; i < valogatottListaUj.Count; i++)
                    {
                        sor[0] = valogatottListaUj[i].Id.ToString();
                        sor[1] = (valogatottListaUj[i] as Kiadas).Kategoria.ToString();
                        sor[2] = valogatottListaUj[i].Megnevezes.ToString();
                        sor[3] = valogatottListaUj[i].Osszeg.ToString();
                        if (valogatottListaUj[i] is RendszeresKiadas)
                        {
                            sor[4] = (valogatottListaUj[i] as RendszeresKiadas).Rendszeresseg.ToString();
                        }
                        else
                        {
                            sor[4] = null;
                        }
                        sor[5] = valogatottListaUj[i].Esedekesseg.Date.ToString("yyyy.MM.dd.");
                        listView2.Items.Add(new ListViewItem(sor));
                    }
                }
                foreach (ListViewItem item in listView2.Items)
                {
                    if (item.Index % 2 == 0)
                    {
                        item.BackColor = Color.FromName("ControlLight");
                    }
                    else
                    {
                        item.BackColor = Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                UzenetAblak.Ablak("A bal oldali lista feltöltése sikertelen " + ex.Message, "Sikertelen", Szint.Hiba);
            }
        }
        public void ForrasListaFrissites(DateTime mettol, DateTime meddig)
        {
            try
            {
                forrasLista.Clear();
                listView1.Items.Clear();
                if (tipus == "bevetel")
                {
                    List<Bevetel> lista = new List<Bevetel>();
                    lista = ABKezelo.BevetelListazas(mettol, meddig);
                    foreach (var item in lista)
                    {
                        forrasLista.Add(item);
                    }
                    string[] sor = new string[6];
                    for (int i = 0; i < forrasLista.Count; i++)
                    {
                        sor[0] = forrasLista[i].Id.ToString();
                        sor[1] = (forrasLista[i] as Bevetel).Kategoria.ToString();
                        sor[2] = forrasLista[i].Megnevezes.ToString();
                        sor[3] = forrasLista[i].Osszeg.ToString();
                        if (forrasLista[i] is RendszeresBevetel)
                        {
                            sor[4] = (forrasLista[i] as RendszeresBevetel).Rendszeresseg.ToString();
                        }
                        else
                        {
                            sor[4] = null;
                        }
                        sor[5] = forrasLista[i].Esedekesseg.Date.ToString("yyyy.MM.dd.");
                        listView1.Items.Add(new ListViewItem(sor));
                    }
                }
                else if (tipus == "kiadas")
                {
                    List<Kiadas> lista = new List<Kiadas>();
                    lista = ABKezelo.KiadasListazas(mettol, meddig);
                    foreach (var item in lista)
                    {
                        forrasLista.Add(item);
                    }
                    string[] sor = new string[6];
                    for (int i = 0; i < forrasLista.Count; i++)
                    {
                        sor[0] = forrasLista[i].Id.ToString();
                        sor[1] = (forrasLista[i] as Kiadas).Kategoria.ToString();
                        sor[2] = forrasLista[i].Megnevezes.ToString();
                        sor[3] = forrasLista[i].Osszeg.ToString();
                        if (forrasLista[i] is RendszeresKiadas)
                        {
                            sor[4] = (forrasLista[i] as RendszeresKiadas).Rendszeresseg.ToString();
                        }
                        else
                        {
                            sor[4] = null;
                        }
                        sor[5] = forrasLista[i].Esedekesseg.Date.ToString("yyyy.MM.dd.");
                        listView1.Items.Add(new ListViewItem(sor));
                    }
                }
                foreach (ListViewItem item in listView1.Items)
                {
                    if (item.Index % 2 == 0)
                    {
                        item.BackColor = Color.FromName("ControlLight");
                    }
                    else
                    {
                        item.BackColor = Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                UzenetAblak.Ablak("A bal oldali lista feltöltése sikertelen " + ex.Message, "Sikertelen", Szint.Hiba);
            }
        }

        private void Hozzaadas()
        {
            try
            {
                if (listView1.SelectedIndices.Count > 0)
                {
                    foreach (var item in forrasLista)
                    {
                        for (int i = 0; i < listView1.SelectedIndices.Count; i++)
                        {
                            if (listView1.SelectedItems[i].SubItems[0].Text == item.Id.ToString())
                            {
                                if (item is RendszeresBevetel)
                                {
                                    valogatottListaUj.Add(new RendszeresBevetel(
                                        (item as RendszeresBevetel).Kategoria,
                                        item.Megnevezes,
                                        item.Osszeg,
                                        new DateTime(valasztottHonap.Year, valasztottHonap.Month, (item.Esedekesseg.Day > DateTime.DaysInMonth(valasztottHonap.Year, valasztottHonap.Month)) ? DateTime.DaysInMonth(valasztottHonap.Year, valasztottHonap.Month) : item.Esedekesseg.Day),
                                        (item as RendszeresBevetel).Rendszeresseg
                                        ));
                                }
                                else if (item is Bevetel)
                                {
                                    valogatottListaUj.Add(new Bevetel(
                                        (item as Bevetel).Kategoria,
                                        item.Megnevezes,
                                        item.Osszeg,
                                        new DateTime(valasztottHonap.Year, valasztottHonap.Month, (item.Esedekesseg.Day > DateTime.DaysInMonth(valasztottHonap.Year, valasztottHonap.Month)) ? DateTime.DaysInMonth(valasztottHonap.Year, valasztottHonap.Month) : item.Esedekesseg.Day)
                                        ));
                                }
                                else if (item is RendszeresKiadas)
                                {
                                    valogatottListaUj.Add(new RendszeresKiadas(
                                        (item as RendszeresKiadas).Kategoria,
                                        item.Megnevezes,
                                        item.Osszeg,
                                        new DateTime(valasztottHonap.Year, valasztottHonap.Month, (item.Esedekesseg.Day > DateTime.DaysInMonth(valasztottHonap.Year, valasztottHonap.Month)) ? DateTime.DaysInMonth(valasztottHonap.Year, valasztottHonap.Month) : item.Esedekesseg.Day),
                                        (item as RendszeresKiadas).Rendszeresseg
                                        ));
                                }
                                else if (item is Kiadas)
                                {
                                    valogatottListaUj.Add(new Kiadas(
                                        (item as Kiadas).Kategoria,
                                        item.Megnevezes,
                                        item.Osszeg,
                                        new DateTime(valasztottHonap.Year, valasztottHonap.Month, (item.Esedekesseg.Day > DateTime.DaysInMonth(valasztottHonap.Year, valasztottHonap.Month)) ? DateTime.DaysInMonth(valasztottHonap.Year, valasztottHonap.Month) : item.Esedekesseg.Day)
                                        ));

                                }
                            }
                        }
                    }
                    HonapepitoTablazatFrissites();
                    modositasTortent = true;
                }
                else
                {
                    UzenetAblak.Ablak("A művelethez jelöljön ki egy vagy több tételt!", "Nincs kijelölt tétel", Szint.Info);
                }
            }
            catch (ABKivetel ex)
            {
                UzenetAblak.Ablak(ex.Message, "Sikertelen", Szint.Hiba);
            }
            catch (Exception ex)
            {
                UzenetAblak.Ablak(ex.Message, "Sikertelen", Szint.Hiba);
            }
        }
        private void Torles()
        {
            try
            {
                if (listView2.SelectedIndices.Count > 0)
                {
                    for (int j = listView2.Items.Count - 1; j > -1; j--)
                    {
                        if (listView2.Items[j].Selected == true)
                        {
                            bool vaneBizonylat = ABKezelo.VanBizonylatEhhezAPenzmozgashoz(valogatottListaUj[j]);
                            if (vaneBizonylat == true)
                            {
                                if (UzenetAblak.Ablak("Ehhez a tételhez van hozzárendelt bizonylat. Ha törli a tételt, törlődik a bizonylat is.\nBiztosan törölni szeretné a tételt?", "Megerősítés kérése", Szint.Kerdes) == DialogResult.Yes)
                                {
                                    valogatottListaUj.RemoveAt(j);
                                }
                            }
                            else
                            {
                                valogatottListaUj.RemoveAt(j);
                            }
                        }
                    }

                    HonapepitoTablazatFrissites();
                    modositasTortent = true;
                }

                else
                {
                    UzenetAblak.Ablak("A művelethez jelöljön ki egy vagy több tételt!", "Nincs kijelölt tétel", Szint.Info);
                }
            }
            catch (ABKivetel ex)
            {
                UzenetAblak.Ablak(ex.Message, "Sikertelen", Szint.Hiba);
            }
            catch (Exception ex)
            {
                UzenetAblak.Ablak(ex.Message, "Sikertelen", Szint.Hiba);
            }
        }

        private void betoltesBtn_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value.Date <= dateTimePicker2.Value.Date &&
                dateTimePicker1.Value.Date >= new DateTime(2019, 01, 01) &&
                dateTimePicker2.Value.Date >= new DateTime(2019, 01, 01) &&
                dateTimePicker1.Value.Date <= new DateTime(2050, 12, 31) &&
                dateTimePicker2.Value.Date <= new DateTime(2050, 12, 31))
            {
                ForrasListaFrissites(dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
            }
            else
            {
                UzenetAblak.Ablak("Az időpontoknak 2019-01-01 és 2050-12-31 közöttieknek kell lenniük.", "Helytelen dátumválasztás", Szint.Info);
            }
        }
        private void hozzaadasBtn_Click(object sender, EventArgs e)
        {
            Hozzaadas();
        }
        private void torlesBtn_Click(object sender, EventArgs e)
        {
            Torles();
        }
        private void okBtn_Click(object sender, EventArgs e)
        {
            if (modositasTortent && UzenetAblak.Ablak("A módosítások még nem mentek végbe az adatbázisban. Az igen gombra kattintva végrehajtódnak.\nBiztosan végrehajtja a módosításokat?", "Megerősítés kérése", Szint.FontosKerdes) == DialogResult.Yes)
            {
                try
                {
                    foreach (var item in valogatottListaUj)
                    {
                        if (item.Id == 0)
                        {
                            item.Esedekesseg = new DateTime(valasztottHonap.Year, valasztottHonap.Month, item.Esedekesseg.Day);
                            if (item is RendszeresBevetel) ABKezelo.RendszeresBevetelFelvitel((RendszeresBevetel)item, false, 0);
                            else if (item is Bevetel) ABKezelo.BevetelFelvitel((Bevetel)item, false, 0);
                            else if (item is RendszeresKiadas) ABKezelo.RendszeresKiadasFelvitel((RendszeresKiadas)item, false, 0);
                            else if (item is Kiadas) ABKezelo.KiadasFelvitel((Kiadas)item, false, 0);
                        }
                    }
                    List<Penzmozgas> torlendo = new List<Penzmozgas>();
                    for (int i = 0; i < valogatottLista.Count; i++)
                    {
                        if (!valogatottListaUj.Contains(valogatottLista[i]))
                        {
                            torlendo.Add(valogatottLista[i]);
                        }

                    }
                    foreach (var item in torlendo)
                    {
                        ABKezelo.PenzmozgasTorles(item);
                    }
                }
                catch (ABKivetel ex)
                {
                    UzenetAblak.Ablak(ex.Message, "Sikertelen", Szint.Hiba);
                }
                catch (Exception ex)
                {
                    UzenetAblak.Ablak(ex.Message, "Sikertelen", Szint.Hiba);
                }
            }
            else
            {
                DialogResult = DialogResult.None;
            }
        }
        private void cancelBtn_Click(object sender, EventArgs e)
        {
            if (modositasTortent && UzenetAblak.Ablak("Elveti módosításokat?", "Megerősítés kérése", Szint.FontosKerdes) == DialogResult.No)
            {
                DialogResult = DialogResult.None;
            }
        }
    }
}
