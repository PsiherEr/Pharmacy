using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyDB.Models
{
    public class Pharmacy
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public int DirectorId { get; set; }
    }
}
