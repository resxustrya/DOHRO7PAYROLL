using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOH7PAYROLL.Models
{
    public class PdfFile
    {
        public String ID { get; set; }
        public String FileName { get; set; }
        public String Date { get; set; }

        public PdfFile(String ID,String FileName,String Date) {
            this.ID = ID;
            this.FileName = FileName;
            this.Date = Date;
        }
    }
}