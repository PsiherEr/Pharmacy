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
        public Supplier Supplier { get; set; }
        public int ManagerId { get; set; }
        public Employee Manager { get; set; }
        public List<MedicineInOrder> MedicinesInOrder { get; set; } = new List<MedicineInOrder>();
    }
}
