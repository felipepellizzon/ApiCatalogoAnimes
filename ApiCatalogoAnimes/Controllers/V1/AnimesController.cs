using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogoAnimes.Exceptions;
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

        /// <summary>
        /// Buscar todos os jogos de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar os jogos sem paginação
        /// </remarks>
        /// <param name="pagina">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantidade">Indica a quantidade de reistros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de jogos</response>
        /// <response code="204">Caso não haja jogos</response>   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimeViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1,50)] int quantidade = 5)
        {
            var animes = await _animeService.Obter(pagina, quantidade);

            if (animes.Count() == 0)
                return NoContent();
            return Ok(animes);
        }

        /// <summary>
        /// Buscar um jogo pelo seu Id
        /// </summary>
        /// <param name="idJogo">Id do jogo buscado</param>
        /// <response code="200">Retorna o jogo filtrado</response>
        /// <response code="204">Caso não haja jogo com este id</response> 
        [HttpGet("{idAnime:guid}")]
        public async Task<ActionResult<List<AnimeViewModel>>> Obter([FromRoute]Guid idAnime)
        {
            var animes = await _animeService.Obter(idAnime);
            if (animes == null)
                return NoContent();

            return Ok(animes);
            
        }

        /// <summary>
        /// Inserir um jogo no catálogo
        /// </summary>
        /// <param name="jogoInputModel">Dados do jogo a ser inserido</param>
        /// <response code="200">Cao o jogo seja inserido com sucesso</response>
        /// <response code="422">Caso já exista um jogo com mesmo nome para a mesma produtora</response>
        [HttpPost]
        public async Task<ActionResult<AnimeViewModel>>InserirAnime([FromBody]AnimeInputModel animeInputModel)
        {
            try
            {
                var anime = await _animeService.Inserir(animeInputModel);

                return Ok(anime);
            }
            catch(AnimeJaCadastradoException ex)
            {
                return UnprocessableEntity("Já existe um anime com este nome para esta produtora");
            }

        }

        /// <summary>
        /// Atualizar um jogo no catálogo
        /// </summary>
        /// /// <param name="idJogo">Id do jogo a ser atualizado</param>
        /// <param name="jogoInputModel">Novos dados para atualizar o jogo indicado</param>
        /// <response code="200">Cao o jogo seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um jogo com este Id</response>
        [HttpPut]
        public async Task<ActionResult>AtualizarAnime([FromRoute]Guid idAnime,[FromBody] AnimeInputModel animeInputModel)
        {
            try
            {
                await _animeService.Atualizar(idAnime, animeInputModel);
                return Ok();
            }
            catch (AnimeJaCadastradoException ex)
            {
                return NotFound("Não existe este anime.");
            }
        }

        /// <summary>
        /// Atualizar o preço de um jogo
        /// </summary>
        /// /// <param name="idJogo">Id do jogo a ser atualizado</param>
        /// <param name="preco">Novo preço do jogo</param>
        /// <response code="200">Cao o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um jogo com este Id</response>
        [HttpPatch("{idAnime:guid}/nota/{nota:int}")]
        public async Task<ActionResult> AtualizarAnime([FromRoute]Guid idAnime,[FromRoute] int nota)
        {
            try
            {
                await _animeService.Atualizar(idAnime, nota);
                return Ok();
            }
            catch (AnimeJaCadastradoException ex)
            {
                return NotFound("Não existe este anime.");
            }
        }

        /// <summary>
        /// Excluir um jogo
        /// </summary>
        /// /// <param name="idJogo">Id do jogo a ser excluído</param>
        /// <response code="200">Cao o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um jogo com este Id</response>
        [HttpDelete("{idAnime:guid}")]
        public async Task<ActionResult>ApagarAnime([FromRoute]Guid idAnime)
        {
            try
            {
                await _animeService.Remover(idAnime);
                return Ok();
            }
            catch (AnimeJaCadastradoException ex)
            {
                return NotFound("Não existe este anime.");
            }
        }

    }
}
