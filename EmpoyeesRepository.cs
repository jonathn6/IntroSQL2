using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IntroSQL
{
    public class EmpoyeesRepository : IEmployeesRepository
    {
        private readonly IDbConnection _connection;

        //Constructor
        public EmpoyeesRepository(IDbConnection connection)
        {
            _connection = connection;
        }
 
        public IEnumerable<Employees> GetAllEmployee()
        {
            return _connection.Query<Employees>("SELECT * FROM employees;");
        }
    }
}
