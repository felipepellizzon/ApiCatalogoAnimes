using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoAnimes.Exceptions
{
    public class AnimeNaoCadastradoException : Exception
    {

        public AnimeNaoCadastradoException()
            : base("Este anime não está cadastrado")
        { }
    }
}
