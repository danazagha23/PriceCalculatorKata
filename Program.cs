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

            ProductServiceModel pm = new ProductServiceModel
            {
                TaxPercentage = tax,
                DiscountPercentage = universalDiscount,
                isTaxPrecedence = isTaxPrecedence,
            };

            Console.WriteLine(pm.GetResultText());
        }
    }
}