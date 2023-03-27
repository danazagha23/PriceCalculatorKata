using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculatorSolution
{
    public class Product
    {
        public string Name { get; set; }
        public int UPC { get; set; }
        public static decimal BasicPrice { get; set; }

        decimal tax;
        decimal universalDiscount;
        decimal upcDiscount;
        public decimal TotalPrice
        {
            get
            {
                return BasicPrice + tax - universalDiscount - upcDiscount;
            }
        }
        public Product(string name, int upc, decimal price)
        {
            Name = name;
            UPC = upc;
            BasicPrice = price;
            tax = PriceCalculations.ProductTax(BasicPrice);
            universalDiscount = PriceCalculations.UniversalProductDiscount(BasicPrice);
            upcDiscount = PriceCalculations.UPCProductDiscount(BasicPrice, upc);
        }
        public void DisplayProductPrice()
        {
            Console.WriteLine($"Price = {TotalPrice}");
            Console.WriteLine($"Total discount amount: {universalDiscount + upcDiscount}");
        }
    }
}
