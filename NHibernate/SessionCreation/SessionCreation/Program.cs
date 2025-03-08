using Microsoft.Extensions.Configuration;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ExecuteSqlRow
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var customer=GetCustomerById(1);
            var customers= GetAllCustomers();


        }
        public static bool CreateCustomer(string name, decimal balance)
        {
            using (var session = CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var customer = new Customer { Name = name, Balance = balance };
                    session.Save(customer);
                    transaction.Commit();
                    return true;
                }
            }
        }
        public static bool DeleteCustomer(int id)
        {
            using (var session = CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var customer = session.Query<Customer>().FirstOrDefault(c => c.Id == id);

                    if (customer is null)
                    {
                        return false;
                    }

                    session.Delete(customer);
                    transaction.Commit();
                    return true;
                }
            }
        }

        public static Customer GetCustomerById(int id)
        {
            using (var session = CreateSession())
            {
                return session.Query<Customer>().FirstOrDefault(c => c.Id == id);
            }
        }

        public static List<Customer> GetAllCustomers()
        {
            using (var session = CreateSession())
            {
                return session.Query<Customer>().ToList();
                //var customersWithOrders = session.Query<Customer>()
                //    .Fetch(c => c.Orders)  // جلب البيانات المرتبطة بالعميل
                //    .ToList();

                //var query = session.QueryOver<Customer>()
                //.JoinQueryOver<Order>(c => c.Orders) // INNER JOIN
                //.Where(o => o.TotalAmount > 5000)//LEFT JOIN
                //.List();



            }
        }

        public static bool UpdateCustomer(int id, string name, decimal balance)
        {
            using (var session = CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var customer = session.Query<Customer>().FirstOrDefault(c => c.Id == id);

                    if (customer is null)
                    {
                        return false;
                    }

                    customer.Name = name;
                    customer.Balance = balance;
                    session.Update(customer);
                    transaction.Commit();
                    return true;
                }
            }
        }
        private static ISession CreateSession()
        {
            var Configuration = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json")
                 .Build();
            var conStr = Configuration.GetSection("constr").Value;

            var mapper = new ModelMapper();

            mapper.AddMappings(typeof(Customer).Assembly.ExportedTypes);

            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();


            var hbConfig=new NHibernate.Cfg.Configuration();

            hbConfig.DataBaseIntegration(db =>
            {
                db.Driver<MicrosoftDataSqlClientDriver>();
                db.Dialect<NHibernate.Dialect.MsSql2012Dialect>();
                db.ConnectionString = conStr;
                db.LogSqlInConsole = true;
            });
            hbConfig.AddMapping(mapping);
            var sessionFactory = hbConfig.BuildSessionFactory();

            return sessionFactory.OpenSession();
        }
    }
}

