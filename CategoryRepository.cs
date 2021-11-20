using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IntroSQL
{
    class CategoryRepository : ICategoryRepository
    {
        private readonly IDbConnection _connection;

        //Constructor
        public CategoryRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public IEnumerable<Category> GetAllCategory()
        {
            return _connection.Query<Category>("SELECT * FROM categories;");
        }
        public void InsertCategory(string newCatName, int newCatDepartmentID)
        {
            _connection.Execute("INSERT INTO categories (Name, DepartmentID) VALUES (@catName, @catDepartmentID);", new { catName = newCatName, catDepartmentID = newCatDepartmentID });

        }
    }
}
