using System;
using System.Collections.Generic;
using System.Text;

namespace BFYOC.Function
{
    public interface IProductStore
    {
        Product GetProduct(string productId);
    }
}
