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
            return decimal.Round(_basicPrice * _percentage / 100, 4, MidpointRounding.AwayFromZero);
        }

        //calculate total Price, costs and discounts separately
        decimal tax, expenses, universalDiscount, upcDiscount, discounts, cap, totalPrice;
        public void CalculateTotalPrice(Product p)
        {
            if (TaxService.isTaxPrecedence)
            {
                tax = TaxService.CalculateTax(CurrencyService.ConvertCurrency(p.Price));
                universalDiscount = DiscountService.CalculateUniversalDiscount(CurrencyService.ConvertCurrency(p.Price));
                expenses = ExpensesService.CalculateTotalExpenses(CurrencyService.ConvertCurrency(p.Price));
                if (DiscountService.DiscountMethod.Equals("additive"))
                {
                    upcDiscount = DiscountService.CalculateUPCDiscount(CurrencyService.ConvertCurrency(p.Price), p.UPC);
                }
                else
                {
                    upcDiscount = DiscountService.CalculateUPCDiscount(CurrencyService.ConvertCurrency(p.Price) - universalDiscount, p.UPC);
                }
                cap = DiscountService.CalculateCapAmount(CurrencyService.ConvertCurrency(p.Price));
            }
            else
            {
                upcDiscount = DiscountService.CalculateUPCDiscount(CurrencyService.ConvertCurrency(p.Price), p.UPC);
                tax = TaxService.CalculateTax(CurrencyService.ConvertCurrency(p.Price) - upcDiscount);
                universalDiscount = DiscountService.CalculateUniversalDiscount(CurrencyService.ConvertCurrency(p.Price) - upcDiscount);
                expenses = ExpensesService.CalculateTotalExpenses(CurrencyService.ConvertCurrency(p.Price) - upcDiscount);
                cap = DiscountService.CalculateCapAmount(CurrencyService.ConvertCurrency(p.Price));
            }

            decimal additionalCosts = tax + expenses;
            discounts = cap > (universalDiscount + upcDiscount) ? universalDiscount + upcDiscount : cap;

            totalPrice = CurrencyService.ConvertCurrency(p.Price) + additionalCosts - discounts;
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
                sb.AppendLine($"cap: {cap.ToTwoDecimalDigits()} ");
                sb.AppendLine(ExpensesService.PrintExpenses());

                sb.AppendLine($"Tax Amount: {tax.ToTwoDecimalDigits()} {CurrencyService.Currency}, ");
                sb.Append($"UPC Discount Amount: {upcDiscount.ToTwoDecimalDigits()} {CurrencyService.Currency}, ");
                sb.AppendLine($"Universal Discount Amount: {universalDiscount.ToTwoDecimalDigits()} {CurrencyService.Currency}, ");
                sb.AppendLine($"Discounts: {discounts.ToTwoDecimalDigits()} {CurrencyService.Currency}");
                
                sb.AppendLine($"   Price: {CurrencyService.ConvertCurrency(product.Price)} {CurrencyService.Currency}");
                sb.AppendLine($"   Total Price: {totalPrice.ToTwoDecimalDigits()} {CurrencyService.Currency}");

                sb.AppendLine($"---------------");
            }
            return sb.ToString();
        }
    }
}