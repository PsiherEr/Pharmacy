using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyDB.Models
{
    public class Client
    {
        public int Phone { get; set; }
        public string FullName { get; set; }
        public virtual List<ReceiptAndClient> ReceiptsAndClient { get; set; } = new List<ReceiptAndClient>();
    }
}
