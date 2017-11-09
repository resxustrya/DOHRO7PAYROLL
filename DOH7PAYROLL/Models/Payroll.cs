using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOH7PAYROLL.Models
{
    public class Payroll
    {
        public String UserId { get; set; }
        public String WorkDays { get; set; }
        public String PayrollDate { get; set; }
        public String Salary { get; set; }
        public String MinutesLate { get; set; }
        public String Coop { get; set; }
        public String Phic { get; set; }
        public String Disallowance { get; set; }
        public String Gsis { get; set; }
        public String Pagibig { get; set; }
        public String ExcessMobile { get; set; }
        
        public String Flag { get; set; }

        public Payroll(String UserId,String PayrollDate, String WorkDays,String Salary, String MinutesLate, String Coop, String Phic, String Disallowance
            , String Gsis,String Pagibig, String ExcessMobile,String Flag) {
            this.UserId = UserId;
            this.PayrollDate = PayrollDate;
            this.WorkDays = WorkDays;
            this.Salary = Salary;
            this.MinutesLate = MinutesLate;
            this.Coop = Coop;
            this.Phic = Phic;
            this.Disallowance = Disallowance;
            this.Gsis = Gsis;
            this.Pagibig = Pagibig;
            this.ExcessMobile = ExcessMobile;
            this.Flag = Flag;
        }
    }
}