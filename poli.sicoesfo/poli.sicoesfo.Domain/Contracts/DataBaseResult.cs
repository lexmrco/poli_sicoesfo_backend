using System;
using System.Collections.Generic;
using System.Text;

namespace poli.sicoesfo.Domain.Contracts
{
    public class DataBaseResult<TEntity>
    {
        public int TotalRows { get; set; }
        public int Page { get; set; }
        public int RowsPerPage { get; set; }
        public IEnumerable<TEntity> Data { get; set; }
    }
}
