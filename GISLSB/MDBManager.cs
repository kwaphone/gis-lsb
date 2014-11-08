using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace GISLSB
{
    class MDBManager
    {
        private string connectstring;

        public MDBManager()
        {

        }

        public MDBManager(string strPath)
        {
            connectstring = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}", strPath);
        }

        // Connect to mdb
        private OleDbConnection GetConn()
        {
            OleDbConnection conn = null;
            try
            {
                conn = new OleDbConnection(connectstring);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库链接失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            return conn;
        }

        // excute sqlquery 
        public bool excute(string strsql)
        {
            bool isSuccess = false;
            OleDbConnection conn = null;
            try
            {
                conn = GetConn();
                OleDbCommand cmd = new OleDbCommand(strsql, conn);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据插入失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return isSuccess;
        }

        // get whole table
        public DataTable GetTableBySQL(string strSQL)
        {
            DataTable dtResult = null;
            OleDbConnection conn = null;
            try
            {
                conn = GetConn();
                OleDbDataAdapter da = new OleDbDataAdapter(strSQL, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                    dtResult = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库读取失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return dtResult;
        }
    }
}
