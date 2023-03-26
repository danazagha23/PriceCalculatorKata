using System;
using PriceCalculatorSolution;

namespace KataProgram
{
    public class Program
    {
        static void Main(string[] args) 
        {
            Product product = new Product("Book", 12345, 20.25M);

            Console.WriteLine(product.DisplayProductPrice());
        }
    }
}