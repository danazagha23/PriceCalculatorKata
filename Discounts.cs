using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriceCalculatorSolution;

namespace DiscountSolution
{
    public class Discounts
    {
        public static int UniversalDiscountPercentage;
        static Dictionary<int, int> UpcDiscounts;
        public static string DiscountMethod;
        public Discounts(string _discountMethod, int _universalDiscount, Dictionary<int, int> _upcDiscount) 
        {
            UniversalDiscountPercentage = _universalDiscount;
            UpcDiscounts = _upcDiscount;
            DiscountMethod = _discountMethod;
        }
        //calculate universal discount 
        public static decimal UniversalProductDiscount(decimal _basicPrice)
        {
            return PriceCalculations.PercentageToAbsolute(_basicPrice, UniversalDiscountPercentage);
        }
        //calculate upc discount
        public static decimal UPCProductDiscount(decimal _basicPrice, int code)
        {
            if (UpcDiscounts.ContainsKey(code))
            {
                return PriceCalculations.PercentageToAbsolute(_basicPrice, UpcDiscounts[code]);
            }
            return 0;
        }

    }
}
