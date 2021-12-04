using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Globalization;
using System.Text.RegularExpressions;
using static RING.Areas.RING.Data.RegisterTable;


namespace RING.Areas.RING.Controllers
{
    public class RegisterController : Controller
    {
        // GET: RING/Register
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult RegisterIN(Register row)
        {
            RegisterView Model = new RegisterView();

            if (string.IsNullOrEmpty(row.Password) || string.IsNullOrEmpty(row.Email))
            {
                Model.Error = "請輸入Email以及密碼";
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
                    sc.CommandText = "select * from UserRegister where Email = @Email";
                    sc.Connection = r;
                    sc.Parameters.AddWithValue("@Email", row.Email);

                    DbDataAdapter rs = new SqlDataAdapter();
                    rs.SelectCommand = sc;
                    DataSet ds = new DataSet();
                    rs.Fill(ds);
                    
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Model.Error = "此信箱已註冊";
                    }
                    else
                    {
                        sc = new SqlCommand();
                        sc.Connection = r;
                        sc.CommandText = @"INSERT INTO UserRegister(Id,Password,Email)VALUES(@Id,@Password,@Email)";
                        sc.Parameters.Add(new SqlParameter("Id", Guid.NewGuid().ToString().ToLower()));
                        sc.Parameters.Add(new SqlParameter("Password", row.Password));
                        sc.Parameters.Add(new SqlParameter("Email", row.Email));

                        sc.ExecuteNonQuery();

                        Model.ResultOk = "註冊完成";
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