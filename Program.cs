using System;
using PriceCalculatorSolution;
using ProductServicesSolution;


namespace PriceCalculatorSolution
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter tax percentage:");
            int tax = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter universal discount percentage:");
            int universalDiscount = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Is Tax Precedence ?:[yes,no]");
            bool isTaxPrecedence = Console.ReadLine().Equals("yes") ? true : false;

            Console.WriteLine("Choose discount method: [additive, multiplicative]");
            string discountMethod = Console.ReadLine();


            Console.WriteLine("Is there any Expenses?:[yes,no]");
            bool exsistExpenses = Console.ReadLine().Equals("yes") ? true : false;
            var expenses = new List<Expense>();

            while (exsistExpenses)
            {
                Console.WriteLine("Define Expense description: [Packaging, Transport, ..]");
                string description = Console.ReadLine();

                Console.WriteLine("Define expense by percentage or an absolute value:[percentage, absolute]");
                bool isPercentageAmount = Console.ReadLine().Equals("percentage") ? true : false;

                Console.WriteLine("Enter Expense Amount:");
                decimal amount = Convert.ToDecimal(Console.ReadLine());

                Expense e = new Expense
                {
                    Description = description,
                    Amount = amount,
                    isAmountPercentage = isPercentageAmount,
                };

                expenses.Add(e);

                Console.WriteLine("Is there any additional Expenses?:[yes,no]");
                exsistExpenses = Console.ReadLine().Equals("yes") ? true : false;
            }

            ProductServiceModel pm = new ProductServiceModel();
            
            TaxService ts = new TaxService(tax, isTaxPrecedence);
            DiscountService ds = new DiscountService(universalDiscount, discountMethod);
            ExpensesService es = new ExpensesService(expenses);
            
            Console.WriteLine(pm.GetResultText());

        }
    }
}