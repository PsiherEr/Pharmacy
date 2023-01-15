using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyDB.Models
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; }
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
        public int ManagerId { get; set; }
        public virtual Employee Manager { get; set; }
        public virtual List<MedicineInOrder> MedicinesInOrder { get; set; } = new List<MedicineInOrder>();
    }
}
