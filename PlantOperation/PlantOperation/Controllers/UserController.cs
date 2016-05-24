using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using LHR.lib;
using Newtonsoft.Json;
using System;

namespace PlantOperation.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "User");
            }
            return View();
        }
        // get data
        [HttpPost]
        public ActionResult Get()
        {
            var pn = Request.Form["pageno"];
            var cmd = new SqlCommand();
            cmd.CommandText = "getUserByPage";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@pageno", SqlDbType.Int).Value = Convert.ToInt32(pn);
            DataTable dt = new DataAdapter().ExecPro(cmd);
            var sr = string.Empty;
            sr = JsonConvert.SerializeObject(dt);
            return Content(sr);
        }
        // return user login form
        public ActionResult Login()
        {
            return View("Login");
        }
        // return new user form
        public ActionResult New()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "User");
            }
            return View("New");
        }
        // return edit user form
        public ActionResult Edit(int id)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "User");
            }
            ViewBag.UserId = id.ToString();
            return View("Edit");
        }
        // change password form
        public ActionResult ChangePassword()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "User");
            }
            return View("ChangePassword");
        }
        [HttpPost]
        public ActionResult ChangePass()
        {
            var userid = Request.Form["userid"].ToString();
            var pass = CoreSecurity.getMd5Hash(Request.Form["pass1"].ToString());
            var sql = "update users set password=@pass where id=" + userid;
            var cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@pass", SqlDbType.VarChar, 50).Value = pass;
            var i = new DataAdapter().RunNonQuery(cmd);
            var sms = "";
            if (i)
            {
                sms = "Your password has been changed!";
            }
            else
            {
                sms = "Cannot change your password.";
            }
            return Content(sms);
        }
        // do login
        [HttpPost]
        public ActionResult DoLogin()
        {
            var username = Request.Form["username"];
            var pass = Request.Form["pass"];
            var sm = "";
            var cmd = new SqlCommand();
            cmd.CommandText = "getUser";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@username", SqlDbType.VarChar, 90).Value = username.ToString();
            DataTable dt = new LHR.lib.DataAdapter().ExecPro(cmd);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dr = dt.Rows[0];
               if (dr[7].ToString() == LHR.lib.CoreSecurity.getMd5Hash(pass))
               {
                    Session["userid"] = dr[0].ToString();
                    Session["username"] = username;
                    Session["groupid"] = dr[8].ToString();
                    sm = "yes";
                }
                else
                {
                    sm = "no";
                }
            }
            else
            {
                sm = "no";
            }
            return Content(sm);
        }
        // sign out
        public ActionResult Signout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "User");
        }
        // insert a new user
        [HttpPost]
        public ActionResult Insert()
        {
            var sms = "";
            var fname = Request.Form["firstname"].ToString();
            var lname = Request.Form["lastname"].ToString();
            var gender = Request.Form["gender"].ToString();
            var email = Request.Form["email"].ToString();
            var phone = Request.Form["phone"].ToString();
            var groupid = Request.Form["groupid"].ToString();
            var username = Request.Form["username"].ToString();
            var password = Request.Form["password"].ToString();
            // check if user name already exist or not
            var cmd = new SqlCommand();
            cmd.CommandText = "getUser";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@username", SqlDbType.VarChar, 90).Value = username;
            DataTable dt = new LHR.lib.DataAdapter().ExecPro(cmd);
            if (dt.Rows.Count>0)
            {
                sms = "Username already exist! Try a new one.";
            }
            else
            {
                // insert user
                var sql = "insert into users(firstname,lastname,gender,email,phone,username,[password],groupid) ";
                sql += " values(@fname, @lname, @gender, @email, @phone, @username, @pass, @groupid)";
                // prepares statement
                var com = new SqlCommand();
                com.CommandText = sql;
                com.CommandType = CommandType.Text;
                com.Parameters.Add("@fname", SqlDbType.VarChar, 50).Value = fname;
                com.Parameters.Add("@lname", SqlDbType.VarChar, 50).Value = lname;
                com.Parameters.Add("@gender", SqlDbType.VarChar, 50).Value = gender;
                com.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = email;
                com.Parameters.Add("@phone", SqlDbType.VarChar, 50).Value = phone;
                com.Parameters.Add("@username", SqlDbType.VarChar, 50).Value = username;
                com.Parameters.Add("@pass", SqlDbType.VarChar, 50).Value = LHR.lib.CoreSecurity.getMd5Hash(password);
                com.Parameters.Add("@groupid", SqlDbType.Int).Value = groupid;
                var i = new LHR.lib.DataAdapter().RunNonQuery(com);
                if (i)
                {
                    sms = "Data has been saved!";
                }
                else
                {
                    sms = "Cannot save data, check your input again!";
                }
            }
            return Content(sms);
        }
        // get total page
        [HttpGet]
        public ActionResult GetTotalPage()
        {

            var cmd = new SqlCommand();
            cmd.CommandText = "getPage";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@tbl", SqlDbType.VarChar, 50).Value = "users";
            DataTable dt = new DataAdapter().ExecPro(cmd);
            var sr = string.Empty;
            sr = JsonConvert.SerializeObject(dt);
            return Content(sr);
        }
        // delete a user by its id
        [HttpPost]
        public ActionResult Delete()
        {
            var userid = Request.Form["userid"].ToString();
            var cmd = new SqlCommand();
            cmd.CommandText = "delete from users where id=" + userid;
            cmd.CommandType = CommandType.Text;
            var i = new LHR.lib.DataAdapter().RunNonQuery(cmd);
            return Content("");
        }
        // get user by its id
        [HttpPost]
        public ActionResult GetUserById()
        {
            var cmd = new SqlCommand();
            cmd.CommandText = "select * from users where id=" + Request.Form["userid"].ToString();
            cmd.CommandType = CommandType.Text;
            DataTable dt = new DataAdapter().RunQuery(cmd);
            var sr = string.Empty;
            sr = JsonConvert.SerializeObject(dt);
            return Content(sr);
        }
        // update user by its id
        [HttpPost]
        public ActionResult Update()
        {
            var sms = "";
            var fname = Request.Form["firstname"].ToString();
            var lname = Request.Form["lastname"].ToString();
            var gender = Request.Form["gender"].ToString();
            var email = Request.Form["email"].ToString();
            var phone = Request.Form["phone"].ToString();
            var groupid = Request.Form["groupid"].ToString();
            var username = Request.Form["username"].ToString();
            var password = Request.Form["password"].ToString();
            var sql = "";
            if (password!="")
            {
                sql = "update users set firstname=@fname, lastname=@lname, gender=@gender, ";
                sql += " email=@email, phone=@phone, groupid=@groupid, username=@username, password=@password where id=@userid";
            }
            else
            {
                sql = "update users set firstname=@fname, lastname=@lname, gender=@gender, ";
                sql += " email=@email, phone=@phone, groupid=@groupid, username=@username where id=@userid";
            }
            var cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@fname", SqlDbType.VarChar, 50).Value = fname;
            cmd.Parameters.Add("@lname", SqlDbType.VarChar, 50).Value = lname;
            cmd.Parameters.Add("@gender", SqlDbType.VarChar, 50).Value = gender;
            cmd.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = email;
            cmd.Parameters.Add("@phone", SqlDbType.VarChar, 50).Value = phone;
            cmd.Parameters.Add("@username", SqlDbType.VarChar, 50).Value = username;
            cmd.Parameters.Add("@groupid", SqlDbType.Int).Value = groupid;
            cmd.Parameters.Add("@userid", SqlDbType.Int).Value = Request.Form["userid"].ToString();
            if (password!="")
            {
                cmd.Parameters.Add("@password", SqlDbType.VarChar, 100).Value = LHR.lib.CoreSecurity.getMd5Hash(password);
            }
            if (new LHR.lib.DataAdapter().RunNonQuery(cmd))
            {
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