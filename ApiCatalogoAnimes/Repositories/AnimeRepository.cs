using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogoAnimes.Entities;

namespace ApiCatalogoAnimes.Repositories
{
    public class AnimeRepository : IAnimeRepository
    {
        private static Dictionary<Guid, Anime> animes = new Dictionary<Guid, Anime>()
        {
            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), new Anime{ Id = Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), Nome = "Demon Slayer", Produtora = "Prod1", Nota = 10} },
            {Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), new Anime{ Id = Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), Nome = "Dragon Ball Z", Produtora = "Tokyo", Nota = 10} },
            {Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), new Anime{ Id = Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), Nome = "Naruto", Produtora = "Tokyo", Nota = 9} },
            {Guid.Parse("da033439-f352-4539-879f-515759312d53"), new Anime{ Id = Guid.Parse("da033439-f352-4539-879f-515759312d53"), Nome = "Dragon Ball Kai", Produtora = "Tokyo", Nota = 7} },
            {Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), new Anime{ Id = Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), Nome = "Street Fighter V", Produtora = "Capcom", Nota = 8} },
            {Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), new Anime{ Id = Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), Nome = "Baki", Produtora = "Prod1", Nota = 9} }
        };

        public Task<List<Anime>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(animes.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Anime> Obter(Guid id)
        {
            if (!animes.ContainsKey(id))
                return Task.FromResult<Anime>(null);

            return Task.FromResult(animes[id]);
        }

        public Task<List<Anime>> Obter(string nome, string produtora)
        {
            return Task.FromResult(animes.Values.Where(jogo => jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora)).ToList());
        }

        public Task<List<Anime>> ObterSemLambda(string nome, string produtora)
        {
            var retorno = new List<Anime>();

            foreach (var jogo in animes.Values)
            {
                if (jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora))
                    retorno.Add(jogo);
            }

            return Task.FromResult(retorno);
        }

        public Task Inserir(Anime anime)
        {
            animes.Add(anime.Id, anime);
            return Task.CompletedTask;
        }

        public Task Atualizar(Anime anime)
        {
            animes[anime.Id] = anime;
            return Task.CompletedTask;
        }

        public Task Remover(Guid id)
        {
            animes.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //Fechar conexão com o banco
        }
    }
}
