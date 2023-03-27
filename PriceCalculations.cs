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
        public static int UniversalDiscountPercentage;
        static Dictionary<int, int> UpcDiscounts;
        public PriceCalculations(int _taxPercentage, int _universalDiscount, Dictionary<int, int> _upcDiscount)
        {
            TaxPercentage = _taxPercentage;
            UniversalDiscountPercentage = _universalDiscount;
            UpcDiscounts = _upcDiscount;
        }
        public static decimal ProductTax(decimal _basicPrice)
        {
            return decimal.Round(_basicPrice * TaxPercentage / 100, 2, MidpointRounding.AwayFromZero);
        }
        public static decimal UniversalProductDiscount(decimal _basicPrice)
        {

            return decimal.Round(_basicPrice * UniversalDiscountPercentage / 100, 2, MidpointRounding.AwayFromZero);
        }
        public static decimal UPCProductDiscount(decimal _basicPrice, int code)
        {
            if(UpcDiscounts.ContainsKey(code))
            {
                return decimal.Round(_basicPrice * UpcDiscounts[code] / 100, 2, MidpointRounding.AwayFromZero);
            }
            return 0;
        }
    }
}
