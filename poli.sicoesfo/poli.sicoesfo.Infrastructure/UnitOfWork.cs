using Microsoft.Extensions.Logging;
using Npgsql;
using poli.sicoesfo.Infrastructure.Domain.Repositories;
using poli.sicoesfo.Infrastructure.Repositories;
using System;
using System.Data;

namespace poli.sicoesfo.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IDatoForenseRepository DatosForenses();
        void BeginTransaction();
        void Commit();
        void RollBack();
        IDbTransaction Transaction { get; }
    }
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private IDatoForenseRepository _datoForenseRepository;

        private bool _disposed;
        public IDbTransaction Transaction => _transaction;
        ILogger<UnitOfWork> _logger;
        private readonly string _connectionString;
        public UnitOfWork(ILogger<UnitOfWork> logger)
        {
            _logger = logger;
            _connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
        }
        private void OpenConnection()
        {
            _connection = new NpgsqlConnection(_connectionString);
            _connection.Open();
            _logger.LogInformation("DB Connection Open");
        }

        public void BeginTransaction()
        {
            ValidateConnection();
            _transaction = _connection.BeginTransaction();

        }
        private void ValidateConnection()
        {
            if (_connection == null || _connection.State != ConnectionState.Open)
            {
                OpenConnection();
            }
        }
        public IDatoForenseRepository DatosForenses()
        {
            ValidateConnection();
            return _datoForenseRepository ?? (_datoForenseRepository = new DatoForenseRepository(_connection, _transaction));
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                //_transaction = _connection.BeginTransaction();
                ResetRepositories();
            }
        }

        public void RollBack()
        {
            try
            {
                _transaction.Rollback();
            }
            catch
            {
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                ResetRepositories();
            }
        }

        private void ResetRepositories()
        {
            _datoForenseRepository = null;
        }
        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }
        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Close();
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            dispose(true);
        }
    }
}
