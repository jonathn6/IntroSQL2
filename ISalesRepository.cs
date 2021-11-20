using System;
using System.Collections.Generic;
using System.Text;

namespace IntroSQL
{
    public interface ISalesRepository
    {
        public IEnumerable<Sales> GetAllSales();
    }
}
