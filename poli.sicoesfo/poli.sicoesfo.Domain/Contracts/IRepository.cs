using poli.sicoesfo.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace poli.sicoesfo.Domain.Contracts
{
    public interface IRepository<TEntity, TFilter> 
        where TEntity : Entity
        where TFilter : Filter
    {
        Task<TEntity> FirstOrDefault(TFilter filter);
        Task<TEntity> GetAsync<TypeofId>(TypeofId id);
        Task<DataBaseResult<TEntity>> GetAllAsync(TFilter filter);
        Task<int> CreateAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> DeleteAsync<TypeofId>(TypeofId id);        
    }
}
