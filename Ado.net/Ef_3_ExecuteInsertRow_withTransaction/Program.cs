using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;

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
            using var connection = new SqlConnection(constr);

            connection.Open();
            using var transaction = connection.BeginTransaction();
            try
            {
                // التحقق من رصيد العميل قبل الخصم
                var checkBalanceCmd = connection.CreateCommand();
                checkBalanceCmd.Transaction = transaction;
                checkBalanceCmd.CommandText = "SELECT balance FROM Customers WHERE id = 1";

                var currentBalance = (decimal?)checkBalanceCmd.ExecuteScalar() ?? 0;

                if (currentBalance < 1000)
                {
                    throw new Exception("رصيد غير كافي!");
                }

                // تنفيذ عمليات التحديث بعد التحقق
                var command = connection.CreateCommand();
                command.Transaction = transaction;

                command.CommandText = "UPDATE Customers SET balance = balance - 1000 WHERE id = 1";
                command.ExecuteNonQuery();

                command.CommandText = "UPDATE Customers SET balance = balance + 1000 WHERE id = 2";
                command.ExecuteNonQuery();

                transaction.Commit();
                Console.WriteLine("تم التحويل بنجاح.");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine("فشل العملية: " + ex.Message);
            }
        }
    }
}
