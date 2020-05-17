using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Common
{
    public abstract class BaseRepository
    {
        private IDbConnection dbConnection;
        public IDbConnection DbConnection
        {
            private set
            {
                dbConnection = value;
            }
            get
            {
                if (dbConnection != null)
                {
                    return dbConnection;
                }

                return null;
            }
        }
        public IDbTransaction DbTransaction { private set; get; }
        public bool IsJoinTransaction { private set; get; }
        public void JoinTransaction(IDbConnection conn, IDbTransaction trans)
        {
            DbConnection = conn;
            DbTransaction = trans;
            IsJoinTransaction = true;
        }
        public void RemoveTransaction()
        {
            DbConnection = null;
            DbTransaction = null;
            IsJoinTransaction = false;
        }
    }
}
