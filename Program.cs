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

            Console.WriteLine("Apply tax before discounts calculation:[Yes,No]");
            bool taxPriority = Console.ReadLine().Equals("Yes") ? true : false;

            Console.WriteLine("Is there any additional cost?:[Yes,No]");
            bool additionalCosts = Console.ReadLine().Equals("Yes") ? true : false;
            int expensesPercentage = 0;
            int expensesAmount = 0;
            if (additionalCosts)
            {
                Console.WriteLine("Define packaging cost by percentage or an absolute value:[percentage, absolute]");
                bool percentagePackagingCost = Console.ReadLine().Equals("percentage") ? true : false;

                Console.WriteLine("Enter Packaging Cost:");
                int packagingCost = Convert.ToInt32(Console.ReadLine());
                if (percentagePackagingCost) expensesPercentage += packagingCost;
                else expensesAmount += packagingCost;

                Console.WriteLine("Define packaging cost by percentage or an absolute value:[percentage, absolute]");
                bool percentageTransportCost = Console.ReadLine().Equals("percentage") ? true : false;

                Console.WriteLine("Enter Transport Cost:");
                int transportCost = Convert.ToInt32(Console.ReadLine());
                if (percentageTransportCost) expensesPercentage += transportCost;
                else expensesAmount += transportCost;
            }

            var upcSpecialCodes = new Dictionary<int, int>()
            {
                {12345, 7},
                {789, 7 }
            };

            AdditionalCosts costs = new AdditionalCosts(tax, expensesPercentage, expensesAmount);
            Discounts discounts = new Discounts(universalDiscount, upcSpecialCodes);
            PriceCalculations calc = new PriceCalculations(taxPriority);

            Product product = new Product("Book", 789, 20.25M);

            product.DisplayProductPrice();
        }
    }
}