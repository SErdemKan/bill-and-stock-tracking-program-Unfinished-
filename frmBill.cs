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
    public partial class frmBill : Form
    {
        public frmBill()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            masaFormu mf = new masaFormu();
            this.Close();

            mf.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        sSiparis ss = new sSiparis();
        int odemeTurID = 0;
        private void frmBill_Load(object sender, EventArgs e)
        {
            if (s._ServisTurNo == 1)
            {

                lbAdisyonId.Text = s._AdisyonId;
                txtİndirimTutarı.TextChanged += new EventHandler(txtİndirimTutarı_TextChanged);
                ss.getByOrder(lvUrunler, Convert.ToInt32(lbAdisyonId.Text));
                if (lvUrunler.Items.Count > 0)
                {
                    decimal toplam = 0;
                    for (int i = 0; i < lvUrunler.Items.Count; i++)
                    {
                        toplam += Convert.ToDecimal(lvUrunler.Items[i].SubItems[3].Text);
                    }
                    lbFiyat.Text = string.Format("{0:0.000}", toplam);
                    lbOdenecek.Text = string.Format("{0:0.000}", toplam);
                    decimal kdv = Convert.ToDecimal(lbOdenecek.Text) * 18 / 100;
                    lbKdv.Text = string.Format("{0:0.000}", kdv);
                }
                groupBox4.Visible = false;
                txtİndirimTutarı.Clear();
            }
            else if (s._ServisTurNo == 2)
            {
                lbAdisyonId.Text = s._AdisyonId;
                cPaketler cp = new cPaketler();
                odemeTurID = cp.OdemeTurIdGetir(Convert.ToInt32(lbAdisyonId.Text));
                txtİndirimTutarı.TextChanged += new EventHandler(txtİndirimTutarı_TextChanged);
                ss.getByOrder(lvUrunler, Convert.ToInt32(lbAdisyonId.Text));

                if (odemeTurID==1)
                {
                    rbNakit.Checked = true;

                }
                else if(odemeTurID == 2)
                {
                    rbKredi.Checked = true;
                }
                else if (odemeTurID == 4)
                {
                    rbTicket.Checked = true;
                }


                if (lvUrunler.Items.Count > 0)
                {
                    decimal toplam = 0;
                    for (int i = 0; i < lvUrunler.Items.Count; i++)
                    {
                        toplam += Convert.ToDecimal(lvUrunler.Items[i].SubItems[3].Text);

                    }
                    lbFiyat.Text = string.Format("{0:0.000}", toplam);
                    lbOdenecek.Text = string.Format("{0:0.000}", toplam);
                    decimal kdv = Convert.ToDecimal(lbOdenecek.Text) * 18 / 100;
                    lbKdv.Text = string.Format("{0:0.000}", kdv);
                }
                groupBox4.Visible = false;
                txtİndirimTutarı.Clear();

            }
        }

        private void txtİndirimTutarı_TextChanged(object sender, EventArgs e)
        {
            
                try
            {
                if (Convert.ToDecimal(txtİndirimTutarı.Text) < Convert.ToDecimal(lbFiyat.Text))
                {
                    try
                    {
                        lbİndirim.Text = string.Format("{0:0.000}", Convert.ToDecimal(txtİndirimTutarı.Text));

                    }
                    catch (Exception)
                    {

                        lbİndirim.Text = string.Format("{0:0.000}", 0);
                    }
                }
                else
                {
                    MessageBox.Show("İndirim tutarı toplam tutardan fazla olamaz !");
                }
            }
            catch (Exception)
            {

                lbİndirim.Text = string.Format("{0:0.000}", 0);
            }
            
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                groupBox4.Visible = true;
                txtİndirimTutarı.Clear();
            }
            else
            {
                groupBox4.Visible = false;
                txtİndirimTutarı.Clear();   
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Çıkmak istediğinize emin misiniz?", "Uyarı!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void lbİndirim_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(lbİndirim.Text)>0)
           {
                decimal odenecek = 0;
               lbOdenecek.Text = lbFiyat.Text;
                odenecek = Convert.ToDecimal(lbOdenecek.Text) - Convert.ToDecimal(lbİndirim.Text);
               lbOdenecek.Text = string.Format("{0:0.000}", odenecek);
                
           }
            decimal kdv = Convert.ToDecimal(lbOdenecek.Text)*18/100;
            lbKdv.Text = string.Format("{0:0.000}", kdv);
        }
        sMasa masa = new sMasa();
        sRezervasyon rez = new sRezervasyon();
        private void button4_Click(object sender, EventArgs e)
        {
            if (s._ServisTurNo==1)
            {
                int masaıd = masa.TableGetByNumber(s._ButtonName);
                int musteriID = 0;
                if (masa.TableGetByState(masaıd,4)==true)
                {
                    musteriID = rez.getByClientIdFromRezervasyon(masaıd);

                }
                else
                {
                    musteriID = 1;
                }
                int odemeTurID = 0;
                if (rbNakit.Checked==true)
                {
                    odemeTurID = 1;
                }
                if (rbKredi.Checked==true)
                {
                    odemeTurID = 2;
                }
                if (rbTicket.Checked == true)
                {
                    odemeTurID = 3;
                }
                sOdeme odeme = new sOdeme();
                //(ADISYONID,ODEMETURID,MUSTERIID,ARATOPLAM,KDVTUTARI,INDIRIM,TOPLAMTUTAR) values (@ADISYONID,@ODEMETURID,@MUSTERIID,@ARATOPLAM,@KDVTUTARI,@INDIRIM,@TOPLAMTUTAR)
                odeme.AdisyonID = Convert.ToInt32(lbAdisyonId.Text);

                odeme.OdemeTurID = odemeTurID;
                odeme.MusteriID = musteriID;
                odeme.AraToplam = Convert.ToInt32(lbOdenecek.Text);



            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
