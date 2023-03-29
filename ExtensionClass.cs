using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculatorSolution
{
    public static class ExtensionClass
    {
        public static decimal ToTwoDecimalDigits(this decimal _amount)
        {
            return decimal.Round(_amount, 2, MidpointRounding.AwayFromZero);
        }
    }
}
