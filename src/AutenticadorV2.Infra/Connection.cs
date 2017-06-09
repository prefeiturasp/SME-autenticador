using CoreLibrary.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticadorV2.Infra
{
    public class Connection
    {
        public static string GetConnectionString(string connectionName)
        {
            TalkDBTransactionCollection collection = new TalkDBTransactionCollection();
            return collection[connectionName].GetConnection.ConnectionString;
        }
    }
}