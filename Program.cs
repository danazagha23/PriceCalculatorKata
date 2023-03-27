using System;
using PriceCalculatorSolution;

namespace KataProgram
{
    public class Program
    {
        static void Main(string[] args) 
        {
            Tax tax = new Tax(20);
            Product product = new Product("Book", 12345, 20.25M);

            Console.WriteLine(product.DisplayProductPrice());
        }
    }
}