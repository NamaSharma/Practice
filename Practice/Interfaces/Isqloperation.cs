using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ATmV2.Interfaces
{
    public interface Isqloperation
    {
        int ExecuteStoreProcScalar(string procName, SqlParameter[] sqlParameterCollection);
    }
    
}
