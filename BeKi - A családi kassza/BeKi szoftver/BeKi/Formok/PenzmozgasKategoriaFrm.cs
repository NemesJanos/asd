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
    public partial class PenzmozgasKategoriaFrm : Form
    {
        BevetelKategoria bevetelKategoria;
        KiadasKategoria kiadasKategoria;
        List<BevetelKategoria> bevetelKategoriaLista;
        List<KiadasKategoria> kiadasKategoriaLista;
        ListBox kijeloltListBox;

        public PenzmozgasKategoriaFrm()
        {
            InitializeComponent();
            LBFrissites();
        }

        private void felvitelBtn_Click(object sender, EventArgs e)
        {
            try
            {
                bool felviheto = true;
                if (bevetelKategoria == null && kiadasKategoria == null)
                {
                    if (bevetelRadioBtn.Checked && textBox1.Text.Trim() != "")
                    {
                        foreach (BevetelKategoria item in bevetelKategoriaLista)
                        {
                            if (item.Megnevezes.ToLower() == textBox1.Text.Trim().ToLower())
                            {
                                UzenetAblak.Ablak("Már létezik ilyen megnevezésű bevétel kategória.", "Már van ilyen", Szint.Info);
                                textBox1.Focus();
                                textBox1.SelectAll();
                                felviheto = false;
                            }
                        }

                        if (felviheto == true && bevetelKategoria == null)
                        {
                            ABKezelo.BevetelKategoriaLetrehozasa(new BevetelKategoria(textBox1.Text.Trim()));
                            bevetelKategoriaLista = ABKezelo.BevetelKategoriaListazas();
                            BeviteliMezoNullazas();
                        }
                        if (felviheto == true && bevetelKategoria != null)
                        {
                            ABKezelo.BevetelKiadasKategoriaModositas(bevetelKategoria);
                            bevetelKategoriaLista = ABKezelo.BevetelKategoriaListazas();
                            BeviteliMezoNullazas();
                        }
                    }
                    else if (kiadasRadioBtn.Checked && textBox1.Text.Trim() != "")
                    {
                        foreach (KiadasKategoria item in kiadasKategoriaLista)
                        {
                            if (item.Megnevezes.ToLower() == textBox1.Text.Trim().ToLower())
                            {
                                UzenetAblak.Ablak("Már létezik ilyen megnevezésű bevétel kategória.", "Már van ilyen", Szint.Info);
                                textBox1.Focus();
                                textBox1.SelectAll();
                                felviheto = false;
                            }
                        }

                        if (felviheto == true && kiadasKategoria == null)
                        {
                            ABKezelo.KiadasKategoriaLetrehozasa(new KiadasKategoria(textBox1.Text.Trim()));
                            kiadasKategoriaLista = ABKezelo.KiadasKategoriaListazas();
                            BeviteliMezoNullazas();
                        }
                        if (felviheto == true && kiadasKategoria != null)
                        {
                            ABKezelo.KiadasKategoriaModositas(kiadasKategoria);
                            kiadasKategoriaLista = ABKezelo.KiadasKategoriaListazas();
                            BeviteliMezoNullazas();
                        }
                    }
                    
                    else
                    {
                        UzenetAblak.Ablak("A típusválasztás és a megnevezés mező kitöltése kötelező.", "Hiba a bevitt adatokban", Szint.Info);
                    }
                }
                else if (kiadasKategoria != null)
                {
                    kiadasKategoria.Megnevezes = textBox1.Text;
                    ABKezelo.KiadasKategoriaModositas(kiadasKategoria);
                    BeviteliMezoNullazas();
                    kiadasKategoria = null;
                }
                else if (bevetelKategoria != null)
                {
                    bevetelKategoria.Megnevezes = textBox1.Text;
                    ABKezelo.BevetelKiadasKategoriaModositas(bevetelKategoria);
                    BeviteliMezoNullazas();
                    bevetelKategoria = null;
                }
            }
            catch (ABKivetel ex)
            {
                UzenetAblak.Ablak(ex.Message, "Hiba", Szint.Hiba);
            }
            catch (Exception ex)
            {
                UzenetAblak.Ablak(ex.Message, "Sikertelen", Szint.Hiba);
            }
            LBFrissites();
        }
        private void modositBtn_Click(object sender, EventArgs e)
        {
            try
            {
                KijeloltListBoxKereses();
                if (kijeloltListBox != null && kijeloltListBox.SelectedItem is BevetelKategoria)
                {
                    bevetelKategoria = (BevetelKategoria)kijeloltListBox.SelectedItem;
                    kiadasKategoria = null;
                    textBox1.Text = bevetelKategoria.Megnevezes;
                    groupBox1.Enabled = false;
                    bevetelRadioBtn.Checked = true;
                }
                else if (kijeloltListBox != null && kijeloltListBox.SelectedItem is KiadasKategoria)
                {
                    kiadasKategoria = (KiadasKategoria)kijeloltListBox.SelectedItem;
                    bevetelKategoria = null;
                    textBox1.Text = kiadasKategoria.Megnevezes;
                    groupBox1.Enabled = false;
                    kiadasRadioBtn.Checked = true;
                }
                else
                {
                    UzenetAblak.Ablak("A módosításhoz ki kell jelölni egy kategóriát.", "Nincs kijelölt elem", Szint.Info);
                }
            }
            catch (ABKivetel ex)
            {
                UzenetAblak.Ablak(ex.Message, "Hiba", Szint.Hiba);
            }
            catch (Exception ex)
            {
                UzenetAblak.Ablak(ex.Message, "Sikertelen", Szint.Hiba);
            }
        }
        private void torolBtn_Click(object sender, EventArgs e)
        {
            try
            {
                KijeloltListBoxKereses();
                if (kijeloltListBox != null)
                {
                    DialogResult dr = UzenetAblak.Ablak("Biztosan törli a kijelölt tételt?", "Megerősítés kérése", Szint.Kerdes);
                    if (dr == DialogResult.Yes)
                    {
                        if (kijeloltListBox.SelectedItem is BevetelKategoria && dr == DialogResult.Yes)
                        {
                            ABKezelo.BevetelKategoriaTorles((BevetelKategoria)kijeloltListBox.SelectedItem);
                        }
                        else if (kijeloltListBox.SelectedItem is KiadasKategoria && dr == DialogResult.Yes)
                        {
                            ABKezelo.KiadasKategoriaTorles((KiadasKategoria)kijeloltListBox.SelectedItem);
                        }
                    } 
                }
                else if (kijeloltListBox == null)
                {
                    UzenetAblak.Ablak("A törléshez ki kell jelölni egy kategóriát.", "Nincs kijelölt elem", Szint.Info);
                }
            }
            catch (ABKivetel ex) 
            {
                UzenetAblak.Ablak(ex.Message, "Hiba", Szint.Hiba);
            }
            catch (Exception ex)
            {
                UzenetAblak.Ablak(ex.Message, "Hiba", Szint.Hiba);
            }
            LBFrissites();
            BeviteliMezoNullazas();
        }
        private void LBFrissites()
        {
            bevetelKategoriaLista = ABKezelo.BevetelKategoriaListazas();
            bevetelKategoriaLista.Sort();
            kiadasKategoriaLista = ABKezelo.KiadasKategoriaListazas();
            kiadasKategoriaLista.Sort();
            listBox1.DataSource = bevetelKategoriaLista;
            listBox1.SelectedIndices.Clear();
            listBox2.DataSource = kiadasKategoriaLista;
            listBox2.SelectedIndices.Clear();
        }
        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ListBox lb && lb.SelectedIndex != -1)
            {
                foreach (Control item in groupBox2.Controls)
                {
                    if (item is ListBox aktualis && aktualis != lb)
                    {
                        aktualis.SelectedIndices.Clear();
                    }
                }
            }
        }
        private void KijeloltListBoxKereses()
        {
            kijeloltListBox = null;
            foreach (Control item in groupBox2.Controls)
            {
                if (item is ListBox lb && lb.SelectedIndex != -1)
                {
                    kijeloltListBox = lb;
                    break;
                }
            }
        }
        private void BeviteliMezoNullazas()
        {
            textBox1.Text = "";
            groupBox1.Enabled = true;
            textBox1.Focus();
        }
        private void ujBtn_Click(object sender, EventArgs e)
        {
            kiadasKategoria = null;
            bevetelKategoria = null;
            BeviteliMezoNullazas();
        }
    }
}
