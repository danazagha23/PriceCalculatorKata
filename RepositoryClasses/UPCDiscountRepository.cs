using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculatorSolution
{
    public class UPCDiscountRepository
    {
        public static Dictionary<int, int> GetAll()
        {
            return new Dictionary<int, int>
            {
                {12345, 7},
                {789, 7 }
            };
        }
    }
}
