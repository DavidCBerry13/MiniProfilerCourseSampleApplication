using System;
using StackExchange.Profiling.Storage;

namespace MiniProfiler.SqlServerCreateScript
{
    class Program
    {
        static void Main(string[] args)
        {
            //No connection string needed because we just want the scripts
            SqlServerStorage sqlServerStorage = new SqlServerStorage("");
            var sqlScripts = sqlServerStorage.TableCreationScripts;

            sqlScripts.ForEach(sql => Console.WriteLine(sql));
            Console.ReadLine();
        }
    }
}
