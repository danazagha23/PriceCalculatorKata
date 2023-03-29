using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriceCalculatorSolution;

namespace CostSolution
{
    public class AdditionalCosts
    {
        public static int TaxPercentage;
        public static decimal PackagingAmount;
        public static decimal TransportAmount;
        public AdditionalCosts(int _tax, decimal _packagingAmount, decimal _transportAmount)
        {
            TaxPercentage = _tax;
            PackagingAmount = _packagingAmount;
            TransportAmount = _transportAmount;
        }

        //calculate tax 
        public static decimal ProductTax(decimal _basicPrice)
        {
            return PriceCalculations.PercentageToAbsolute(_basicPrice, TaxPercentage);
        }

        //calculate expenses 
        public static decimal PackagingCost(decimal _basicPrice, bool _isPercentage)
        {
            return PackagingAmount;
        }
    }
}
