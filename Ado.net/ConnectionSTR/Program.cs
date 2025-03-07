using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Text.Json;

namespace ConnectionSTR
{
    internal class Program
    {
        static void Main(string[] args)
        {
           var Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var appset=File.ReadAllText("appsettings.json");
            var result= JsonSerializer.Deserialize<data>(appset);
           
            Console.WriteLine(result.constr);
            Console.WriteLine(Configuration.GetSection("constr").Value);




        }
    }
}
public class data {

public string constr { get; set; }
}
