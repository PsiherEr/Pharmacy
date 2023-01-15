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
        public virtual Medicine Medicine { get; set; }
        public int WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public int Quantity { get; set; }
    }
}
