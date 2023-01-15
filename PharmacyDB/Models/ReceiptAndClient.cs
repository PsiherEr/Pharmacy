using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyDB.Models
{
    public class ReceiptAndClient
    {
        public int ReceiptId { get; set; }
        public virtual Receipt Receipt { get; set; }
        public int ClientPhone { get; set; }
        public virtual Client Client { get; set; }
    }
}
