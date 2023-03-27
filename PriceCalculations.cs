using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculatorSolution
{
    public class PriceCalculations
    {
        public static int TaxPercentage;
        public static int DiscountPercentage;
        public PriceCalculations(int _taxPercentage, int _discountPercentage)
        {
            TaxPercentage = _taxPercentage;
            DiscountPercentage = _discountPercentage;
        }
        public static decimal ProductTax(decimal _basicPrice)
        {
            return decimal.Round(_basicPrice * TaxPercentage / 100, 2, MidpointRounding.AwayFromZero);
        }
        public static decimal ProductDiscount(decimal _basicPrice)
        {
            return decimal.Round(_basicPrice * DiscountPercentage / 100, 2, MidpointRounding.AwayFromZero);
        }
    }
}
