using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeKi
{
    enum Szint
    {
        Info,
        Kerdes,
        FontosKerdes,
        Hiba
    }

    class UzenetAblak
    {
        static DialogResult dr;

        static public DialogResult Ablak(string uzenet, string cimsor, Szint szint)
        {
         //   dr = DialogResult.None;
            Form ablak = new Form();
            ablak.StartPosition = FormStartPosition.CenterParent;
            ablak.BackColor = Color.FromName("GradientActiveCaption");
            ablak.MinimizeBox = false;
            ablak.MaximizeBox = false;
            ablak.FormBorderStyle = FormBorderStyle.Fixed3D;
            ablak.MinimumSize = new Size(450, 150);
            ablak.MaximumSize = new Size(450, 2000);
            ablak.Width = 450;
            ablak.Height = 150;
            ablak.Padding = new Padding(10);
            ablak.AutoSize = true;
            ablak.Text = cimsor;
            Label uzenetLabel = new Label();
            uzenetLabel.Text = uzenet;
            uzenetLabel.Font = new Font(Control.DefaultFont.FontFamily, 10, FontStyle.Regular);
            uzenetLabel.Location = new Point(20, 20);
            uzenetLabel.MaximumSize = new Size(310, 400);
            uzenetLabel.AutoSize = true;
            ablak.Controls.Add(uzenetLabel);
            PictureBox pb = new PictureBox();
            pb.Width = 64;
            pb.Height = 64;
            pb.Location = new Point(ablak.ClientSize.Width - 84, 20);
            pb.SizeMode = PictureBoxSizeMode.Zoom;
            ablak.Controls.Add(pb);
            
            if (szint == Szint.Info  || szint == Szint.Hiba)
            {
                Button okButton = new Button();
                ablak.Controls.Add(okButton);
                okButton.BackColor = Color.FromName("ButtonFace");
                okButton.Width = 80;
                okButton.Height = 30;
                ablak.AcceptButton = okButton;
                okButton.Text = "OK";
                okButton.DialogResult = DialogResult.OK;
                okButton.Location = new Point(ablak.ClientSize.Width / 2 - okButton.Width / 2, ((uzenetLabel.Bottom - okButton.Top < 100) ? 100 : uzenetLabel.Bottom+20));
                if (szint == Szint.Info)
                {
                    pb.Image = Properties.Resources.piggy_jobbra_atlatszo_info;
                }
                else if (szint == Szint.Hiba)
                {
                    pb.Image = Properties.Resources.piggy_jobbra_atlatszo_meghalt;
                }
            }
            if (szint == Szint.Kerdes || szint == Szint.FontosKerdes)
            {
                Button igenButton = new Button();
                Button nemButton = new Button();
                ablak.Controls.Add(igenButton);
                ablak.Controls.Add(nemButton);
                ablak.AcceptButton = igenButton;
                ablak.CancelButton = nemButton;
                foreach (Control item in ablak.Controls)
                {
                    if (item is Button)
                    {
                        item.BackColor = Color.FromName("ButtonFace");
                        item.Width = 80;
                        item.Height = 30;
                    }
                }
                if (szint == Szint.Kerdes)
                {
                    pb.Image = Properties.Resources.piggy_jobbra_atlatszo_kerdez;
                }
                if (szint == Szint.FontosKerdes)
                {
                    pb.Image = Properties.Resources.piggy_jobbra_atlatszo_szomoru;
                }
                igenButton.Text = "Igen";
                igenButton.DialogResult = DialogResult.Yes;
                igenButton.Location = new Point(115, (uzenetLabel.Bottom - igenButton.Top < 100) ? 100 : uzenetLabel.Bottom + 20);
                nemButton.Text = "Nem";
                nemButton.DialogResult = DialogResult.No;
                nemButton.Location = new Point(igenButton.Right + 40, ((uzenetLabel.Bottom - nemButton.Top < 100) ? 100 : uzenetLabel.Bottom + 20));
            }
            foreach (Control item in ablak.Controls)
            {
                if (item is Button btn)
                {
                    btn.Click += Btn_Click;
                }
            }
            ablak.ShowDialog();
            return dr;
        }

        private static void Btn_Click(object sender, EventArgs e)
        {
            if (sender is Button kattintottGomb)
            {
                dr = kattintottGomb.DialogResult;
            }
        }
    }
}
