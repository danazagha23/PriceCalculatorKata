using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculatorSolution
{
    public class Product
    {
        public string Name { get; set; }
        public int UPC { get; set; }
        public decimal BasicPrice { get; set; }
        public decimal PriceWithTax 
        { 
            get
            {
                decimal tax = (decimal.Round(BasicPrice ,2, MidpointRounding.AwayFromZero) * 20) / 100;
                return BasicPrice + tax;
            }
        }    
        public Product(string name, int upc, decimal priceWithPrice) 
        {
            Name = name;
            UPC = upc;
            BasicPrice = priceWithPrice;
        }
        public string DisplayProductPrice()
        {
            return $"Product price reported as ${BasicPrice} before tax and ${PriceWithTax} after 20% tax.";
        }
    }
}
