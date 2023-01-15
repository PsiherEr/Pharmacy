using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyDB.Models
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public virtual List<MedicineInWarehouse> MedicinesInWarehouse { get; set; } = new List<MedicineInWarehouse>();
        public int ManagerId { get; set; }
        public virtual Employee Manager { get; set; }
    }
}
