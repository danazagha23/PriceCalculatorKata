using System;
using CostSolution;
using DiscountSolution;
using PriceCalculatorSolution;
using ProductSolution;

namespace KataProgram
{
    public class Program
    {
        static void Main(string[] args) 
        {
            Console.WriteLine("Enter product name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter product upc:");
            int upc = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter product price:");
            decimal price = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Enter tax percentage:");
            int tax = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter universal discount percentage:");
            int universalDiscount = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Apply tax before discounts calculation:[Yes,No]");
            bool taxPriority = Console.ReadLine().Equals("Yes") ? true : false;

            Console.WriteLine("Is there any additional cost?:[Yes,No]");
            bool additionalCosts = Console.ReadLine().Equals("Yes") ? true : false;
            decimal packagingAmount = 0;
            decimal transportAmount = 0;
            if (additionalCosts)
            {
                Console.WriteLine("Define packaging cost by percentage or an absolute value:[percentage, absolute]");
                bool percentagePackagingCost = Console.ReadLine().Equals("percentage") ? true : false;

                Console.WriteLine("Enter Packaging Cost:");
                decimal packagingCost = Convert.ToDecimal(Console.ReadLine());
                if (percentagePackagingCost) packagingAmount = PriceCalculations.PercentageToAbsolute(price, packagingCost);
                else packagingAmount = packagingCost;

                Console.WriteLine("Define transport cost by percentage or an absolute value:[percentage, absolute]");
                bool percentageTransportCost = Console.ReadLine().Equals("percentage") ? true : false;

                Console.WriteLine("Enter transport Cost:");
                decimal transportCost = Convert.ToDecimal(Console.ReadLine());
                if (percentageTransportCost) transportAmount = PriceCalculations.PercentageToAbsolute(price, transportCost);
                else transportAmount = transportCost;
            }

            var upcSpecialCodes = new Dictionary<int, int>()
            {
                {12345, 7},
                {789, 7 }
            };

            AdditionalCosts costs = new AdditionalCosts(tax, packagingAmount, transportAmount);
            Discounts discounts = new Discounts(universalDiscount, upcSpecialCodes);
            PriceCalculations calc = new PriceCalculations(taxPriority);
            Product product = new Product(name, upc, price);


            product.DisplayProductPrice();
        }
    }
}