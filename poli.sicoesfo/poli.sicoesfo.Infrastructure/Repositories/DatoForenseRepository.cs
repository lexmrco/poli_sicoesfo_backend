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

        public override string InsertSql => @"INSERT INTO cifo.""datosForenses""
(id, fechamuerte, horamuerte, tipomuerte, edad, estadocivil, escolaridad, factorvulnerabilidad, coddanedepartamento, codigodanemunicipio, tipodezona, escenario, actividaddurantehecho, circunstancia, mecanismo)
VALUES(@Id, @FechaMuerte, @HoraMuerte, @TipoMuerte, @Edad, @EstadoCivil, @Escolaridad, @FactorVulnerabilidad, @CodDaneDepartamento, @CodigoDaneMunicipio, @TipoDeZona, @Escenario, @ActividadDuranteHecho, @Circunstancia, @Mecanismo);
;";

        public override string UpdateSql => throw new NotImplementedException();

        public override string SelectSql => @"select id ""Id"", fechamuerte ""FechaMuerte"", horamuerte ""HoraMuerte"", tipomuerte ""TipoMuerte"", edad ""Edad"", estadocivil ""EstadoCivil"", 
escolaridad ""Escolaridad"", factorvulnerabilidad ""FactorVulnerabilidad"", coddanedepartamento ""CodDaneDepartamento"", codigodanemunicipio ""CodigoDaneMunicipio"", 
tipodezona ""TipoDeZona"", escenario ""Escenario"", actividaddurantehecho ""ActividadDuranteHecho"", circunstancia ""Circunstancia"", mecanismo ""Mecanismo""
            FROM cifo.""datosForenses""";

        protected override void SetWhereClause(DatoForenseFilter filter)
        {
            this._orderbyClause = "order by fechamuerte desc";
            base.SetWhereClause(filter);
        }
    }    
}
