using Dapper;
using Npgsql;
using poli.sicoesfo.Domain.Contracts;
using poli.sicoesfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace poli.sicoesfo.Infrastructure.Repositories
{
    public abstract class Repository<TEntity, TFilter> : IRepository<TEntity, TFilter>
        where TEntity : Entity
        where TFilter : Filter
    {
        protected IDbTransaction _transaction { get; private set; }

        private readonly IDbConnection _connection;

        protected IDbConnection Connection { get { return _connection; } }

        public abstract string TableName { get; }
        public virtual string SelectSql => $"SELECT * FROM ( SELECT ROW_NUMBER() OVER ( {_orderbyClause} ) AS RowNum, * FROM {TableName} {_whereClause}) AS QueryResult WHERE RowNum >= @MinRow AND RowNum <= @MaxRow ORDER BY RowNum";
        public virtual string SelectFirstSql => $"select tfrom {TableName} where Id = @id";
        public abstract string InsertSql { get; }
        public abstract string UpdateSql { get; }
        public virtual string DeleteSql => $"delete {TableName} where Id = @id";

        protected virtual void SetWhereClause(TFilter filter) { }

        protected string _baseClause;
        protected string _orderbyClause;
        protected string _whereClause;

        public Repository(IDbConnection connection, IDbTransaction transaction = null)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public async virtual Task<TEntity> GetAsync<TTypeOfId>(TTypeOfId id)
        {
            return await Connection.QueryFirstOrDefaultAsync<TEntity>(string.Concat(SelectFirstSql), new { id }, _transaction);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(TFilter filter)
        {
            SetWhereClause(filter);
            string query = string.Concat(SelectSql, " ", _whereClause, " ", _orderbyClause, $" LIMIT {filter.Limit};");
            return await Connection.QueryAsync<TEntity>(query, filter, _transaction);
            
        }

        public async virtual Task<TEntity> FirstOrDefault(TFilter filter)
        {
            SetWhereClause(filter);
            string query = string.Concat(SelectSql, " ", _whereClause, " ", _orderbyClause);
            return await Connection.QueryFirstOrDefaultAsync<TEntity>(query, filter, _transaction);
        }

        public async Task<int> CreateAsync(TEntity entity)
        {
            return await Connection.ExecuteAsync(InsertSql, entity, _transaction);
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            return await Connection.ExecuteAsync(UpdateSql, entity, _transaction);
        }

        public async virtual Task<int> DeleteAsync<TypeofId>(TypeofId id)
        {
            return await Connection.ExecuteAsync(DeleteSql, new { id }, _transaction);
        }                 
    }    
}