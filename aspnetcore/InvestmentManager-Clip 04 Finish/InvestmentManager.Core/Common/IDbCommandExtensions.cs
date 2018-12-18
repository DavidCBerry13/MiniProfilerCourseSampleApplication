using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace InvestmentManager.Core.Common
{
    public static class IDbCommandExtensions
    {

        public static IDbDataParameter AddParameterWithValue(this IDbCommand command, String name, Object value)
        {
            IDbDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            command.Parameters.Add(parameter);

            return parameter;
        }

    }
}
