using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace poli.sicoesfo.Models
{
    public class DatoForensePostModel
    {
        [DataMember(Name = "fechaMuerte")]
        public string FechaMuerte { get; set; }
        [DataMember(Name = "horaMuerte")]
        public string HoraMuerte { get; set; }
        [DataMember(Name = "tipoMuerte")]
        public string TipoMuerte { get; set; }
        [DataMember(Name = "edad")]
        public int? Edad { get; set; }
        [DataMember(Name = "estadoCivil")]
        public string EstadoCivil { get; set; }
        [DataMember(Name = "escolaridad")]
        public string Escolaridad { get; set; }
        [DataMember(Name = "factorVulnerabilidad")]
        public string FactorVulnerabilidad { get; set; }
        [DataMember(Name = "tipoDeZona")]
        public string TipoDeZona { get; set; }
        [DataMember(Name = "escenario")]
        public string Escenario { get; set; }
        [DataMember(Name = "actividadDuranteHecho")]
        public string ActividadDuranteHecho { get; set; }
        [DataMember(Name = "circunstancia")]
        public string Circunstancia { get; set; }
        [DataMember(Name = "mecanismo")]
        public string Mecanismo { get; set; }

        [DataMember(Name = "codDaneDepartamento")]
        public int? CodDaneDepartamento { get; set; }
        [DataMember(Name = "codigoDaneMunicipio")]

        public int? CodigoDaneMunicipio { get; set; }
    }
}
