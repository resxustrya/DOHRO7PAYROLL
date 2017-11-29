using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOH7PAYROLL.Models
{
    public class Payroll
    {
        public String Id { get; set; }
        public Employee Employee { get; set; }
        public String WorkDays { get; set; }
        public String StartDate { get; set; }
        public String EndDate { get; set; }
        public String Salary { get; set; }
        public String Adjustment { get; set; }
        public String Remarks { get; set; }
        public String DaysAbsent { get; set; }
        public String MinutesLate { get; set; }
        public String Coop { get; set; }
        public String Phic { get; set; }
        public String Disallowance { get; set; }
        public String Gsis { get; set; }
        public String Pagibig { get; set; }
        public String ExcessMobile { get; set; }

        public String Flag { get; set; }

        public Payroll() { }
        public Payroll(String Id,Employee Employee,String StartDate,String EndDate,String Adjustment, String WorkDays,String DaysAbsent,String Salary, String MinutesLate, String Coop, String Phic, String Disallowance
            , String Gsis,String Pagibig, String ExcessMobile, String Remarks, String Flag) {
            this.Id = Id;
            this.Employee = Employee;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
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
            this.Adjustment = Adjustment;
            this.Remarks = Remarks;
            this.DaysAbsent = DaysAbsent;
        }

    }
}