using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOH7PAYROLL.Models
{
    public class Employee
    {
        public String PersonnelID { get; set; }
        public String Firstname { get; set; }
        public String Lastname { get; set; }
        public String MiddleName { get; set; }
        public String JobType { get; set; }
        public String Tin { get; set; }
        public String Section { get; set; }
        public String Disbursement { get; set; }
        public String DivisionID { get; set; }
        public String UserType { get; set; }
        public String PIN { get; set; }



        public Employee(String PersonnelID, String Firstname, String Lastname,String MiddleName, String JobType,
            String Tin, String Section,String Disbursement,String DivisionID,String UserType,String PIN) {
            this.PersonnelID = PersonnelID;
            this.Firstname = Firstname;
            this.MiddleName = MiddleName;
            this.Lastname = Lastname;
            this.JobType = JobType;
            this.Tin = Tin;
            this.Section = Section;
            this.Disbursement = Disbursement;
            this.DivisionID = DivisionID;
            this.UserType = UserType;
            this.PIN = PIN;
        }
        public Employee() { }
    }
}