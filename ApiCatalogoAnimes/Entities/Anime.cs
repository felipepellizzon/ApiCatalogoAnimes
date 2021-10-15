using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoAnimes.Entities
{
    public class Anime
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public int Nota { get; set; }

        public string Produtora { get; set; }
    }
}
