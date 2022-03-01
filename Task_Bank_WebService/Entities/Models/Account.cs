using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int Acc_No { get; set; }
        public double Balance { get; set; }
        public DateTime OpenDate { get; set; }
        public string Branch { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
