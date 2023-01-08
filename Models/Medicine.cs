﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyDB.Models
{
    public class Medicine
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public DateTime SellBy { get; set; }
        public MedicineInOrder MedicineInOrder { get; set; }
        public List<MedicineInReceipt> MedicineInReceipts { get; set; } = new List<MedicineInReceipt>();
        public List<MedicineInWarehouse> MedicineInWarehouses { get; set; } = new List<MedicineInWarehouse>();
    }
}
