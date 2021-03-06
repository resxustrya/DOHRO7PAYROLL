﻿using System;
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

        public ActionResult CreateJoPayslip()
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
            outer.AddCell(new PdfPCell(new Phrase("Job Order Payslip For The Month Of May 2017", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 20,
                PaddingBottom = 20,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            outer.AddCell(new PdfPCell(new Phrase("ID No.", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(0, 0, 0),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("12122388", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 15,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("Employee name", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
            {

                BackgroundColor = new BaseColor(0, 0, 0),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("PANGCATAN", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 15,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("Section", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(0, 0, 0),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("MSD", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(124, 124, 124),
                Colspan = 15,
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

            outer.AddCell(new PdfPCell(new Phrase("Basic Salary:", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 10,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            outer.AddCell(new PdfPCell(new Phrase("19,620.00", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 10,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });


            outer.AddCell(new PdfPCell(new Phrase("Deductions:", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 20,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });


            outer.AddCell(new PdfPCell(new Phrase("Adjustment:", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                PaddingLeft = 10,
                Border = 0,
                Colspan = 10,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("1,000.00", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 10,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            outer.AddCell(new PdfPCell(new Phrase("Tardiness/Absences:", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                PaddingLeft = 10,
                Border = 0,
                Colspan = 10,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("1,000.00", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 10,
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
            outer.AddCell(new PdfPCell(new Phrase("Net Amount:", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(0, 0, 0),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 10,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            outer.AddCell(new PdfPCell(new Phrase("19,620.00", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(0, 0, 0),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 10,
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

            outer.AddCell(new PdfPCell(new Phrase("Deductions:", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 20,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });


            outer.AddCell(new PdfPCell(new Phrase("10% TAX:", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                PaddingLeft = 10,
                Border = 0,
                Colspan = 10,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("1,000.00", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 10,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("2% TAX:", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                PaddingLeft = 10,
                Border = 0,
                Colspan = 10,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("1,000.00", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 10,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("2% TAX:", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                PaddingLeft = 10,
                Border = 0,
                Colspan = 10,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("1,000.00", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 10,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("Coop:", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                PaddingLeft = 10,
                Border = 0,
                Colspan = 10,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("1,000.00", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 10,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("Disallowance:", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                PaddingLeft = 10,
                Border = 0,
                Colspan = 10,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("1,000.00", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 10,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("Pag-Ibig:", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                PaddingLeft = 10,
                Border = 0,
                Colspan = 10,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("1,000.00", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 10,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("PHIC:", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                PaddingLeft = 10,
                Border = 0,
                Colspan = 10,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("1,000.00", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 10,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("GSIS:", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                PaddingLeft = 10,
                Border = 0,
                Colspan = 10,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("1,000.00", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 10,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("Excess Mobile:", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                PaddingLeft = 10,
                Border = 0,
                Colspan = 10,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("1,000.00", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 10,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("SPACE:", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                PaddingLeft = 10,
                Border = 0,
                Colspan = 20,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("Total Deductions:", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
            {

                PaddingLeft = 10,
                BackgroundColor = new BaseColor(0, 0, 0),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 10,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("1,000.00", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(0, 0, 0),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 10,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });

            outer.AddCell(new PdfPCell(new Phrase("SPACE:", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                PaddingLeft = 10,
                Border = 0,
                Colspan = 20,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("Net Income:", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                PaddingLeft = 10,
                BackgroundColor = new BaseColor(0, 0, 0),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 10,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("1,000.00", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(0, 0, 0),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 10,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });


            doc.Open();
            doc.Add(outer);
            doc.Close();
            return File(strAttachment, "application/pdf");
        }



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
            int size = 7;
            outer.AddCell(new PdfPCell(new Phrase("Republic of Philippines", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 20,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            outer.AddCell(new PdfPCell(new Phrase("DEPARTMENT OF HEALTH", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 20,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            outer.AddCell(new PdfPCell(new Phrase("Regional Office VII", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 20,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            outer.AddCell(new PdfPCell(new Phrase("Osmeña Blvd., Cebu City", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 20,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            outer.AddCell(new PdfPCell(new Phrase("Tel. No.: (032) 418-7653 Fax: (032) 254-0109", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 20,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            outer.AddCell(new PdfPCell(new Phrase("Salary and Other Benefits Slip For The Month Of May 2017", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 20,
                PaddingBottom = 20,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            outer.AddCell(new PdfPCell(new Phrase("ID No.", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(0, 0, 0),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("12122388", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 15,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("Employee name", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {

                BackgroundColor = new BaseColor(0, 0, 0),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("PANGCATAN", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 15,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("Designation", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(0, 0, 0),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("MSD", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(124, 124, 124),
                Colspan = 15,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("SPACE", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 20,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("FIRST HALF SALARY", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(0, 0, 0),
                Border = 0,
                Colspan = 9,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("SECOND HALF SALARY", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(0, 0, 0),
                Border = 0,
                Colspan = 9,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("BASIC SALARY", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("32,747.00", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("SALARY & PERA/ACA", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("8,635.32", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("PERA / ACA", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("2,000.00", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("SPACE", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("SPACE", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("TOTAL SALARY", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                BackgroundColor = new BaseColor(124, 124, 124),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("0.00", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                BackgroundColor = new BaseColor(124, 124, 124),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("HAZARD PAY", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("8,186.75", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("DEDUCTIONS", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(0, 0, 0),
                Border = 0,
                Colspan = 9,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(255, 255, 255),
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("MORTUARY", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(100.00)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("DEDUCTION (Late)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("32,747.00", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("HWMPC", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(200.00)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("TAX", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(6.055.86)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("OTHER ACCOUNTS PAYABLE", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(296.40)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("MEDICARE", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(375.00)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("NET HAZARD PAY", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("8,111.35", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("GSIS Premium", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(2,774.79)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("CELLPHONE COMMUNICATION", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(200.00)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("UOLI PREM. GSIS", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(0.00)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("DEDUCTION", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(296.40)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("MEM. GSIS", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(375.00)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("NET CELLPHONE COMM.", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("8,111.35", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("ED. GSIS", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(2,774.79)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("ANNIVERSARY ALLOWANCE", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(200.00)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("GSIS SAL.", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(0.00)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("UNIFORM AND CLOTHING ALL.", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(296.40)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("GSIS POL", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(375.00)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("MASTERAL", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("8,111.35", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("GSIS CAL/EMER", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("32,747.00", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("DEDUCTION", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(200.00)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("GSIS UOLI", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(6.055.86)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("NET MASTERAL", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(296.40)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("PAG-IBIG Premium", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(375.00)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("LOYALTY BONUS", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("8,111.35", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("PAG-IBIG Loan", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(2,774.79)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("MID-YEAR BONUS", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(200.00)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("CFI", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(0.00)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("DEDUCTION", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(296.40)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("VSMMC", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(375.00)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("NET MID-YEAR BONUS", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("8,111.35", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("COOP", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(2,774.79)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("YEAR-END BONUS", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(200.00)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("GSIS...", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(0.00)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("DEDUCTION", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(296.40)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("DISALLOWANCES", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(375.00)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("NET MID-YEAR BONUS", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("8,111.35", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("SOS/HELP", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(375.00)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("MONETIZATION", new Font(FontFactory.GetFont("HELVETICA", size, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("8,111.35", new Font(FontFactory.GetFont("HELVETICA", size, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("GSIS/CA", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(375.00)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("DEDUCTION", new Font(FontFactory.GetFont("HELVETICA", size, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("8,111.35", new Font(FontFactory.GetFont("HELVETICA", size, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("REL", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(375.00)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("NET MONETIZATION", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("8,111.35", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("TOTAL DEDUCTIONS", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(375.00)", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("PBB (Performance Based Bonus)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("8,111.35", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("NET SALARY & PERA/ACA", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(375.00)", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("PES", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("8,111.35", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("NET 1ST HALF SALARY", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(375.00)", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("ONA", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("8,111.35", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("SUBSISTENCE", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(375.00)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("DEDUCTION", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("8,111.35", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("DEDUCTION (Absences)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(375.00)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("NET ONA", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("8,111.35", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("NET SUBSISTENCE", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(375.00)", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("PEI", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("8,111.35", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("LONGEIVTY", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(375.00)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("LONGEVITY DIFFERENTIAL", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("8,111.35", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("DEDUCTION", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(375.00)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("RATA", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("8,111.35", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("NET LONGEVITY", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(375.00)", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("OTHERS", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("8,111.35", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("HAZARD DIFFERENTIAL", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(375.00)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("SPACE", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(224, 224, 224)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("SPACE", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(224, 224, 224)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("SALARY DIFFERENTIAL", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("(375.00)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("SPACE", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("SPACE", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });

            outer.AddCell(new PdfPCell(new Phrase("SPACE", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(224, 224, 224)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("SPACE", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(224, 224, 224)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("SPACE", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(224, 224, 224)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("SPACE", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(224, 224, 224)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });

            outer.AddCell(new PdfPCell(new Phrase("SPACE", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("SPACE", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("SPACE", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("SPACE", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("SPACE", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(224, 224, 224)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("SPACE", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(224, 224, 224)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("SPACE", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(224, 224, 224)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("SPACE", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(224, 224, 224)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });


            outer.AddCell(new PdfPCell(new Phrase("TOTAL PAY (1ST HALF)", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("16.530.45", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("TOTAL PAY (2ND HALF)", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("14,539.00", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(124, 124, 124),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("SPACE", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("SPACE", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("SPACE", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            outer.AddCell(new PdfPCell(new Phrase("SPACE", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });


            outer.AddCell(new PdfPCell(new Phrase("TOTAL PAY", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 15,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("49,620.00", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("TOTAL DEDUCTION", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 15,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("(17,550.16)", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                BackgroundColor = new BaseColor(224, 224, 224),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });

            outer.AddCell(new PdfPCell(new Phrase("NET PAYMENT", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(0, 0, 0),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 15,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("49,620.00", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(0, 0, 0),
                Border = PdfPCell.BOTTOM_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 5,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("TOTAL PAY (1ST HALF)", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(0, 0, 0),
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                BorderColor = new BaseColor(255, 255, 255),
                Colspan = 15,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            outer.AddCell(new PdfPCell(new Phrase("(18,550.16)", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BackgroundColor = new BaseColor(0, 0, 0),
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

        public PdfPTable GetItems()
        {
            float[] widths = { 2, 5, 5, 4, 4, 4, 3, 3, 4, 3, 4, 3, 4, 3, 4, 3, 4, 3, 4, 2, 2, 4, 2, 5, 1, 4 };
            PdfPTable data = new PdfPTable(26);
            data.SetWidths(widths);
            data.TotalWidth = 400;
            data.WidthPercentage = 100;

            data.AddCell(new PdfPCell(new Phrase("1", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Rowspan=10,
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("PANGCATAN,ASNAUI O", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Rowspan = 6,
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("COMPUTER PROGRAMMER 1", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Rowspan = 5,
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("13,851.00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("0.00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("15,851.00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Rowspan=2,
                PaddingTop=10,
                PaddingBottom= 0,
                PaddingRight= 0,
                PaddingLeft= 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Rowspan=4,
                PaddingTop = 15,
                PaddingBottom = 0,
                PaddingRight = 0,
                PaddingLeft = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("LIF", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("1,246.59", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("ED5", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("OPL", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("FUN", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("500.00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("NHM", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("CSL", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("1,909.00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Rowspan=5,
                Colspan = 2,
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("6,960.00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Rowspan=5,
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("07", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("       ", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("O", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Rowspan=10,
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("PANGCATAN ASNAUI O 0618 (NO PICTURE)", new Font(FontFactory.GetFont("HELVETICA", 5, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Rowspan = 10,
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            //2ND ROW
            data.AddCell(new PdfPCell(new Phrase("(0.00)", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("(0.00)", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("OP1", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("FIR", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("HOU", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("MUL", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("GNP", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("DA1", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("      ", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("      ", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            //3rd ROW
            data.AddCell(new PdfPCell(new Phrase("%2,000.00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("0.00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("CA.00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("OP2", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("ADI", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("REL", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("PHO", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("GNS", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("MOr", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("15", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("            ", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            //4TH ROW
            data.AddCell(new PdfPCell(new Phrase("(0.00)", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("0.00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("CB.00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("OP3", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("UA2", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("   ", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("GPL", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("PA1", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("CEL", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("GCL", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("  ", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("            ", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            //5TH ROW
            data.AddCell(new PdfPCell(new Phrase("#.00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("0.00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("CC.00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("162.50.00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Rowspan=6,
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("OP4", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("DA2", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("HEL", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("PA2", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("LBP", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("DIG", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("22", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("            ", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });



            //6TH ROW
            data.AddCell(new PdfPCell(new Phrase("13,851.00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("(.00)", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("0.00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("R.00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("OP5", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("DA3", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("EML", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("GAL", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("RIC", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("CRP", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("UCP", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Rowspan=4,
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("  ", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Rowspan = 4,
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("6,981.00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("  ", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("            ", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });


            //7TH ROW
            data.AddCell(new PdfPCell(new Phrase("Grade : 06 Step : 1", new Font(FontFactory.GetFont("HELVETICA", 6, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("^.00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("M.00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("T.00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("ED1", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("UOI", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("UM", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("GAL", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("RIC", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("CRP", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            
            data.AddCell(new PdfPCell(new Phrase("6,981.00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("30", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("            ", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });


            //8TH ROW
            data.AddCell(new PdfPCell(new Phrase("Actual Basic Salary:", new Font(FontFactory.GetFont("HELVETICA", 6, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Rowspan=3,
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("13,851.00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Rowspan = 3,
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("(.00)", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Rowspan = 3,
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("C.00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Rowspan = 3,
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("E.00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Rowspan = 3,
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("ED2", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("HOS", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("ECA", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("SOS", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("CAL", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("CFI", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            data.AddCell(new PdfPCell(new Phrase("6,981.00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("  ", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("            ", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });


            //9TH ROW
            data.AddCell(new PdfPCell(new Phrase("OP4", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("DA2", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("HEL", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("PA2", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("LBP", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("DIG", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase(".00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            data.AddCell(new PdfPCell(new Phrase("6,981.00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("  ", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            data.AddCell(new PdfPCell(new Phrase("            ", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Padding = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });







            //10TH ROW
            for (int i = 0; i < 24; i++) {
                data.AddCell(new PdfPCell(new Phrase("#.00", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
                {
                    Padding = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_CENTER
                });
            }
            
           


            return data;
        }

        public PdfPTable GetFooter()
        {
            float[] widths = { 2, 5, 5, 4, 4, 4, 3, 3, 4, 3, 4, 3, 4, 3, 4, 3, 4, 3, 4, 2, 2, 4, 2, 5, 1, 4 };
            PdfPTable data = new PdfPTable(26);
            data.SetWidths(widths);
            data.TotalWidth = 400;
            data.WidthPercentage = 100;

            data.AddCell(new PdfPCell(new Phrase("#=STEP INCREMENT; ^-ADDT'L.COMP; %-PERA; CA=COMP3; CB-COMP4; CC-COMP5; R-RA; T-TA; E-EXTRA. ORD.EXP.; M-MISC.EXP; C-CELLARD", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Colspan = 26,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            return data;
        }

        public PdfPTable GetHeader()
        {
            float[] widths = { 2, 5, 5, 4, 4, 4, 3, 3, 4, 3, 4, 3, 4, 3, 4, 3, 4, 3, 4, 2, 2, 4, 2, 5, 1, 4 };
            PdfPTable header = new PdfPTable(26);
            header.SetWidths(widths);
            header.TotalWidth = 400;
            header.WidthPercentage = 100;
            header.AddCell(new PdfPCell(new Phrase("MANAGEMENT SUPPORT DIVISION", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 10,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("GENRAL PAYROLL", new Font(FontFactory.GetFont("HELVETICA", 11, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Rowspan = 2,
                Colspan = 9,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("Page No.: 1 of 9", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 7,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("MANAGEMENT SUPPORT DIVISION", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 10,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("Journal Voucher No. ______________", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 7,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            header.AddCell(new PdfPCell(new Phrase("WE HEREBY ACKNOWLEDGE to have received from the DOH - RO7, Osmeña Blvd, Cebu City, the sums therein specified opposite our respective names,", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 26,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("being in full compensation for our services for the period December 1 - 31, 2017, except as noted otherwise in the Remarks columns.", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 26,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            header.AddCell(new PdfPCell(new Phrase("NO.", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                Border = 0,
                Colspan = 26,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            header.AddCell(new PdfPCell(new Phrase("NO.", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("NO.", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("NO.", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("S  A  L  A  R  Y", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Colspan = 3,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("D E D U C T I O N S", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Colspan = 13,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("T", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("T", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("T", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("T", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("T", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("T", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            header.AddCell(new PdfPCell(new Phrase("NO.", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Rowspan = 2,
                PaddingTop = 15,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("NAME", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Rowspan = 2,
                PaddingTop = 15,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("DESIGNATION SALARY", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Rowspan = 2,
                PaddingTop = 15,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("BASIC *", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                PaddingTop = 15,
                Rowspan = 2,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("SUBS/\nLAUNDRY/\nHAZARD", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.TOP_BORDER | PdfPCell.RIGHT_BORDER | PdfPCell.LEFT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("GROSS\nINCOME\nO.A.INC.", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.TOP_BORDER | PdfPCell.RIGHT_BORDER | PdfPCell.LEFT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("WTAX", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                PaddingTop = 8,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("G  S  I  S", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                PaddingTop = 8,
                Colspan = 6,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("PAGIBIG", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                PaddingTop = 8,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("OTHERS", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                PaddingTop = 8,
                Colspan = 4,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            header.AddCell(new PdfPCell(new Phrase("TOTAL\nDEDUCTIONS", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                Rowspan = 2,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("NET AMOUNT RECEIVED", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                Rowspan = 2,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("PE\nRI\nOD", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                Rowspan = 2,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("SIGNATURE", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                PaddingTop = 8,
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                Rowspan = 2,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("IN\nIT\nIA\nL", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                Rowspan = 2,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("REMARKS", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                PaddingTop = 8,
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                Rowspan = 2,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });


            header.AddCell(new PdfPCell(new Phrase("(Less Abs/Undertime)", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER | PdfPCell.LEFT_BORDER,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("PHIL HEALTH", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("COD", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("AMOUNT", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("COD", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("AMOUNT", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("COD", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("AMOUNT", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("COD", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("AMOUNT", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("COD", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("AMOUNT", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("COD", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            header.AddCell(new PdfPCell(new Phrase("AMOUNT", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            return header;
        }




        public ActionResult CreateRegularSummary()
        {
            String title = "Summary";
            MemoryStream workStream = new MemoryStream();
            StringBuilder status = new StringBuilder("");
            string strPDFFileName = String.Format(title + ".pdf");

            Document doc = new Document();

            doc.SetMargins(0,0,0,0);
            doc.SetPageSize(PageSize.LEGAL.Rotate());

            PdfPTable main = new PdfPTable(1);
            main.TotalWidth = 400;
            main.WidthPercentage = 100;

            float[] widths = { 2, 5, 5, 4, 4, 4, 3, 3, 4, 3, 4, 3, 4, 3, 4, 3, 4, 3, 4, 1, 2, 3, 1, 4, 1, 4 };
            PdfPTable header = new PdfPTable(26);
            header.SetWidths(widths);
            header.TotalWidth = 400;
            header.WidthPercentage = 100;
           

            PdfPTable items = new PdfPTable(26);
            items.SetWidths(widths);
            items.TotalWidth = 400;
            items.WidthPercentage = 100;

            String strAttachment = Server.MapPath(Url.Content("~/public/Pdf/" + strPDFFileName));
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(strAttachment, FileMode.Create));
            header = GetHeader();
            items = GetItems();

            main.AddCell(new PdfPCell(header)
            {
                Border = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            main.AddCell(new PdfPCell(GetItems())
            {
                Border = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            main.AddCell(new PdfPCell(GetFooter())
            {
                Border = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });


            doc.Open();
            doc.Add(main);
            doc.Close();
            return File(strAttachment, "application/pdf");
        }

        // GET: Testing
        public ActionResult Index()
        {
            MemoryStream workStream = new MemoryStream();
            StringBuilder status = new StringBuilder("");
            String strPDFFileName = String.Format("Sample.pdf");

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
            for (int i = 0; i < 40; i++)
            {
                main.AddCell(main.Rows.Count + " " + main.CalculateHeights() + "");
                if (i % 10 == 0 && i > 0)
                {
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
