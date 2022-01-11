using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetDapperUtilityLibrary.MSSql
{
    public class MSSqlService : IDisposable
    {
        private SqlConnection _SqlConnection { get; set; }
        private SqlTransaction _SqlTransaction { get; set; }

        public MSSqlService(string connectionString)
        {
            try
            {
                _SqlConnection = new SqlConnection(connectionString);
                _SqlConnection.Open();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
            try
            {
                if (_SqlTransaction != null)
                {
                    _SqlTransaction.Commit();
                    _SqlTransaction = null;
                }

                _SqlConnection.Close();
                _SqlConnection = null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void BeginTransaction()
        {
            try
            {
                _SqlTransaction = _SqlConnection.BeginTransaction();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CompleteTransaction(bool isCommit)
        {
            try
            {
                if (isCommit) _SqlTransaction.Commit();
                else _SqlTransaction.Rollback();

                _SqlTransaction = null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Execute(string query, object param, 
            bool isStoredProc = true)
        {
            try
            {
                if (_SqlTransaction == null) BeginTransaction();

                CommandType commandType = isStoredProc ? CommandType.StoredProcedure : CommandType.Text;
                
                var affectedRows = _SqlConnection.Execute(query, param, _SqlTransaction, null, commandType);

                if (affectedRows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                _SqlTransaction.Rollback();
                _SqlTransaction = null;
                throw;
            }
        }

        public async Task<bool> ExecuteAsync(string query, 
            object param, bool isStoredProc = true)
        {
            try
            {
                if (_SqlTransaction == null) BeginTransaction();

                CommandType commandType = isStoredProc ? CommandType.StoredProcedure : CommandType.Text;

                var affectedRows = await _SqlConnection.ExecuteAsync(query, param, _SqlTransaction, null, commandType);

                if (affectedRows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                _SqlTransaction.Rollback();
                _SqlTransaction = null;
                throw;
            }
        }

        public bool ExecuteMany(string query, List<object> param,
            bool isStoredProc = true)
        {
            try
            {
                if (_SqlTransaction == null) BeginTransaction();

                CommandType commandType = isStoredProc ? CommandType.StoredProcedure : CommandType.Text;

                var affectedRows = _SqlConnection.Execute(query, param, _SqlTransaction, null, commandType);

                if (affectedRows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                _SqlTransaction.Rollback();
                _SqlTransaction = null;
                throw;
            }
        }

        public async Task<bool> ExecuteManyAsync(string query,
            List<object> param, bool isStoredProc = true)
        {
            try
            {
                if (_SqlTransaction == null) BeginTransaction();

                CommandType commandType = isStoredProc ? CommandType.StoredProcedure : CommandType.Text;

                var affectedRows = await _SqlConnection.ExecuteAsync(query, param, _SqlTransaction, null, commandType);

                if (affectedRows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                _SqlTransaction.Rollback();
                _SqlTransaction = null;
                throw;
            }
        }

        public T QueryFirstOrDefault<T>(string query, object param, 
            bool isStoredProc = true)
        {
            try
            {
                if (_SqlTransaction == null) BeginTransaction();

                CommandType commandType = isStoredProc ? CommandType.StoredProcedure : CommandType.Text;

                return _SqlConnection.QueryFirstOrDefault<T>(query, param, _SqlTransaction, null, commandType);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string query, object param,
            bool isStoredProc = true)
        {
            try
            {
                if (_SqlTransaction == null) BeginTransaction();

                CommandType commandType = isStoredProc ? CommandType.StoredProcedure : CommandType.Text;

                return await _SqlConnection.QueryFirstOrDefaultAsync<T>(query, param, _SqlTransaction, null, commandType);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<T> QueryList<T>(string query, object param = null,
            bool isStoredProc = true)
        {
            try
            {
                if (_SqlTransaction == null) BeginTransaction();

                CommandType commandType = isStoredProc ? CommandType.StoredProcedure : CommandType.Text;

                return _SqlConnection.Query<T>(query, param, _SqlTransaction, true, null, commandType).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<T>> QueryListAsync<T>(string query, object param = null,
            bool isStoredProc = true)
        {
            try
            {
                if (_SqlTransaction == null) BeginTransaction();

                CommandType commandType = isStoredProc ? CommandType.StoredProcedure : CommandType.Text;

                return (await _SqlConnection.QueryAsync<T>(query, param, _SqlTransaction, null, commandType)).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
