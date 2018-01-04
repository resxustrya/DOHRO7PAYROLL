using System;
using System.Text;
using System.Web.Mvc;
using DOH7PAYROLL.Repo;
using DOH7PAYROLL.Models;
using System.Web.Routing;
using System.IO;
using System.Collections.Generic;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace DOH7PAYROLL.Controllers
{
    public class TestingController : Controller
    {

        public ActionResult CreateRegularPayslip()
        {
            String title = "Sample_Title";
            MemoryStream workStream = new MemoryStream();
            StringBuilder status = new StringBuilder("");
            string strPDFFileName = String.Format(title + ".pdf");

            Document doc = new Document();
            doc.SetMargins(0f, 0f, 35f, 0f);
            doc.SetPageSize(PageSize.A4);

            PdfPTable outer = new PdfPTable(20);
            outer.TotalWidth = 400;
            String strAttachment = Server.MapPath(Url.Content("~/public/Pdf/" + strPDFFileName));
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(strAttachment, FileMode.Create));

            outer.AddCell(new PdfPCell(new Phrase("Republic of Philippines", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 20,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            outer.AddCell(new PdfPCell(new Phrase("DEPARTMENT OF HEALTH", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 20,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            outer.AddCell(new PdfPCell(new Phrase("Regional Office VII", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 20,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            outer.AddCell(new PdfPCell(new Phrase("Osmeña Blvd., Cebu City", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 20,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            outer.AddCell(new PdfPCell(new Phrase("Tel. No.: (032) 418-7653 Fax: (032) 254-0109", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 20,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            outer.AddCell(new PdfPCell(new Phrase("Salary Slip For The Month Of May 2017", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 20,
                PaddingBottom=20,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            //
            outer.AddCell(new PdfPCell(new Phrase("ID No.", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255,255,255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("12122388", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 15,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("Employee name", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
               
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("PANGCATAN", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 15,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("Designation", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("MSD", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(255, 255, 255),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(124, 124, 124),
                Colspan = 15,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            //
            outer.AddCell(new PdfPCell(new Phrase("SPACE", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 20,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            //FIRST HALF SALARY - TITLE
            outer.AddCell(new PdfPCell(new Phrase("FIRST HALF SALARY", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border=0,
                Colspan = 9,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            //SECOND HALF SALARY - TITLE
            outer.AddCell(new PdfPCell(new Phrase("SECOND HALF SALARY", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = 0,
                Colspan = 9,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            // FIRST HALF SALARY - BASIC SALARY
            outer.AddCell(new PdfPCell(new Phrase("BASIC SALARY", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("32,747.00", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            // SECOND HALF SALARY - BASIC SALARY
            outer.AddCell(new PdfPCell(new Phrase("SALARY & PERA/ACA", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("7,635.32", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            //******//

            // FIRST HALF SALARY - PERA/ACA
            outer.AddCell(new PdfPCell(new Phrase("PERA / ACA", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("2,000.00", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            // SECOND HALF SALARY - SPACE
            outer.AddCell(new PdfPCell(new Phrase("SPACE", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("SPACE", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            //*****//
            // FIRST HALF SALARY - TOTAL SALARY
            outer.AddCell(new PdfPCell(new Phrase("TOTAL SALARY", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.BOTTOM_BORDER|PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                BackgroundColor = new BaseColor(224,224,224),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("0.00", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                BackgroundColor = new BaseColor(224, 224, 224),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            // SECOND HALF SALARY - SPACE
            outer.AddCell(new PdfPCell(new Phrase("HAZARD PAY", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("8,186.75", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            //*****//
            //FIRST HALF SALARY - TITLE
            outer.AddCell(new PdfPCell(new Phrase("DEDUCTIONS", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = 0,
                Colspan = 9,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            //SECOND HALF SALARY - TITLE
            outer.AddCell(new PdfPCell(new Phrase("MORTUARY", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(100.00)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            // FIRST HALF SALARY - BASIC SALARY
            outer.AddCell(new PdfPCell(new Phrase("DEDUCTION (Late)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("32,747.00", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            // SECOND HALF SALARY - BASIC SALARY
            outer.AddCell(new PdfPCell(new Phrase("HWMPC", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(200.00)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });

            //******//

            // FIRST HALF SALARY - PERA/ACA
            outer.AddCell(new PdfPCell(new Phrase("TAX", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(6.055.86)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            // SECOND HALF SALARY - SPACE
            outer.AddCell(new PdfPCell(new Phrase("OTHER ACCOUNTS PAYABLE", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(296.40)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            //*****//
            // DEDUCTIONS
            outer.AddCell(new PdfPCell(new Phrase("MEDICARE", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(375.00)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            // SECOND HALF SALARY - SPACE
            outer.AddCell(new PdfPCell(new Phrase("NET HAZARD PAY", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                BorderColor = new BaseColor(255,255,255),
                Border = PdfPCell.RIGHT_BORDER,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("7,111.35", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            //***//
            outer.AddCell(new PdfPCell(new Phrase("GSIS Premium", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(2,774.79)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            // SECOND HALF SALARY - BASIC SALARY
            outer.AddCell(new PdfPCell(new Phrase("CELLPHONE COMMUNICATION", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(200.00)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });

            //******//

            // FIRST HALF SALARY - PERA/ACA
            outer.AddCell(new PdfPCell(new Phrase("UOLI PREM. GSIS", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(0.00)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            // SECOND HALF SALARY - SPACE
            outer.AddCell(new PdfPCell(new Phrase("DEDUCTION", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(296.40)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            //*****//
            // DEDUCTIONS
            outer.AddCell(new PdfPCell(new Phrase("MEM. GSIS", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(375.00)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            // SECOND HALF SALARY - SPACE
            outer.AddCell(new PdfPCell(new Phrase("NET CELLPHONE COMM.", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                BorderColor = new BaseColor(255,255,255),
                Border = PdfPCell.RIGHT_BORDER,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("7,111.35", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            //**//
            outer.AddCell(new PdfPCell(new Phrase("ED. GSIS", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(2,774.79)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            // SECOND HALF SALARY - BASIC SALARY
            outer.AddCell(new PdfPCell(new Phrase("ANNIVERSARY ALLOWANCE", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(200.00)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });

            //******//

            // FIRST HALF SALARY - PERA/ACA
            outer.AddCell(new PdfPCell(new Phrase("GSIS SAL.", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(0.00)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            // SECOND HALF SALARY - SPACE
            outer.AddCell(new PdfPCell(new Phrase("UNIFORM AND CLOTHING ALL.", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(296.40)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //*****//
            // DEDUCTIONS
            outer.AddCell(new PdfPCell(new Phrase("GSIS POL", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(375.00)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            // SECOND HALF SALARY - SPACE
            outer.AddCell(new PdfPCell(new Phrase("MASTERAL", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("7,111.35", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            ///*****????////
            outer.AddCell(new PdfPCell(new Phrase("GSIS CAL/EMER", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("32,747.00", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            // SECOND HALF SALARY - BASIC SALARY
            outer.AddCell(new PdfPCell(new Phrase("DEDUCTION", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(200.00)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });

            //******//

            // FIRST HALF SALARY - PERA/ACA
            outer.AddCell(new PdfPCell(new Phrase("GSIS UOLI", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(6.055.86)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            // SECOND HALF SALARY - SPACE
            outer.AddCell(new PdfPCell(new Phrase("NET MASTERAL", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                BackgroundColor = new BaseColor(224, 224, 224),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(296.40)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                BackgroundColor = new BaseColor(224,224,224),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            //*****//
            // DEDUCTIONS
            outer.AddCell(new PdfPCell(new Phrase("PAG-IBIG Premium", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(375.00)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            // SECOND HALF SALARY - SPACE
            outer.AddCell(new PdfPCell(new Phrase("LOYALTY BONUS", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("7,111.35", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
             
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            //***//
            outer.AddCell(new PdfPCell(new Phrase("PAG-IBIG Loan", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(2,774.79)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            // SECOND HALF SALARY - BASIC SALARY
            outer.AddCell(new PdfPCell(new Phrase("MID-YEAR BONUS", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(200.00)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });

            //******//

            // FIRST HALF SALARY - PERA/ACA
            outer.AddCell(new PdfPCell(new Phrase("CFI", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(0.00)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            // SECOND HALF SALARY - SPACE
            outer.AddCell(new PdfPCell(new Phrase("DEDUCTION", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(296.40)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            //*****//
            // DEDUCTIONS
            outer.AddCell(new PdfPCell(new Phrase("VSMMC", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(375.00)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            // SECOND HALF SALARY - SPACE
            outer.AddCell(new PdfPCell(new Phrase("NET MID-YEAR BONUS", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255,255,255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("7,111.35", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //**//
            outer.AddCell(new PdfPCell(new Phrase("COOP", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(2,774.79)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            // SECOND HALF SALARY - BASIC SALARY
            outer.AddCell(new PdfPCell(new Phrase("MONETIZATION", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(200.00)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });

            //******//

            // FIRST HALF SALARY - PERA/ACA
            outer.AddCell(new PdfPCell(new Phrase("GSIS...", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(0.00)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            // SECOND HALF SALARY - SPACE
            outer.AddCell(new PdfPCell(new Phrase("DEDUCTION", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(296.40)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //*****//
            // DEDUCTIONS
            outer.AddCell(new PdfPCell(new Phrase("DISALLOWANCES", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(375.00)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            // SECOND HALF SALARY - SPACE
            outer.AddCell(new PdfPCell(new Phrase("NET MONETIZATION", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255,255,255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("7,111.35", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            //?MEOWOK//
            outer.AddCell(new PdfPCell(new Phrase("SOS/HELP", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("32,747.00", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            // SECOND HALF SALARY - BASIC SALARY
            outer.AddCell(new PdfPCell(new Phrase("PBB (Performance Based Bonus)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(200.00)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });

            //******//

            // FIRST HALF SALARY - PERA/ACA
            outer.AddCell(new PdfPCell(new Phrase("GSIS/CA", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(6.055.86)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            // SECOND HALF SALARY - SPACE
            outer.AddCell(new PdfPCell(new Phrase("PES", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(296.40)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            //*****//
            // DEDUCTIONS
            outer.AddCell(new PdfPCell(new Phrase("REL", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(375.00)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            // SECOND HALF SALARY - SPACE
            outer.AddCell(new PdfPCell(new Phrase("CNA", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("7,111.35", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            //***//
            outer.AddCell(new PdfPCell(new Phrase("TOTAL DEDUCTIONS", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER| PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(2,774.79)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            // SECOND HALF SALARY - BASIC SALARY
            outer.AddCell(new PdfPCell(new Phrase("DEDUCTION", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(200.00)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });

            //******//

            // FIRST HALF SALARY - PERA/ACA
            outer.AddCell(new PdfPCell(new Phrase("Net Salary & PERA/ACA", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER| PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(0.00)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            // SECOND HALF SALARY - SPACE
            outer.AddCell(new PdfPCell(new Phrase("NET CNA", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                BorderColor = new BaseColor(255,255,255),
                Border = PdfPCell.RIGHT_BORDER,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(296.40)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            //*****//
            // DEDUCTIONS
            outer.AddCell(new PdfPCell(new Phrase("SUBSISTENCE", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(375.00)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            // SECOND HALF SALARY - SPACE
            outer.AddCell(new PdfPCell(new Phrase("LONGEVITY DIFFERENTIAL", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("7,111.35", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //**//
            outer.AddCell(new PdfPCell(new Phrase("DEDUCTION", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(2,774.79)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            // SECOND HALF SALARY - BASIC SALARY
            outer.AddCell(new PdfPCell(new Phrase("HAZARD DIFFERENTIAL", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(200.00)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });

            //******//

            // FIRST HALF SALARY - PERA/ACA
            outer.AddCell(new PdfPCell(new Phrase("NET SUBSISTENCE", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                BorderColor = new BaseColor(255,255,255),
                Border = PdfPCell.RIGHT_BORDER,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(0.00)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            // SECOND HALF SALARY - SPACE
            outer.AddCell(new PdfPCell(new Phrase("SALARY DIFFERENTIAL", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(296.40)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //*****//
            // DEDUCTIONS
            outer.AddCell(new PdfPCell(new Phrase("LONGEVITY", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(375.00)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            // SECOND HALF SALARY - SPACE
            outer.AddCell(new PdfPCell(new Phrase("RATA", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("7,111.35", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            outer.AddCell(new PdfPCell(new Phrase("DEDUCTION", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(375.00)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            // SECOND HALF SALARY - SPACE
            outer.AddCell(new PdfPCell(new Phrase("SPACE", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("SPACE", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            outer.AddCell(new PdfPCell(new Phrase("NET LONGEVITY", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                BorderColor = new BaseColor(255,255,255),
                Border = PdfPCell.RIGHT_BORDER,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(375.00)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            // SECOND HALF SALARY - SPACE
            outer.AddCell(new PdfPCell(new Phrase("SPACE", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("SPACE", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            outer.AddCell(new PdfPCell(new Phrase("CLOTHING", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(375.00)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            // SECOND HALF SALARY - SPACE
            outer.AddCell(new PdfPCell(new Phrase("SPACE", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("SPACE", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            outer.AddCell(new PdfPCell(new Phrase("TOTAL PAY (1st HALF)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                BorderColor = new BaseColor(255,255,255),
                Border = PdfPCell.RIGHT_BORDER,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(375.00)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            //SPACE BETWEEN FIRST AND SECOND HALF SALARY
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            // SECOND HALF SALARY - SPACE
            outer.AddCell(new PdfPCell(new Phrase("TOTAL PAY (2nd HALF)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                BorderColor = new BaseColor(255, 255, 255),
                Border = PdfPCell.RIGHT_BORDER,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("32,747.00", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            outer.AddCell(new PdfPCell(new Phrase("SPACE", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 20,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            outer.AddCell(new PdfPCell(new Phrase("TOTAL PAY", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER|PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 15,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("49,620.00", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("TOTAL DEDUCTION", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
            {

                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER| PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 15,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("(18,550.16)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("NET PAYMENT", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER| PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 15,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("31,070.07", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("TOTAL PAY (1st HALF)", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER| PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 15,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("31,070.07", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });


            doc.Open();
            doc.Add(outer);
            doc.Close();
            return File(strAttachment, "application/pdf");
        }

        // GET: Testing
        public ActionResult Index()
        {
            MemoryStream workStream = new MemoryStream();
            StringBuilder status = new StringBuilder("");
            string strPDFFileName = String.Format("Sample.pdf");

            Document doc = new Document();
            doc.SetMargins(20f, 20f, 20f, 20f);
            doc.SetPageSize(PageSize.LEGAL.Rotate());
            

            PdfPTable main = new PdfPTable(1);
            main.TotalWidth = 200;
            main.WidthPercentage = 50;
           
            String strAttachment = Server.MapPath(Url.Content("~/public/Pdf/" + strPDFFileName));
            String imageURL = Server.MapPath(Url.Content("~/public/img/logo.png"));
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(strAttachment, FileMode.Create));
            doc.Open();
            for (int i = 0; i < 40; i++) {
                main.AddCell(main.Rows.Count + " " + main.CalculateHeights() + "");
                if (i % 10 == 0 && i > 0) {
                    main.AddCell("BREAK\nBREAK");
                    doc.Add(main);
                    main.FlushContent();
                }
            }
            doc.Add(main);
            doc.Close();
            return File(strAttachment, "application/pdf");
        }

    }
}
