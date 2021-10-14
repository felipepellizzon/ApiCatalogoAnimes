using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogoAnimes.InputModel;
using ApiCatalogoAnimes.Services;
using ApiCatalogoAnimes.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogoAnimes.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class AnimesController : ControllerBase
    {

        private readonly IAnimeService _animeService;

        public AnimesController(IAnimeService animeService)
        {
            _animeService = animeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimeViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1,50)] int quantidade = 5)
        {
            var animes = await _animeService.Obter(pagina, quantidade);

            if (animes.Count() == 0)
                return NoContent();
            return Ok(animes);
        }
        [HttpGet("{idAnime:guid}")]
        public async Task<ActionResult<List<AnimeViewModel>>> Obter([FromRoute]Guid idAnime)
        {
            var animes = await _animeService.Obter(idAnime);
            if (animes == null)
                return NoContent();

            return Ok(animes);
            
        }

        [HttpPost]
        public async Task<ActionResult<AnimeViewModel>>InserirAnime([FromBody]AnimeInputModel animeInputModel)
        {
            try
            {
                var anime = await _animeService.Inserir(animeInputModel);

                return Ok(anime);
            }
            //catch(AnimeJaCadastradoException ex)
            catch(Exception ex)
            {
                return UnprocessableEntity("Já existe um anime com este nome para esta produtora");
            }

        }

        [HttpPut]
        public async Task<ActionResult>AtualizarAnime([FromRoute]Guid idAnime,[FromBody] AnimeInputModel animeInputModel)
        {
            try
            {
                await _animeService.Atualizar(idAnime, animeInputModel);
                return Ok();
            }
            //catch (AnimeJaCadastradoException ex)
            catch(Exception ex)
            {
                return NotFound("Não existe este anime.");
            }
        }

        [HttpPatch("{idAnime:guid}/genero/{genero:string}")]
        public async Task<ActionResult> AtualizarAnime([FromRoute]Guid idAnime,[FromRoute] string genero)
        {
            try
            {
                await _animeService.Atualizar(idAnime, genero);
                return Ok();
            }
            //catch (AnimeJaCadastradoException ex)
            catch (Exception ex)
            {
                return NotFound("Não existe este anime.");
            }
        }

        [HttpDelete("{idAnime:guid}")]
        public async Task<ActionResult>ApagarAnime([FromRoute]Guid idAnime)
        {
            try
            {
                await _animeService.Remover(idAnime);
                return Ok();
            }
            //catch (AnimeJaCadastradoException ex)
            catch (Exception ex)
            {
                return NotFound("Não existe este anime.");
            }
        }

    }
}
