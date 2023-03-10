using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyDB.Models
{
    public class MedicineInReceipt
    {
        public int MedicineId { get; set; }
        public virtual Medicine Medicine { get; set; }
        public int ReceiptId { get; set; }
        public virtual Receipt Receipt { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
