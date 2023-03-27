using System;
using PriceCalculatorSolution;

namespace KataProgram
{
    public class Program
    {
        static void Main(string[] args) 
        {
            Console.WriteLine("Enter tax percentage:");
            int tax = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter discount percentage:");
            int discount = Convert.ToInt32(Console.ReadLine());

            PriceCalculations calc = new PriceCalculations(tax, discount);
            Product product = new Product("Book", 12345, 20.25M);

            Console.WriteLine(product.DisplayProductPrice());
        }
    }
}