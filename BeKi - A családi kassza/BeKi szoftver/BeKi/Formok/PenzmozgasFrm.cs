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
    public partial class PenzmozgasFrm : Form
    {
        Penzmozgas penzmozgas;
        List<KiadasKategoria> kiadasKategoriaLista;
        List<BevetelKategoria> bevetelKategoriaLista;
        MonthCalendar utoljaraValasztva;

        #region Konstruktorok
        public PenzmozgasFrm()
        {
            InitializeComponent();
            kiadasKategoriaLista = new List<KiadasKategoria>();
            bevetelKategoriaLista = new List<BevetelKategoria>();
            PenzmozgasKategoriakFrissitese();
            ComboboxFrissites();
        }
        public PenzmozgasFrm(DateTime melyikHonap) : this()
        {
            teljesitveGroupBox.Enabled = false;
            esedekessegMCalendar.SelectionStart = new DateTime(melyikHonap.Year, melyikHonap.Month, DateTime.Now.Day);
            teljesitesMCalendar.SelectionStart = new DateTime(melyikHonap.Year, melyikHonap.Month, DateTime.Now.Day);
        }
        internal PenzmozgasFrm(Penzmozgas modosit) : this ()
        {
            penzmozgas = modosit;
            tipusGroupBox.Enabled = false;
            textBox1.Text = penzmozgas.Megnevezes;
            numericUpDown1.Value = penzmozgas.Osszeg;
            esedekessegMCalendar.SelectionStart = penzmozgas.Esedekesseg.Date;
            if (penzmozgas is Bevetel)
            {
                bevetelRadio.Checked = true;
                BevetelKategoria jelenlegi = null;
                foreach (var item in bevetelKategoriaLista)
                {
                    if ((item as BevetelKategoria).Megnevezes == (penzmozgas as Bevetel).Kategoria.Megnevezes)
                    {
                        jelenlegi = item;
                    }
                }
                comboBox1.SelectedIndex = comboBox1.Items.IndexOf(jelenlegi);
                nemRendszeresRadio.Checked = true;

                if (penzmozgas.TeljesitesDatuma.Date == new DateTime(0001,01,01))
                {
                    teljesitesMCalendar.SelectionStart = DateTime.Now.Date;
                    teljesitveCheckBox.Checked = false;
                    teljesitveGroupBox.Enabled = false;
                }
                else
                {
                    teljesitveCheckBox.Checked = true;
                    teljesitesMCalendar.SelectionStart = penzmozgas.TeljesitesDatuma.Date;
                }
                if (penzmozgas is RendszeresBevetel rb)
                {
                    if (rb.Rendszeresseg == Rendszeresseg.Havonta)
                    {
                        havontaRadio.Checked = true;
                    }
                    else if (rb.Rendszeresseg == Rendszeresseg.Negyedévente)
                    {
                        negyedeventeRadio.Checked = true;
                    }
                    else if (rb.Rendszeresseg == Rendszeresseg.Félévente)
                    {
                        feleventeRadio.Checked = true;
                    }
                    else if (rb.Rendszeresseg == Rendszeresseg.Évente)
                    {
                        eventeRadio.Checked = true;
                    }
                    else
                    {
                        nemRendszeresRadio.Checked = true;
                    }
                }
            }
            else if (penzmozgas is Kiadas)
            {
                kiadasRadio.Checked = true;
                KiadasKategoria jelenlegi = null;
                foreach (var item in kiadasKategoriaLista)
                {
                    if ((item as KiadasKategoria).Megnevezes == (penzmozgas as Kiadas).Kategoria.Megnevezes)
                    {
                        jelenlegi = item;
                    }
                }
                comboBox1.SelectedIndex = comboBox1.Items.IndexOf(jelenlegi);

                nemRendszeresRadio.Checked = true;

                if (penzmozgas.TeljesitesDatuma.Date == new DateTime(0001, 01, 01))
                {
                    teljesitesMCalendar.SelectionStart = DateTime.Now.Date;
                    teljesitveCheckBox.Checked = false;
                    teljesitveGroupBox.Enabled = false;
                }
                else
                {
                    teljesitveCheckBox.Checked = true;
                    teljesitesMCalendar.SelectionStart = penzmozgas.TeljesitesDatuma.Date;
                }
                if (penzmozgas is RendszeresKiadas rk)
                {
                    if (rk.Rendszeresseg == Rendszeresseg.Havonta)
                    {
                        havontaRadio.Checked = true;
                    }
                    else if (rk.Rendszeresseg == Rendszeresseg.Negyedévente)
                    {
                        negyedeventeRadio.Checked = true;
                    }
                    else if (rk.Rendszeresseg == Rendszeresseg.Félévente)
                    {
                        feleventeRadio.Checked = true;
                    }
                    else if (rk.Rendszeresseg == Rendszeresseg.Évente)
                    {
                        eventeRadio.Checked = true;
                    }
                    else
                    {
                        nemRendszeresRadio.Checked = true;
                    }
                }
            }
        }
        #endregion

        #region Metódusok, Funkciók
        private void PenzmozgasKategoriakFrissitese()
        {
            kiadasKategoriaLista = ABKezelo.KiadasKategoriaListazas();
            bevetelKategoriaLista = ABKezelo.BevetelKategoriaListazas();
            kiadasKategoriaLista.Sort();
            bevetelKategoriaLista.Sort();
        }
        private void ComboboxFrissites()
        {
            comboBox1.DataSource = null;
            if (bevetelRadio.Checked) comboBox1.DataSource = bevetelKategoriaLista;
            else if (kiadasRadio.Checked) comboBox1.DataSource = kiadasKategoriaLista;
        }
        #endregion

        #region Gombok
        private void kategoriakBtn(object sender, EventArgs e)
        {
            PenzmozgasKategoriaFrm frm = new PenzmozgasKategoriaFrm();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                PenzmozgasKategoriakFrissitese();
                ComboboxFrissites();
                if (bevetelRadio.Checked && bevetelKategoriaLista.Count > 0)
                {
                    int i = 1;
                    int legnagyobbISorszama = 0;
                    while (i < bevetelKategoriaLista.Count)
                    {
                        if (bevetelKategoriaLista[legnagyobbISorszama].Id < bevetelKategoriaLista[i].Id)
                        {
                            legnagyobbISorszama = i;
                        }
                        i++;
                    }
                    comboBox1.SelectedIndex = legnagyobbISorszama;
                }
                else if (kiadasRadio.Checked && kiadasKategoriaLista.Count > 0)
                {
                    int i = 1;
                    int legnagyobbIdSorszama = 0;
                    while (i < kiadasKategoriaLista.Count)
                    {
                        if (kiadasKategoriaLista[legnagyobbIdSorszama].Id < kiadasKategoriaLista[i].Id)
                        {
                            legnagyobbIdSorszama = i;
                        }
                        i++;
                    }
                    comboBox1.SelectedIndex = legnagyobbIdSorszama;
                }
            }
        }
        private void OK_Click(object sender, EventArgs e)
        {
            int idModositashoz = 0;
            string rendsz = "";
            foreach (Control item in rendszeressegGropupBox.Controls)
            {
                if (item is RadioButton && (item as RadioButton).Checked)
                {
                    rendsz = item.Text;
                    break;
                }
            }

            if (penzmozgas != null)
            {
                idModositashoz = penzmozgas.Id;
            }

            try
            {
                if (bevetelRadio.Checked)  // BEVÉTEL
                {
                    if (nemRendszeresRadio.Checked)
                    {
                        if (esedekessegMCalendar.SelectionStart.Date >= DateTime.Now.Date || penzmozgas != null)
                        {
                            if (teljesitveCheckBox.Checked)
                            {
                                ABKezelo.BevetelFelvitel(new Bevetel((BevetelKategoria)comboBox1.SelectedItem, textBox1.Text, (int)numericUpDown1.Value, esedekessegMCalendar.SelectionStart.Date, teljesitesMCalendar.SelectionStart.Date), true, idModositashoz);
                            }
                            else
                            {
                                ABKezelo.BevetelFelvitel(new Bevetel((BevetelKategoria)comboBox1.SelectedItem, textBox1.Text, (int)numericUpDown1.Value, esedekessegMCalendar.SelectionStart.Date), false, idModositashoz);
                            }
                        }
                        else
                        {
                            if (UzenetAblak.Ablak($"Itt az összeg érkezésének várható napját kell megadnia.\nMúltbéli dátumot adott meg. Biztosan folytatja?", "Folytatja?", Szint.Kerdes) == DialogResult.Yes)
                            {
                                if (teljesitveCheckBox.Checked)
                                {
                                    ABKezelo.BevetelFelvitel(new Bevetel((BevetelKategoria)comboBox1.SelectedItem, textBox1.Text, (int)numericUpDown1.Value, esedekessegMCalendar.SelectionStart.Date, teljesitesMCalendar.SelectionStart.Date), true, idModositashoz);
                                }
                                else
                                {
                                    ABKezelo.BevetelFelvitel(new Bevetel((BevetelKategoria)comboBox1.SelectedItem, textBox1.Text, (int)numericUpDown1.Value, esedekessegMCalendar.SelectionStart.Date), false, idModositashoz);
                                }
                            }
                            else
                            {
                                DialogResult = DialogResult.None;
                            }
                        }
                    }
                    else if (!nemRendszeresRadio.Checked)
                    {
                        if (esedekessegMCalendar.SelectionStart.Date >= DateTime.Now.Date || penzmozgas != null)
                        {
                            if (teljesitveCheckBox.Checked)
                            {
                                ABKezelo.RendszeresBevetelFelvitel(new RendszeresBevetel((BevetelKategoria)comboBox1.SelectedItem, textBox1.Text, (int)numericUpDown1.Value, esedekessegMCalendar.SelectionStart.Date, (Rendszeresseg)Enum.Parse(typeof(Rendszeresseg), rendsz), teljesitesMCalendar.SelectionStart.Date), true, idModositashoz);
                            }
                            else
                            {
                                ABKezelo.RendszeresBevetelFelvitel(new RendszeresBevetel((BevetelKategoria)comboBox1.SelectedItem, textBox1.Text, (int)numericUpDown1.Value, esedekessegMCalendar.SelectionStart.Date, (Rendszeresseg)Enum.Parse(typeof(Rendszeresseg), rendsz)), false, idModositashoz);
                            }
                        }
                        else
                        {
                            if (UzenetAblak.Ablak($"Itt az összeg érkezésének várható napját kell megadnia.\nMúltbéli dátumot adott meg. Biztosan folytatja?", "Folytatja?", Szint.Kerdes) == DialogResult.Yes)
                            {
                                if (teljesitveCheckBox.Checked)
                                {
                                    ABKezelo.RendszeresBevetelFelvitel(new RendszeresBevetel((BevetelKategoria)comboBox1.SelectedItem, textBox1.Text, (int)numericUpDown1.Value, esedekessegMCalendar.SelectionStart.Date, (Rendszeresseg)Enum.Parse(typeof(Rendszeresseg), rendsz), teljesitesMCalendar.SelectionStart.Date), true, idModositashoz);
                                }
                                else
                                {
                                    ABKezelo.RendszeresBevetelFelvitel(new RendszeresBevetel((BevetelKategoria)comboBox1.SelectedItem, textBox1.Text, (int)numericUpDown1.Value, esedekessegMCalendar.SelectionStart.Date, (Rendszeresseg)Enum.Parse(typeof(Rendszeresseg), rendsz)), false, idModositashoz);
                                }
                            }
                            else
                            {
                                DialogResult = DialogResult.None;
                            }
                        }
                    }
                }
                else if (kiadasRadio.Checked)  // KIADÁS
                {
                    if (nemRendszeresRadio.Checked)
                    {
                        if (esedekessegMCalendar.SelectionStart.Date >= DateTime.Now.Date || penzmozgas != null)
                        {
                            if (teljesitveCheckBox.Checked)
                            {
                                ABKezelo.KiadasFelvitel(new Kiadas((KiadasKategoria)comboBox1.SelectedItem, textBox1.Text, (int)numericUpDown1.Value, esedekessegMCalendar.SelectionStart.Date, teljesitesMCalendar.SelectionStart.Date), true, idModositashoz);
                            }
                            else
                            {
                                ABKezelo.KiadasFelvitel(new Kiadas((KiadasKategoria)comboBox1.SelectedItem, textBox1.Text, (int)numericUpDown1.Value, esedekessegMCalendar.SelectionStart.Date), false, idModositashoz);
                            }
                        }
                        else
                        {
                            if (UzenetAblak.Ablak($"Itt az összeg fizetési határidejét kell megadnia.\nMúltbéli dátumot adott meg. Biztosan folytatja?", "Folytatja?", Szint.Kerdes) == DialogResult.Yes)
                            {
                                if (teljesitveCheckBox.Checked)
                                {
                                    ABKezelo.KiadasFelvitel(new Kiadas((KiadasKategoria)comboBox1.SelectedItem, textBox1.Text, (int)numericUpDown1.Value, esedekessegMCalendar.SelectionStart.Date, teljesitesMCalendar.SelectionStart.Date), true, idModositashoz);
                                }
                                else
                                {
                                    ABKezelo.KiadasFelvitel(new Kiadas((KiadasKategoria)comboBox1.SelectedItem, textBox1.Text, (int)numericUpDown1.Value, esedekessegMCalendar.SelectionStart.Date), false, idModositashoz);
                                }
                            }
                            else
                            {
                                DialogResult = DialogResult.None;
                            }
                        }
                    }
                    else if (!nemRendszeresRadio.Checked)
                    {
                        if (esedekessegMCalendar.SelectionStart.Date >= DateTime.Now.Date || penzmozgas != null)
                        {
                            if (teljesitveCheckBox.Checked)
                            {
                                ABKezelo.RendszeresKiadasFelvitel(new RendszeresKiadas((KiadasKategoria)comboBox1.SelectedItem, textBox1.Text, (int)numericUpDown1.Value, esedekessegMCalendar.SelectionStart.Date, (Rendszeresseg)Enum.Parse(typeof(Rendszeresseg), rendsz), teljesitesMCalendar.SelectionStart.Date), true, idModositashoz); 
                            }
                            else
                            {
                                ABKezelo.RendszeresKiadasFelvitel(new RendszeresKiadas((KiadasKategoria)comboBox1.SelectedItem, textBox1.Text, (int)numericUpDown1.Value, esedekessegMCalendar.SelectionStart.Date, (Rendszeresseg)Enum.Parse(typeof(Rendszeresseg), rendsz)), false, idModositashoz);
                            }
                        }
                        else
                        {
                            if (UzenetAblak.Ablak($"Itt az összeg fizetési határidejét kell megadnia.\nMúltbéli dátumot adott meg. Biztosan folytatja?", "Folytatja?", Szint.Kerdes) == DialogResult.Yes)
                            {
                                if (teljesitveCheckBox.Checked)
                                {
                                    ABKezelo.RendszeresKiadasFelvitel(new RendszeresKiadas((KiadasKategoria)comboBox1.SelectedItem, textBox1.Text, (int)numericUpDown1.Value, esedekessegMCalendar.SelectionStart.Date, (Rendszeresseg)Enum.Parse(typeof(Rendszeresseg), rendsz), teljesitesMCalendar.SelectionStart.Date), true, idModositashoz);
                                }
                                else
                                {
                                    ABKezelo.RendszeresKiadasFelvitel(new RendszeresKiadas((KiadasKategoria)comboBox1.SelectedItem, textBox1.Text, (int)numericUpDown1.Value, esedekessegMCalendar.SelectionStart.Date, (Rendszeresseg)Enum.Parse(typeof(Rendszeresseg), rendsz)), false, idModositashoz);
                                }
                            }
                            else
                            {
                                DialogResult = DialogResult.None;
                            }
                        }
                    }
                }
            }
            catch (ABKivetel ex)
            {
                UzenetAblak.Ablak(ex.InnerException.Message, "Hiba", Szint.Hiba);
            }
            catch (Exception ex)
            {
                UzenetAblak.Ablak(ex.Message, "Hiba", Szint.Hiba);
                DialogResult = DialogResult.None;
            }
        }
        #endregion

        #region Changed eventek
        private void teljesitveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (teljesitveCheckBox.Checked)
            {
                teljesitveGroupBox.Enabled = true;
            }
            else if (!teljesitveCheckBox.Checked)
            {
                teljesitveGroupBox.Enabled = false;
            }
        }
        private void MCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            if (esedekessegTeljesitesUA.Checked && sender is MonthCalendar mc)
            {
                if (mc == esedekessegMCalendar)
                {
                    teljesitesMCalendar.SelectionStart = mc.SelectionStart.Date;
                }
                if (mc != esedekessegMCalendar)
                {
                    esedekessegMCalendar.SelectionStart = mc.SelectionStart.Date;
                }
            }
            utoljaraValasztva = (MonthCalendar)sender;
        }
        private void esedekessegTeljesitesUA_CheckedChanged(object sender, EventArgs e)
        {
            if (esedekessegTeljesitesUA.Checked)
            {
                rendszeressegGropupBox.Enabled = false;
                nemRendszeresRadio.Checked = true;
                teljesitveCheckBox.Checked = true;
            }
            else if (!esedekessegTeljesitesUA.Checked)
            {
                rendszeressegGropupBox.Enabled = true;
                teljesitveCheckBox.Checked = false;
            }

            if (utoljaraValasztva != null)
            {
                if (utoljaraValasztva == esedekessegMCalendar)
                {
                    teljesitesMCalendar.SelectionStart = utoljaraValasztva.SelectionStart.Date;
                }
                if (utoljaraValasztva != esedekessegMCalendar)
                {
                    esedekessegMCalendar.SelectionStart = utoljaraValasztva.SelectionStart.Date;
                }
            }
        }
        private void TipusRadio_CheckedChanged(object sender, EventArgs e)
        {
            PenzmozgasKategoriakFrissitese();
            ComboboxFrissites();
        }
        #endregion
        private void numericUpDown1_Enter(object sender, EventArgs e)
        {
            numericUpDown1.Select(0, numericUpDown1.Value.ToString().Length + 1);
        }
        private void numericUpDown1_MouseClick(object sender, MouseEventArgs e)
        {
            if (numericUpDown1.Value == 0)
            {
                numericUpDown1.Select(0, numericUpDown1.Value.ToString().Length + 1);
            }
        }
    }
}