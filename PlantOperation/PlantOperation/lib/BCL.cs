using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
namespace LHR.lib
{

    // this class is for system security setup
    public class CoreSecurity
    {
        // this method is to calculate hash md5 from a string
        public static string getMd5Hash(string inputString)
        {
            MD5 md5hash = MD5.Create();
            byte[] data = md5hash.ComputeHash(Encoding.UTF8.GetBytes(inputString));
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                str.Append(data[i].ToString("X2"));
            }
            return str.ToString();
        }
    }
    // base class for base object

    public class BaseObject
    {
        public int Id { get; set; }
        private string _name;
        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public String Name
        {
            get { return this._name; }
            set { this._name = value; }
        }
        public int IsDeleted { get; set; }
        public BaseObject()
        {
            this.Id = 0;
            this.Name = "";
            this.Description = "";
            this.IsDeleted = 0;
        }
    }
    // data access class
    public class DataAdapter
    {
        private string conStr;
        private SqlConnection con;
        public DataAdapter()
        {
            this.conStr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
            con = new SqlConnection(this.conStr);
        }
        // the method to run none query sql
        public bool RunNonQuery(SqlCommand sqlcmd)
        {
            bool State = false;
            //try
            //{
            con.Open();
            sqlcmd.Connection = con;
            sqlcmd.ExecuteNonQuery();
            con.Close();
            State = true;

            //}
            //catch (Exception e)
            //{
            //    con.Close();
            //    State = false;

            //}

            return State;
        }

        // the method to run sql that return row
        // the sql command must be select statement
        public DataTable RunQuery(SqlCommand sqlcmd)
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter(sqlcmd.CommandText, con);
                da.Fill(dt);
                con.Close();
            }
            catch (Exception e)
            {
                con.Close();
            }
            return dt;
        }
        // method to return a single row of data
        public DataRow RunScalarQuery(SqlCommand sqlcmd)
        {

            DataTable dt = new DataTable();
            DataRow dr = dt.NewRow();
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd.CommandText, con);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    dr = dt.Rows[0];
                }
                con.Close();
            }
            catch (Exception e)
            {
                con.Close();
            }
            return dr;

        }
        /// <summary>
        /// Executes Nonquery stored procedures such as insert or delete
        /// </summary>
        /// <param name="sqlcmd"></param>
        /// <returns></returns>
        public bool ExecNonPro(SqlCommand sqlcmd)
        {
            bool State = false;
            try
            {
                con.Open();
                sqlcmd.Connection = con;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                string result = sqlcmd.ExecuteScalar().ToString();
                int i = Convert.ToInt32(result);
                State = Convert.ToBoolean(i);
                con.Close();
            }
            catch (Exception)
            {
                con.Close();
            }
            return State;
        }
        /// <summary>
        /// Get the latest transaction of a tool.
        /// It returns the image on or off.
        /// </summary>
        /// <param name="sqlcmd"></param>
        /// <returns></returns>
        public string ExecSalarPro(SqlCommand sqlcmd)
        {
            string str = "";
            try
            {
                con.Open();
                sqlcmd.Connection = con;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                str = sqlcmd.ExecuteScalar().ToString();
                con.Close();
            }
            catch (Exception)
            {
                con.Close();
            }
            return str;
        }
        /// <summary>
        /// Executes stored procedure that returns data as table
        /// </summary>
        /// <param name="sqlcmd"></param>
        /// <returns></returns>
        public DataTable ExecPro(SqlCommand sqlcmd)
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                sqlcmd.Connection = con;
                sqlcmd.CommandType = CommandType.StoredProcedure;

                // dataAdapter
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                con.Close();
            }
            catch (Exception)
            {
                con.Close();
            }
            return dt;
        }
    }
}
