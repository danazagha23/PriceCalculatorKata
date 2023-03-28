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
        public static bool TaxPriority;
        static Dictionary<int, int> UpcDiscounts;
        public PriceCalculations(int _taxPercentage, int _universalDiscount, Dictionary<int, int> _upcDiscount, bool taxPriority)
        {
            TaxPercentage = _taxPercentage;
            UniversalDiscountPercentage = _universalDiscount;
            UpcDiscounts = _upcDiscount;
            TaxPriority = taxPriority;
        }
        static decimal tax;
        public static decimal ProductTax(decimal _basicPrice)
        { 
            tax = decimal.Round(_basicPrice * TaxPercentage / 100, 2, MidpointRounding.AwayFromZero);
            return tax;
        }

        static decimal universalDiscount;
        public static decimal UniversalProductDiscount(decimal _basicPrice)
        {
            universalDiscount = decimal.Round(_basicPrice * UniversalDiscountPercentage / 100, 2, MidpointRounding.AwayFromZero);
            return universalDiscount;
        }

        static decimal upcDiscount;
        public static decimal UPCProductDiscount(decimal _basicPrice, int code)
        {
            if(UpcDiscounts.ContainsKey(code))
            {
                upcDiscount = decimal.Round(_basicPrice * UpcDiscounts[code] / 100, 2, MidpointRounding.AwayFromZero);
                return upcDiscount;
            }
            return 0;
        }
    }
}
