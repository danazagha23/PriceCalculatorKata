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
        public bool isTaxPrecedence { get; set; }
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
        decimal tax, universalDiscount, upcDiscount, totalPrice;
        public void CalculateTotalPrice(Product p)
        {
            if (isTaxPrecedence)
            {
                tax = CalculateTax(p.Price);
                universalDiscount = CalculateUniversalDiscount(p.Price);
                upcDiscount = CalculateUPCDiscount(p.Price, p.UPC);
            }
            else
            {
                upcDiscount = CalculateUPCDiscount(p.Price, p.UPC);
                tax = CalculateTax(p.Price - upcDiscount);
                universalDiscount = CalculateUniversalDiscount(p.Price - upcDiscount);
            }

            decimal additionalCosts = tax;
            decimal discounts = upcDiscount + universalDiscount;

            totalPrice = p.Price + additionalCosts - discounts;
        }

        public string GetResultText()
        {
            StringBuilder sb = new StringBuilder(1024);
            foreach (var product in Products)
            {
                CalculateTotalPrice(product);
                sb.Append(product.Name);
                sb.AppendLine($"  UPC: {product.UPC}");
                sb.AppendLine($"  Tax: {TaxPercentage}");
                sb.AppendLine($"   Universal Discount: {DiscountPercentage}");
                sb.AppendLine($"   UPC Discount: {UpcCodeDiscounts[product.UPC]}");
                
                sb.AppendLine($"  Tax Amount: {tax}");
                sb.AppendLine($"   UPC Discount Amount: {upcDiscount}");
                sb.AppendLine($"   Universal Discount Amount: {universalDiscount}");
                
                sb.AppendLine($"   Price: {product.Price}");
                sb.AppendLine($"   Total Price: {totalPrice:c}");

                sb.AppendLine($"---------------");
            }
            return sb.ToString();
        }
    }
}