using _1_ReverseEnginering.Data;

namespace _1_ReverseEnginering
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new appcontext();

            var Orders = context.OrderDetails.ToList();

            foreach (var order in Orders)
            {
                Console.WriteLine(order);

            }

            Console.WriteLine("Hello, World!");
        }
    }
}
//Download Sql Server
//Download Tools
//Download Design

//open package manager console
//scaffold-dbcontext 'Data Source=DESKTOP-Q18AF5P\SQLEXPRESS;Initial Catalog=db9253;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False'
//Microsoft.EntityFrameworkCore.SqlServer -context appcontext -contextDir Data -OutputDir Models