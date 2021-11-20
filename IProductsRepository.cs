using System;
using System.Collections.Generic;
using System.Text;

namespace IntroSQL
{
    public interface IProductsRepository
    {
        public IEnumerable<Products> GetAllProducts();
        public void CreateProduct(string name, double price, int categoryID);
    }
}
