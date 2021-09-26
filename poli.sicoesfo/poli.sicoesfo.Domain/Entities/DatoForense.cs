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
    }
}
