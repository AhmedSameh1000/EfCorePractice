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

            var ql = "Select * from customers";
            Connection.Open();

            var adaptor = new SqlDataAdapter(ql, Connection);

            var dataTable = new DataTable();
            adaptor.Fill(dataTable);

            Connection.Close();

            foreach (DataRow item in dataTable.Rows)
            {
                var Customer = new Cusotmer()
                {
                    Id = Convert.ToInt32(item["id"]),
                    Name = item["name"]?.ToString(),
                    Balance = Convert.ToDecimal(item["balance"])
                };
                Console.WriteLine(Customer);
            }

            //foreach (DataColumn item in dataTable.Columns)
            //{
            //    Console.WriteLine(item.ToString());
            //}



        }
    }


    public class Cusotmer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Balance: {Balance}";
        }
    }
}
