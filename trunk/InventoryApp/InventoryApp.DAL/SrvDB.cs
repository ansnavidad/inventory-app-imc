using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace InventoryApp.DAL
{
    public class SrvDB
    {
        public static string GetDefaultConnection()
        {
            string conn = "";

            try
            {
                //conn = Mdb.getConnectionString("srvrdb");
                try
                {
                    conn = System.Configuration.ConfigurationManager.ConnectionStrings["LocalDB"].ToString();
                    
                }
                catch (Exception ex)
                {
                    return "Data Source=localhost;Initial Catalog=Example_MVVM;Integrated Security=Yes;";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return conn;
        }///////////////////////////////////////////////////

        public static DataTable ExecuteQuery(string query)
        {
            string strConn;
            SqlConnection conn;
            SqlCommand cmd;
            DataTable dt;

            //Get connections string
            try
            {
                strConn = SrvDB.GetDefaultConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //Open database connections
            try
            {
                conn = new SqlConnection(strConn);
                conn.Open();
               
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //Execute
            try
            {
                cmd = new SqlCommand(query, conn);
                cmd.CommandText = query;
                SqlDataReader reader = cmd.ExecuteReader();
                dt = new DataTable();

                dt.Load(reader);
                reader.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                //Mdb.setMsgAppLog(22, "[SrvDB.ExecuteQuery] Error on ExecuteReader : " + ex.Message, "", 1);
                throw ex;
            }

            return dt;
        }///////////////////////////////////////////////////

        public static int ExecuteNonQuery(string stmt)
        {
            string strConn;
            SqlConnection conn;
            SqlCommand cmd;
            
            //Get connections string
            try
            {
                strConn = SrvDB.GetDefaultConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //Open database connections
            try
            {
                conn = new SqlConnection(strConn);
                conn.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //Execute
            try
            {
                cmd = new SqlCommand(stmt, conn);
                cmd.CommandTimeout = 0;
                int res = 0;
                res = cmd.ExecuteNonQuery();
                conn.Close();

                return res;
            }
            catch (Exception ex)
            {
                conn.Close();
                //Mdb.setMsgAppLog(22, "[SrvDB.ExecuteNonQuery] Error on executescalar : " + ex.Message, "", 1);
                throw ex;
            }
        }///////////////////////////////////////////////////

        public static long ExecuteNonQueryLong(string stmt)
        {
            string strConn;
            SqlConnection conn;
            SqlCommand cmd;

            //Get connections string
            try
            {
                strConn = SrvDB.GetDefaultConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //Open database connections
            try
            {
                conn = new SqlConnection(strConn);
                conn.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //Execute
            try
            {
                cmd = new SqlCommand(stmt, conn);
                cmd.CommandTimeout = 0;
                long res = 0;
                res = cmd.ExecuteNonQuery();
                conn.Close();

                return res;
            }
            catch (Exception ex)
            {
                conn.Close();
                //Mdb.setMsgAppLog(22, "[SrvDB.ExecuteNonQuery] Error on executescalar : " + ex.Message, "", 1);
                throw ex;
            }
        }///////////////////////////////////////////////////

        #region execute SP

        public static DataSet ExecuteCommand(SqlCommand cmd)
        {
            string strConn = SrvDB.GetDefaultConnection();
            
            SqlConnection cnn = new SqlConnection(strConn);
            cmd.Connection = cnn;

            DataSet ds = new DataSet();

            try
            {
                using (cnn)
                {
                    using (cmd)
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        da.Fill(ds);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }//end ExecuteSP

        #endregion


    }
}