using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataObjects
{
    internal class Part
    {
        public int PartId { get; set; }
        public string PartNumber { get; set; }
        public string LongDescription { get; set; }
        public string UnitOfMeasure { get; set; }
        public double CorePrice { get; set; }
        public double UnitPrice { get; set; }

    }
}
