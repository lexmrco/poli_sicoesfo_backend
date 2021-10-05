using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace poli.sicoesfo.Domain.Entities
{
    public class DatoForense: Entity
    {
        public Guid Id { get; set; }
        public DateTime? FechaMuerte { get; set; }
       
        [DataType(DataType.Time)]
        public TimeSpan? HoraMuerte { get; set; }
        public string TipoMuerte { get; set; }
        public int? Edad { get; set; }
        public string EstadoCivil { get; set; }
        public string Escolaridad { get; set; }
        public string FactorVulnerabilidad { get; set; }
        public string TipoDeZona { get; set; }
        public string Escenario { get; set; }
        public string ActividadDuranteHecho { get; set; }
        public string Circunstancia { get; set; }
        public string Mecanismo { get; set; }
        public int? CodDaneDepartamento { get; set; }
        public int? CodigoDaneMunicipio { get; set; }
    }
}
