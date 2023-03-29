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
        public decimal BasicPrice { get; set; }
        public string Currency { get; set; }

        decimal tax = 0;
        decimal universalDiscount = 0;
        decimal upcDiscount = 0;
        decimal packagingCost = AdditionalCosts.PackagingAmount;
        decimal transportCost = AdditionalCosts.TransportAmount;
        bool taxPriority;


        public Product(string name, int upc, decimal price, string currency)
        {
            Name = name;
            UPC = upc;
            BasicPrice = price;
            Currency = currency;
            taxPriority = PriceCalculations.TaxPriority;
            
            if (taxPriority)
            {
                tax = AdditionalCosts.ProductTax(BasicPrice);
                universalDiscount = Discounts.UniversalProductDiscount(BasicPrice);
                if (Discounts.DiscountMethod.Equals("additive"))
                {
                    upcDiscount = Discounts.UPCProductDiscount(BasicPrice, UPC);
                } 
                else
                {
                    upcDiscount = Discounts.UPCProductDiscount(BasicPrice - universalDiscount, UPC);
                }
            }
            else
            {
                upcDiscount = Discounts.UPCProductDiscount(BasicPrice, UPC);
                tax = AdditionalCosts.ProductTax(BasicPrice - upcDiscount);
                universalDiscount = Discounts.UniversalProductDiscount(BasicPrice - upcDiscount);
            }
        }
        public void DisplayProductPrice()
        {
            Console.WriteLine($"Cost = {BasicPrice.ToTwoDecimalDigits()} " + Currency);
            Console.WriteLine($"Tax = {tax.ToTwoDecimalDigits()} " + Currency);
            decimal discount = Discounts.capAmount > (universalDiscount + upcDiscount) ? universalDiscount + upcDiscount : Discounts.capAmount;
            Console.WriteLine($"Discounts: {discount.ToTwoDecimalDigits()} " + Currency);
            Console.WriteLine($"Packaging: {packagingCost.ToTwoDecimalDigits()} " + Currency);
            Console.WriteLine($"Transport: {transportCost.ToTwoDecimalDigits()} " + Currency);
            Console.WriteLine($"Total = {TotalPrice().ToTwoDecimalDigits()} " + Currency);
        }
        public decimal TotalPrice()
        {
            return BasicPrice + tax + packagingCost + transportCost - universalDiscount - upcDiscount;    
        }
    }
}
