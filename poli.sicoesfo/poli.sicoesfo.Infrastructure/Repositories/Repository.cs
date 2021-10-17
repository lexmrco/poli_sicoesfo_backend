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
        protected string _pagination;

        public Repository(IDbConnection connection, IDbTransaction transaction = null)
        {
            _connection = connection;
            _transaction = transaction;
        }

        private void BuildPPagination(TFilter filter)
        {
            int offset = (filter.Page * filter.ItemsPerpage) - filter.ItemsPerpage;
            _pagination = $"limit {filter.ItemsPerpage} offset {offset}";
        }

        public async virtual Task<TEntity> GetAsync<TTypeOfId>(TTypeOfId id)
        {
            return await Connection.QueryFirstOrDefaultAsync<TEntity>(string.Concat(SelectFirstSql), new { id }, _transaction);
        }

        public async Task<DataBaseResult<TEntity>> GetAllAsync(TFilter filter)
        {
            var dataResult = Activator.CreateInstance<DataBaseResult<TEntity>>();
            SetWhereClause(filter);
            BuildPPagination(filter);
            string query = string.Concat(SelectSql, " ", _whereClause, " ", _orderbyClause, " ", _pagination);
            string s2 = SelectSql.Substring(SelectSql.ToLower().IndexOf("from"));
            string countQuery = string.Concat($"select count(*) {s2}", " ", _whereClause);
            int count = await Connection.ExecuteScalarAsync<int>(countQuery, filter, _transaction);
            dataResult.TotalRows = count;
            dataResult.Page = filter.Page;
            dataResult.RowsPerPage = filter.ItemsPerpage;
            dataResult.Data = await Connection.QueryAsync<TEntity>(query, filter, _transaction);
            return dataResult;            
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