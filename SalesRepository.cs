using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IntroSQL
{
    public class SalesRepository : ISalesRepository
    {
        private readonly IDbConnection _connection;

        //Constructor
        public SalesRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Sales> GetAllSales()
        {
            return _connection.Query<Sales>("SELECT * FROM sales;");
        }
    }
}
