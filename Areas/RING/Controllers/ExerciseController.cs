using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Web.Http;
using Newtonsoft.Json;
using System.Data;

namespace RING.Areas.RING.Controllers
{
    public class ExerciseController : Controller
    {
        //GET: RING/Exercise
        public ActionResult Manager()
        {
            using (SqlCommand sc =new SqlCommand("", new SqlConnection(
                      System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Exercise"].ConnectionString)))
            {
                try
                {
                    sc.Connection.Open();
                    sc.CommandText = @"SELECT *
                                       FROM UserRegister;";
                    sc.ExecuteReader();
                    
                }
                finally
                {
                    sc.Connection.Close();
                }
            }

            return View();
        }
    }
}
namespace RING.Areas.RING.Controllers.Api
{
    public class ExerciseController : ApiController
    {
        [System.Web.Http.HttpPost]
        public object Insert([FromBody]Data.ExerciseTable row)
        {
            object ret = new Data.ExerciseTable() { };
            List<Data.ExerciseTable> rows = new List<Data.ExerciseTable>();
            try
            {

                if (!string.IsNullOrWhiteSpace(row.Account)&& !string.IsNullOrWhiteSpace(row.Password1) && !string.IsNullOrWhiteSpace(row.Email))
                {
                    using (SqlCommand sc = new SqlCommand("", new SqlConnection(
                         System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Exercise"].ConnectionString)))
                    {
                        try
                        {
                            sc.Connection.Open();
                            sc.CommandText = "INSERT INTO MochiProduct(Id,Account,Password1,Password2,UserName,Email)VALUES(@Id,@Account,@Password1,@Password2,@UserName,@Email)";
                            sc.Parameters.Add(new SqlParameter("Id", Guid.NewGuid().ToString().ToLower()));
                            sc.Parameters.Add(new SqlParameter("Account", row.Account));
                            sc.Parameters.Add(new SqlParameter("Password1",row.Password1));
                            sc.Parameters.Add(new SqlParameter("Password2", row.Password2));
                            sc.Parameters.Add(new SqlParameter("UserName", row.UserName));
                            sc.Parameters.Add(new SqlParameter("Email", row.Email));

                            sc.ExecuteNonQuery();
                        }
                        finally
                        {
                            sc.Connection.Close();
                        } // ret= sc;
                    }

                }
            }
            catch (Exception ex)
            {
                ret = ex.ToString();
            }
            return ret;
        }

        //public static int Insert(this UPGIEIP.Data.ITable table)
        //{
        //    int retInt = -1;
        //    //UPGIEIP.Data.Log.AddLog("Debug", "UPGIEIP.Database.Insert()", "table.TableName() = " + table.TableName());
        //    if (!string.IsNullOrWhiteSpace(table.TableName()))
        //    {
        //        string names = "", values = "";
        //        List<string> kfs = table.KeyFields();

        //        using (System.Data.SqlClient.SqlCommand sc = Data.Database.NewSqlCommand(" SELECT TOP 0 * FROM " + table.TableName() + " WHERE 1=0 ;"))
        //        {
        //            System.Data.DataTable dt = new System.Data.DataTable();
        //            using (System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter(sc))
        //            {
        //                sda.Fill(dt);
        //            }
        //            //UPGIEIP.Data.Log.AddLog("Debug", "UPGIEIP.Database.Insert()", sc.FillPara());
        //            if (dt.Columns.Count > 0)
        //            {
        //                try
        //                {
        //                    sc.Connection.Open();
        //                    foreach (System.Reflection.PropertyInfo p in table.GetType().GetProperties())
        //                    {
        //                        object v = p.GetValue(table, null);
        //                        if (v != null
        //                            && !string.IsNullOrWhiteSpace(v.ToString())
        //                            && dt.Columns.Contains(p.Name) // 檢查資料表欄位
        //                            )
        //                        {
        //                            if (!string.IsNullOrWhiteSpace(names))
        //                            {
        //                                names += ",";
        //                                values += ",";
        //                            }
        //                            names += p.Name;
        //                            values += "@" + p.Name;
        //                            sc.Parameters.Add(new System.Data.SqlClient.SqlParameter(p.Name, v));
        //                        }
        //                        if (kfs.Contains(p.Name))
        //                            kfs.Remove(p.Name);
        //                    }
        //                    sc.CommandText = "INSERT INTO " + table.TableName() + "(" + names + ") VALUES(" + values + ") ";
        //                    //UPGIEIP.Data.Log.AddLog("Debug", "UPGIEIP.Database.Insert()", sc.FillPara());
        //                    if (kfs.Count == 0)
        //                        retInt = sc.ExecuteNonQuery();
        //                }
        //                catch (Exception ex)
        //                {
        //                    ex.AddLog(sc.FillPara());
        //                    throw;
        //                }
        //                finally
        //                {
        //                    sc.Connection.Close();
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        UPGIEIP.Data.Log.LogTypes.Error.AddLog("查無模組對應的 xxxFile 資料表");
        //    }

        //    return retInt;
        //}

        //[System.Web.Http.HttpPost]
        //    public object Update([FromBody]Data.ExerciseTable row)
        //    {
        //    var ret = new Data.ExerciseTable() { };
        //    List<Data.ExerciseTable> rows = new List<Data.ExerciseTable>();
        //    //ret.Alert = row.ToJson();
        //    if (!string.IsNullOrWhiteSpace(row.ID))
        //    {

        //        row.UpdateFromPost();
        //    }
        //    else
        //        ret.Alert = "請輸入 ID";
        //    return ret;
        //}




    }
}
