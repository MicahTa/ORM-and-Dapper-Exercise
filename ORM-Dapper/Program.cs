using System;
using System.Data;
using System.IO;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace BestBuyCRUD
{
    class Program
    {
        static IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

        static string connString = config.GetConnectionString("DefaultConnection");
        static IDbConnection connection = new MySqlConnection(connString);
        static void Main(string[] args) {
            ListProducts();
            DeleteProduct();
            ListProducts();
        }
        public static void ListProducts() {
            var productList = new ProductRepository(connection);
            var products = productList.GetAllProducts();

            foreach (var i in products)
            {
                Console.WriteLine($"{i.ProductID} {i.Name}");
            }
        }
        public static void ListDepartments() {
            var departmentClass = new DepartmentRepository(connection);
            var departments = departmentClass.GetDepartments();

            foreach (var i in departments)
            {
                Console.WriteLine($"{i.DepartmentID} {i.Name}");
            }
        }
        public static void CreateAndListProducts() {
            // Create
            var productList = new ProductRepository(connection);

            Console.WriteLine($"Product Name: ");
            var productName = Console.ReadLine();

            Console.WriteLine($"Product Price: ");
            var price = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine($"Catagory: ");
            var catagory = Convert.ToInt32(Console.ReadLine());

            productList.CreateProduct(productName, price, catagory);

            // List
            ListProducts();
        }
        public static void UpdateProductName() {
            var productList = new ProductRepository(connection);

            Console.WriteLine($"What product do you want to change ID: ");
            var productID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"New name for {productID}: ");
            var newName = Console.ReadLine();

        productList.UpdateProductName(productID, newName);
        }
                public static void DeleteProduct() {
            var productList = new ProductRepository(connection);

            Console.WriteLine($"What product would you like to remove ID: ");
            var productID = Convert.ToInt32(Console.ReadLine());

            productList.DeleteProduct(productID);
        }
        public static void DepartmentUpdate() {
            var departments = new DepartmentRepository(connection);

            Console.WriteLine($"Do you want to update the a department? yes or no: ");

            if (Console.ReadLine().ToUpper() == "YES")
            {
                Console.WriteLine($"Department ID: ");
                var id = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine($"New Department Name: ");
                var newName = Console.ReadLine();

                departments.UpdateDepartment(id, newName);
                var department = departments.GetDepartments();

                foreach (var i in department)
                {
                    Console.WriteLine($"{i.DepartmentID} {i.Name}");
                }
            }
        }

    }
}
