using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculatorSolution
{
    public class Product
    {
        public string Name { get; set; }
        public int UPC { get; set; }
        public decimal Price { get; set; }

        // Calculated Properties
        public decimal? TotalPrice { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(1024);

            sb.Append(Name);
            sb.AppendLine($"  UPC: {UPC}");
            sb.AppendLine($"   Price: {Price:c}");
            if (TotalPrice.HasValue)
            {
                sb.AppendLine($"   Total Price: {TotalPrice:c}");
            }
            return sb.ToString();
        }
    }
}