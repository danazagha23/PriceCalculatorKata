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
        public string ResultText { get; set; }

        //convert percentage to absolute value
        public static decimal PercentageToAbsolute(decimal _basicPrice, decimal _percentage)
        {
            return decimal.Round(_basicPrice * _percentage / 100, 2, MidpointRounding.AwayFromZero);
        }

        //calculate total Price and each cost and discount separately
        decimal tax, expenses, universalDiscount, upcDiscount, totalPrice;
        public void CalculateTotalPrice(Product p)
        {
            if (TaxService.isTaxPrecedence)
            {
                tax = TaxService.CalculateTax(p.Price);
                universalDiscount = DiscountService.CalculateUniversalDiscount(p.Price);
                expenses = ExpensesService.CalculateTotalExpenses(p.Price);
                if (DiscountService.DiscountMethod.Equals("additive"))
                {
                    upcDiscount = DiscountService.CalculateUPCDiscount(p.Price, p.UPC);
                }
                else
                {
                    upcDiscount = DiscountService.CalculateUPCDiscount(p.Price - universalDiscount, p.UPC);
                }
            }
            else
            {
                upcDiscount = DiscountService.CalculateUPCDiscount(p.Price, p.UPC);
                tax = TaxService.CalculateTax(p.Price - upcDiscount);
                universalDiscount = DiscountService.CalculateUniversalDiscount(p.Price - upcDiscount);
                expenses = ExpensesService.CalculateTotalExpenses(p.Price - upcDiscount);
            }

            decimal additionalCosts = tax + expenses;
            decimal discounts = upcDiscount + universalDiscount;

            totalPrice = p.Price + additionalCosts - discounts;
        }

        //print result
        public string GetResultText()
        {
            StringBuilder sb = new StringBuilder(1024);
            foreach (var product in Products)
            {
                CalculateTotalPrice(product);
                sb.AppendLine(product.Name);

                sb.Append($"Tax: {TaxService.TaxPercentage} %, ");
                sb.Append($"Discount: {DiscountService.UniversalDiscountPercentage} %, ");
                if (DiscountService.UpcCodeDiscounts.ContainsKey(product.UPC))
                {
                    sb.AppendLine($"UPC Discount: {DiscountService.UpcCodeDiscounts[product.UPC]} % ");
                }
                sb.Append($"for UPC = {product.UPC} ");
                sb.AppendLine(ExpensesService.PrintExpenses());

                sb.AppendLine($"Tax Amount: {tax}$, ");
                sb.Append($"UPC Discount Amount: {upcDiscount}$, ");
                sb.Append($"Universal Discount Amount: {universalDiscount}$, ");
                sb.AppendLine($"Discounts: {universalDiscount + upcDiscount}$");
                
                sb.AppendLine($"   Price: {product.Price}$");
                sb.AppendLine($"   Total Price: {totalPrice:c}$");

                sb.AppendLine($"---------------");
            }
            return sb.ToString();
        }
    }
}