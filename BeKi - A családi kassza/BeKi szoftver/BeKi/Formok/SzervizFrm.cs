using Microsoft.Win32;
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
    public partial class SzervizFrm : Form
    {
        public SzervizFrm()
        {
            InitializeComponent();
            if (AutoStartCheck() != null)
            {
                autoIndulasCheckBox.Checked = true;
            }
            talcaertesitesCheckBox.Checked = ABKezelo.PenzmozgasErtesitesBeallitasLekerdezes();
            label1.Text = "BeKi - A családi kassza";
            label2.Text = "Ez a szoftver Nemes János\nOKJ szoftverfeljesztő szakdolgozata.\nA szoftver otthoni felhasználása ingyenes.\n" +
                "Részben vagy egészben történő\nkereskedelmi forgalomba hozatala,\nminden formában tilos.";
        }
        private object AutoStartCheck()
        {
            object ellenorzes = null;
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                ellenorzes = key.GetValue("BeKi");
            }
            return ellenorzes;
        }
        private void AutoStartOn()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.SetValue("BeKi", Application.ExecutablePath + " systray");
            }
        }
        private void AutoStartOff()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.DeleteValue("BeKi", false);
            }
        }
        private void okBtn_Click(object sender, EventArgs e)
        {
            if (autoIndulasCheckBox.Checked)
            {
                AutoStartOn();
            }
            else if (!autoIndulasCheckBox.Checked)
            {
                AutoStartOff();
            }
            if (talcaertesitesCheckBox.Checked)
            {
                ABKezelo.PenzmozgasErtesitesBeallitas(true);
            }
            else if (!talcaertesitesCheckBox.Checked)
            {
                ABKezelo.PenzmozgasErtesitesBeallitas(false);
            }
        }

        private void biztonsagiMentesBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (mentesUtvonalValasztoDialog.ShowDialog() == DialogResult.OK)
                {
                    ABKezelo.BiztonsagiMentes(mentesUtvonalValasztoDialog.SelectedPath + @"\" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss") + ".bak");
                }
            }
            catch (ABKivetel ex)
            {
                UzenetAblak.Ablak(ex.Message, "Sikertelen", Szint.Hiba);
            }
        }

        private void mentesVisszaallitasBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (mentesVisszaallitasDialog.ShowDialog() == DialogResult.OK)
                {
                    if (UzenetAblak.Ablak("Lecserél minden adatot a biztosági mentésben tárolt adatokra?", "A művelet nem vonható vissza!", Szint.FontosKerdes) == DialogResult.Yes)
                    {
                        ABKezelo.BiztonsagiMentesVisszaallitasa(mentesVisszaallitasDialog.FileName.ToString());
                        UzenetAblak.Ablak("A mentés visszaállítása sikerült", "Sikerült", Szint.Info);
                    }
                }
            }
            catch (ABKivetel ex)
            {
                UzenetAblak.Ablak(ex.Message, "Sikertelen", Szint.Hiba);
            }
        }

        private void infoButton_Click(object sender, EventArgs e)
        {
            if (this.Width != 663)
            {
                this.Width = 663;
            }
            else
            {
                this.Width = 417;
            }
        }
    }
}
