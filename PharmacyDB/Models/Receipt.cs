using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyDB.Models
{
    public class Receipt
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime CreationDate { get; set; }
        public ReceiptAndClient ReceiptAndClient { get; set; }
        public List<MedicineInReceipt> MedicinesInReceipt { get; set; } = new List<MedicineInReceipt>();
    }
}
