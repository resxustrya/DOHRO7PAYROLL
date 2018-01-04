using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOH7PAYROLL.Models
{
    public class InCharge
    {
        public String PersonnelID { get; set; }
        public String Firstname { get; set; }
        public String Lastname { get; set; }
        public String MiddleName { get; set; }
        public String DivisionID { get; set; }
        public String Description { get; set; }



        public InCharge(String PersonnelID, String Firstname, String Lastname,String MiddleName,
            String DivisionID,String Description) {
            this.PersonnelID = PersonnelID;
            this.Firstname = Firstname;
            this.MiddleName = MiddleName;
            this.Lastname = Lastname;
            this.DivisionID = DivisionID;
            this.Description = Description;
        }
       
    }
}