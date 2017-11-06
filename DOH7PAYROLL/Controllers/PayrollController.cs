using System;
using System.Text;
using System.Web.Mvc;
using DOH7PAYROLL.Repo;
using DOH7PAYROLL.Models;
using System.IO;
using ClosedXML.Excel;
using System.Data;
using System.Collections.Generic;
using iTextSharp.text.pdf;
using iTextSharp.text;



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



        [HttpPost]
        public int GetMins(String id,String from, String to) {
            return connection.GetMins("0001", from, to);
        }

        public String ifWeekend(String date) {
            return connection.ifWeekend(date) ?"WEEKEND":"NOT WEEKEND";
        }

        [HttpGet]
        public FileResult Export()
        {
            DateTime localDate = DateTime.Now;
            int year = localDate.Year;
            int month = localDate.Month;
            int day = localDate.Day;
            int hour = localDate.Hour;
            int mins = localDate.Minute;
            int sec = localDate.Second;
            string filename = "JO_Payroll_" + year + "_" + month + "_" + day + "_" + hour + "_" + mins + "_" + sec+ ".xlsx";
            Employee entities = new Employee();
            DataTable dt = new DataTable("Payroll");
            dt.Columns.AddRange(new DataColumn[17] {
                                            new DataColumn("ID"),
                                            new DataColumn("Firstname"),
                                            new DataColumn("Lastname"),
                                            new DataColumn("Position"),
                                            new DataColumn("Salary"),
                                            new DataColumn("Half_Salary"),
                                            new DataColumn("Absences"),
                                            new DataColumn("Net Amount"),
                                            new DataColumn("Tax 10%"),
                                            new DataColumn("Tax 3%"),
                                            new DataColumn("Coop"),
                                            new DataColumn("Disallowance"),
                                            new DataColumn("Pagibig"),
                                            new DataColumn("PHIC"),
                                            new DataColumn("GSIS"),
                                            new DataColumn("Excess Mobile"),
                                            new DataColumn("Total Amount")});

            List<Employee> list =  connection.GeneratePayroll();

            foreach (var emp in list)
            {
                string ID = emp.PersonnelID;
                string fname = emp.Firstname;
                string lname = emp.Lastname;
                string position = emp.JobType;
                decimal salary = decimal.Parse(emp.Payroll.Salary);
                decimal half_salary = salary / 2;
                int minutes_late = int.Parse(emp.Payroll.MinutesLate);
                int working_days = int.Parse(emp.Payroll.WorkDays);
                decimal per_day = 0;
                decimal absences = 0;
                if (working_days != 0 && salary != 0){
                    per_day = salary / working_days;
                    absences = (minutes_late * (((per_day) / 8) / 60));
                }
               
                decimal net_amount = 0;
                if (working_days != 0){
                    net_amount = (half_salary - absences);
                }
                decimal tax10 = (decimal)0.10;
                decimal tax3 = (decimal)0.03;
                decimal tax_10 = (net_amount * tax10);
                decimal tax_3 = (net_amount * tax3);

                decimal coop = decimal.Parse(emp.Payroll.Coop);
                decimal disallowance = decimal.Parse(emp.Payroll.Disallowance);
                decimal pagibig = decimal.Parse(emp.Payroll.Pagibig);
                decimal phic = decimal.Parse(emp.Payroll.Phic);
                decimal gsis = decimal.Parse(emp.Payroll.Gsis);
                decimal excess = decimal.Parse(emp.Payroll.ExcessMobile);
                decimal total_amount = (net_amount - tax_10 - tax_3 - coop - disallowance - pagibig - phic - gsis - excess);


                dt.Rows.Add(ID,fname,lname,position,salary.ToString("#,##0.00"),half_salary.ToString("#,##0.00"),
                    absences.ToString("#,##0.00"), net_amount.ToString("#,##0.00"), tax_10.ToString("#,##0.00"), tax_3.ToString("#,##0.00"), 
                    coop.ToString("#,##0.00"), disallowance.ToString("#,##0.00"), pagibig.ToString("#,##0.00"),
                    phic.ToString("#,##0.00"), gsis.ToString("#,##0.00"), excess.ToString("#,##0.00"), total_amount.ToString("#,##0.00"));
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
               wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
                }
            }
        }

        public FileResult CreatePdf()
        {
            MemoryStream workStream = new MemoryStream();
            StringBuilder status = new StringBuilder("");
            DateTime dTime = DateTime.Now;
            //file name to be created   
            string strPDFFileName = string.Format("Payroll_" + dTime.ToString("yyyyMMdd") + ".pdf");

            Document doc = new Document();
            doc.SetMargins(20f, 20f, 20f, 20f);
            doc.SetPageSize(PageSize.A4.Rotate());
          
            //Create PDF Table with 5 columns  
            PdfPTable tableLayout = new PdfPTable(16);
            //Create PDF Table  

            //file will created in this path  
            string strAttachment = Server.MapPath("~/Downloadss/" + strPDFFileName);


            PdfWriter.GetInstance(doc, workStream).CloseStream = false;
            doc.Open();

            //Add Content to PDF   
            doc.Add(Add_Content_To_PDF(tableLayout));

            // Closing the document  
            doc.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;


            return File(workStream, "application/pdf", strPDFFileName);

        }

        protected PdfPTable Add_Content_To_PDF(PdfPTable tableLayout)
        {            
            float[] headers = { 5,12,5,5,5,5,5,5,5,5,7,5,5,5,5,5};
            
            tableLayout.SetWidths(headers); 
            tableLayout.WidthPercentage = 100;
            tableLayout.SplitLate = false;
            

            //Add Title to the PDF file at the top  

            List<Employee> employees = connection.GeneratePayroll();

           
            
            tableLayout.AddCell(new PdfPCell(new Phrase("Employee(Job Order) Payroll", new Font(Font.FontFamily.HELVETICA, 14, 1, new BaseColor(0, 0, 0)))) {
                Colspan = 16, Border = 0, Padding = 15, HorizontalAlignment = Element.ALIGN_CENTER,VerticalAlignment = Element.ALIGN_CENTER
            });


            ////Add header  
            AddCellToHeader(tableLayout, "ID");
            AddCellToHeader(tableLayout, "Name");
            AddCellToHeader(tableLayout, "Job Type");
            AddCellToHeader(tableLayout, "Salary");
            AddCellToHeader(tableLayout, "Half Salary");
            AddCellToHeader(tableLayout, "Deduction");
            AddCellToHeader(tableLayout, "Net Amount");
            AddCellToHeader(tableLayout, "Tax 10%");
            AddCellToHeader(tableLayout, "Tax 3%");
            AddCellToHeader(tableLayout, "Coop");
            AddCellToHeader(tableLayout, "Disallowance");
            AddCellToHeader(tableLayout, "Pagibig");
            AddCellToHeader(tableLayout, "PHIC");
            AddCellToHeader(tableLayout, "GSIS");
            AddCellToHeader(tableLayout, "Excess Mobile");
            AddCellToHeader(tableLayout, "Total Amount");
            ////Add body  

            foreach (var emp in employees)
            {
                string ID = emp.PersonnelID;
                string fname = emp.Firstname;
                string lname = emp.Lastname;
                string position = emp.JobType;
                if (position == "JO")
                    position = "Job Order";
                decimal salary = decimal.Parse(emp.Payroll.Salary);
                decimal half_salary = salary / 2;
                int minutes_late = int.Parse(emp.Payroll.MinutesLate);
                int working_days = int.Parse(emp.Payroll.WorkDays);
                decimal per_day = 0;
                decimal absences = 0;
                if (working_days != 0 && salary != 0)
                {
                    per_day = salary / working_days;
                    absences = (minutes_late * (((per_day) / 8) / 60));
                }

                decimal net_amount = 0;
                if (working_days != 0)
                {
                    net_amount = (half_salary - absences);
                }
                decimal tax10 = (decimal)0.10;
                decimal tax3 = (decimal)0.03;
                decimal tax_10 = (net_amount * tax10);
                decimal tax_3 = (net_amount * tax3);

                decimal coop = decimal.Parse(emp.Payroll.Coop);
                decimal disallowance = decimal.Parse(emp.Payroll.Disallowance);
                decimal pagibig = decimal.Parse(emp.Payroll.Pagibig);
                decimal phic = decimal.Parse(emp.Payroll.Phic);
                decimal gsis = decimal.Parse(emp.Payroll.Gsis);
                decimal excess = decimal.Parse(emp.Payroll.ExcessMobile);
                decimal total_amount = (net_amount - tax_10 - tax_3 - coop - disallowance - pagibig - phic - gsis - excess);

                AddCellToBody(tableLayout, ID,"left");
                AddCellToBody(tableLayout, fname+" "+lname, "left");
                AddCellToBody(tableLayout, position, "left");
                AddCellToBody(tableLayout, salary.ToString("#,##0.00"), "right");
                AddCellToBody(tableLayout, half_salary.ToString("#,##0.00"), "right");
                AddCellToBody(tableLayout, absences.ToString("#,##0.00"), "right");
                AddCellToBody(tableLayout, net_amount.ToString("#,##0.00"), "right");
                AddCellToBody(tableLayout, tax_10.ToString("#,##0.00"), "right");
                AddCellToBody(tableLayout, tax_3.ToString("#,##0.00"), "right");
                AddCellToBody(tableLayout, coop.ToString("#,##0.00"), "right");
                AddCellToBody(tableLayout, disallowance.ToString("#,##0.00"), "right");
                AddCellToBody(tableLayout, pagibig.ToString("#,##0.00"), "right");
                AddCellToBody(tableLayout, phic.ToString("#,##0.00"), "right");
                AddCellToBody(tableLayout, gsis.ToString("#,##0.00"), "right");
                AddCellToBody(tableLayout, excess.ToString("#,##0.00"), "right");
                AddCellToBody(tableLayout, total_amount.ToString("#,##0.00"), "right");
            }


            return tableLayout;
        }
        // Method to add single cell to the Header  
        private static void AddCellToHeader(PdfPTable tableLayout, string cellText)
        {

            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 7, 3, BaseColor.BLACK)))
            {
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER, Padding = 3
                
    });
        }

     
        // Method to add single cell to the body  
        private static void AddCellToBody(PdfPTable tableLayout, string cellText,string position)
        {

            if (position.Equals("left"))
            {
                tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 7, 1, BaseColor.BLACK)))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Padding = 3
                });
            }
            else {
                tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 7, 1, BaseColor.BLACK)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    Padding = 3
                });
            }
        }

        public ActionResult Regular()
        {
            return View();
        }

    }
}