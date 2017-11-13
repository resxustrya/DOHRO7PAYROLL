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
        public String DisplayName { get; set; }
        public String Date { get; set; }

        public PdfFile(String ID,String DisplayName,String FileName,String Date) {
            this.ID = ID;
            this.FileName = FileName;
            this.DisplayName = DisplayName;
            this.Date = Date;
        }
    }
}