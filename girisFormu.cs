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
    public partial class girisFormu : Form
    {
        public girisFormu()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            sPersoneller p = new sPersoneller();

            p.personelGetbyInformation(comboBox1);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            s gnl = new s();
            sPersoneller p = new sPersoneller();
            bool result = p.personelEntryControl(textBox1.Text,s._personelId);
            if (result)
            {
                
                sPersonelHareketleri ch = new sPersonelHareketleri();
                ch.PersonelId = s._personelId;
                ch.Islem = "Giriş Yaptı";
                ch.Tarih = DateTime.Now;
                ch.PersonelActionSave(ch);

                

                this.Hide();
                menuFormu mf = new menuFormu();
                mf.Show();


            }
            else
            {
                MessageBox.Show("Şifreniz yanlış", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            sPersoneller p = (sPersoneller)comboBox1.SelectedItem;
            s._personelId = p.PersonelId;
            s._gorevId = p.PersonelGorevId;   
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Çıkmak istediğinize emin misiniz", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        bool islem = false;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!islem)
            {

                this.Opacity += 0.1;
                
            }
            if (this.Opacity==100.0)
            {
                islem = true;
            }
            
        }
    }
}
 