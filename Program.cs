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
            string productName = "The Little Prince";
            int productUpc = 12345;
            decimal productPrice = 20.25M;

            Console.WriteLine("Tax =");
            int tax = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Discount =");
            int universalDiscount = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Additive or Multiplicative discount =");
            string discountMethod = Console.ReadLine();

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
                if (percentagePackagingCost) packagingAmount = PriceCalculations.PercentageToAbsolute(productPrice, packagingCost);
                else packagingAmount = packagingCost;

                Console.WriteLine("Define transport cost by percentage or an absolute value:[percentage, absolute]");
                bool percentageTransportCost = Console.ReadLine().Equals("percentage") ? true : false;

                Console.WriteLine("Enter transport Cost:");
                decimal transportCost = Convert.ToDecimal(Console.ReadLine());
                if (percentageTransportCost) transportAmount = PriceCalculations.PercentageToAbsolute(productPrice, transportCost);
                else transportAmount = transportCost;
            }

            var upcSpecialCodes = new Dictionary<int, int>()
            {
                {12345, 7},
                {789, 7 }
            };

            AdditionalCosts costs = new AdditionalCosts(tax, packagingAmount, transportAmount);
            Discounts discounts = new Discounts(discountMethod, universalDiscount, upcSpecialCodes);
            PriceCalculations calc = new PriceCalculations(taxPriority);
            Product product = new Product(productName, productUpc, productPrice);


            product.DisplayProductPrice();
        }
    }
}