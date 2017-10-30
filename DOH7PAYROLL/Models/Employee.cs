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
        public String JobType { get; set; }

        public Payroll Payroll { get; set; }

        public Employee(String PersonnelID, String Firstname, String Lastname, String JobType,Payroll payroll) {
            this.PersonnelID = PersonnelID;
            this.Firstname = Firstname;
            this.Lastname = Lastname;
            this.JobType = JobType;
            this.Payroll = payroll;
        }
    }
}