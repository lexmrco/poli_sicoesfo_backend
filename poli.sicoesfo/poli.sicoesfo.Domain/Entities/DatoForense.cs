using System;
using System.Collections.Generic;
using System.Text;

namespace poli.sicoesfo.Domain.Entities
{
    public class DatoForense
    {
        public Guid Id { get; set; }
        public DateTime FechaMuerte { get; set; }
        public DateTime HoraMuerte { get; set; }
    }
}
