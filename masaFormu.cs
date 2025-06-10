using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BabenNew
{
    public partial class masaFormu : Form
    { 
        public masaFormu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            siparisFormu sf = new siparisFormu();
            int uzunluk = btnMasa1.Text.Length;

            s._ButtonValue = btnMasa1.Text.Substring(uzunluk -6, 6);
            s._ButtonName = btnMasa1.Name;
            this.Close();
            sf.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            siparisFormu sf = new siparisFormu();
            int uzunluk = btnMasa2.Text.Length;

            s._ButtonValue = btnMasa2.Text.Substring(uzunluk - 6, 6);
            s._ButtonName = btnMasa2.Name;
            this.Close();
            sf.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            siparisFormu sf = new siparisFormu();
            int uzunluk = btnMasa3.Text.Length;

            s._ButtonValue = btnMasa3.Text.Substring(uzunluk - 6, 6);
            s._ButtonName = btnMasa3.Name;
            this.Close();
            sf.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            siparisFormu sf = new siparisFormu();
            int uzunluk = btnMasa4.Text.Length;

            s._ButtonValue = btnMasa4.Text.Substring(uzunluk - 6, 6);
            s._ButtonName = btnMasa4.Name;
            this.Close();
            sf.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            siparisFormu sf = new siparisFormu();
            int uzunluk = btnMasa5.Text.Length;

            s._ButtonValue = btnMasa5.Text.Substring(uzunluk - 6, 6);
            s._ButtonName = btnMasa5.Name;
            this.Close();
            sf.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            siparisFormu sf = new siparisFormu();
            int uzunluk = btnMasa6.Text.Length;

            s._ButtonValue = btnMasa6.Text.Substring(uzunluk - 6, 6);
            s._ButtonName = btnMasa6.Name;
            this.Close();
            sf.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            siparisFormu sf = new siparisFormu();
            int uzunluk = btnMasa7.Text.Length;

            s._ButtonValue = btnMasa7.Text.Substring(uzunluk - 6, 6);
            s._ButtonName = btnMasa7.Name;
            this.Close();
            sf.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            siparisFormu sf = new siparisFormu();
            int uzunluk = btnMasa8.Text.Length;

            s._ButtonValue = btnMasa8.Text.Substring(uzunluk - 6, 6);
            s._ButtonName = btnMasa8.Name;
            this.Close();
            sf.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            siparisFormu sf = new siparisFormu();
            int uzunluk = btnMasa9.Text.Length;

            s._ButtonValue = btnMasa9.Text.Substring(uzunluk - 6, 6);
            s._ButtonName = btnMasa9.Name;
            this.Close();
            sf.ShowDialog();
        }
        s bglnt = new s();
        private void masaFormu_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(bglnt.conString);
            SqlCommand cmd = new SqlCommand("Select DURUM,ID from masalar", con);
            SqlDataReader sdr = null;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();

            }
            sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                foreach (Control item in this.Controls)
                {
                    if (item is Button)
                    {
                        if (item.Name == "btnMasa" + sdr["ID"].ToString() && sdr["DURUM"].ToString() == "1")
                        {
                            item.BackgroundImage = (System.Drawing.Image)(Properties.Resources.bosmasa);

                        }
                        else if (item.Name == "btnMasa" + sdr["ID"].ToString() && sdr["DURUM"].ToString() == "2")
                        {
                            sMasa sm = new sMasa();
                            DateTime dt1 = Convert.ToDateTime(sm.SessionSum(2, sdr["ID"].ToString()));
                            DateTime dt2 = DateTime.Now;
                            string st1 = Convert.ToDateTime(sm.SessionSum(2,sdr["ID"].ToString())).ToShortTimeString();
                            string st2 = DateTime.Now.ToShortTimeString();
                            DateTime t1 = dt1.AddMinutes(DateTime.Parse(st1).TimeOfDay.TotalMinutes);
                            DateTime t2 = dt2.AddMinutes(DateTime.Parse(st2).TimeOfDay.TotalMinutes);

                            var fark = t2 - t1;
                            item.Text = "Masa" + sdr["ID"].ToString()+"\n"+string.Format("{0}{1}{2}", fark.Days > 0 ? string.Format("{0} gün", fark.Days) : "",
                               fark.Hours > 0 ? string.Format("{0} Saat", fark.Hours) : "",
                              fark.Minutes > 0 ? string.Format("{0} Dakika", fark.Minutes) : "").Trim();
                           item.BackgroundImage = (System.Drawing.Image)(Properties.Resources.newdolumasa);
                        }
                        else if (item.Name == "btnMasa" + sdr["ID"].ToString() && sdr["DURUM"].ToString() == "3")
                        {
                            item.BackgroundImage = (System.Drawing.Image)(Properties.Resources.newrezervemasa);
                        }
                        else if (item.Name == "btnMasa" + sdr["ID"].ToString() && sdr["DURUM"].ToString() == "4")
                        {
                            item.BackgroundImage = (System.Drawing.Image)(Properties.Resources.newrezervemasa2);
                        }

                    }
                }
            }

        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Close();
            menuFormu mf = new menuFormu();
            mf.Show();
        }

        private void btnMasa10_Click(object sender, EventArgs e)
        {
            siparisFormu sf = new siparisFormu();
            int uzunluk = btnMasa10.Text.Length;

            s._ButtonValue = btnMasa10.Text.Substring(uzunluk - 6, 6);
            s._ButtonName = btnMasa10.Name;
            this.Close();
            sf.ShowDialog();
        }
    }
}
