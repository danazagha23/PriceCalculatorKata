﻿using System;
using PriceCalculatorSolution;
using ProductServicesSolution;

namespace PriceCalculatorSolution
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter tax percentage:");
            int tax = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter discount percentage:");
            int discount = Convert.ToInt32(Console.ReadLine());

            ProductServiceModel pm = new ProductServiceModel
            {
                TaxPercentage = tax,
                DiscountPercentage = discount
            };

            Console.WriteLine(pm.GetResultText());
        }
    }
}