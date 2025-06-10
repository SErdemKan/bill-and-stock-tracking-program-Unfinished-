using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BabenNew
{
    class cPaketler
    {
        private int _ID;
        private int _AdditionID_;
        private int _ClientId;
        private string _Description;
        private int _State;
        private int _Paytypeid;

        public int ID { get => _ID; set => _ID = value; }
        public int AdditionID_ { get => _AdditionID_; set => _AdditionID_ = value; }
        public int ClientId { get => _ClientId; set => _ClientId = value; }
        public string Description { get => _Description; set => _Description = value; }
        public int State { get => _State; set => _State = value; }
        public int Paytypeid { get => _Paytypeid; set => _Paytypeid = value; }

        s genel = new s();
        //paket servis açma
        public bool orderServieceOpen(cPaketler order)
        {
            bool result = false;
            SqlConnection con = new SqlConnection(genel.conString);
            SqlCommand cmd = new SqlCommand("Insert Into paketSiparis(ADISYONID,MUSTERIID,ODEMETURID,ACIKLAMA)values(@ADISYONID,@MUSTERIID,@ODEMETURID,@ACIKLAMA)", con);
            try
            {
                if (con.State==ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Parameters.Add("@ADISYONID", SqlDbType.Int).Value = order._AdditionID_;
                cmd.Parameters.Add("@MUSTERIID", SqlDbType.Int).Value = order._ClientId;
                cmd.Parameters.Add("@ODEMETURID", SqlDbType.Int).Value = order._Paytypeid;
                cmd.Parameters.Add("@ACIKLAMA", SqlDbType.VarChar).Value = order._Description;
                result = Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
            catch (Exception ex)
            {
                string hata = ex.Message;
                throw;
            }
            finally
            {
                con.Dispose();
                con.Close();
            }
            return result;
        }
        //paket servis kapatma
        public void orderServieceClose(int AdditionID)
        {
        
            SqlConnection con = new SqlConnection(genel.conString);
            SqlCommand cmd = new SqlCommand("Update paketSiparis set paketSiparis.Durum=1 from paketSiparis InnerJoin adisyonlar on paketSiparis.ADISYONID=adisyonlar.ID where paketSiparis.ADISYONID=@AdditionID", con);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Parameters.Add("@AdditionID", SqlDbType.Int).Value = AdditionID;
                
                Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
            catch (Exception ex)
            {
                string hata = ex.Message;
                throw;
            }
            finally
            {
                con.Dispose();
                con.Close();
            }
            
        }
        // ödeme tur idsi
        public int OdemeTurIdGetir(int adisyonID)
        {
            int odemeTurID = 0;
            SqlConnection con = new SqlConnection(genel.conString);
            SqlCommand cmd = new SqlCommand("Select paketSiparis.ODEMETURID from paketSiparis Inner Join adisyonlar on paketSiparis.ADISYONID=adisyonlar.ID where adisyonlar.ID=@adisyonID", con);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Parameters.Add("@adisyonID", SqlDbType.Int).Value = adisyonID;


                odemeTurID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                string hata = ex.Message;
                throw;
            }
            finally
            {
                con.Dispose();
                con.Close();
            }
            return odemeTurID;

        }
        //siparis kontrol
        public int musteriSonAdisyonIDGetir(int musteriID)
        {
            int no = 0;

            SqlConnection con = new SqlConnection(genel.conString);
            SqlCommand cmd = new SqlCommand("Select adisyonlar.ID from adisyonlar Inner Join paketSiparis on paketSiparis.ADISYONID=adisyonlar.ID where" +
                " (adisyonlar.Durum=0) and (paketSiparis.Durum=0) and paketSiparis.MUSTERIID=@musteriID", con);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Parameters.Add("@musteriID", SqlDbType.Int).Value = musteriID;


                no = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                string hata = ex.Message;
                throw;
            }
            finally
            {
                con.Dispose();
                con.Close();
            }


            return no;
        }
        // müsteri arama ekranında adisyonubul butonu açık mı değil mi kontrol. 
        public bool getCheckOpenAdditionID(int additionID)
        {
            bool result = false;
            SqlConnection con = new SqlConnection(genel.conString);
            SqlCommand cmd = new SqlCommand("Select *from adisyonlar where (Durum=0) and (ID=@additionID) ", con);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Parameters.Add("@additionID", SqlDbType.Int).Value = additionID;
                
                result = Convert.ToBoolean(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                string hata = ex.Message;
                throw;
            }
            finally
            {
                con.Dispose();
                con.Close();
            }
            return result;
        }
    }
}
