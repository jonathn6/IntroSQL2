using System;
using System.Collections.Generic;
using System.Text;

namespace IntroSQL
{
    public interface ICategoryRepository
    {        
        public IEnumerable<Category> GetAllCategory();
    }
}
