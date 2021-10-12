using System;
using System.Collections.Generic;
using System.Text;

namespace BFYOC.Function
{
    public class Product
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }

        public override string ToString()
        {
            return $"The product name for your product id {ProductId} is {ProductName}";
        }
    }
}
