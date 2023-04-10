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
        public DiscountService(decimal universalDiscountPercentage, string discountMethod, string capAmount) 
        {
            UniversalDiscountPercentage = universalDiscountPercentage;
            UpcCodeDiscounts = UPCDiscountRepository.GetAll();
            DiscountMethod = discountMethod;
            cap = capAmount;
        }

        public static string? cap;
        public static decimal CapAmount { get; set; }
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
                return ProductServiceModel.PercentageToAbsolute(basicPrice, UpcCodeDiscounts[code]);
            }
            return 0;
        }
        //calculate cap amount
        public static decimal CalculateCapAmount(decimal basicPrice)
        {
            if (cap.Contains("%"))
            {
                CapAmount = ProductServiceModel.PercentageToAbsolute(basicPrice, Convert.ToDecimal(cap.Substring(0, cap.Length-1)));
            }
            else
            {
                CapAmount = Convert.ToDecimal(cap);
            }
            return CapAmount;
        }
    }
}
