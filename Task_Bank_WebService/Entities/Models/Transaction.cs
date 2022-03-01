using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime TRX_Date { get; set; }
        public double Amount { get; set; }
        /// <summary>
        /// Deposit or Withdraw
        /// </summary>
        public string Type { get; set; }

        public int Account_Id { get; set; }
        //public Account Account { get; set; }
    }
}
