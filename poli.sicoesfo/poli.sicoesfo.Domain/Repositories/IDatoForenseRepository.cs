using poli.sicoesfo.Domain.Contracts;
using poli.sicoesfo.Domain.Entities;
using poli.sicoesfo.Domain.Filters;

namespace poli.sicoesfo.Infrastructure.Domain.Repositories
{
    public interface IDatoForenseRepository: IRepository<DatoForense, DatoForenseFilter>
    {
    }
}
