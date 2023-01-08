using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyDB.Models
{
    public class MedicineInWarehouse
    {
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        public int Quantity { get; set; }
    }
}
