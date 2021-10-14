using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoAnimes.ViewModel
{
    public class AnimeViewModel
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public byte Nota { get; set; } 

        public string Produtora { get; set; }
    }
}
