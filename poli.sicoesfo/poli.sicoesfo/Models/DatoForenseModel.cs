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
    }
}
