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
            ViewBag.List = connection.GetEmployee(id,search);
            ViewBag.Prev = DatabaseConnect.start;
            ViewBag.Next= DatabaseConnect.end;
            ViewBag.Max = int.Parse(DatabaseConnect.max_size);
            ViewBag.Search = search;
            return View();
        }

        public ActionResult ViewPdf(String pdf) {
            string strAttachment = Server.MapPath(Url.Content("~/public/Pdf/" + pdf));
            return File(strAttachment, "application/pdf");
        }

        public ActionResult Payroll(String id,String search)
        {
            ViewBag.List = connection.FetchPdf(id,search);
            ViewBag.Prev = DatabaseConnect.start;
            ViewBag.Next = DatabaseConnect.end;
            ViewBag.Max = int.Parse(DatabaseConnect.max_size);
            ViewBag.Search = search;
            return View();
        }

        [HttpPost]
        public ActionResult Insert(String id, String filter_dates, String working_days,String salary,String minutes_late, String coop,String phic,String disallowance,String gsis,String pagibig, String excess,String type_request)
        {
            String message = "";
            Payroll payroll = new Payroll(id, filter_dates, working_days, salary.Replace(",",""), minutes_late, coop.Replace(",", ""), phic.Replace(",", ""), 
                disallowance.Replace(",", ""), gsis.Replace(",", ""), pagibig.Replace(",", ""), excess.Replace(",", ""), "");
            if(type_request.Equals("0"))
                message = connection.Insert(payroll);
            else
                message = connection.Update(payroll);

            TempData["message"] = message;

            return RedirectToAction("Job_Order", new { id = 3,search = ""});
        }



        [HttpPost]
        public String GetMins(String id,String from, String to,String am_in,String am_out,String pm_in,String pm_out) {
            return connection.GetMins(id, from, to,am_in,am_out,pm_in,pm_out);
            
;        }

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

        public class MyPageEvent : PdfPageEventHelper {
            public void OnStartPage(PdfWriter writer, Document document)
            {
                float fontSize = 80;
                float xPosition = 300;
                float yPosition = 400;
                float angle = 45;
                try
                {
                    PdfContentByte under = writer.DirectContentUnder;
                    BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.EMBEDDED);
                    under.BeginText();
                    under.SetColorFill(BaseColor.LIGHT_GRAY);
                    under.SetFontAndSize(baseFont, fontSize);
                    under.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "WATERMARK", xPosition, yPosition, angle);
                    under.EndText();
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.Message);
                }
            }
        }
        

        public ActionResult CreatePdf(String filter_dates)
        {
            String message = "";
            int month = int.Parse(filter_dates.Split('/')[0]);
            int day_from = int.Parse(filter_dates.Split('/')[1]);
            int day_to= int.Parse(filter_dates.Split('/')[3]);
            int year = int.Parse(filter_dates.Split('/')[4]);
            filter_dates = DatabaseConnect.getMonthName(month) + " " + day_from + "-" + day_to+","+year;
            
            MemoryStream workStream = new MemoryStream();
            StringBuilder status = new StringBuilder("");
            string strPDFFileName = String.Format("Payroll_" + DatabaseConnect.getMonthName(month) + "_" + day_from + "_" + day_to + "_" + year + ".pdf");

            Document doc = new Document();
            doc.SetMargins(20f, 20f, 20f, 20f);
            doc.SetPageSize(PageSize.LEGAL.Rotate());

            PdfPTable outer = new PdfPTable(1);
            outer.TotalWidth = 400;
            outer.WidthPercentage = 100;
            outer.SplitRows = true;
            outer.SplitLate = true;

            PdfPTable body = new PdfPTable(18);
            float[] headers = { 5, 5, 5, 6, 7, 7, 7, 7, 5, 5, 5, 5, 5, 5, 5, 5, 7, 6 };
            body.SetWidths(headers);
            body.TotalWidth = 400;
            body.WidthPercentage = 100;
            body.SplitRows = true;
            body.SplitLate = true;
            body.HeaderRows = 6;

            PdfPTable footer = new PdfPTable(18);
            footer.SetWidths(headers);
            footer.TotalWidth = 400;
            footer.WidthPercentage = 100;

            String strAttachment = Server.MapPath(Url.Content("~/public/Pdf/"+strPDFFileName));
            String imageURL = Server.MapPath(Url.Content("~/public/img/logo.png"));
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(strAttachment,FileMode.Create));
            doc.Open();
            body = Add_Content_To_PDF(writer, doc, body, filter_dates, imageURL);
            footer = addFooter(footer);
            outer.AddCell(new PdfPCell(body)
            {
                Border=0,
            });
            outer.AddCell(new PdfPCell(footer)
            {
                Border = 0,
            });
            doc.Add(outer);
            doc.Close();

            message = connection.InsertPDF(strPDFFileName);
            TempData["pdf"] = message;
            ViewBag.List = connection.FetchPdf("3", "");
           // return File(strAttachment, "application/pdf");

            return RedirectToAction("Payroll", new { id = 3, search = "" });
        }

     
        protected PdfPTable Add_Content_To_PDF(PdfWriter writer,Document document,PdfPTable tableLayout,String filter_range1,String imageURL)
        {
            List<Employee> employees = connection.GeneratePayroll();
            addHeader(tableLayout, filter_range1, imageURL);
            String description = "";
            decimal overall_net = 0;
            decimal grand_overall_net = 0;
            decimal grand_mo_rate = 0;
            decimal grand_half_rate = 0;
            decimal grand_absences = 0;
            decimal grand_tax_10 = 0;
            decimal grand_tax_3 = 0;
            decimal grand_coop = 0;
            decimal grand_disallow = 0;
            decimal grand_pagibig = 0;
            decimal grand_phic = 0;
            decimal grand_gsis = 0;
            decimal grand_excess = 0;
            decimal grand_total_amount = 0;
            int count = 0;
            foreach (var emp in employees)
            {
                count++;
                string TIN = emp.Tin;
                string section = emp.Section;
                string ID = emp.PersonnelID;
                string fname = emp.Firstname;
                string lname = emp.Lastname;
                string position = emp.JobType;
                decimal salary = decimal.Parse(emp.Payroll.Salary);
                grand_mo_rate += salary;
                decimal half_salary = salary / 2;
                grand_half_rate += half_salary;
                int minutes_late = int.Parse(emp.Payroll.MinutesLate);
                int working_days = int.Parse(emp.Payroll.WorkDays);
                decimal per_day = 0;
                decimal absences = 0;
                if (working_days != 0 && salary != 0)
                {
                    per_day = salary / working_days;
                    absences = (minutes_late * (((per_day) / 8) / 60));
                }
                grand_absences += absences;

                decimal net_amount = 0;
                if (working_days != 0)
                {
                    net_amount = (half_salary - absences);
                }
                grand_overall_net += net_amount;
                decimal tax10 = (decimal)0.10;
                decimal tax3 = (decimal)0.03;
                decimal tax_10 = (net_amount * tax10);
                grand_tax_10 += tax_10;
                decimal tax_3 = (net_amount * tax3);
                grand_tax_3 += tax_3;
                decimal coop = decimal.Parse(emp.Payroll.Coop);
                grand_coop += coop;
                decimal disallowance = decimal.Parse(emp.Payroll.Disallowance);
                grand_disallow += disallowance;
                decimal pagibig = decimal.Parse(emp.Payroll.Pagibig);
                grand_pagibig += pagibig;
                decimal phic = decimal.Parse(emp.Payroll.Phic);
                grand_phic += phic;
                decimal gsis = decimal.Parse(emp.Payroll.Gsis);
                grand_gsis += gsis;
                decimal excess = decimal.Parse(emp.Payroll.ExcessMobile);
                grand_excess += excess;
                decimal total_amount = net_amount - tax_10 - tax_3 - coop - disallowance - pagibig - phic - gsis - excess;
                grand_total_amount += total_amount;
                if (description.Equals("")) { 
                    addSection(tableLayout, section);
                    overall_net += net_amount;
                }
                else{
                    if (!section.Equals(description))
                    {
                        addOverall(tableLayout, overall_net.ToString("#,##0.00"));
                        addSection(tableLayout, section);
                        overall_net = net_amount;
                    }
                    else {
                        overall_net += net_amount;
                    }
                }
                description = section;
                AddCellToBody(tableLayout, TIN, "left");
                AddCellToBody(tableLayout, fname, "left");
                AddCellToBody(tableLayout, lname, "left");
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
                AddCellToBody(tableLayout, minutes_late+" min(s)", "right");
            }
            addOverall(tableLayout, overall_net.ToString("#,##0.00"));
            addGrandOverall(tableLayout, grand_mo_rate.ToString("#,##0.00"), grand_half_rate.ToString("#,##0.00"),
                grand_absences.ToString("#,##0.00"), grand_overall_net.ToString("#,##0.00"), grand_tax_10.ToString("#,##0.00"),
                grand_tax_3.ToString("#,##0.00"), grand_coop.ToString("#,##0.00"), grand_disallow.ToString("#,##0.00"),
                grand_pagibig.ToString("#,##0.00"), grand_phic.ToString("#,##0.00"), grand_gsis.ToString("#,##0.00"),grand_excess.ToString("#,##0.00"), grand_total_amount.ToString("#,##0.00"));
            return tableLayout;
        }
        protected static void addHeader(PdfPTable tableLayout, String filter_range1, String imageURL)
        {

            Image jpg = Image.GetInstance(imageURL);
            //Resize image depend upon your need
            jpg.ScaleToFit(50f, 50f);
            //Give space before image
            //Give some space after the image
            jpg.Alignment = Element.ALIGN_LEFT;


            tableLayout.AddCell(new PdfPCell(jpg)
            {
                Colspan = 1,
                Rowspan = 3,
                Border = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            tableLayout.AddCell(new PdfPCell(new Phrase("PAYROLL FOR JOB PERSONNEL", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                Colspan = 17,
                Border = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("DOH-RO7", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                Colspan = 17,
                Border = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(filter_range1, new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                Colspan = 17,
                Border = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("We acknowledge receipt of the sum shown opposite our names as full renumeration for services rendered for the period started:", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                Colspan = 14,
                Border = 0,
                Padding = 3,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("*NO WORK NO PAY POLICY", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                Colspan = 4,
                Border = 0,
                Padding = 3,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            AddCellToHeader(tableLayout, "TIN");
            AddCellToHeader(tableLayout, "Name");
            AddCellToHeader(tableLayout, "Position");
            AddCellToHeader(tableLayout, "MO. RATE");
            AddCellToHeader(tableLayout, "HALF MO.");
            AddCellToHeader(tableLayout, "Tardiness");
            AddCellToHeader(tableLayout, "Net Amount");
            AddCellToHeader(tableLayout, "Deductions");
            AddCellToHeader(tableLayout, "Total Amt.");
            AddCellToHeader(tableLayout, "Remarks");
            AddCellToHeader(tableLayout, "");
            AddCellToHeader(tableLayout, "Firstname");
            AddCellToHeader(tableLayout, "");
            AddCellToHeader(tableLayout, "");
            AddCellToHeader(tableLayout, "");
            AddCellToHeader(tableLayout, "Absences");
            AddCellToHeader(tableLayout, "W/Tax 10%");
            AddCellToHeader(tableLayout, "W/Tax 3%");
            AddCellToHeader(tableLayout, "Coop");
            AddCellToHeader(tableLayout, "Disallow.");
            AddCellToHeader(tableLayout, "Pag-Ibig");
            AddCellToHeader(tableLayout, "PHIC");
            AddCellToHeader(tableLayout, "GSIS");
            AddCellToHeader(tableLayout, "Excess Mobile");
            AddCellToHeader(tableLayout, "");
            AddCellToHeader(tableLayout, "");
        }

        private static void addSection(PdfPTable tableLayout,String section) {
            AddCellToBody(tableLayout, "", "left");
            tableLayout.AddCell(new PdfPCell(new Phrase(section, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
            {
                BorderWidth = 0.2f,
                Colspan = 2,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
        }

        private static void addOverall(PdfPTable tableLayout, String overall)
        {
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            tableLayout.AddCell(new PdfPCell(new Phrase(overall, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD,BaseColor.RED))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
        }

        private static void addGrandOverall(PdfPTable tableLayout, String month,String half,String absent,String net
            ,String tax_10,String tax_3,String coop,String disallow,String pagibig,String phic,String gsis,String excess,
            String total)
        {
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            tableLayout.AddCell(new PdfPCell(new Phrase("Grand Total", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                Colspan = 2,
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(month, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(half, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(absent, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(net, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.RED))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(tax_10, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(tax_3, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(coop, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(disallow, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(pagibig, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(phic, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(gsis, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(excess, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(total, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            AddCellToBody(tableLayout, "", "right");
        }


        private static PdfPTable addFooter(PdfPTable tableLayout) {
            tableLayout.HeaderRows = 0;
            tableLayout.AddCell(new PdfPCell(new Phrase("1) Certified: Supporting documents complete and proper, Cash available ", new Font(FontFactory.GetFont("Times New Roman", 8, Font.NORMAL))))
            {

                Border = 0,
                Colspan = 6,
                VerticalAlignment = Element.ALIGN_LEFT,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Padding = 3
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("2) Approved Payment:", new Font(FontFactory.GetFont("Times New Roman", 8, Font.NORMAL))))
            {
                Border = 0,
                Colspan = 6,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 3
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("3) Each employee whose name appears above has been", new Font(FontFactory.GetFont("Times New Roman", 8, Font.NORMAL))))
            {
                Border = 0,
                Colspan = 6,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 3
            });

            tableLayout.AddCell(new PdfPCell(new Phrase("   Subject to ADA (where applicable)", new Font(FontFactory.GetFont("Times New Roman", 8, Font.NORMAL))))
            {
                Border = 0,
                Colspan = 6,
                VerticalAlignment = Element.ALIGN_LEFT,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Padding = 3
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("    ", new Font(Font.FontFamily.HELVETICA, 8, 0, BaseColor.BLACK)))
            {
                Border = 0,
                Colspan = 6,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 3
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("   paid the amount indicated opposite his/her name", new Font(FontFactory.GetFont("Times New Roman", 8, Font.NORMAL))))
            {
                Border = 0,
                Colspan = 6,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 3
            });

            //NAMES
            tableLayout.AddCell(new PdfPCell(new Phrase("Angieline T. Adlaon, CPA, MBA", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
            {

                Border = 0,
                Colspan = 6,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 3,
                PaddingTop = 8
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("Sophia M. Mancao, MD, DPSP", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
            {
                Border = 0,
                Colspan = 6,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 3,
                PaddingTop = 8
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("Josephine D. Vergara", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
            {
                Border = 0,
                Colspan = 6,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 3,
                PaddingTop = 8
            });

            tableLayout.AddCell(new PdfPCell(new Phrase("Accountant III", new Font(FontFactory.GetFont("Times New Roman", 8, Font.NORMAL))))
            {
                Border = 0,
                Colspan = 6,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 3
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("OIC- Director III", new Font(FontFactory.GetFont("Times New Roman", 8, Font.NORMAL))))
            {
                Border = 0,
                Colspan = 6,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 3
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("Administrative Officer V", new Font(FontFactory.GetFont("Times New Roman", 8, Font.NORMAL))))
            {
                Border = 0,
                Colspan = 6,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 3
            });
            return tableLayout;
        }

        // Method to add single cell to the Header  
        private static void AddCellToHeader(PdfPTable tableLayout, string cellText)
        {
            switch (cellText) {
                case "Net Amount":
                    tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
                    {
                        BorderWidth = 0.2f,
                        Rowspan =2,
                        VerticalAlignment = Element.ALIGN_CENTER,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    });
                    break;
                case "Name":
                    tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
                    {
                        BorderWidth = 0.2f,
                        Colspan = 2,
                        VerticalAlignment = Element.ALIGN_CENTER,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    });
                    break;
                case "Firstname":
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
                    {
                        BorderWidth = 0.2f,
                        Colspan = 2,
                        VerticalAlignment = Element.ALIGN_CENTER,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    });
                    break;
                case "Tardiness":
                case "Absences":
                case "W/Tax 10%":
                case "W/Tax 3%":
                case "MO. RATE":
                case "HALF MO.":
                case "Coop":
                case "Disallow.":
                case "Pag-Ibig":
                case "PHIC":
                case "Position":
                case "GSIS":
                case "Excess Mobile":
                case "TIN":
                case "Total Amt.":
                case "Remarks":
                case "":
                    tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
                    {
                        BorderWidth = 0.2f,
                        VerticalAlignment = Element.ALIGN_CENTER,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    });
                    break;
                case "Deductions":
                    tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
                    {
                        BorderWidth = 0.2f,
                        Colspan = 8,
                        VerticalAlignment = Element.ALIGN_CENTER,
                        HorizontalAlignment = Element.ALIGN_CENTER,

                    });
                    break;
            }
        }
        // Method to add single cell to the body  
        private static void AddCellToBody(PdfPTable tableLayout, string cellText,string position)
        {

            if (position.Equals("left"))
            {
                tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(FontFactory.GetFont("Times New Roman", 8, Font.NORMAL))))
                {
                    BorderWidth = 0.2f,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                });
            }
            else {
                tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(FontFactory.GetFont("Times New Roman", 8, Font.NORMAL))))
                {
                    BorderWidth = 0.2f,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                });
            }
        }

        public ActionResult Regular()
        {
            return View();
        }

    }
}