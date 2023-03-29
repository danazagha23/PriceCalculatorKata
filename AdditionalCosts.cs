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
        public static int ExpensesPercentage;
        public static int ExpensesAmount;

        public AdditionalCosts(int _tax, int _expensesPercentage, int _expensesAmount)
        {
            TaxPercentage = _tax;
            ExpensesPercentage = _expensesPercentage;
            ExpensesAmount = _expensesAmount;
        }

        //calculate tax 
        public static decimal ProductTax(decimal _basicPrice)
        {
            return PriceCalculations.PercentageToAbsolute(_basicPrice, TaxPercentage);
        }

        //calculate expenses 
        public static decimal ProductExpenses(decimal _basicPrice)
        {
            return PriceCalculations.PercentageToAbsolute(_basicPrice, ExpensesPercentage) + ExpensesAmount;
        }
    }
}
