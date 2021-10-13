using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogoAnimes.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class AnimesController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<object>>> Obter()
        {
            return Ok();
        }
        [HttpGet("{idAnime:guid}")]
        public async Task<ActionResult<List<object>>> Obter(Guid idAnime)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<object>>InserirAnime(object anime)
        {
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult>AtualizarAnime(Guid idAnime, object anime)
        {
            return Ok();
        }

        [HttpPatch("{idAnime:guid}/genero/{genero:string}")]
        public async Task<ActionResult> AtualizarAnime(Guid idAnime, string genero)
        {
            return Ok();
        }

        [HttpDelete("{idAnime:guid}")]
        public async Task<ActionResult>ApagarAnime(Guid idAnime)
        {
            return Ok();
        }

    }
}
