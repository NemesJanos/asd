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
    public partial class EvesKimutatasFrm : Form
    {
        public delegate string MelyikDatum();

        DateTime mettol;
        DateTime meddig;
        List<Bevetel> bevetelLista;
        List<Kiadas> kiadasLista;

        public EvesKimutatasFrm()
        {
            InitializeComponent();
        }

        private void KimutatasFrm_Load(object sender, EventArgs e)
        {
            foreach (Control item in Controls)
            {
                if (item is DataGridView dgv)
                {
                    foreach (DataGridViewColumn oszlop in dgv.Columns)
                    {
                        if (oszlop.Index != 0)
                        {
                            oszlop.ValueType = typeof(Int32);
                            oszlop.DefaultCellStyle.Format = "###,###,###";
                            oszlop.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        }
                        else if (oszlop.Index == 13)
                        {
                            oszlop.ValueType = typeof(Int32);
                            oszlop.DefaultCellStyle.Format = "###,###,###";
                            oszlop.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            oszlop.DefaultCellStyle.Font = new Font(bevetelDGV.DefaultCellStyle.Font, FontStyle.Bold);
                            oszlop.HeaderCell.Style.Font = new Font(bevetelDGV.DefaultCellStyle.Font, FontStyle.Bold);
                        }
                        else  if(oszlop.Index == 0)
                        {
                            oszlop.DefaultCellStyle.Font = new Font(bevetelDGV.DefaultCellStyle.Font, FontStyle.Bold);
                            oszlop.HeaderCell.Style.Font = new Font(bevetelDGV.DefaultCellStyle.Font, FontStyle.Bold);
                            oszlop.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                            oszlop.Width = 160;
                        }
                    }
                }
            }
            evszamValasztoNumUpDown.Value = DateTime.Now.Year;
            TeljesitesTablakFeltoltese();
        }

        private void TeljesitesTablakFeltoltese()
        {
            try
            {
                mettol = new DateTime((int)evszamValasztoNumUpDown.Value, 1, 1);
                meddig = new DateTime((int)evszamValasztoNumUpDown.Value, 12, 31);

                bevetelLista = ABKezelo.BevetelListazas(mettol, meddig);
                kiadasLista = ABKezelo.KiadasListazas(mettol, meddig);

                //bevetelDGV feltöltés
                bevetelDGV.Rows.Clear();
                foreach (var item in bevetelLista)
                {
                    if (item.TeljesitesDatuma > new DateTime(1000, 01, 01))
                    {
                        if (bevetelDGV.Rows.Count == 0)
                        {
                            bevetelDGV.Rows.Add();
                            bevetelDGV.Rows[bevetelDGV.Rows.Count - 1].Cells[0].Value = item.Megnevezes;
                            bevetelDGV.Rows[bevetelDGV.Rows.Count - 1].Cells[int.Parse(item.TeljesitesDatuma.ToString("MM"))].Value = item.Osszeg;
                        }
                        else if (bevetelDGV.Rows.Count > 0)
                        {
                            int index = 0;
                            while (bevetelDGV.Rows.Count > index && bevetelDGV.Rows[index].Cells[0].Value.ToString() != item.Megnevezes.ToString())
                            {
                                index++;
                            }
                            if (bevetelDGV.Rows.Count > index)
                            {
                                if (bevetelDGV.Rows[index].Cells[int.Parse(item.TeljesitesDatuma.ToString("MM"))].Value == null)
                                {
                                    bevetelDGV.Rows[index].Cells[int.Parse(item.TeljesitesDatuma.ToString("MM"))].Value = item.Osszeg;
                                }
                                else
                                {
                                    int jelenlegi = (int)bevetelDGV.Rows[index].Cells[int.Parse(item.TeljesitesDatuma.ToString("MM"))].Value;
                                    bevetelDGV.Rows[index].Cells[int.Parse(item.TeljesitesDatuma.ToString("MM"))].Value = (jelenlegi + item.Osszeg);
                                }
                            }
                            else
                            {
                                bevetelDGV.Rows.Add();
                                bevetelDGV.Rows[bevetelDGV.Rows.Count - 1].Cells[0].Value = item.Megnevezes;
                                bevetelDGV.Rows[bevetelDGV.Rows.Count - 1].Cells[int.Parse(item.TeljesitesDatuma.ToString("MM"))].Value = item.Osszeg;
                            }
                        } 
                    }
                }
                //kiadasDGV feltöltés
                kiadasDGV.Rows.Clear();
                foreach (var item in kiadasLista)
                {
                    if (item.TeljesitesDatuma > new DateTime(1000, 01, 01))
                    {
                        if (kiadasDGV.Rows.Count == 0)
                        {
                            kiadasDGV.Rows.Add();
                            kiadasDGV.Rows[kiadasDGV.Rows.Count - 1].Cells[0].Value = item.Megnevezes;
                            kiadasDGV.Rows[kiadasDGV.Rows.Count - 1].Cells[int.Parse(item.TeljesitesDatuma.ToString("MM"))].Value = item.Osszeg;
                        }
                        else if (kiadasDGV.Rows.Count > 0)
                        {
                            int index = 0;
                            while (kiadasDGV.Rows.Count > index && kiadasDGV.Rows[index].Cells[0].Value.ToString() != item.Megnevezes.ToString())
                            {
                                index++;
                            }
                            if (kiadasDGV.Rows.Count > index)
                            {
                                if (kiadasDGV.Rows[index].Cells[int.Parse(item.TeljesitesDatuma.ToString("MM"))].Value == null)
                                {
                                    kiadasDGV.Rows[index].Cells[int.Parse(item.TeljesitesDatuma.ToString("MM"))].Value = item.Osszeg;
                                }
                                else
                                {
                                    int jelenlegi = (int)kiadasDGV.Rows[index].Cells[int.Parse(item.TeljesitesDatuma.ToString("MM"))].Value;
                                    kiadasDGV.Rows[index].Cells[int.Parse(item.TeljesitesDatuma.ToString("MM"))].Value = (jelenlegi + item.Osszeg);
                                }
                            }
                            else
                            {
                                kiadasDGV.Rows.Add();
                                kiadasDGV.Rows[kiadasDGV.Rows.Count - 1].Cells[0].Value = item.Megnevezes;
                                kiadasDGV.Rows[kiadasDGV.Rows.Count - 1].Cells[int.Parse(item.TeljesitesDatuma.ToString("MM"))].Value = item.Osszeg;
                            }
                        } 
                    }
                }
                //összeg oszlopok feltöltése
                foreach (DataGridViewRow item in bevetelDGV.Rows)
                {
                    double osszeg = 0;
                    for (int i = 1; i < bevetelDGV.Columns.Count - 1; i++)
                    {
                        if (item.Cells[i].Value != null)
                        {
                            osszeg += (int)item.Cells[i].Value;
                        }
                    }
                    item.Cells[13].Value = osszeg;
                }
                foreach (DataGridViewRow item in kiadasDGV.Rows)
                {
                    double osszeg = 0;
                    for (int i = 1; i < kiadasDGV.Columns.Count - 1; i++)
                    {
                        if (item.Cells[i].Value != null)
                        {
                            osszeg += (int)item.Cells[i].Value;
                        }
                    }
                    item.Cells[13].Value = osszeg;
                }
                bevetelDGV.Sort(bevetelDGV.Columns[0], ListSortDirection.Ascending);
                kiadasDGV.Sort(kiadasDGV.Columns[0], ListSortDirection.Ascending);
                DGVkijelolesmegszuntetese();
            }
            catch (Exception ex)
            {
                UzenetAblak.Ablak(ex.Message + " " + "\nValamelyik tábla feltöltése nem sikerült!", "Hiba történt", Szint.Hiba);
            }
        }
        private void DGVkijelolesmegszuntetese()
        {
            foreach (Control item in Controls)
            {
                if (item is DataGridView dgv)
                {
                    dgv.ClearSelection();
                }
            }
        }

        private void evszamValasztoNumUpDown_ValueChanged(object sender, EventArgs e)
        {
            TeljesitesTablakFeltoltese();
        }

        private void bevetelDGV_SelectionChanged(object sender, EventArgs e)
        {
            if (bevetelDGV.MultiSelect)
            {
                foreach (DataGridViewCell item in bevetelDGV.SelectedCells)
                {
                    if ((item as DataGridViewCell).ColumnIndex > 0)
                    {
                        item.Selected = true;
                    }
                    else
                    {
                        item.Selected = false;
                    }
                }
            }
            if (bevetelDGV.SelectedCells.Count > 0)
            {
                int osszesen = 0;
                foreach (var item in bevetelDGV.SelectedCells)
                {
                    if ((item as DataGridViewCell).Value != null && int.TryParse((item as DataGridViewCell).Value.ToString(), out int ertek))
                    {
                        osszesen += ertek;
                    }
                }
                bevetelRichTextBox.Text = osszesen.ToString();
                if (osszesen >= 0 && osszesen < 999999999)
                {
                    bevetelRichTextBox.Text = osszesen.ToString("###,###,###");
                }
                else
                {
                    bevetelRichTextBox.Text = "############";
                }
                bevetelRichTextBox.SelectAll();
                bevetelRichTextBox.SelectionAlignment = HorizontalAlignment.Right;
                bevetelRichTextBox.DeselectAll();
            }

        }
        private void kiadasDGV_SelectionChanged(object sender, EventArgs e)
        {
            if (kiadasDGV.MultiSelect)
            {
                foreach (DataGridViewCell item in kiadasDGV.SelectedCells)
                {
                    if ((item as DataGridViewCell).ColumnIndex > 0)
                    {
                        item.Selected = true;
                    }
                    else
                    {
                        item.Selected = false;
                    }
                }
            }
            if (kiadasDGV.SelectedCells.Count > 0)
            {
                int osszesen = 0;
                foreach (var item in kiadasDGV.SelectedCells)
                {
                    if ((item as DataGridViewCell).Value != null && int.TryParse((item as DataGridViewCell).Value.ToString(), out int ertek))
                    {
                        osszesen += ertek;
                    }
                }
                kiadasRichTextBox.Text = osszesen.ToString();
                if (osszesen >= 0 && osszesen < 999999999)
                {
                    kiadasRichTextBox.Text = osszesen.ToString("###,###,###");
                }
                else
                {
                    kiadasRichTextBox.Text = "############";
                }
                kiadasRichTextBox.SelectAll();
                kiadasRichTextBox.SelectionAlignment = HorizontalAlignment.Right;
                kiadasRichTextBox.DeselectAll();
            }
        }
    }
}

