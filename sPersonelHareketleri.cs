﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace BabenNew
{
    class sPersonelHareketleri
    {
        s gnl = new s();

        #region f
        private int _ID;
        private int _PersonelId;
        private string _Islem;
        private DateTime _Tarih;
        private bool _Durum;
        #endregion

        #region p
        public int ID { get => _ID; set => _ID = value; }
        public int PersonelId { get => _PersonelId; set => _PersonelId = value; }
        public string Islem { get => _Islem; set => _Islem = value; }
        public DateTime Tarih { get => _Tarih; set => _Tarih = value; }
        public bool Durum { get => _Durum; set => _Durum = value; } 
        #endregion

        public bool PersonelActionSave(sPersonelHareketleri ph)
        {
            bool result = false;

            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Insert Into personelHareketleri(PERSONELID,ISLEM,TARIH)Values(@personelID,@islem,@tarih)", con);
            try
            {
                if (con.State==ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Parameters.Add("@personelId", SqlDbType.Int).Value = ph._PersonelId;
                cmd.Parameters.Add("@islem", SqlDbType.VarChar).Value = ph._Islem;
                cmd.Parameters.Add("@tarih", SqlDbType.DateTime).Value = ph._Tarih;
                result = Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                throw;
            }
            return result;
        }
    }
}
