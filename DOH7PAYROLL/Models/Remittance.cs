using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOH7PAYROLL.Models
{
    public class Remittance
    {
        public String ID { get; set; }
        public String UserID { get; set; }
        public String MaxCount { get; set; }
        public String Count { get; set; }
        public String Amount { get; set; }
        
        public Remittance(String ID,String UserID,String MaxCount,String Count,String Amount) {
            this.ID = ID;
            this.UserID = UserID;
            this.MaxCount = MaxCount;
            this.Count = Count;
            this.Amount = Amount;
        }
    }
}