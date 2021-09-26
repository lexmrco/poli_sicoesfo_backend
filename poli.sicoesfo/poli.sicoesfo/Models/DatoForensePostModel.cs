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
    }
}
