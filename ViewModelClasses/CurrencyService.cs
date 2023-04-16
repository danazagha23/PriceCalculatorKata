using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculatorSolution
{
    public class CurrencyService
    {
        public CurrencyService(string currency)
        {
            Currency = currency;
        }
        public static string Currency { get; set; }

        public static decimal ConvertCurrency(decimal price)
        {
            if (Currency.Equals("GBP"))
            {
                return decimal.Round(price * 0.8M, 4, MidpointRounding.AwayFromZero);
            }

            if (Currency.Equals("JPY"))
            {
                return decimal.Round(price * 133.58M, 4, MidpointRounding.AwayFromZero);
            }

            return price;
        }
    }
}
