using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogoAnimes.InputModel;
using ApiCatalogoAnimes.ViewModel;

namespace ApiCatalogoAnimes.Services
{
    public interface IAnimeService : IDisposable
    {
        Task<List<AnimeViewModel>> Obter(int pagina, int quantidade);
        Task<AnimeViewModel> Obter(Guid id);
        Task<AnimeViewModel> Inserir(AnimeInputModel anime);
        Task Atualizar(Guid id, AnimeInputModel anime);
        Task Atualizar(Guid id, int nota);
        Task Remover(Guid id);
    }
}
