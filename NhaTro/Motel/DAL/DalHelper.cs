using Common;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class DalHelper
    {
        public static string ConnectionString => GlobalConfiguration.SQLConnectionString;

        #region Get Connection
        public static IDbConnection GetConnection(string connectionString = "")
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = ConnectionString;
            }

            return new SqlConnection(connectionString);
        }
        #endregion

        #region Relationship Sql
        #region Excute text
        public static async Task<IEnumerable<T>> Query<T>(string sql, DynamicParameters param = null, IDbTransaction dbTransaction = null, IDbConnection connection = null)
        {
            if (connection != null)
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                param = param ?? new DynamicParameters();
                return await connection.QueryAsync<T>(sql, param, dbTransaction);
            }
            else
            {
                using (IDbConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    param = param ?? new DynamicParameters();
                    return await conn.QueryAsync<T>(sql, param);
                }
            }
        }

        public static async Task<IEnumerable<T>> ExecuteQuery<T>(string sql, DynamicParameters param = null, IDbTransaction dbTransaction = null, IDbConnection connection = null)
        {
            if (connection != null)
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                param = param ?? new DynamicParameters();
                return await connection.QueryAsync<T>(sql, param, commandType: CommandType.Text, transaction: dbTransaction);
            }
            else
            {
                using (IDbConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    param = param ?? new DynamicParameters();
                    return await conn.QueryAsync<T>(sql, param, commandType: CommandType.Text);
                }
            }
        }

        public static async Task<T> ExecuteScadar<T>(string sql, DynamicParameters param = null, IDbTransaction dbTransaction = null, IDbConnection connection = null)
        {
            if (connection != null)
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                param = param ?? new DynamicParameters();
                return await connection.ExecuteScalarAsync<T>(sql, param, commandType: CommandType.Text, transaction: dbTransaction);
            }
            else
            {
                using (IDbConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    param = param ?? new DynamicParameters();
                    return await conn.ExecuteScalarAsync<T>(sql, param, commandType: CommandType.Text);
                }
            }
        }

        public static async Task<int> Execute(string sql, DynamicParameters param = null, IDbTransaction dbTransaction = null, IDbConnection connection = null)
        {
            if (connection != null)
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                param = param ?? new DynamicParameters();
                return await connection.ExecuteAsync(sql, param, commandType: CommandType.Text, transaction: dbTransaction);
            }
            else
            {
                using (IDbConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    param = param ?? new DynamicParameters();
                    return await conn.ExecuteAsync(sql, param, commandType: CommandType.Text);
                }
            }
        }

        public static async Task<IDictionary<string, object>> ReturnExecute(string sql, string[] outParamsName, DynamicParameters param = null, IDbTransaction dbTransaction = null, IDbConnection connection = null)
        {
            if (connection != null)
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                param = param ?? new DynamicParameters();
                await connection.ExecuteAsync(sql, param, commandType: CommandType.Text, transaction: dbTransaction);

                Dictionary<string, object> result = new Dictionary<string, object>();
                foreach (var item in outParamsName)
                {
                    result.Add(item, param.Get<object>(item));
                }

                return result;
            }
            else
            {
                using (IDbConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    param = param ?? new DynamicParameters();
                    await conn.ExecuteAsync(sql, param, commandType: CommandType.Text);

                    Dictionary<string, object> result = new Dictionary<string, object>();
                    foreach (var item in outParamsName)
                    {
                        result.Add(item, param.Get<object>(item));
                    }

                    return result;
                }
            }
        }


        #endregion

        #region Excute SP
        public static async Task<IEnumerable<T>> SPExecuteQuery<T>(string sql, DynamicParameters param = null, IDbConnection connection = null)
        {
            if (connection != null)
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                param = param ?? new DynamicParameters();
                return await connection.QueryAsync<T>(sql, param, commandType: CommandType.StoredProcedure);
            }
            else
            {
                using (IDbConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    param = param ?? new DynamicParameters();
                    return await conn.QueryAsync<T>(sql, param, commandType: CommandType.StoredProcedure);
                }
            }
        }

        public static async Task<T> SPExecuteScadar<T>(string sql, DynamicParameters param = null, IDbTransaction dbTransaction = null, IDbConnection connection = null)
        {
            if (connection != null)
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                param = param ?? new DynamicParameters();
                return await connection.ExecuteScalarAsync<T>(sql, param, commandType: CommandType.StoredProcedure, transaction: dbTransaction);
            }
            else
            {
                using (IDbConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    param = param ?? new DynamicParameters();
                    return await conn.ExecuteScalarAsync<T>(sql, param, commandType: CommandType.StoredProcedure);
                }
            }
        }

        public static async Task<int> SPExecute(string sql, DynamicParameters param = null, IDbTransaction dbTransaction = null, IDbConnection connection = null)
        {
            if (connection != null)
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                param = param ?? new DynamicParameters();
                return await connection.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure, transaction: dbTransaction);
            }
            else
            {
                using (IDbConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    param = param ?? new DynamicParameters();
                    return await conn.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure);
                }
            }
        }

        public static async Task<IDictionary<string, object>> SPReturnExecute(string sql, string[] outParamsName, DynamicParameters param = null, IDbTransaction dbTransaction = null, IDbConnection connection = null)
        {
            if (connection != null)
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                param = param ?? new DynamicParameters();
                await connection.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure, transaction: dbTransaction);

                Dictionary<string, object> result = new Dictionary<string, object>();
                foreach (var item in outParamsName)
                {
                    result.Add(item, param.Get<object>(item));
                }

                return result;
            }
            else
            {
                using (IDbConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    param = param ?? new DynamicParameters();
                    await conn.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure);

                    Dictionary<string, object> result = new Dictionary<string, object>();
                    foreach (var item in outParamsName)
                    {
                        result.Add(item, param.Get<object>(item));
                    }

                    return result;
                }
            }
        }
        #endregion
        #endregion

    }
}
