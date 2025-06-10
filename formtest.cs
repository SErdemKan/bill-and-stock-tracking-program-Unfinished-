using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BabenNew
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        string stringtimes = "";
        List<Times> timesAll;
        private void Form1_Load(object sender, EventArgs e)
        {
            int counter = 0;
            foreach (Process p in Process.GetProcesses())
            {
                if (p.ProcessName == "Ezan Vakti V2")
                {
                    counter++;
                    if (counter >= 2)
                        Application.Exit();
                }
            }
            if (counter < 2)
            {
                try
                {
                    RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                    key.SetValue("Ezan Vakti V2", "\"" + Application.ExecutablePath + "\"");
                }
                catch { }
                if (Directory.Exists(Application.StartupPath + "\\" + "Counties") == false)
                    Directory.CreateDirectory(Application.StartupPath + "\\" + "Counties");

                settingsGet();
                timesAll = getTimes(textBox1.Text);
                if (File.Exists(Application.StartupPath + "\\" + "Counties\\" + textBox1.Text + ".ini") == false)
                    saveDatas(stringtimes, textBox1.Text);
                Thread th = new Thread(ezanControl); th.Start();
            }
        }
        private TimeSpan dateCalc(string time)
        {
            DateTime timer = Convert.ToDateTime(time+":"+00);
            TimeSpan result =  timer- Convert.ToDateTime(DateTime.Now.ToShortTimeString());
            return result;
        }
        private void ezanControl()
        {
            this.Hide();
            for(; ; )
            {
                if (ezancontrol == true)
                {
                    foreach (Times time in timesAll)
                    {
                        if (ezancontrol == false)
                            break;
                        if (time.MiladiTarihKisa == DateTime.Now.ToShortDateString())
                        {
                            label6.Text = time.Imsak;
                            label2.Text = time.MiladiTarihKisa;
                            label8.Text = time.GunesDogus;
                            label10.Text = time.Ogle;
                            label12.Text = time.Ikindi;
                            label14.Text = time.Aksam;
                            label16.Text = time.Yatsi;
                            if (dateCalc(time.GunesDogus).ToString().Contains('-') == false)
                            {
                                label5.Text = dateCalc(time.GunesDogus).ToString();

                                label4.Text = "Güneş";
                            }
                            else if (dateCalc(time.Ogle).ToString().Contains('-') == false)
                            {
                                label5.Text = dateCalc(time.Ogle).ToString();
                                label4.Text = "Öğle";
                            }
                            else if (dateCalc(time.Ikindi).ToString().Contains('-') == false)
                            {
                                label5.Text = dateCalc(time.Ikindi).ToString();
                                label4.Text = "İkindi";
                            }
                            else if (dateCalc(time.Aksam).ToString().Contains('-') == false)
                            {
                                label5.Text = dateCalc(time.Aksam).ToString();
                                label4.Text = "Akşam";
                            }
                            else if (dateCalc(time.Yatsi).ToString().Contains('-') == false)
                            {
                                label5.Text = dateCalc(time.Yatsi).ToString();
                                label4.Text = "Yatsı";
                            }
                            if (Convert.ToInt32(label5.Text.Split(':')[1])==fire)
                            {
                                panel2.BackColor = Color.Blue;
                                mute(mute_ * 1000 * 60);
                                panel2.BackColor = Color.Black;
                            }
                            break;
                        }
                        Thread.Sleep(200);
                    }
                }
                else if (close == true)
                    break;
            }
            Application.Exit();
        }

        private const int APPCOMMAND_VOLUME_MUTE = 0x80000;
        private const int WM_APPCOMMAND = 0x319;

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageW(IntPtr hWnd, int Msg,IntPtr wParam, IntPtr lParam);

        private void mute(int sleep)
        {
            SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle,(IntPtr)APPCOMMAND_VOLUME_MUTE);
            Thread.Sleep(sleep);
            SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle, (IntPtr)APPCOMMAND_VOLUME_MUTE);
        }
        private void saveDatas(string allTimes,string countyCode)
        {
            FileStream fs = new FileStream(Application.StartupPath + "\\" + "Counties\\" + countyCode + ".ini", FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(allTimes);
            sw.Flush();
            sw.Close();
            fs.Close();
        }
        private List<Times> getTimes(string countyCode)
        {
            List<Times> times=null;
            try
            {
                stringtimes = new WebClient().DownloadString("http://ezanvakti.herokuapp.com/vakitler/" + countyCode);
                times = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Times>>(stringtimes);
            }
            catch 
            {
                if (File.Exists(countyCode+".ini")==true)
                {
                    string readAllLines = File.ReadAllText(Application.StartupPath + "\\" + "Counties\\" + countyCode + ".ini");
                    times = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Times>>(readAllLines);
                }
            }
                return times;
        }
        private List<Countries> getCountries()
        {
            List<Countries> countries = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Countries>>(new WebClient().DownloadString("http://ezanvakti.herokuapp.com/ulkeler"));
            return countries;
        }
        private List<Cities> getCities(string countryCode)
        {
            List<Cities> cities = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cities>>(new WebClient().DownloadString("http://ezanvakti.herokuapp.com/sehirler/" + countryCode));
            return cities;
        }
        private List<Counties> getCounties(string cityCode)
        {
            List<Counties> counties = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Counties>>(new WebClient().DownloadString("http://ezanvakti.herokuapp.com/ilceler/" + cityCode));
            return counties;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        bool close = false;
        private void button1_Click(object sender, EventArgs e)
        {
            ezancontrol = false;
            close = true;
            this.Hide();
        }
        string counties = "9541";
        int fire = 1;
        int mute_ = 4;

        private void settingsGet()
        {
            try
            {
                FileStream fs = new FileStream(Application.StartupPath + "\\" + "Settings.ini", FileMode.Open, FileAccess.Read);
                StreamReader sw = new StreamReader(fs);
                string yazi = sw.ReadLine();
                while (yazi != null)
                {
                    if (yazi.Split(':')[0] == "ilce") counties = yazi.Split(':')[1];
                    if (yazi.Split(':')[0] == "fire") fire = Convert.ToInt32(yazi.Split(':')[1]);
                    if (yazi.Split(':')[0] == "susturma") mute_ = Convert.ToInt32(yazi.Split(':')[1]);
                    yazi = sw.ReadLine();
                }
                sw.Close();
                fs.Close();
                textBox1.Text = counties;
                numericUpDown1.Value = fire;
                numericUpDown3.Value = mute_;
            }
            catch { }
        }
        private void settingsSet()
        {
            FileStream fs = new FileStream(Application.StartupPath + "\\" + "Settings.ini", FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("ilce:" + textBox1.Text);
            sw.WriteLine("fire:" + numericUpDown1.Value);
            sw.WriteLine("susturma:" + numericUpDown3.Value);
            sw.Flush();
            sw.Close();
            fs.Close();
            settingsGet();

        }

        bool ezancontrol = true;
        private void button3_Click(object sender, EventArgs e)
        {
            ezancontrol = false;
            if (textBox1.Text.Length >= 4)
            {
                List<Times> testtimes = getTimes(textBox1.Text);
                if (testtimes != null)
                {
                    panel2.BackColor = Color.Black;
                    timesAll = testtimes;
                    settingsSet();
                    MessageBox.Show("Ayarlarınız başarıyla kaydedildi.", "@kodzamani.tk");
                }
                else
                    MessageBox.Show("Girdiğiniz İlçe kodu geçersiz.", "@kodzamani.tk");
            }
            else
                MessageBox.Show("Girdiğiniz İlçe kodu geçersiz.", "@kodzamani.tk");
            ezancontrol = true;

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            counties = "9541";
            fire = 1;
            mute_ = 4;
            textBox1.Text = counties;
            numericUpDown1.Value = fire;
            numericUpDown3.Value = mute_;
            button3.PerformClick();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;
        }
        int Move;
        int Mouse_X;
        int Mouse_Y;

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            notifyIcon1.ShowBalloonTip(2000, "Ezan Vakti V2", "Ben buradayım çift tıklayarak tekrar açabilirsin.",ToolTipIcon.Info);
            notifyIcon1.Visible = true;
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://namazvakitleri.diyanet.gov.tr/tr-TR");
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (notifyIcon1.Visible == true)
            {
                notifyIcon1.Visible = false;
                this.Show();
            }
        }
    }
    public class Times
    {
        public string Imsak;
        public string GunesDogus;
        public string Gunes;
        public string GunesBatis;
        public string Ogle;
        public string Ikindi;
        public string Aksam;
        public string Yatsi;

        public string HicriTarihKisa;
        public string HicriTarihKisaIso8601;
        public string HicriTarihUzun;
        public string HicriTarihUzunIso8601;

        public string MiladiTarihKisa;
        public string MiladiTarihKisaIso8601;
        public string MiladiTarihUzun;
        public string MiladiTarihUzunIso8601;

        public string KibleSaati;

        public string AyinSekliURL;
    }
    public class Countries
    {
        public string UlkeAdi;
        public string UlkeAdiEn;
        public string UlkeID;
    }
    public class Cities
    {
        public string SehirAdi;
        public string SehirAdiEn;
        public string SehirID;
    }
    public class Counties
    {
        public string IlceAdi;
        public string IlceAdiEn;
        public string IlceID;
    }
}
