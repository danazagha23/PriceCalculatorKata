using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculatorSolution
{
    public class Tax
    {
        static int TaxPercentage;
        public Tax(int _taxPercentage) 
        {
            TaxPercentage = _taxPercentage; 
        }
        public static decimal ProductTax(decimal _basicPrice)
        {
            return (decimal.Round(_basicPrice, 2, MidpointRounding.AwayFromZero) * TaxPercentage) / 100;
        }
    }
}
