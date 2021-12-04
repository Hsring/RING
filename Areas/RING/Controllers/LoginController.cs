using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Globalization;
using System.Text.RegularExpressions;
using static RING.Areas.RING.Data.LoginTable;
namespace RING.Areas.RING.Controllers
{
    public class LoginController : Controller
    {
        // GET: RING/Login
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LoginIN(Login row)
        {
            LoginView Model = new LoginView();

            // 檢查輸入資料
            if (string.IsNullOrEmpty(row.Password) || string.IsNullOrEmpty(row.Email))
            {
                Model.Error = "Email或密碼輸入錯誤";
            }
            else
            {
                SqlConnection r = null;

                try
                {
                    string s = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Exercise"].ConnectionString;
                    r = new SqlConnection();
                    r.ConnectionString = s;
                    r.Open();

                    SqlCommand sc = new SqlCommand();
                    sc.CommandText = "select * from UserRegister where Email = @Email and Password = @Password"; 
                    sc.Connection = r;
                    sc.Parameters.AddWithValue("@Email", row.Email);
                    sc.Parameters.AddWithValue("@Password", row.Password);

                    SqlDataAdapter rs = new SqlDataAdapter();
                    rs.SelectCommand = sc;
                    DataSet ds = new DataSet();
                    rs.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Session["Email"] = row.Email;

                        Model.ResultOk = "登入成功";
                    }
                    else
                    {
                        Model.Error = "帳號或密碼錯誤";
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (r != null)
                    {
                        r.Close();
                    }
                }

            }
            ContentResult resultJson = new ContentResult();
            resultJson.ContentType = "application/json";
            resultJson.Content = JsonConvert.SerializeObject(Model); ;
            return resultJson;
        }


           
    }
}