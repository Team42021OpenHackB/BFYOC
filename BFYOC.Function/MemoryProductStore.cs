using System;
using System.Collections.Generic;
using System.Text;

namespace BFYOC.Function
{
    public class MemoryProductStore : IProductStore
    {

        private Dictionary<string, Product> _productStore;

        public MemoryProductStore()
        {
            _productStore = new Dictionary<string, Product>();
        }

        public Product GetProduct(string productId)
        {
            if (!_productStore.ContainsKey(productId))
            {
                _productStore.Add(productId, new Product { ProductId = productId, ProductName = "Starfruit Explosion" });
            }
            return _productStore[productId];
        }
    }
}
