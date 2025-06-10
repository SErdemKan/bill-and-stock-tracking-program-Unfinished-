using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;


namespace BabenNew
{
    class sPersoneller
    {
        s gnl = new s();

        #region Fields

        private int _PersonelGorevId;
        private int _PersonelId;
        private string _PersonelAd;
        private string _PersonelSoyad;
        private string _PersonelParola;
        private string _PersonelKullaniciAdi;
        private bool _PersonelDurum;
        #endregion

        #region Properties

        public int PersonelGorevId { get => _PersonelGorevId; set => _PersonelGorevId = value; }
        public string PersonelAd { get => _PersonelAd; set => _PersonelAd = value; }
        public string PersonelSoyad { get => _PersonelSoyad; set => _PersonelSoyad = value; }
        public string PersonelParola { get => _PersonelParola; set => _PersonelParola = value; }
        public string PersonelKullaniciAdi { get => _PersonelKullaniciAdi; set => _PersonelKullaniciAdi = value; }
        public bool PersonelDurum { get => _PersonelDurum; set => _PersonelDurum = value; }
        public int PersonelId { get => _PersonelId; set => _PersonelId = value; }

        #endregion

        public bool personelEntryControl(string password, int UserId)
        {
            bool result = false;
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select * from Personeller where ID=@Id and PAROLA=@password", con);
            cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = UserId;
            cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = password;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }
                result = Convert.ToBoolean(cmd.ExecuteScalar());


            }
            catch (SqlException ex)
            {

                string hata = ex.Message;
                throw;
            }



            return result;

        }
        public void personelGetbyInformation(ComboBox cb)
        {
            try
            {
                cb.Items.Clear();
                SqlConnection con = new SqlConnection(gnl.conString);
                SqlCommand cmd = new SqlCommand("Select * from personeller", con);


                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    sPersoneller a = new sPersoneller();
                    a.PersonelId = Convert.ToInt32(dr["ID"]);
                    a._PersonelGorevId = Convert.ToInt32(dr["GOREVID"]);
                    a._PersonelAd = Convert.ToString(dr["AD"]);
                    a._PersonelSoyad = Convert.ToString(dr["SOYAD"]);
                    a._PersonelParola = Convert.ToString(dr["PAROLA"]);
                    a._PersonelKullaniciAdi = Convert.ToString(dr["KULLANICIADI"]);
                    a._PersonelDurum = Convert.ToBoolean(dr["Durum"]);
                    cb.Items.Add(a);
                }
                dr.Close();
                con.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show("HATA");
            }
        }
        public override string ToString()
        {
            return PersonelAd+ " " +PersonelSoyad ;
        }


    }
}
