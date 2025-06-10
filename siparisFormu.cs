using System;
using System.Collections;
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
    public partial class siparisFormu : Form
    {
        public siparisFormu()
        {
            InitializeComponent();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            s._ServisTurNo = 1;
            s._AdisyonId = AdditionId.ToString();
            frmBill frm = new frmBill();
            this.Close();
            frm.Show();

        }
        void islem(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            switch (btn.Name)
            {
                case "btn1":
                    textBox2.Text += (1).ToString();
                    break;
                case "btn2":
                    textBox2.Text += (2).ToString();
                    break;
                case "btn3":
                    textBox2.Text += (3).ToString();
                    break;
                case "btn4":
                    textBox2.Text += (4).ToString();
                    break;
                case "btn5":
                    textBox2.Text += (5).ToString();
                    break;
                case "btn6":
                    textBox2.Text += (6).ToString();
                    break;
                case "btn7":
                    textBox2.Text += (7).ToString();
                    break;
                case "btn8":
                    textBox2.Text += (8).ToString();
                    break;
                case "btn9":
                    textBox2.Text += (9).ToString();
                    break;
                case "btn0":
                    textBox2.Text += (0).ToString();
                    break;




                default:
                    MessageBox.Show("sayı girin");
                    break;
            }

        }
        int tableId;
        int AdditionId = 0;
        private void siparisFormu_Load(object sender, EventArgs e)
        {

            masanolnl.Text = s._ButtonValue;
            sMasa sm = new sMasa();
            tableId = sm.TableGetByNumber(s._ButtonName);
            if (sm.TableGetByState(tableId,2)==true || sm.TableGetByState(tableId,4)==true)
            {
                sAdisyon ad = new sAdisyon();
                AdditionId = ad.getByAddition(tableId);
                 
                sSiparis orders = new sSiparis();
                orders.getByOrder(lvSiparisler, AdditionId);

               




            }

            btn1.Click += new EventHandler(islem);
            btn2.Click += new EventHandler(islem);
            btn3.Click += new EventHandler(islem);
            btn4.Click += new EventHandler(islem);
            btn5.Click += new EventHandler(islem);
            btn6.Click += new EventHandler(islem);
            btn7.Click += new EventHandler(islem);
            btn8.Click += new EventHandler(islem);
            btn9.Click += new EventHandler(islem);
            btn0.Click += new EventHandler(islem);


        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAnaYemek1_Click(object sender, EventArgs e)
        {
            lvMenu.Items.Clear();
            sUrunCesitleri uc = new sUrunCesitleri();
                uc.getByProductTypes(lvMenu, btnAnaYemek1);
            

            
        }

        private void btnİcecekler2_Click(object sender, EventArgs e)
        {
            lvMenu.Items.Clear();
            sUrunCesitleri uc = new sUrunCesitleri();
            uc.getByProductTypes(lvMenu, btnİcecekler2);
            
        }

        private void btnTatlılar3_Click(object sender, EventArgs e)
        {
            lvMenu.Items.Clear();
            sUrunCesitleri uc = new sUrunCesitleri();
            uc.getByProductTypes(lvMenu, btnTatlılar3);
        }

        private void btnSalatalar4_Click(object sender, EventArgs e)
        {
            lvMenu.Items.Clear();
            sUrunCesitleri uc = new sUrunCesitleri();
            uc.getByProductTypes(lvMenu, btnSalatalar4);
        }

        private void btnAperatifler5_Click(object sender, EventArgs e)
        {
            lvMenu.Items.Clear();
            sUrunCesitleri uc = new sUrunCesitleri();
            uc.getByProductTypes(lvMenu, btnAperatifler5);
        }

        private void btnCorba6_Click(object sender, EventArgs e)
        {
            lvMenu.Items.Clear();
            sUrunCesitleri uc = new sUrunCesitleri();
            uc.getByProductTypes(lvMenu, btnCorba6);
        }
        int sayac = 0;
        int sayac2 = 0;
        
        private void lvMenu_DoubleClick(object sender, EventArgs e)
        {

           if (textBox2.Text=="")
            {
                textBox2.Text = "1";
            }
            if (lvMenu.Items.Count>0)
            {
                sayac = lvSiparisler.Items.Count;
                lvSiparisler.Items.Add(lvMenu.SelectedItems[0].Text);
                lvSiparisler.Items[sayac].SubItems.Add(textBox2.Text);
                lvSiparisler.Items[sayac].SubItems.Add(lvMenu.SelectedItems[0].SubItems[2].Text);
                lvSiparisler.Items[sayac].SubItems.Add((Convert.ToDecimal(lvMenu.SelectedItems[0].SubItems[1].Text) * Convert.ToDecimal(textBox2.Text)).ToString());
                lvSiparisler.Items[sayac].SubItems.Add("0");
                sayac2 = lvyeni.Items.Count;
                lvSiparisler.Items[sayac].SubItems.Add(sayac2.ToString());


                lvyeni.Items.Add(AdditionId.ToString());
                lvyeni.Items[sayac2].SubItems.Add(lvMenu.SelectedItems[0].SubItems[2].Text);
                lvyeni.Items[sayac2].SubItems.Add(textBox2.Text);
                lvyeni.Items[sayac2].SubItems.Add(tableId.ToString());
                lvyeni.Items[sayac2].SubItems.Add(sayac2.ToString());
                sayac2++;
                textBox2.Text = "";



            }
        }
        ArrayList silinenler = new ArrayList();
        private void btnSiparis_Click(object sender, EventArgs e)
        {
            sMasa masa = new sMasa();
            sAdisyon newAddition = new sAdisyon();
            sSiparis saveOrder = new sSiparis();
            masaFormu mf = new masaFormu();

            bool sonuc = false;
             
            if (masa.TableGetByState(tableId, 1) == true)
            {
                newAddition.ServisTurNo = 1;
                
                newAddition.PersonelId = 1;
                newAddition.MasaId = tableId;
                newAddition.Tarih = DateTime.Now;
                sonuc = newAddition.setByAdditionNew(newAddition);
                masa.setChangeTableState(s._ButtonName, 2);
                if (lvSiparisler.Items.Count>0)
                {
                    for (int i = 0; i < lvSiparisler.Items.Count; i++)
                    {
                        saveOrder.MasaId = tableId;
                        saveOrder.UrunId = Convert.ToInt32(lvSiparisler.Items[i].SubItems[2].Text);
                        saveOrder.AdısyonId = newAddition.getByAddition(tableId);
                        saveOrder.Adet= Convert.ToInt32(lvSiparisler.Items[i].SubItems[1].Text);
                        saveOrder.setSaveOrder(saveOrder);  
                    }
                    this.Close();
                    mf.Show();
                }

            }
            else if (masa.TableGetByState(tableId,2)==true || masa.TableGetByState(tableId, 4))
            {
               
                if (lvyeni.Items.Count>0)
                {
                    for (int i = 0; i < lvyeni.Items.Count; i++)
                    {
                        saveOrder.MasaId = tableId;
                        saveOrder.UrunId = Convert.ToInt32(lvyeni.Items[i].SubItems[1].Text);
                        saveOrder.AdısyonId = newAddition.getByAddition(tableId);
                        saveOrder.Adet = Convert.ToInt32(lvyeni.Items[i].SubItems[2].Text);
                        saveOrder.setSaveOrder(saveOrder);
                    }
                    
                }
                if (silinenler.Count>0)
                {
                    foreach (string item in silinenler)
                    {
                        saveOrder.setDeleteOrder(Convert.ToInt32(item));
                    }
                }
                this.Close();
               
                mf.Show();
            }
            else if (masa.TableGetByState(tableId, 3) == true)
            {
                
                newAddition.ServisTurNo = 1;
                newAddition.PersonelId = 1;
                newAddition.MasaId = tableId;
                newAddition.Tarih = DateTime.Now;
                sonuc = newAddition.setByAdditionNew(newAddition);
                masa.setChangeTableState(s._ButtonName, 4);
                if (lvSiparisler.Items.Count > 0)
                {
                    for (int i = 0; i < lvSiparisler.Items.Count; i++)
                    {
                        saveOrder.MasaId = tableId;
                        saveOrder.UrunId = Convert.ToInt32(lvSiparisler.Items[i].SubItems[2].Text);
                        saveOrder.AdısyonId = newAddition.getByAddition(tableId);
                        saveOrder.Adet = Convert.ToInt32(lvSiparisler.Items[i].SubItems[1].Text);
                        saveOrder.setSaveOrder(saveOrder);
                    }
                    this.Close();
                    mf.Show();
                }
            }


            
            
          

        }

        private void lvyeni_DoubleClick(object sender, EventArgs e)
        {
           
        }

        private void lvSiparisler_DoubleClick(object sender, EventArgs e)
        {
            if (lvSiparisler.Items.Count > 0)
            {
                if (lvSiparisler.SelectedItems[0].SubItems[4].Text != "0")
                {
                    sSiparis saveOrder = new sSiparis();
                    saveOrder.setDeleteOrder(Convert.ToInt32(lvSiparisler.Items[0].SubItems[4].Text));

                }
                else
                {
                    for (int i = 0; i < lvyeni.Items.Count; i++)
                    {
                        if (lvyeni.Items[i].SubItems[4].Text == lvSiparisler.SelectedItems[0].SubItems[5].Text)
                        {
                            lvyeni.Items.RemoveAt(i);
                        }
                    }
                }
                lvSiparisler.Items.RemoveAt(lvSiparisler.SelectedItems[0].Index);
            }
        }

        private void btnKahvalti7_Click(object sender, EventArgs e)
        {
            lvMenu.Items.Clear();
            sUrunCesitleri uc = new sUrunCesitleri();
            uc.getByProductTypes(lvMenu, btnKahvalti7);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            sUrunCesitleri su = new sUrunCesitleri();
            su.getProductSearch(lvMenu, textBox1.Text);


        }

        private void lvMenu_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button22_Click(object sender, EventArgs e)
        {
            masaFormu mf = new masaFormu();
            this.Close();
            mf.Show();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
