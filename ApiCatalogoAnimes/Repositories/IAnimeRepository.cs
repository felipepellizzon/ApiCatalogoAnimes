using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogoAnimes.Entities;

namespace ApiCatalogoAnimes.Repositories
{
    public interface IAnimeRepository : IDisposable
    {
        Task<List<Anime>> Obter(int pagina, int quantidade);
        Task<Anime> Obter(Guid id);
        Task<List<Anime>> Obter(string nome, string produtora);
        Task Inserir(Anime anime);
        Task Atualizar(Anime anime);
        Task Remover(Guid id);
    }
}
