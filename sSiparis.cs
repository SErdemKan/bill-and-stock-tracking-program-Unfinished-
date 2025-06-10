using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BabenNew
{
    class sSiparis
    {
        s genel = new s();
        private int _Id;
        private int _AdısyonId;
        private int _UrunId;
        private int _adet;
        private int _masaId;

        public int Id { get => _Id; set => _Id = value; }
        public int AdısyonId { get => _AdısyonId; set => _AdısyonId = value; }
        public int UrunId { get => _UrunId; set => _UrunId = value; }
        public int Adet { get => _adet; set => _adet = value; }
        public int MasaId { get => _masaId; set => _masaId = value; }

        //siparişleri getir
        public void getByOrder(ListView lv , int AdisyonId)
        {
            SqlConnection con = new SqlConnection(genel.conString);
            SqlCommand cmd = new SqlCommand("select URUNAD,FIYAT,satislar.ID,URUNID,satislar.ADET from satislar Inner Join urunler on Satislar.URUNID=Urunler.ID where ADISYONID=@AdisyonId",con);
            SqlDataReader dr = null;
            cmd.Parameters.Add("@AdisyonId", SqlDbType.Int).Value = AdisyonId;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                dr = cmd.ExecuteReader();
                int sayac = 0;
                while (dr.Read())
                {
                    lv.Items.Add(dr["URUNAD"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["ADET"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["URUNID"].ToString());
                    lv.Items[sayac].SubItems.Add(Convert.ToString(Convert.ToDecimal(dr["FIYAT"])*Convert.ToDecimal(dr["ADET"])));
                    lv.Items[sayac].SubItems.Add(dr["ID"].ToString());
                    sayac++;

                }
            }
            catch (SqlException ex)
            {

               string hata = ex.Message;
            }
            finally
            {
                dr.Close();
                con.Dispose();
                con.Close();
            }
                
        }
        public bool setSaveOrder(sSiparis Bilgiler)
        {
            bool sonuc = false;
            SqlConnection con = new SqlConnection(genel.conString);
            SqlCommand cmd = new SqlCommand("Insert Into satislar(ADISYONID,URUNID,ADET,MASAID) values(@AdisyonNo,@UrunId,@Adet,@masaId)", con);
            try
            {
                if (con.State==ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Parameters.Add("@AdisyonNo",SqlDbType.Int).Value=Bilgiler._AdısyonId;
                cmd.Parameters.Add("@UrunId", SqlDbType.Int).Value = Bilgiler._UrunId;
                cmd.Parameters.Add("Adet", SqlDbType.Int).Value = Bilgiler._adet;
                cmd.Parameters.Add("@masaId", SqlDbType.Int).Value = Bilgiler._masaId;
                sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {

                string hata = ex.Message;

            }
            finally
            {
                con.Dispose();
                con.Close();
            }
            return sonuc;

        }
        public void setDeleteOrder(int satisId)
        {
            SqlConnection con = new SqlConnection(genel.conString);
            SqlCommand cmd = new SqlCommand("Delete From satislar Where ID=@satisId", con);
            cmd.Parameters.Add("@satisID", SqlDbType.Int).Value = satisId;
            if (con.State==ConnectionState.Closed)
            {
                con.Open();

            }
            cmd.ExecuteNonQuery();
            con.Dispose();
            con.Close();


        }

    }
    
}
