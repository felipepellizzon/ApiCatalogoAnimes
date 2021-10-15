using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoAnimes.Exceptions
{
    public class AnimeJaCadastradoException : Exception
    {
        public AnimeJaCadastradoException()
            : base("Este anime já está cadastrado.")
        { }
    }
}
