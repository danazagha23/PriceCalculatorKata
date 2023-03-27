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
        decimal discount;
        public decimal TotalPrice
        {
            get
            {
                return BasicPrice + tax - discount;
            }
        }
        public Product(string name, int upc, decimal price)
        {
            Name = name;
            UPC = upc;
            BasicPrice = price;
            tax = PriceCalculations.ProductTax(BasicPrice);
            discount = PriceCalculations.ProductDiscount(BasicPrice);
        }
        public string DisplayProductPrice()
        {
            return $"Tax = {PriceCalculations.TaxPercentage} %, discount = {PriceCalculations.DiscountPercentage} % Tax amount = ${tax}; Discount amount = ${discount} Price before = ${BasicPrice}, price after = ${TotalPrice}";
        }
    }
}
