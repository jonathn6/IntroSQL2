using System;
using System.Collections.Generic;
using System.Text;

namespace IntroSQL
{
    public interface IEmployeesRepository
    {
        public IEnumerable<Employees> GetAllEmployee();
    }
}
