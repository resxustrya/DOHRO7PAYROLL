using System;
using System.Web.Mvc;
using DOH7PAYROLL.Repo;
using DOH7PAYROLL.Models;
using ClosedXML;
using ClosedXML.Excel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace DOH7PAYROLL.Controllers
{
    public class PayrollController : Controller
    {
        DatabaseConnect connection = new DatabaseConnect();
        // GET: Payroll
        public ActionResult Job_Order(string id,string search)
        {
            ViewBag.List = connection.Select(id,search);
            ViewBag.Prev = DatabaseConnect.start;
            ViewBag.Next= DatabaseConnect.end;
            ViewBag.Max = int.Parse(DatabaseConnect.max_size);
            ViewBag.Search = search;
            return View();
        }

        
        [HttpPost]
        public ActionResult Insert(String id, String working_days,String salary,String minutes_late, String coop,String phic,String disallowance,String gsis,String pagibig, String excess,String type_request)
        {
            String message = "";
            Payroll payroll = new Payroll(id, working_days, salary, minutes_late, coop,phic,disallowance,gsis,pagibig,excess,"");
            if(type_request.Equals("0"))
                message = connection.Insert(payroll);
            else
                message = connection.Update(payroll);

            TempData["message"] = message;

            return RedirectToAction("Job_Order", new { id = 3,search = ""});
        }

        public ActionResult ExportData()
        {
            String constring = ConfigurationManager.ConnectionStrings["RConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(constring);
            string query = "SELECT * From payroll";
            DataTable dt = new DataTable();
            dt.TableName = "Employee";
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            da.Fill(dt);
            con.Close();

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= EmployeeReport.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            return RedirectToAction("Index", "ExportData");
        }


        public ActionResult Regular()
        {
            return View();
        }
    }
}