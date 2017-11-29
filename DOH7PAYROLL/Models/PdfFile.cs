using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOH7PAYROLL.Models
{
    public class PdfFile
    {
        public String ID { get; set; }
        public String UserID { get; set; }
        public String StartDate { get; set; }
        public String EndDate { get; set; }
        public String FileName { get; set; }
        public String PayType { get; set; }
        public String Date { get; set; }

        public PdfFile(String ID,String UserID,String StartDate,String EndDate,String FileName,String Date,String PayType) {
            this.ID = ID;
            this.UserID = UserID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.FileName = FileName;
            this.Date = Date;
            this.PayType = PayType;
        }
    }
}