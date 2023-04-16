using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculatorSolution
{
    public class ProductRepository
    {
        public static List<Product> GetAll()
        {
            return new List<Product>
            {
                  new Product {
                    Name = "The Little Prince",
                    UPC = 12345,
                    Price = 20.25M,
                  },
                  new Product {
                    Name = "The Little Prince 2",
                    UPC = 111,
                    Price = 20.25M,
                  },
                  new Product {
                    Name = "Book2",
                    UPC = 789,
                    Price = 15.33M,
                    },

            };
        }
    }
}