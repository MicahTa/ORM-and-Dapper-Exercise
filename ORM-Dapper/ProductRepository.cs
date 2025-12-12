using System.Collections.Generic;
using System.Data;
using Dapper;

namespace BestBuyCRUD
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection SqlConnection;

        public ProductRepository(IDbConnection connection)
        {
            SqlConnection = connection;
        }
        public IEnumerable<Product> GetAllProducts()
        {
            return SqlConnection.Query<Product> ("SELECT * FROM products;");
        }
        public void UpdateProductName(int productID, string updatedName)
        {
            SqlConnection.Execute("UPDATE products SET Name = @updatedName WHERE ProductID = @productID;",
                new { updatedName = updatedName, productID = productID });
        }
        public void CreateProduct(string name, double price, int categoryID)
        {
            SqlConnection.Execute("INSERT INTO products (Name, Price, CategoryID) VALUES (@name, @price, @categoryID);" ,
                new {name = name, price = price, categoryID = categoryID});
        }
        public void DeleteProduct(int productID)
        {
            SqlConnection.Execute("DELETE FROM reviews WHERE ProductID = @productID;",
                new { productID = productID });

            SqlConnection.Execute("DELETE FROM sales WHERE ProductID = @productID;",
               new { productID = productID });

            SqlConnection.Execute("DELETE FROM products WHERE ProductID = @productID;",
               new { productID = productID });
        }
    }
}