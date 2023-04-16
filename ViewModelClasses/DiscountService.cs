using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriceCalculatorSolution;

namespace ProductServicesSolution
{
    public class DiscountService
    {
        public DiscountService(decimal universalDiscountPercentage, string discountMethod) 
        {
            UniversalDiscountPercentage = universalDiscountPercentage;
            UpcCodeDiscounts = UPCDiscountRepository.GetAll();
            DiscountMethod = discountMethod;
        }

        public static Dictionary<int, int> UpcCodeDiscounts { get; set; }
        public static decimal UniversalDiscountPercentage { get; set; }
        public static string DiscountMethod { get; set; }

        //calculate universal discount 
        public static decimal CalculateUniversalDiscount(decimal basicPrice)
        {
            return ProductServiceModel.PercentageToAbsolute(basicPrice, UniversalDiscountPercentage);
        }

        //calculate upc discount
        public static decimal CalculateUPCDiscount(decimal basicPrice, int code)
        {
            if (UpcCodeDiscounts.ContainsKey(code))
            {
                return decimal.Round(basicPrice * UpcCodeDiscounts[code] / 100, 2, MidpointRounding.AwayFromZero);
            }
            return 0;
        }
    }
}
