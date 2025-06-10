using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BabenNew
{
    class sRezervasyon
    {

        #region fields

        private int _ID;
        private int _TableID;
        private int _ClientID;
        private DateTime _Date;
        private int _ClientCount;
        private int _Description;
        private int _AdditionID;
        #endregion
        #region as
        public int ID { get => _ID; set => _ID = value; }
        public int TableID { get => _TableID; set => _TableID = value; }
        public int ClientID { get => _ClientID; set => _ClientID = value; }
        public DateTime Date { get => _Date; set => _Date = value; }
        public int ClientCount { get => _ClientCount; set => _ClientCount = value; }
        public int Description { get => _Description; set => _Description = value; }
        public int AdditionID { get => _AdditionID; set => _AdditionID = value; }
        #endregion
        s genel = new s();
        //müsteriıd masa numarasına gore
        public int getByClientIdFromRezervasyon(int tableID)
        {
            
            int clientId = 0;
            SqlConnection con = new SqlConnection(genel.conString);
            SqlCommand cmd = new SqlCommand("Select top 1 MUSTERIID from rezervasyonlar where MASAID=@masaid order by MUSTERIID Desc" , con);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Parameters.Add("masaid", SqlDbType.Int).Value = tableID;




                clientId = Convert.ToInt32(cmd.ExecuteNonQuery());

            }
            catch (SqlException ex) 
            {
                string hata = ex.Message;
                throw;

            }

            finally
            {
                con.Dispose();
                con.Close();
            }


            return clientId;
        }


    }
}
