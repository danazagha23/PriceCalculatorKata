using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriceCalculatorSolution;

namespace ProductServicesSolution
{
    public class TaxService
    {
        public TaxService(decimal taxPercentage, bool taxPrecedence) 
        { 
            TaxPercentage = taxPercentage;
            isTaxPrecedence = taxPrecedence;
        }
        public static decimal TaxPercentage { get; set; }
        public static bool isTaxPrecedence { get; set; }

        //calculate tax 
        public static decimal CalculateTax(decimal basicPrice)
        {
            return ProductServiceModel.PercentageToAbsolute(basicPrice, TaxPercentage);
        }

    }
}
