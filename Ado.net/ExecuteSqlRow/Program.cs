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
            var Connection = new SqlConnection(constr);

            var sql = "SELECT * FROM Customers";

            var command=new SqlCommand(sql, Connection);
            command.CommandType=CommandType.Text;
            Connection.Open();

            var reader = command.ExecuteReader();   

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.WriteLine(reader.GetName(i) + ": " + reader.GetValue(i));
                }
                Console.WriteLine("--------------------");


                //var customer = new Cusotmer
                //{
                //    Id = reader.GetInt32("id"),
                //    Name = reader.GetString("name"),
                //    Balance = reader.GetDecimal("balance")
                //};
                //Console.WriteLine(customer);
            }
            Connection.Close();
        }
    }


    public class Cusotmer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }

        public override string ToString()
        {
            return $"Id: { Id}, Name: { Name}, Balance: { Balance}";
        }
    }
}
