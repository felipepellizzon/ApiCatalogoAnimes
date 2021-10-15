using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogoAnimes.Entities;
using ApiCatalogoAnimes.Exceptions;
using ApiCatalogoAnimes.InputModel;
using ApiCatalogoAnimes.Repositories;
using ApiCatalogoAnimes.ViewModel;

namespace ApiCatalogoAnimes.Services
{
    public class AnimeService : IAnimeService
    {
        private readonly IAnimeRepository _animeRepository;

        public AnimeService(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }

        public async Task<List<AnimeViewModel>> Obter(int pagina, int quantidade)
        {
            var animes = await _animeRepository.Obter(pagina, quantidade);

            return animes.Select(anime => new AnimeViewModel
            {
                Id = anime.Id,
                Nome = anime.Nome,
                Produtora = anime.Produtora,
                Nota = anime.Nota
            })
                .ToList();
        }

        public async Task<AnimeViewModel> Obter(Guid id)
        {
            var anime = await _animeRepository.Obter(id);

            if (anime == null)
                return null;

            return new AnimeViewModel
            {
                Id = anime.Id,
                Nome = anime.Nome,
                Produtora = anime.Produtora,
                Nota=anime.Nota
                
            };

        }

        public async Task<AnimeViewModel> Inserir(AnimeInputModel anime)
        {
            var entidadeAnime = await _animeRepository.Obter(anime.Nome, anime.Produtora);

            if (entidadeAnime.Count > 0)
                throw new AnimeJaCadastradoException();

            var animeInsert = new Anime
            {
                Id=Guid.NewGuid(),
                Nome=anime.Nome,
                Produtora=anime.Produtora,
                Nota=anime.Nota
            };

            await _animeRepository.Inserir(animeInsert);

            return new AnimeViewModel
            {
                Id = animeInsert.Id,
                Nome = anime.Nome,
                Produtora = anime.Produtora,
                Nota = anime.Nota
            };
        }

        public async Task Atualizar(Guid id, AnimeInputModel anime)
        {
            var entidadeAnime = await _animeRepository.Obter(id);

            if(entidadeAnime == null)
                throw new AnimeJaCadastradoException();

            entidadeAnime.Nome = anime.Nome;
            entidadeAnime.Produtora = anime.Produtora;
            entidadeAnime.Nota = anime.Nota;

            await _animeRepository.Atualizar(entidadeAnime);

        }

        public async Task Atualizar(Guid id, int nota)
        {
            var entidadeAnime = await _animeRepository.Obter(id);

            if (entidadeAnime == null)
                throw new AnimeJaCadastradoException();

            entidadeAnime.Nota = nota;

            await _animeRepository.Atualizar(entidadeAnime);

        }

        public async Task Remover(Guid id)
        {
            var anime = await _animeRepository.Obter(id);

            if(anime == null)
                throw new AnimeJaCadastradoException();

            await _animeRepository.Remover(id);
        }

        public void Dispose()
        {
            _animeRepository?.Dispose();
        }

    }
}
