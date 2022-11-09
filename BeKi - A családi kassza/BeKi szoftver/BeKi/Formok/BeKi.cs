using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeKi
{
    public partial class BeKi : Form
    {

        List<Bevetel> bevetelLista;
        List<Kiadas> kiadasLista;
        List<Bevetel> rendezetlenBevetel;
        List<Kiadas> rendezetlenKiadas;
        DateTime mettol;
        DateTime meddig;
        DataGridView kijeloltDGV;
        bool tablazatFeltoltesTortenik = false; // A táblázatfeltöltés idejére off: DGV_SelectionChanged
        private static bool systemShutDown = false;
        NotifyIcon icon;

        public BeKi()
        {
            InitializeComponent();
            NotifyIconLetrehozas();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //Indulaskor a jelenlegi honap beállítasa.
            evszamValasztoNumUpDown.Value = DateTime.Now.Year;
            honapValasztoComboBox.SelectedIndex = DateTime.Now.Month - 1;
            try
            {
                ABKezelo.Csatlakozas();
            }
            catch (ABKivetel ex)
            {
                UzenetAblak.Ablak(ex.Message, "Sikertelen csatlakozás", Szint.Hiba);
            }

            mettol = new DateTime((int)evszamValasztoNumUpDown.Value, honapValasztoComboBox.SelectedIndex + 1, 1);
            meddig = new DateTime((int)evszamValasztoNumUpDown.Value, honapValasztoComboBox.SelectedIndex + 1, DateTime.DaysInMonth((int)evszamValasztoNumUpDown.Value, honapValasztoComboBox.SelectedIndex + 1));

            ListakFeltoltese(mettol, meddig);
            TablafejlecekFeltoltese();
            TablazatokFeltoltese();
            DGVkijelolesekMegszuntetese();
            evszamValasztoNumUpDown.ValueChanged += evszamValasztoNumUpDown_ValueChanged;
            honapValasztoComboBox.SelectedIndexChanged += honapValasztoComboBox_SelectedIndexChanged;
            try
            {
                if (ABKezelo.PenzmozgasErtesitesBeallitasLekerdezes() == true)
                {
                    idozito.Tick += idozito_Tick;
                    idozito.Start();
                    PenzmozgasHataridoEllenorzes();
                }
            }
            catch (ABKivetel ex)
            {
                UzenetAblak.Ablak(ex.Message + "\nAdatai biztonságban vannak, folytathatja a munkát.", "Értesítési hiba", Szint.Info);
            }
        }
        private void BeKi_Activated(object sender, EventArgs e)
        {
            if (Program.SystemTrayStart == true)
            {
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
                Program.SystemTrayStart = false;
            }
        }
        private void NotifyIconLetrehozas()
        {
            try
            {
                icon = new NotifyIcon();

                icon.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
                icon.Text = @"BeKi - A családi kassza";
                icon.ContextMenu = new ContextMenu(new MenuItem[] {
                         new MenuItem("Főablak megnyitása", (s, e) => {this.Show(); this.WindowState = FormWindowState.Normal; }),
                         new MenuItem("Kilépés", (s, e) => { Application.Exit(); })
                     });
                icon.Visible = true;
                icon.DoubleClick += Icon_DoubleClick;
            }
            catch (Exception ex)
            {
                UzenetAblak.Ablak(ex.Message, "Hiba", Szint.Hiba);
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //https://stackoverflow.com/questions/2683679/how-to-know-user-has-clicked-x-or-the-close-button
            if (new StackTrace().GetFrames().Any(x => x.GetMethod().Name == "Exit") || systemShutDown == true) //systray ikon Kilépés (Application.Exit)
            {
                systemShutDown = false;
                icon.Visible = false;
                try
                {
                    ABKezelo.KapcsolatBontas();
                }
                catch (Exception)
                {
                }
            }
            else
            {
                e.Cancel = true;
                Hide();
            }
        }
        protected override void WndProc(ref Message m)
        {
            //https://docs.microsoft.com/en-us/dotnet/api/microsoft.win32.systemevents.sessionending?redirectedfrom=MSDN&view=netframework-4.8
            //Ha a program fut a tálcán, ezzel a felülírt metódussal lehet elkapni a Win shutdown-t. A leállítást megakasztva van ideje lezárni az AB kapcsolatot.
            if (m.Msg == 0x11)
            {
                systemShutDown = true;
            }
            base.WndProc(ref m);
        }

        #region metódusok, funkciók
        private void DGVkijelolesekMegszuntetese()
        {
            foreach (Control item in Controls)
            {
                if (item is DataGridView dgv)
                {
                    dgv.ClearSelection();
                }
            }
            kijeloltDGV = null;
            bizonylatBtn.ForeColor = Color.Black;
        }
        private void TablafejlecekFeltoltese()
        {
            try
            {
                foreach (Control item in Controls)
                {
                    if (item is DataGridView dgv)
                    {
                        dgv.Columns.Add("Kategoria", "Kategória");
                        dgv.Columns.Add("Megnevezes", "Megnevezés");
                        dgv.Columns.Add("Osszeg", "Összeg");
                        dgv.Columns.Add("Esedekesseg", "Esedékesség");
                        dgv.Columns.Add("Teljesitve", "Teljesítve");
                        dgv.Columns.Add("Rendszeresseg", "Rendszeresség");
                    }
                }
            }
            catch (Exception ex)
            {
                UzenetAblak.Ablak(ex.Message + " " + "\nValamelyik tábla feltöltése nem sikerült!", "Hiba történt", Szint.Hiba);
                try
                {
                    ABKezelo.KapcsolatBontas();
                }
                catch (Exception)
                {
                }
                finally
                {
                    Application.Exit();
                }
            }
            foreach (Control item in Controls)
            {
                if (item is DataGridView dgv)
                {
                    dgv.Columns[1].Width = 130;
                    dgv.Columns[2].Width = 80;
                    dgv.Columns[3].Width = 79;
                    dgv.Columns[4].Width = 79;
                    dgv.Columns[5].Width = 90;
                }
            }
        }
        private void ListakFeltoltese(DateTime mettol, DateTime meddig)
        {
            try
            {
                bevetelLista = new List<Bevetel>();
                bevetelLista = ABKezelo.BevetelListazas(mettol, meddig);
                kiadasLista = new List<Kiadas>();
                kiadasLista = ABKezelo.KiadasListazas(mettol, meddig);
                Osszegzes();
            }
            catch (ABKivetel ex)
            {
                UzenetAblak.Ablak(ex.Message, "Sikertelen", Szint.Hiba);
            }
            catch (Exception ex)
            {
                UzenetAblak.Ablak(ex.Message, "Hiba", Szint.Hiba);
            }
        }
        private void TablazatokFeltoltese()
        {
            try
            {
                bevetelDGV.Rows.Clear();
                kiadasDGV.Rows.Clear();

                mettol = new DateTime((int)evszamValasztoNumUpDown.Value, honapValasztoComboBox.SelectedIndex + 1, 1);
                meddig = new DateTime((int)evszamValasztoNumUpDown.Value, honapValasztoComboBox.SelectedIndex + 1, DateTime.DaysInMonth((int)evszamValasztoNumUpDown.Value, honapValasztoComboBox.SelectedIndex + 1));
                ListakFeltoltese(mettol, meddig);

                tablazatFeltoltesTortenik = true;
                //Bevételtáblázat feltöltése
                if (bevetelLista.Count > 0)
                {
                    foreach (var item in bevetelLista)
                    {
                        bevetelDGV.Rows.Add();
                        bevetelDGV.Rows[bevetelDGV.Rows.Count - 1].HeaderCell.Value = item.Id;
                        bevetelDGV.Rows[bevetelDGV.Rows.Count - 1].Cells["Kategoria"].Value = item.Kategoria;
                        bevetelDGV.Rows[bevetelDGV.Rows.Count - 1].Cells["Megnevezes"].Value = item.Megnevezes;
                        bevetelDGV.Rows[bevetelDGV.Rows.Count - 1].Cells["Osszeg"].Value = item.Osszeg.ToString("###,###,###,###");
                        bevetelDGV.Rows[bevetelDGV.Rows.Count - 1].Cells["Esedekesseg"].Value = item.Esedekesseg.ToString("yyyy.MM.dd.");
                        bevetelDGV.Rows[bevetelDGV.Rows.Count - 1].Cells["Teljesitve"].Value = (item.TeljesitesDatuma > new DateTime(2020 - 01 - 01)) ? item.TeljesitesDatuma.ToString("yyyy.MM.dd.") : "";
                        if (item.GetType() == typeof(RendszeresBevetel))
                        {
                            bevetelDGV.Rows[bevetelDGV.Rows.Count - 1].Cells["Rendszeresseg"].Value = (item as RendszeresBevetel).Rendszeresseg;
                        }
                    }
                }

                //Kiadástáblázat feltöltése
                if (kiadasLista.Count > 0)
                {
                    foreach (var item in kiadasLista)
                    {
                        kiadasDGV.Rows.Add();
                        kiadasDGV.Rows[kiadasDGV.Rows.Count - 1].HeaderCell.Value = item.Id;
                        kiadasDGV.Rows[kiadasDGV.Rows.Count - 1].Cells["Kategoria"].Value = item.Kategoria;
                        kiadasDGV.Rows[kiadasDGV.Rows.Count - 1].Cells["Megnevezes"].Value = item.Megnevezes;
                        kiadasDGV.Rows[kiadasDGV.Rows.Count - 1].Cells["Osszeg"].Value = item.Osszeg.ToString("###,###,###,###");
                        kiadasDGV.Rows[kiadasDGV.Rows.Count - 1].Cells["Esedekesseg"].Value = item.Esedekesseg.ToString("yyyy.MM.dd.");
                        kiadasDGV.Rows[kiadasDGV.Rows.Count - 1].Cells["Teljesitve"].Value = (item.TeljesitesDatuma > new DateTime(2020 - 01 - 01)) ? item.TeljesitesDatuma.ToString("yyyy.MM.dd.") : "";
                        if (item.GetType() == typeof(RendszeresKiadas))
                        {
                            kiadasDGV.Rows[kiadasDGV.Rows.Count - 1].Cells["Rendszeresseg"].Value = (item as RendszeresKiadas).Rendszeresseg;
                        }
                    }
                }
                tablazatFeltoltesTortenik = false;
            }
            catch (Exception ex)
            {
                UzenetAblak.Ablak(ex.Message + " " + "\nValamelyik tábla feltöltése nem sikerült! A program kilép.", "Hiba történt", Szint.Hiba);
                try
                {
                    ABKezelo.KapcsolatBontas();
                }
                catch (Exception)
                {
                }
                finally
                {
                    Application.Exit();
                }
            }
        }
        private void KijeloltDGVkereses()
        {
            foreach (Control item in Controls)
            {
                if (item is DataGridView dgv && dgv.SelectedCells.Count > 0)
                {
                    kijeloltDGV = dgv;
                    break;
                }
            }
        }
        private bool DuplikacioEllenorzes(Penzmozgas tetel)
        {
            //Az új hónap létrehozásához ellenőrzés
            try
            {
                if (tetel is RendszeresBevetel && bevetelLista.Count > 0)
                {
                    foreach (var item in bevetelLista)
                    {
                        if ((tetel as RendszeresBevetel).CompareTo(item as RendszeresBevetel) == 0)
                        {
                            return false;
                        }
                    }
                }
                else if (tetel is RendszeresKiadas && kiadasLista.Count > 0)
                {
                    foreach (var item in kiadasLista)
                    {
                        if ((tetel as RendszeresKiadas).CompareTo(item as RendszeresKiadas) == 0)
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UzenetAblak.Ablak(ex.Message, "Hiba történt", Szint.Hiba);
            }
            return true;

        }
        private void UjHonapLetrehozas()
        {
            try
            {
                List<Bevetel> regiBevetelek = new List<Bevetel>();
                List<Kiadas> regiKiadasok = new List<Kiadas>();
                regiBevetelek = ABKezelo.Bevetel_ElmultEvListazasa(meddig);
                regiKiadasok = ABKezelo.Kiadas_ElmultEvListazasa(meddig);
                int bevSor = bevetelDGV.Rows.Count;
                int kiadSor = kiadasDGV.Rows.Count;

                foreach (var item in regiBevetelek)
                {
                    if (DuplikacioEllenorzes(item))
                    {
                        if (item is RendszeresBevetel havonta && havonta.Rendszeresseg == Rendszeresseg.Havonta && havonta.Esedekesseg.ToString("yyyy.MM") == (meddig.AddMonths(-1).ToString("yyyy.MM")))
                        {
                            ABKezelo.RendszeresBevetelFelvitel(new RendszeresBevetel(havonta.Kategoria, havonta.Megnevezes, havonta.Osszeg, havonta.Esedekesseg.AddMonths(1), havonta.Rendszeresseg), false, 0);
                        }
                        else if (item is RendszeresBevetel negyedEvente && negyedEvente.Rendszeresseg == Rendszeresseg.Negyedévente && negyedEvente.Esedekesseg.ToString("yyyy.MM") == (meddig.AddMonths(-3).ToString("yyyy.MM")))
                        {
                            ABKezelo.RendszeresBevetelFelvitel(new RendszeresBevetel(negyedEvente.Kategoria, negyedEvente.Megnevezes, negyedEvente.Osszeg, negyedEvente.Esedekesseg.AddMonths(3), negyedEvente.Rendszeresseg), false, 0);
                        }
                        else if (item is RendszeresBevetel felEvente && felEvente.Rendszeresseg == Rendszeresseg.Félévente && felEvente.Esedekesseg.ToString("yyyy.MM") == (meddig.AddMonths(-6).ToString("yyyy.MM")))
                        {
                            ABKezelo.RendszeresBevetelFelvitel(new RendszeresBevetel(felEvente.Kategoria, felEvente.Megnevezes, felEvente.Osszeg, felEvente.Esedekesseg.AddMonths(6), felEvente.Rendszeresseg), false, 0);
                        }
                        else if (item is RendszeresBevetel evente && evente.Rendszeresseg == Rendszeresseg.Évente && evente.Esedekesseg.ToString("yyyy.MM") == (meddig.AddMonths(-12).ToString("yyyy.MM")))
                        {
                            ABKezelo.RendszeresBevetelFelvitel(new RendszeresBevetel(evente.Kategoria, evente.Megnevezes, evente.Osszeg, evente.Esedekesseg.AddMonths(12), evente.Rendszeresseg), false, 0);
                        }
                    }
                }
                foreach (var item in regiKiadasok)
                {
                    if (DuplikacioEllenorzes(item))
                    {
                        if (item is RendszeresKiadas havonta && havonta.Rendszeresseg == Rendszeresseg.Havonta && havonta.Esedekesseg.ToString("yyyy.MM") == (meddig.AddMonths(-1).ToString("yyyy.MM")))
                        {
                            ABKezelo.RendszeresKiadasFelvitel(new RendszeresKiadas(havonta.Kategoria, havonta.Megnevezes, havonta.Osszeg, havonta.Esedekesseg.AddMonths(1), havonta.Rendszeresseg), false, 0);
                        }
                        else if (item is RendszeresKiadas negyedEvente && negyedEvente.Rendszeresseg == Rendszeresseg.Negyedévente && negyedEvente.Esedekesseg.ToString("yyyy.MM") == (meddig.AddMonths(-3).ToString("yyyy.MM")))
                        {
                            ABKezelo.RendszeresKiadasFelvitel(new RendszeresKiadas(negyedEvente.Kategoria, negyedEvente.Megnevezes, negyedEvente.Osszeg, negyedEvente.Esedekesseg.AddMonths(3), negyedEvente.Rendszeresseg), false, 0);
                        }
                        else if (item is RendszeresKiadas felEvente && felEvente.Rendszeresseg == Rendszeresseg.Félévente && felEvente.Esedekesseg.ToString("yyyy.MM") == (meddig.AddMonths(-6).ToString("yyyy.MM")))
                        {
                            ABKezelo.RendszeresKiadasFelvitel(new RendszeresKiadas(felEvente.Kategoria, felEvente.Megnevezes, felEvente.Osszeg, felEvente.Esedekesseg.AddMonths(6), felEvente.Rendszeresseg), false, 0);
                        }
                        else if (item is RendszeresKiadas evente && evente.Rendszeresseg == Rendszeresseg.Évente && evente.Esedekesseg.ToString("yyyy.MM") == (meddig.AddMonths(-12).ToString("yyyy.MM")))
                        {
                            ABKezelo.RendszeresKiadasFelvitel(new RendszeresKiadas(evente.Kategoria, evente.Megnevezes, evente.Osszeg, evente.Esedekesseg.AddMonths(12), evente.Rendszeresseg), false, 0);
                        }
                    }
                }
                TablazatokFeltoltese();
                if (bevetelDGV.Rows.Count == bevSor && kiadasDGV.Rows.Count == kiadSor)
                {
                    UzenetAblak.Ablak("Nincs generálható tétel az előző hónapok alapján.", "Nincs érintett adat", Szint.Info);
                }
            }
            catch (ABKivetel ex)
            {
                UzenetAblak.Ablak(ex.Message, "Hiba történt", Szint.Hiba);
            }
            catch (Exception ex)
            {
                UzenetAblak.Ablak(ex.Message, "Hiba történt", Szint.Hiba);
            }

        }
        private void PenzmozgasModositas()
        {
            KijeloltDGVkereses();

            if (kijeloltDGV != null)
            {
                if (kijeloltDGV.Name == "bevetelDGV" && bevetelLista.Count > 0)
                {
                    foreach (var item in bevetelLista)
                    {
                        if (item.Id == (int)kijeloltDGV.SelectedRows[0].HeaderCell.Value)
                        {
                            PenzmozgasFrm frm = new PenzmozgasFrm(item);
                            frm.ShowDialog();
                        }
                    }
                }
                else if (kijeloltDGV.Name == "kiadasDGV" && kiadasLista.Count > 0)
                {
                    foreach (var item in kiadasLista)
                    {
                        if (item.Id == (int)kijeloltDGV.SelectedRows[0].HeaderCell.Value)
                        {
                            PenzmozgasFrm frm = new PenzmozgasFrm(item);
                            frm.ShowDialog();
                        }
                    }
                }
                TablazatokFeltoltese();
                DGVkijelolesekMegszuntetese();
            }
            else
            {
                UzenetAblak.Ablak("A módosításhoz ki kell jelölni egy bevétel vagy kiadás sort!", "Nincs kijelölt sor", Szint.Info);
            }
        }
        private void PenzmozgasTorles()
        {
            KijeloltDGVkereses();
            if (kijeloltDGV != null)
            {
                if (UzenetAblak.Ablak("Szeretné töröni a kijelölt tételt?", "Törlés...", Szint.Kerdes) == DialogResult.Yes)
                {
                    if (kijeloltDGV.Name == bevetelDGV.Name) //Bevétel törlés
                    {
                        int id = (int)bevetelDGV.SelectedRows[0].HeaderCell.Value;
                        foreach (var item in bevetelLista) //Azért "var", hogy ne kelljen dupla foreach, mert ebben van tárolva a "Bevetel" és a "RendszeresBevetel" is. (Kiadás ugyanígy.) 
                        {
                            if (item.Id == id)
                            {
                                try
                                {
                                    ABKezelo.PenzmozgasTorles(item);
                                }
                                catch (ABKivetel ex)
                                {
                                    UzenetAblak.Ablak(ex.Message, "Sikertelen", Szint.Hiba);
                                }
                            }
                        }
                    }
                    if (kijeloltDGV.Name == kiadasDGV.Name) //Kiadás törlés
                    {
                        int id = (int)kiadasDGV.SelectedRows[0].HeaderCell.Value;
                        foreach (var item in kiadasLista)
                        {
                            if (item.Id == id)
                            {
                                try
                                {
                                    ABKezelo.PenzmozgasTorles(item);
                                }
                                catch (ABKivetel ex)
                                {
                                    UzenetAblak.Ablak(ex.Message, "Sikertelen", Szint.Hiba);
                                }
                            }
                        }
                    }
                    TablazatokFeltoltese();
                }
                DGVkijelolesekMegszuntetese();
            }
            else
            {
                UzenetAblak.Ablak("A törléshez ki kell jelölni egy bevétel vagy kiadás sort!", "Nincs kijelölt sor", Szint.Info);
            }
        }
        private void HaVanBizonylatZolduljonAGomb(DataGridView dgv)
        {
            bool vanE = false;
            if (dgv.Name == "bevetelDGV" && bevetelLista.Count > 0)
            {
                foreach (var item in bevetelLista)
                {
                    if (item.Id == (int)dgv.SelectedRows[0].HeaderCell.Value)
                    {
                        try
                        {
                            vanE = ABKezelo.VanBizonylatEhhezAPenzmozgashoz(item);
                        }
                        catch (ABKivetel ex)
                        {
                            UzenetAblak.Ablak(ex.Message, "Sikertelen bizonylatellenőrzés.", Szint.Info);
                        }
                    }
                }
            }
            else if (dgv.Name == "kiadasDGV" && kiadasLista.Count > 0)
            {
                foreach (var item in kiadasLista)
                {
                    if (item.Id == (int)dgv.SelectedRows[0].HeaderCell.Value)
                    {
                        try
                        {
                            vanE = ABKezelo.VanBizonylatEhhezAPenzmozgashoz(item);
                        }
                        catch (ABKivetel ex)
                        {
                            UzenetAblak.Ablak(ex.Message, "Sikertelen bizonylatellenőrzés.", Szint.Info);
                        }
                    }
                }
            }
            if (vanE)
            {
                bizonylatBtn.ForeColor = Color.Green;
            }
            else
            {
                bizonylatBtn.ForeColor = Color.Black;
            }
        }
        private void Osszegzes()
        {
            int bevetelOsszeg = 0;
            int kiadasOsszeg = 0;
            foreach (var item in bevetelLista)
            {
                bevetelOsszeg += item.Osszeg;
            }
            foreach (var item in kiadasLista)
            {
                kiadasOsszeg += item.Osszeg;
            }

            if (bevetelOsszeg >= 0 && bevetelOsszeg < 999999999)
            {
                richTextBox1.Text = bevetelOsszeg.ToString("###,###,###");
            }
            else
            {
                richTextBox1.Text = "############";
            }
            if (kiadasOsszeg >= 0 && kiadasOsszeg < 999999999)
            {
                richTextBox2.Text = kiadasOsszeg.ToString("###,###,###");
            }
            else
            {
                richTextBox2.Text = "############";
            }

            foreach (Control item in groupBox1.Controls)
            {
                if (item is RichTextBox rtb)
                {
                    rtb.SelectAll();
                    rtb.SelectionAlignment = HorizontalAlignment.Right;
                    rtb.DeselectAll();
                }
            }
        }
        private void PenzmozgasHataridoEllenorzes()
        {
            try
            {
                rendezetlenBevetel = new List<Bevetel>();
                rendezetlenKiadas = new List<Kiadas>();
                foreach (var item in bevetelLista)
                {
                    if (item.Esedekesseg.Date.Month == DateTime.Now.Month && item.Esedekesseg <= DateTime.Now && item.TeljesitesDatuma < new DateTime(1000, 01, 01))
                    {
                        rendezetlenBevetel.Add(item);
                    }
                }
                foreach (var item in kiadasLista)
                {
                    if (item.Esedekesseg.Date.Month == DateTime.Now.Month && item.Esedekesseg <= DateTime.Now && item.TeljesitesDatuma < new DateTime(1000, 01, 01))
                    {
                        rendezetlenKiadas.Add(item);
                    }
                }
                icon.BalloonTipTitle = "Rendezetlen tételek!";
                if (rendezetlenBevetel.Count > 0 && rendezetlenKiadas.Count > 0)
                {
                    icon.BalloonTipText = "Lejárt esedékességű bevételek és kiadások vannak az aktuális hónapban!";
                }
                else if (rendezetlenBevetel.Count > 0)
                {
                    icon.BalloonTipText = "Lejárt esedékességű bevételek vannak az aktuális hónapban!";
                }
                else if (rendezetlenKiadas.Count > 0)
                {
                    icon.BalloonTipText = "Lejárt esedékességű kiadások vannak az aktuális hónapban!";
                }
                if (rendezetlenBevetel.Count > 0 || rendezetlenKiadas.Count > 0)
                {
                    icon.ShowBalloonTip(3000);
                    icon.BalloonTipClicked += Icon_DoubleClick; 
                }
            }
            catch (Exception ex)
            {
                UzenetAblak.Ablak(ex.Message, "", Szint.Hiba);
            }
        }
        #endregion

        #region Gombok, kattintások, események
        private void honapGeneralasBtn_Click(object sender, EventArgs e)
        {
            if (UzenetAblak.Ablak("Szeretné létrehozni a pénzmozgásokat az elmúlt időszakok alapján?", "Megerősítés kérése", Szint.Kerdes) == DialogResult.Yes)
            {
                UjHonapLetrehozas();
                DGVkijelolesekMegszuntetese();
            }
        }
        private void modositasBtn_Click(object sender, EventArgs e)
        {
            PenzmozgasModositas();
        }
        private void penzmozgasBtn_Click(object sender, EventArgs e)
        {
            KijeloltDGVkereses();
            PenzmozgasFrm frm = new PenzmozgasFrm(mettol);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                TablazatokFeltoltese();
            }
            DGVkijelolesekMegszuntetese();
        }
        private void penzmozgasTorlesBtn_Click(object sender, EventArgs e)
        {
            PenzmozgasTorles();
        }
        private void bizonylatBtn_Click(object sender, EventArgs e)
        {
            KijeloltDGVkereses();
            if (kijeloltDGV != null)
            {
                BizonylatFrm frm;
                if (kijeloltDGV.Name == bevetelDGV.Name) //Bevételhez
                {
                    int id = (int)bevetelDGV.SelectedRows[0].HeaderCell.Value;
                    foreach (var item in bevetelLista) //Azért "var", hogy ne kelljen dupla foreach, mert ebben van tárolva a "Bevetel" és a "RendszeresBevetel" is. (Kiadás ugyanígy.) 
                    {
                        if (item.Id == id)
                        {
                            frm = new BizonylatFrm(item);
                            frm.ShowDialog();
                        }
                    }
                }
                if (kijeloltDGV.Name == kiadasDGV.Name) //Kiadáshoz
                {
                    int id = (int)kiadasDGV.SelectedRows[0].HeaderCell.Value;
                    foreach (var item in kiadasLista)
                    {
                        if (item.Id == id)
                        {
                            frm = new BizonylatFrm(item);
                            frm.ShowDialog();
                        }
                    }
                }
                DGVkijelolesekMegszuntetese();
            }
            else
            {
                UzenetAblak.Ablak("A bizonylatfelvitelhez ki kell jelölni egy bevétel vagy kiadás sort!", "Nincs kijelölt sor", Szint.Info);
            }
        }
        private void DGV_SelectionChanged(object sender, EventArgs e)
        {
            if (sender is DataGridView dgv && dgv.SelectedCells.Count > 0 && !tablazatFeltoltesTortenik)
            {
                foreach (Control item in Controls)
                {
                    if (item is DataGridView jelenlegi && jelenlegi != dgv)
                    {
                        jelenlegi.ClearSelection();
                    }
                }
                HaVanBizonylatZolduljonAGomb(dgv);
            }
        }
        private void bevetellistaEpito_Click(object sender, EventArgs e)
        {
            List<Penzmozgas> lista = new List<Penzmozgas>();
            foreach (var item in bevetelLista)
            {
                lista.Add(item);
            }
            HonapEpitoFrm frm = new HonapEpitoFrm(lista, "bevetel", new DateTime((int)evszamValasztoNumUpDown.Value, honapValasztoComboBox.SelectedIndex + 1, 1));
            frm.ShowDialog();
            TablazatokFeltoltese();
            DGVkijelolesekMegszuntetese();
        }
        private void kiadaslistaEpito_Click(object sender, EventArgs e)
        {
            List<Penzmozgas> lista = new List<Penzmozgas>();
            foreach (var item in kiadasLista)
            {
                lista.Add(item);
            }
            HonapEpitoFrm frm = new HonapEpitoFrm(lista, "kiadas", new DateTime((int)evszamValasztoNumUpDown.Value, honapValasztoComboBox.SelectedIndex + 1, 1));
            frm.ShowDialog();
            TablazatokFeltoltese();
            DGVkijelolesekMegszuntetese();
        }
        private void honapValasztoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TablazatokFeltoltese();
            DGVkijelolesekMegszuntetese();
        }
        private void evszamValasztoNumUpDown_ValueChanged(object sender, EventArgs e)
        {
            TablazatokFeltoltese();
            DGVkijelolesekMegszuntetese();
        }
        private void szervizBtn_Click(object sender, EventArgs e)
        {
            SzervizFrm frm = new SzervizFrm();
            frm.ShowDialog();
            try
            {
                if (ABKezelo.PenzmozgasErtesitesBeallitasLekerdezes() == true)
                {
                    idozito.Tick += idozito_Tick;
                    idozito.Start();
                }
                else if (ABKezelo.PenzmozgasErtesitesBeallitasLekerdezes() == false)
                {
                    idozito.Tick -= idozito_Tick;
                    idozito.Stop();
                }
            }
            catch (ABKivetel ex)
            {
                UzenetAblak.Ablak(ex.Message, "Értesítési hiba", Szint.Info);
            }
            TablazatokFeltoltese();
            DGVkijelolesekMegszuntetese();
        }
        private void honapHatraBtn_Click(object sender, EventArgs e)
        {
            if (mettol.AddMonths(-1) >= new DateTime(2019, 01, 01))
            {
                evszamValasztoNumUpDown.ValueChanged -= evszamValasztoNumUpDown_ValueChanged;
                mettol = mettol.AddMonths(-1);
                evszamValasztoNumUpDown.Value = mettol.Year;
                honapValasztoComboBox.SelectedIndex = mettol.Month - 1;
                evszamValasztoNumUpDown.ValueChanged += evszamValasztoNumUpDown_ValueChanged;
            }
        }
        private void honapEloreBtn_Click(object sender, EventArgs e)
        {
            if (mettol.AddMonths(+1) <= new DateTime(2050, 12, 31))
            {
                evszamValasztoNumUpDown.ValueChanged -= evszamValasztoNumUpDown_ValueChanged;
                mettol = mettol.AddMonths(1);
                evszamValasztoNumUpDown.Value = mettol.Year;
                honapValasztoComboBox.SelectedIndex = mettol.Month - 1;
                evszamValasztoNumUpDown.ValueChanged += evszamValasztoNumUpDown_ValueChanged;
            }
        }
        private void Kimutatas_Click(object sender, EventArgs e)
        {
            EvesKimutatasFrm frm = new EvesKimutatasFrm();
            frm.ShowDialog();
        }
        private void DGV_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DGVkijelolesekMegszuntetese();
        }
        private void DGV_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DGVkijelolesekMegszuntetese();
        }
        private void idozito_Tick(object sender, EventArgs e)
        {
            PenzmozgasHataridoEllenorzes();
        }
        private void Icon_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            //https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.notifyicon.doubleclick?view=netframework-4.8
        }
        private void bevetelDGV_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                PenzmozgasModositas();
            }
        }
        #endregion

    }
}