using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using LHR.lib;
using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Web;

namespace PlantOperation.Controllers
{
    public class OperationController : Controller
    {
        // GET: Operation
        public ActionResult Index()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "User");
            }
            return View();
        }
        // insert data
        [HttpPost]
        public ActionResult Insert()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "User");
            }
           
            var query = "?" + Request.Form["str"].ToString();
            NameValueCollection data = HttpUtility.ParseQueryString(query);
            // get day of the date
            var myDate = Convert.ToDateTime(data["date"].ToString());
            var day = myDate.Day.ToString();
            var month = myDate.Month.ToString();
            var year = myDate.Year.ToString();
            var cmd = new SqlCommand();
            cmd.CommandText = "addOperation";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@date", SqlDbType.Int).Value = day;
            cmd.Parameters.Add("@primary", SqlDbType.Float).Value = data["primary"].ToString();
            cmd.Parameters.Add("@a12", SqlDbType.Float).Value = data["a12"].ToString();
            cmd.Parameters.Add("@a19", SqlDbType.Float).Value = data["a19"].ToString();
            cmd.Parameters.Add("@a1x2", SqlDbType.Float).Value = data["a1x2"].ToString();
            cmd.Parameters.Add("@a2x3", SqlDbType.Float).Value = data["a2x3"].ToString();
            cmd.Parameters.Add("@m30", SqlDbType.Float).Value = data["m30"].ToString();
            cmd.Parameters.Add("@other", SqlDbType.Float).Value = data["other"].ToString();
            cmd.Parameters.Add("@l1", SqlDbType.Float).Value = data["l1"].ToString();
            cmd.Parameters.Add("@l2", SqlDbType.Float).Value = data["l2"].ToString();
            cmd.Parameters.Add("@l3", SqlDbType.Float).Value = data["l3"].ToString();
            cmd.Parameters.Add("@l4", SqlDbType.Float).Value = data["l4"].ToString();
            cmd.Parameters.Add("@l5", SqlDbType.Float).Value = data["l5"].ToString();
            cmd.Parameters.Add("@l6", SqlDbType.Float).Value = data["l6"].ToString();
            cmd.Parameters.Add("@l7", SqlDbType.Float).Value = data["l7"].ToString();
            cmd.Parameters.Add("@l8", SqlDbType.Float).Value = data["l8"].ToString();
            cmd.Parameters.Add("@l9", SqlDbType.Float).Value = data["l9"].ToString();
            cmd.Parameters.Add("@l10", SqlDbType.Float).Value = data["l10"].ToString();
            cmd.Parameters.Add("@l11", SqlDbType.Float).Value = data["l11"].ToString();
            cmd.Parameters.Add("@l12", SqlDbType.Float).Value = data["l12"].ToString();
            cmd.Parameters.Add("@l13", SqlDbType.Float).Value = data["l13"].ToString();
            cmd.Parameters.Add("@cs430", SqlDbType.Float).Value = data["c1"].ToString();
            cmd.Parameters.Add("@ch440", SqlDbType.Float).Value = data["c2"].ToString();
            cmd.Parameters.Add("@stcj411", SqlDbType.Float).Value = data["c3"].ToString();
            cmd.Parameters.Add("@swcj411", SqlDbType.Float).Value = data["c4"].ToString();
            cmd.Parameters.Add("@sunny", SqlDbType.Bit).Value = Convert.ToByte(data["s"].ToString());
            cmd.Parameters.Add("@cloudy", SqlDbType.Bit).Value = Convert.ToByte(data["c"].ToString());
            cmd.Parameters.Add("@rain", SqlDbType.Bit).Value = Convert.ToByte(data["r"].ToString());
            cmd.Parameters.Add("@note", SqlDbType.VarChar,50).Value = data["note"].ToString();
            cmd.Parameters.Add("@fulldate", SqlDbType.VarChar, 50).Value = data["date"].ToString();
            cmd.Parameters.Add("@userid", SqlDbType.Int).Value = Convert.ToInt32(Session["userid"].ToString());
            // insert data
            var i = new LHR.lib.DataAdapter().ExecNonPro(cmd);
            // return data back to the list
            var sql1 = "select * from operation where month(fulldate)=" + month + " and year(fulldate)=" + year + " order by [date] asc";
            var com = new SqlCommand();
            com.CommandText = sql1;
            com.CommandType = CommandType.Text;
            DataTable dt = new DataAdapter().RunQuery(com);
            var sr = string.Empty;
            sr = JsonConvert.SerializeObject(dt);
            return Content(sr);
        }
        [HttpPost]
        public ActionResult Update()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "User");
            }
            var query = "?" + Request.Form["str"].ToString();
            NameValueCollection data = HttpUtility.ParseQueryString(query);
            // get day of the date
            var myDate = Convert.ToDateTime(data["date"].ToString());
            var day = myDate.Day.ToString();
            var month = myDate.Month.ToString();
            var year = myDate.Year.ToString();
            var sr = string.Empty;
            if (Session["groupid"].ToString()=="1" || (Session["groupid"].ToString() == "2" && month ==DateTime.Now.Month.ToString() && year==DateTime.Now.Year.ToString()))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "editOperation";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@date", SqlDbType.Int).Value = day;
                cmd.Parameters.Add("@primary", SqlDbType.Float).Value = data["primary"].ToString();
                cmd.Parameters.Add("@a12", SqlDbType.Float).Value = data["a12"].ToString();
                cmd.Parameters.Add("@a19", SqlDbType.Float).Value = data["a19"].ToString();
                cmd.Parameters.Add("@a1x2", SqlDbType.Float).Value = data["a1x2"].ToString();
                cmd.Parameters.Add("@a2x3", SqlDbType.Float).Value = data["a2x3"].ToString();
                cmd.Parameters.Add("@m30", SqlDbType.Float).Value = data["m30"].ToString();
                cmd.Parameters.Add("@other", SqlDbType.Float).Value = data["other"].ToString();
                cmd.Parameters.Add("@l1", SqlDbType.Float).Value = data["l1"].ToString();
                cmd.Parameters.Add("@l2", SqlDbType.Float).Value = data["l2"].ToString();
                cmd.Parameters.Add("@l3", SqlDbType.Float).Value = data["l3"].ToString();
                cmd.Parameters.Add("@l4", SqlDbType.Float).Value = data["l4"].ToString();
                cmd.Parameters.Add("@l5", SqlDbType.Float).Value = data["l5"].ToString();
                cmd.Parameters.Add("@l6", SqlDbType.Float).Value = data["l6"].ToString();
                cmd.Parameters.Add("@l7", SqlDbType.Float).Value = data["l7"].ToString();
                cmd.Parameters.Add("@l8", SqlDbType.Float).Value = data["l8"].ToString();
                cmd.Parameters.Add("@l9", SqlDbType.Float).Value = data["l9"].ToString();
                cmd.Parameters.Add("@l10", SqlDbType.Float).Value = data["l10"].ToString();
                cmd.Parameters.Add("@l11", SqlDbType.Float).Value = data["l11"].ToString();
                cmd.Parameters.Add("@l12", SqlDbType.Float).Value = data["l12"].ToString();
                cmd.Parameters.Add("@l13", SqlDbType.Float).Value = data["l13"].ToString();
                cmd.Parameters.Add("@cs430", SqlDbType.Float).Value = data["c1"].ToString();
                cmd.Parameters.Add("@ch440", SqlDbType.Float).Value = data["c2"].ToString();
                cmd.Parameters.Add("@stcj411", SqlDbType.Float).Value = data["c3"].ToString();
                cmd.Parameters.Add("@swcj411", SqlDbType.Float).Value = data["c4"].ToString();
                cmd.Parameters.Add("@sunny", SqlDbType.Bit).Value = Convert.ToByte(data["s"].ToString());
                cmd.Parameters.Add("@cloudy", SqlDbType.Bit).Value = Convert.ToByte(data["c"].ToString());
                cmd.Parameters.Add("@rain", SqlDbType.Bit).Value = Convert.ToByte(data["r"].ToString());
                cmd.Parameters.Add("@note", SqlDbType.VarChar, 50).Value = data["note"].ToString();
                cmd.Parameters.Add("@fulldate", SqlDbType.VarChar, 50).Value = data["date"].ToString();
                cmd.Parameters.Add("@userid", SqlDbType.Int).Value = Convert.ToInt32(Session["userid"].ToString());
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = data["id"].ToString();
                // insert data
                var i = new LHR.lib.DataAdapter().ExecNonPro(cmd);
                // return data back to the list
                var sql1 = "select * from operation where month(fulldate)=" + month + " and year(fulldate)=" + year + " order by [date] asc";
                var com = new SqlCommand();
                com.CommandText = sql1;
                com.CommandType = CommandType.Text;
                DataTable dt = new DataAdapter().RunQuery(com);

                sr = JsonConvert.SerializeObject(dt);
            }
            else
            {
                // return data back to the list
                var sql1 = "select * from operation where month(fulldate)=" + month + " and year(fulldate)=" + year + " order by [date] asc";
                var com = new SqlCommand();
                com.CommandText = sql1;
                com.CommandType = CommandType.Text;
                DataTable dt = new DataAdapter().RunQuery(com);

                sr = JsonConvert.SerializeObject(dt);
            }
            return Content(sr);
        }
        [HttpPost]
        public ActionResult Get()
        {
            string dd = Request.Form["date"].ToString();
            var dstr = Convert.ToDateTime(dd);
            var month = dstr.Month.ToString();
            var year = dstr.Year.ToString();
            // return data back to the list
            var sql1 = "select * from operation where month(fulldate)=" + month + " and year(fulldate)=" + year + " order by [date] asc";
            var com = new SqlCommand();
            com.CommandText = sql1;
            com.CommandType = CommandType.Text;
            DataTable dt = new DataAdapter().RunQuery(com);
            var sr = string.Empty;
            sr = JsonConvert.SerializeObject(dt);
            return Content(sr);
        }
        [HttpPost]
        public ActionResult GetById()
        {
            var id = Request.Form["id"].ToString();
            var sql1 = "select * from operation where id=" + id;
            var com = new SqlCommand();
            com.CommandText = sql1;
            com.CommandType = CommandType.Text;
            DataTable dt = new DataAdapter().RunQuery(com);
            var sr = string.Empty;
            sr = JsonConvert.SerializeObject(dt);
            return Content(sr);
        }
        [HttpPost]
        public ActionResult GetTotal()
        {
            string dd = Request.Form["date"].ToString();
            var dstr = Convert.ToDateTime(dd);
            var month = dstr.Month.ToString();
            var year = dstr.Year.ToString();
            var cmd = new SqlCommand();
            cmd.CommandText = "getTotal";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@month", SqlDbType.Int).Value = month;
            cmd.Parameters.Add("@year", SqlDbType.Int).Value = year;
            DataTable dt = new DataAdapter().ExecPro(cmd);
            var sr = string.Empty;
            sr = JsonConvert.SerializeObject(dt);
            return Content(sr);
        }
        // delete a report by its id
        [HttpPost]
        public ActionResult Delete()
        {
            var id = Request.Form["id"].ToString();
            var myDate = Convert.ToDateTime(Request.Form["cdate"].ToString());
            var day = myDate.Day.ToString();
            var month = myDate.Month.ToString();
            var year = myDate.Year.ToString();
            var sms = "";
            if (Session["groupid"].ToString() == "1" || (Session["groupid"].ToString() == "2" && month == DateTime.Now.Month.ToString() && year == DateTime.Now.Year.ToString()))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "delete from operation where id=" + id;
                cmd.CommandType = CommandType.Text;
                var i = new DataAdapter().RunNonQuery(cmd);
                sms = "yes";
            }
            else
            {
                sms = "no";
            }

            return Content(sms);
        }
    }
}