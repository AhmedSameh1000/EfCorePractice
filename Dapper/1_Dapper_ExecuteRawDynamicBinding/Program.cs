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
            var sql = "SELECT * FROM Customers where name ='New Name'";

            //var resultAsDynamic = db.Query(sql);

            //foreach (var item in resultAsDynamic)
            //{
            //    Console.WriteLine(item);
            //}
            //Console.WriteLine("-------------------------------------------------");

            //var resultAsCustomer = db.Query<Customer>(sql);
            //foreach (var item in resultAsCustomer) {
            //    Console.WriteLine(item);
            //}


            var resultAsCustomer = db.QuerySingle<Customer>(sql);//throw ex when found 2
            var resultAsCustomer1 = db.QueryFirst<Customer>(sql);//not throw ex when found 2 just get first one 
          Console.WriteLine(resultAsCustomer);

        }
    }

     

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }

        public override string ToString()
        {
            return $"[{ Id}], Name: { Name}, Balance: { Balance}";
        }
    }
}
