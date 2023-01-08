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
        public List<MedicineInWarehouse> MedicinesInWarehouse { get; set; } = new List<MedicineInWarehouse>();
        public int ManagerId { get; set; }
        public Employee Manager { get; set; }
    }
}
