using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BabenNew
{
    public partial class menuFormu : Form
    {
        public menuFormu()
        {
            InitializeComponent();
            
        }

        private void btnmasa_Click(object sender, EventArgs e)
        {
            masaFormu mf = new masaFormu();
            this.Close();
            mf.Show();
            
        }

        private void btnpkt_Click(object sender, EventArgs e)
        {
            siparisFormu sf = new siparisFormu();
            this.Close();
            sf.Show();

        }

        private void btnrez_Click(object sender, EventArgs e)
        {
            rezFormu rf = new rezFormu();
            this.Close();
            rf.Show();
        }

        private void btnmus_Click(object sender, EventArgs e)
        {
            musterilerFormu msf = new musterilerFormu();
            this.Close();
            msf.Show();

        }

        private void btnkasa_Click(object sender, EventArgs e)
        {
            kasaFormu kf = new kasaFormu();
            this.Close();
            kf.Show();

        }

        private void btnmutfak_Click(object sender, EventArgs e)
        {
            mutfakFormu mtf = new mutfakFormu();
            this.Close();
            mtf.Show();

        }

        private void btnCıkıs_Click(object sender, EventArgs e)
        {
            cikisFormu ckf = new cikisFormu();
            if (MessageBox.Show("Çıkmak istediğinize emin misiniz?","Uyarı!" ,MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                Application.Exit();
            }

        }

        private void btnKilit_Click(object sender, EventArgs e)
        {
            kilitFormu klt = new kilitFormu();
            this.Close();
            klt.Show();

        }

        private void btnAyar_Click(object sender, EventArgs e)
        {
            ayarlarFormu af = new ayarlarFormu();
            this.Close();
            af.Show();

        }

        private void btnRpr_Click(object sender, EventArgs e)
        {
            raporlarFormu rpf = new raporlarFormu();
            this.Close();
            rpf.Show();

        }

        private void menuFormu_Load(object sender, EventArgs e)
        {
            btnAyar.Visible = false;
            btnRpr.Visible = false;
        }

        private void spotifyopen_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Users\Erdem\AppData\Roaming\Spotify\Spotify.exe");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            btnRpr.Visible = true;
            btnAyar.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
        }
    }
}
