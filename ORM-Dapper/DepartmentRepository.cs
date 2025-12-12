using System.Collections.Generic;
using System.Data;
using Dapper;

namespace BestBuyCRUD
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IDbConnection SqlConnection;

        public DepartmentRepository(IDbConnection connection)
        {
            SqlConnection = connection;
        }
        public IEnumerable<Department> GetDepartments()
        {
            return SqlConnection.Query<Department>("SELECT * FROM departments;");
        }
        public void CreateDepartment(string name)
        {
            SqlConnection.Execute("INSERT INTO departments Name Values(@name);", new { name = name });
        }
        public void UpdateDepartment(int id, string newName)
        {
            SqlConnection.Execute("UPDATE departments SET Name = @newName WHERE DepartmentID = @id;", new { newName = newName, id = id });
        }
    }
}
