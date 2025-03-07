using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ExecuteSqlRow
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var Configuration = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json")
                 .Build();
            var constr = Configuration.GetSection("constr").Value;
            var db = new SqlConnection(constr);
         
            var customer = new Customer { Name = "kahled Doe", Balance = 400 };
         
            var sql= "insert into customers (Name, Balance) values (@Name, @Balance);" +
                "select cast (scope_identity() as int) as id";

            customer.Id=db.Query<int>(sql, new { customer.Name, customer.Balance }).Single();

        }
    }



    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }

        public override string ToString()
        {
            return $"[{Id}], Name: {Name}, Balance: {Balance}";
        }
    }
}
