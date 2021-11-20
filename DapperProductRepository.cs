using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IntroSQL
{
    class DapperProductRepository : IProductsRepository
    {
        private readonly IDbConnection _connection;

        //Constructor
        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public IEnumerable<Products> GetAllProducts()
        {
            return _connection.Query<Products>("SELECT * FROM Products;");
        }
        public void CreateProduct(string name, double price, int categoryID)
        {
            throw new NotImplementedException();
        }
        public void InsertProduct(string newName, double newPrice, int newCat, bool newOnSale, int newStockLevel)
        {
            _connection.Execute("INSERT INTO products (Name, Price, CategoryID, OnSale, StockLevel) VALUES (@prodName, @prodPrice, @prodCat, @prodOnSale, @prodStockLevel);", new { prodName = newName, prodPrice = newPrice, prodCat = newCat, prodOnSale = newOnSale, prodStockLevel = newStockLevel });

        }
        public bool DeleteProduct(int productID)
        {
            int sqlReturn = 0;
            var sqlDeleteString = "";
            try
            {
                sqlDeleteString = "DELETE sales WHERE ProductID = " + productID;
                sqlReturn = _connection.Execute(sqlDeleteString);
                sqlDeleteString = "DELETE FROM reviews WHERE ProductID = " + productID;
                sqlReturn = _connection.Execute(sqlDeleteString);
                sqlDeleteString = "DELETE FROM products WHERE ProductID = " + productID;
                sqlReturn = _connection.Execute(sqlDeleteString);
                return true;
            } catch
            {
                Exception ex;
                //Console.WriteLine($"ERROR:::{ex.Message}");
                //Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine($"The SQL the caused the error was: {sqlDeleteString}");
                Console.WriteLine();
                Console.WriteLine();
                return false;
            }


        }
        public void UpdateProduct(int productToUpdate, bool updateName, bool updatePrice, bool updateCat, bool updateOnSale, bool updateStockLevel, string newName, double newPrice, int newCat, bool newOnSale, int newStockLevel)
        {
            var sqlValueString = "UPDATE products SET ";
            if (updateName == true)
            {
                sqlValueString = sqlValueString + "Name = '" + newName + "'";
            }
            if (updatePrice == true)
            {
                sqlValueString = sqlValueString + ", Price=" + newPrice;
            }
            if (updateCat == true)
            {
                sqlValueString = sqlValueString + ", CategoryID=" + newCat;
            }
            if (updateOnSale == true)
            {
                sqlValueString = sqlValueString + ", OnSale=" + newOnSale;
            }
            if (updateStockLevel == true)
            {
                sqlValueString = sqlValueString + ", StockLevel=" + newStockLevel;
            }
            sqlValueString = sqlValueString + " WHERE productID = " + productToUpdate;

            _connection.Execute(sqlValueString);

        }
    }
}
