using PriceCalculatorSolution;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProductServicesSolution
{
    public class ProductServiceModel
    {
        //constructor
        public ProductServiceModel() 
        {
            Products = ProductRepository.GetAll();
        }

        //properities
        public List<Product> Products { get; set; }
        public decimal? TaxPercentage { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public string ResultText { get; set; }

        //calculate tax
        public decimal CalculateTax(decimal basicPrice)
        {
            
            if (TaxPercentage.HasValue)
            {
                return decimal.Round(basicPrice * (decimal)TaxPercentage / 100, 2, MidpointRounding.AwayFromZero);
            }
            return 0;
        }

        public decimal CalculateDiscount(decimal basicPrice)
        {
            if (DiscountPercentage.HasValue)
            {
                return decimal.Round(basicPrice * (decimal)DiscountPercentage / 100, 2, MidpointRounding.AwayFromZero);
            }
            return 0;    
        }

        public decimal CalculateTotalPrice(decimal basicPrice)
        {
            decimal additionalCosts = CalculateTax(basicPrice);
            decimal discounts = CalculateDiscount(basicPrice);

            return basicPrice + additionalCosts - discounts;
        }

        public string GetResultText()
        {
            StringBuilder sb = new StringBuilder(1024);
            foreach (var product in Products)
            {
                sb.Append(product.Name);
                sb.AppendLine($"  UPC: {product.UPC}");
                sb.AppendLine($"  Tax: {TaxPercentage}");
                sb.AppendLine($"   Discount: {DiscountPercentage}");
                sb.AppendLine($"  Tax Amount: {CalculateTax(product.Price)}");
                sb.AppendLine($"   Discount Amount: {CalculateDiscount(product.Price)}");
                sb.AppendLine($"   Price: {product.Price}");
                sb.AppendLine($"   Total Price: {CalculateTotalPrice(product.Price):c}");
                
                sb.AppendLine($"---------------");
            }
            return sb.ToString();
        }
    }
}
