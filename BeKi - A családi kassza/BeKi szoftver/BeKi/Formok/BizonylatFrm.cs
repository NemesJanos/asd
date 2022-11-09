using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeKi
{
    public partial class BizonylatFrm : Form
    {
        Penzmozgas penzmozgas;
        internal Penzmozgas Penzmozgas { get => penzmozgas; /*set => penzmozgas = value; */}

        internal BizonylatFrm(Penzmozgas penzmozgasTetel)
        {
            InitializeComponent();
            penzmozgas = penzmozgasTetel;
            BizonylatokListazasaListBoxba();
        }

        void BizonylatokListazasaListBoxba()
        {
            listBox1.DataSource = null;
            listBox1.DataSource = ABKezelo.BizonylatListazas(penzmozgas);
        }
        private void bizonylatFajlbolBtn(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    byte[] tomb = File.ReadAllBytes(openFileDialog1.FileName);
                    if (penzmozgas is Bevetel)
                    {
                        ABKezelo.BizonylatFelvitel(new BevetelBizonylat(openFileDialog1.FileName, tomb), penzmozgas);
                    }
                    else if (penzmozgas is Kiadas)
                    {
                        ABKezelo.BizonylatFelvitel(new KiadasBizonylat(openFileDialog1.FileName, tomb), penzmozgas);
                    }
                }
                BizonylatokListazasaListBoxba();
            }
            catch (Exception ex)
            {
                UzenetAblak.Ablak(ex.Message, "Sikertelen", Szint.Hiba);
            }
        }
        private void bizonylatfilebaMenteseBtn(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedIndex != -1)
                {
                    if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    {
                        byte[] b = (byte[])(listBox1.SelectedItem as Bizonylat).Fajl;
                        File.WriteAllBytes(folderBrowserDialog1.SelectedPath + "\\" + listBox1.SelectedItem.ToString(), b);
                    }
                }
                else
                {
                    UzenetAblak.Ablak("A kimentéshez jelöljön ki egy elemet a listában!", "Nincs kijelölt elem", Szint.Info);
                }
            }
            catch (Exception ex)
            {
                UzenetAblak.Ablak("Sikertelen mentés!\n" + ex.Message, "Hiba", Szint.Hiba);
            }
        }
        private void TorlesBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.Items.Count > 0)
                {
                    if (listBox1.SelectedIndex != -1)
                    {
                        if (UzenetAblak.Ablak("Törlölni szeretné a kijelölt tételt?", "Biztos benne?", Szint.Kerdes) == DialogResult.Yes)
                        {
                            ABKezelo.BizonylatTorles((Bizonylat)listBox1.SelectedItem, penzmozgas);
                        }
                        BizonylatokListazasaListBoxba();
                    }
                    else
                    {
                        UzenetAblak.Ablak("A törléshez jelöljön ki egy bizonylatot!", "Nincs kijelölt bizonylat", Szint.Info);
                    } 
                }
                else
                {
                    UzenetAblak.Ablak("Nincs bizonylat a listában!", "Üres lista", Szint.Info);
                }
            }
            catch (ABKivetel ex)
            {
                UzenetAblak.Ablak(ex.Message, "Hiba", Szint.Hiba);
            }
        }
        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (listBox1.SelectedIndex != -1)
                {
                    if (!Directory.Exists(@"Temp\")) Directory.CreateDirectory(@"Temp\");
                    byte[] b = (byte[])(listBox1.SelectedItem as Bizonylat).Fajl;
                    File.WriteAllBytes(@"Temp\" + listBox1.SelectedItem.ToString(), b);
                    Process.Start(@"Temp\" + listBox1.SelectedItem.ToString());
                }
            }
            catch (Exception ex)
            {
                UzenetAblak.Ablak(ex.Message, "Hba", Szint.Hiba);
            }
        }
        private void BizonylatFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (Directory.Exists("Temp")) Directory.Delete("Temp", true);
            }
            catch (Exception)
            {

            }
        }
    }
}
