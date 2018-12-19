using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace InvestmentManager.Core.Common
{
    public static class IDataReaderExtensions
    {


        public static String GetNullableString(this IDataReader reader, int index)
        {
            return (reader.IsDBNull(index)) ? null : reader.GetString(index);
        }


        public static DateTime? GetNullableDateTime(this IDataReader reader, int index)
        {
            return (reader.IsDBNull(index)) ? (DateTime?)null : reader.GetDateTime(index);
        }
    }
}
