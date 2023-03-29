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
            Console.WriteLine("Enter product price with currency:");
            string Price = Console.ReadLine();

            string[] priceCurrency = Price.Split(' ');
            decimal productPrice = Convert.ToDecimal(priceCurrency[0]);
            string currency = priceCurrency[1];

            Console.WriteLine("Tax =");
            int tax = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Discount =");
            int universalDiscount = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Cap in [percentage or absolute]");
            bool capInPercentage = Console.ReadLine().Equals("percentage") ? true : false;

            Console.WriteLine("Cap =");
            decimal capAmount = Convert.ToDecimal(Console.ReadLine()); 
            if (capInPercentage)
            {
                capAmount = PriceCalculations.PercentageToAbsolute(productPrice, capAmount);
            }

            Console.WriteLine("Additive or Multiplicative discount =");
            string? discountMethod = Console.ReadLine();

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
            Discounts discounts = new Discounts(capAmount, discountMethod, universalDiscount, upcSpecialCodes);
            PriceCalculations calc = new PriceCalculations(taxPriority);
            Product product = new Product(name, upc, productPrice, currency);


            product.DisplayProductPrice();
        }
    }
}