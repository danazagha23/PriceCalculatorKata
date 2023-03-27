using System;
using PriceCalculatorSolution;

namespace KataProgram
{
    public class Program
    {
        static void Main(string[] args) 
        {
            Console.WriteLine("Enter tax percentage:");
            int tax = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter universal discount percentage:");
            int universalDiscount = Convert.ToInt32(Console.ReadLine());

            var upcSpecialCodes = new Dictionary<int, int>()
            {
                {12345, 7},
                {789, 7 }
            };

            PriceCalculations calc = new PriceCalculations(tax, universalDiscount, upcSpecialCodes);
            Product product = new Product("Book", 789, 20.25M);

            product.DisplayProductPrice();
        }
    }
}