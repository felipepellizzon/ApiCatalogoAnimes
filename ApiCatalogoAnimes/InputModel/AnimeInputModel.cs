﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoAnimes.InputModel
{
    public class AnimeInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do anime deve conter entre 3 e 100 caracteres.")]
        public string Nome { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome da produtora deve conter entre 1 e 100 caracteres.")]
        public string Produtora { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "A nota do anime deve ser de 1 a 10.")]
        public byte Nota { get; set; }

    }
}
