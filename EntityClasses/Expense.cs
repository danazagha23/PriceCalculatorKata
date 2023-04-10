using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PriceCalculatorSolution
{
    public class Expense
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public bool isAmountPercentage { get; set; }

    }
}
