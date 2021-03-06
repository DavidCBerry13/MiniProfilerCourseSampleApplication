﻿using StackExchange.Profiling;
using StackExchange.Profiling.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace InvestmentManager.DataAccess.Dapper.Repositories
{
    internal class DapperBaseRepository
    {

        public DapperBaseRepository(String connectionString)
        {
            _connectionString = connectionString;
        }


        private String _connectionString;



        protected IDbConnection GetConnection()
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            var wrapperConnection =
                new ProfiledDbConnection(sqlConnection, MiniProfiler.Current);

            return wrapperConnection;
        }

    }
}
