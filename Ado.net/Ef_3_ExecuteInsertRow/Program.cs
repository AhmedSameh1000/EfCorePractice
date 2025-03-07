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

            var sql = "insert into customers values (@name,@balance)";
            var nameparams = new SqlParameter()
            {
                ParameterName= "@name",
                SqlDbType = SqlDbType.VarChar,
                Direction=ParameterDirection.Input,
                Value="Sayed"
            };
            var balaceparams = new SqlParameter()
            {
                ParameterName = "@balance",
                SqlDbType = SqlDbType.Decimal,
                Direction = ParameterDirection.Input,
                Value = 10.3
            };
            var command = new SqlCommand(sql, Connection);
            command.Parameters.Add(nameparams);
            command.Parameters.Add(balaceparams);
            command.CommandType = CommandType.Text;
            Connection.Open();

            if (command.ExecuteNonQuery() > 0)
                Console.WriteLine("Done");
            else
                Console.WriteLine("Error");
                Connection.Close();

            Console.ReadKey();


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
