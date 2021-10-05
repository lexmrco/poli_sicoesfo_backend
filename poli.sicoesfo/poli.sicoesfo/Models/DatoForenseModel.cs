using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace poli.sicoesfo.Models
{
    public class DatoForenseModel
    {
        public Guid Id { get; set; }
        public string FechaMuerte { get; set; }
        public string HoraMuerte { get; set; }
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
