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

        decimal tax;
        decimal universalDiscount;
        decimal upcDiscount;
        decimal packagingCost = AdditionalCosts.PackagingAmount;
        decimal transportCost = AdditionalCosts.TransportAmount;
        bool taxPriority;


        public Product(string name, int upc, decimal price)
        {
            Name = name;
            UPC = upc;
            BasicPrice = price;
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
            Console.WriteLine($"Cost = {BasicPrice}");
            Console.WriteLine($"Tax = {tax}");
            decimal discount = Discounts.capAmount > (universalDiscount + upcDiscount) ? universalDiscount + upcDiscount : Discounts.capAmount;
            Console.WriteLine($"Discounts: {discount}");
            Console.WriteLine($"Packaging: {packagingCost}");
            Console.WriteLine($"Transport: {transportCost}");
            Console.WriteLine($"Total = {TotalPrice()}");
        }
        public decimal TotalPrice()
        {
            return BasicPrice + tax + packagingCost + transportCost - universalDiscount - upcDiscount;    
        }
    }
}
