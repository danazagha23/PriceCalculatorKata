using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculatorSolution
{
    public class PriceCalculations
    {
        
        public static bool TaxPriority;
        
        public PriceCalculations(bool taxPriority)
        {
            TaxPriority = taxPriority;
        }

        //convert percentage to absolute value
        public static decimal PercentageToAbsolute(decimal _basicPrice, int _percentage)
        {
            return decimal.Round(_basicPrice * _percentage / 100, 2, MidpointRounding.AwayFromZero);
        }
    }
}
