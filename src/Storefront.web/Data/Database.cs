using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Storefront.web.Data
{
    public abstract class DbCommand
    {
        public virtual void Execute(IDatabase db)
        {
            throw new NotImplementedException();
        }

        public virtual Task ExecuteAsync(IDatabase db)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class DbCommand<T>
    {
        public virtual T Execute(IDatabase db)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> ExecuteAsync(IDatabase db)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class DbQuery<TModel>
    {
        public virtual TModel Execute(IDatabase db)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TModel> ExecuteAsync(IDatabase db)
        {
            throw new NotImplementedException();
        }
    }

    public interface IDatabase : IDisposable
    {
        DbConnection GetConnection();
        Task<DbConnection> GetConnectionAsync();
        T RunTransaction<T>(Func<T> query);
        Task<T> RunTransactionAsync<T>(Func<Task<T>> query);
        TransactionScope BeginTransaction();
    }

    public class Database : IDatabase
    {
        //private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        private readonly string _connectionString;
        private DbConnection _connection;

        public Database(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>Get the current connection, or open a new connection</summary>
        /// <remarks>For code that hasn't moved over to async yet, all new code should use async versions</remarks>
        public DbConnection GetConnection()
        {
            if (_connection == null)
                _connection = new SqlConnection(_connectionString);

            if (string.IsNullOrWhiteSpace(_connection.ConnectionString))
                _connection.ConnectionString = _connectionString;

            if (_connection.State == ConnectionState.Closed)
                _connection.Open();

            return _connection;
        }

        /// <summary>Get the current connection, or open a new connection</summary>
        public async Task<DbConnection> GetConnectionAsync()
        {
            if (_connection == null)
                _connection = new SqlConnection(_connectionString);

            if (string.IsNullOrWhiteSpace(_connection.ConnectionString))
                _connection.ConnectionString = _connectionString;

            if (_connection.State == ConnectionState.Closed)
                await _connection.OpenAsync();

            return _connection;
        }

        /// <summary>Initializes a transasction scope with common options</summary>
        /// <remarks>Wrap in a using statement</remarks>
        public TransactionScope BeginTransaction()
        {
            var transOptions = new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted };
            return new TransactionScope(TransactionScopeOption.Required, transOptions, TransactionScopeAsyncFlowOption.Enabled );
        }

        /// <summary>Perform an operation with either an implied transaction or an ambient transaction already in progress</summary>
        /// <remarks>For code that hasn't moved over to async yet, all new code should use async versions</remarks>
        public T RunTransaction<T>(Func<T> query)
        {
            try
            {
                // This either starts a new trans or joins the ambient trans
                using (TransactionScope trans = BeginTransaction())
                {
                    GetConnection().EnlistTransaction(Transaction.Current);

                    var result = query();

                    // Commit the transaction. If an exception has been thrown, Complete is not called and the transaction is rolled back
                    trans.Complete();

                    return result;
                }
            }
            catch (Exception ex)
            {
                //_logger.Error(ex, $"Error during database operation: {ex}");

                throw;
            }
        }

        /// <summary>Perform an operation with either an implied transaction or an ambient transaction already in progress</summary>
        public async Task<T> RunTransactionAsync<T>(Func<Task<T>> query)
        {
            try
            {
                // This either starts a new trans or joins the ambient trans
                using (TransactionScope trans = BeginTransaction())
                {
                    (await GetConnectionAsync()).EnlistTransaction(Transaction.Current);

                    var result = await query();

                    // Commit the transaction. If an exception has been thrown, Complete is not called and the transaction is rolled back
                    trans.Complete();

                    return result;
                }
            }
            catch (Exception ex)
            {
                //_logger.Error(ex, $"Error during database operation: {ex}");

                throw;
            }
        }

        /// <summary>Dispose of the transaction and close the connection</summary>
        public void Dispose()
        {
            if (_connection != null && _connection.State != ConnectionState.Closed)
            {
                _connection.Close();
                _connection = null;
            }
        }
    }
}