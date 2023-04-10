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
        decimal tax, expenses, universalDiscount, upcDiscount, discounts, cap, totalPrice;
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
                cap = DiscountService.CalculateCapAmount(p.Price);
            }
            else
            {
                upcDiscount = DiscountService.CalculateUPCDiscount(p.Price, p.UPC);
                tax = TaxService.CalculateTax(p.Price - upcDiscount);
                universalDiscount = DiscountService.CalculateUniversalDiscount(p.Price - upcDiscount);
                expenses = ExpensesService.CalculateTotalExpenses(p.Price - upcDiscount);
                cap = DiscountService.CalculateCapAmount(p.Price);
            }

            decimal additionalCosts = tax + expenses;
            discounts = cap > (universalDiscount + upcDiscount) ? universalDiscount + upcDiscount : cap;

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
                sb.AppendLine($"for UPC = {product.UPC} ");
                sb.Append($"{DiscountService.DiscountMethod} discounts, ");
                sb.AppendLine($"cap: {cap}");
                sb.AppendLine(ExpensesService.PrintExpenses());

                sb.AppendLine($"Tax Amount: {tax}$, ");
                sb.Append($"UPC Discount Amount: {upcDiscount}$, ");
                sb.AppendLine($"Universal Discount Amount: {universalDiscount}$, ");
                sb.AppendLine($"Discounts: {discounts}$");
                
                sb.AppendLine($"   Price: {product.Price}$");
                sb.AppendLine($"   Total Price: {totalPrice:c}$");

                sb.AppendLine($"---------------");
            }
            return sb.ToString();
        }
    }
}