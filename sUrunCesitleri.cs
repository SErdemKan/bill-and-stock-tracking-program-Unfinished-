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
    class sUrunCesitleri
    {
        s genel = new s();
        private int _UrunTurNo;
        private int _KategoriAd;
        private string _Acıklama;

        public int UrunTurNo { get => _UrunTurNo; set => _UrunTurNo = value; }
        public int KategoriAd { get => _KategoriAd; set => _KategoriAd = value; }
        public string Acıklama { get => _Acıklama; set => _Acıklama = value; }

        
            public void getByProductTypes(ListView Cesitler, Button btn)
            
        {
            Cesitler.Items.Clear();
        SqlConnection conn = new SqlConnection(genel.conString);
        SqlCommand cmdn = new SqlCommand("Select URUNAD,FIYAT,urunler.ID From kategoriler Inner Join urunler on kategoriler.ID=urunler.KATEGORIID where urunler.KATEGORIID " +
            "= @KATEGORIID",conn);
            string aa = btn.Name;
            int uzunluk = aa.Length;

            cmdn.Parameters.Add("@KATEGORIID", SqlDbType.Int).Value = aa.Substring(uzunluk - 1, 1);
            if (conn.State==ConnectionState.Closed)
            {
                conn.Open();

            }
            SqlDataReader dr = cmdn.ExecuteReader();
            int i = 0;
            while (dr.Read())
            {
                Cesitler.Items.Add(dr["URUNAD"].ToString());
                Cesitler.Items[i].SubItems.Add(dr["FIYAT"].ToString());
                Cesitler.Items[i].SubItems.Add(dr["ID"].ToString());
                i++;
            }
            dr.Close();
            conn.Dispose();
            conn.Close();

        }
        public void getProductSearch(ListView Cesitler, string txt)

        {
            Cesitler.Items.Clear();
            SqlConnection conn = new SqlConnection(genel.conString);
            SqlCommand cmdn = new SqlCommand("Select * from urunler where URUNAD like '%"+txt+"%'"  , conn);
            

            cmdn.Parameters.Add("@URUNAD", SqlDbType.VarChar).Value = txt;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();

            }
            SqlDataReader dr = cmdn.ExecuteReader();
            int i = 0;
            while (dr.Read())
            {
                Cesitler.Items.Add(dr["URUNAD"].ToString());
                Cesitler.Items[i].SubItems.Add(dr["FIYAT"].ToString());
                Cesitler.Items[i].SubItems.Add(dr["ID"].ToString());
                i++;
            }
            dr.Close();
            conn.Dispose();
            conn.Close();

        }

    }  

}
