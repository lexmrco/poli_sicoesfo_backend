using poli.sicoesfo.Domain.Entities;
using poli.sicoesfo.Domain.Filters;
using poli.sicoesfo.Infrastructure.Domain.Repositories;
using System;
using System.Data;

namespace poli.sicoesfo.Infrastructure.Repositories
{
    public class DatoForenseRepository : Repository<DatoForense, DatoForenseFilter>, IDatoForenseRepository
    {
        public DatoForenseRepository(IDbConnection connection, IDbTransaction transaction = null) : base(connection, transaction)
        {
        }
        public override string TableName => "cifo.datosForenses";

        public override string InsertSql => @"INSERT INTO cifo.""datosForenses"" (id, fechamuerte, horamuerte)
            VALUES(@Id, @FechaMuerte, @HoraMuerte);";

        public override string UpdateSql => throw new NotImplementedException();

        public override string SelectSql => @"SELECT id ""Id"", fechamuerte ""FechaMuerte"", horamuerte ""HoraMuerte""
            FROM cifo.""datosForenses""";

    }
}
