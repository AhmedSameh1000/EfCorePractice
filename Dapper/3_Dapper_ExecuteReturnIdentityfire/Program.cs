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

            var customer = new Customer { Name = "John Doe", Balance = 1000 };

            var sql = "insert into customers (Name, Balance) values (@Name, @Balance)";

            db.Execute(sql, new { customer.Name, customer.Balance });
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
