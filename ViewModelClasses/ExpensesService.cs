using ProductServicesSolution;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PriceCalculatorSolution
{
    public class ExpensesService
    {
        public static List<Expense> Expenses { get; set; }

        public ExpensesService(List<Expense> expenses)
        {
            Expenses = expenses;
        }

        //print each expense 
        public static string PrintExpenses()
        {
            StringBuilder sb = new StringBuilder(1024);
            foreach (Expense e in Expenses)
            {
                sb.Append(e.Description + " cost: " + e.Amount);
            }
            return sb.ToString();
        }

        //calculate expenses 
        public static decimal CalculateTotalExpenses(decimal basicPrice)
        {
            decimal total = 0;
            foreach (Expense e in Expenses)
            {
                if (e.isAmountPercentage)
                {
                    total += ProductServiceModel.PercentageToAbsolute(basicPrice, e.Amount);
                }
                else
                {
                    total += e.Amount;
                }
                e.ToString();
            }
            return total;
        }

    }
}
