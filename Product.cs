using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscountSolution;
using CostSolution;
using PriceCalculatorSolution;

namespace ProductSolution
{
    public class Product
    {
        public string Name { get; set; }
        public int UPC { get; set; }
        public static decimal BasicPrice { get; set; }

        decimal tax;
        decimal universalDiscount;
        decimal upcDiscount;
        bool taxPriority;

        public Product(string name, int upc, decimal price)
        {
            Name = name;
            UPC = upc;
            BasicPrice = price;
        }
        public void DisplayProductPrice()
        {
            Console.WriteLine($"Price = {TotalPrice()}");
            Console.WriteLine($"Total discount amount: {universalDiscount + upcDiscount}");
        }
        public decimal TotalPrice()
        {
            taxPriority = PriceCalculations.TaxPriority;
            upcDiscount = Discounts.UPCProductDiscount(BasicPrice, UPC);
            if (taxPriority)
            {
                tax = AdditionalCosts.ProductTax(BasicPrice);
                universalDiscount = Discounts.UniversalProductDiscount(BasicPrice);
                return BasicPrice + tax - universalDiscount - upcDiscount;    
            }
            else
            {
                tax = AdditionalCosts.ProductTax(BasicPrice - upcDiscount);
                universalDiscount = Discounts.UniversalProductDiscount(BasicPrice - upcDiscount);
                return BasicPrice + tax - universalDiscount - upcDiscount;
            }
        }
    }
}
