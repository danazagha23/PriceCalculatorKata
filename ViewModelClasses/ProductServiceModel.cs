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
            UpcCodeDiscounts = UPCDiscountRepository.GetAll();
        }

        //properities
        public List<Product> Products { get; set; }
        public Dictionary<int, int> UpcCodeDiscounts { get; set; }
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

        public decimal CalculateUniversalDiscount(decimal basicPrice)
        {
            if (DiscountPercentage.HasValue)
            {
                return decimal.Round(basicPrice * (decimal)DiscountPercentage / 100, 2, MidpointRounding.AwayFromZero);
            }
            return 0;
        }
        public decimal CalculateUPCDiscount(decimal basicPrice, int code)
        {
            if (UpcCodeDiscounts.ContainsKey(code))
            {
                return decimal.Round(basicPrice * UpcCodeDiscounts[code] / 100, 2, MidpointRounding.AwayFromZero);
            }
            return 0;
        }

        public decimal CalculateTotalPrice(Product p)
        {
            decimal additionalCosts = CalculateTax(p.Price);
            decimal discounts = CalculateUniversalDiscount(p.Price) + CalculateUPCDiscount(p.Price, p.UPC);

            return p.Price + additionalCosts - discounts;
        }

        public string GetResultText()
        {
            StringBuilder sb = new StringBuilder(1024);
            foreach (var product in Products)
            {
                sb.Append(product.Name);
                sb.AppendLine($"  UPC: {product.UPC}");
                sb.AppendLine($"  Tax: {TaxPercentage}");
                sb.AppendLine($"   Universal Discount: {DiscountPercentage}");
                sb.AppendLine($"   UPC Discount: {UpcCodeDiscounts[product.UPC]}");
                sb.AppendLine($"  Tax Amount: {CalculateTax(product.Price)}");
                sb.AppendLine($"   Discount Amount: {CalculateUPCDiscount(product.Price, product.UPC)}");
                sb.AppendLine($"   Price: {product.Price}");
                sb.AppendLine($"   Total Price: {CalculateTotalPrice(product):c}");

                sb.AppendLine($"---------------");
            }
            return sb.ToString();
        }
    }
}