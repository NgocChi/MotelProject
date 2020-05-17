using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Common
{
    public interface IBaseQueries
    {
        
        void JoinTransaction(IDbConnection conn, IDbTransaction trans);
    }
}
