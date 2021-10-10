﻿using System.ComponentModel.DataAnnotations;

namespace poli.sicoesfo.Models
{
    public class AuthenticateModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
