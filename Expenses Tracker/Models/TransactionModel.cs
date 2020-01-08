using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expenses_Tracker.Models
{
    public class TransactionModel
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Vendor { get; set; }
        public string Category { get; set; }
        public Guid User_Id { get; set; }
    }
}
