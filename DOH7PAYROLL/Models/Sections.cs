using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOH7PAYROLL.Models
{
    public class Sections
    {
        public String SectionID { get; set; }
        public String DivisionID { get; set; }
        public String Description { get; set; }
        public String HeadID { get; set; }

        public Sections(String SectionID,String DivisionID,String Description,String HeadID)
        {
            this.SectionID = SectionID;
            this.DivisionID = DivisionID;
            this.Description = Description;
            this.HeadID = HeadID;
        }
    }
}