using Catalago.Exceptions;
using Catalago.InputModel;
using Catalago.Services;
using Catalago.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Catalago.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {

        private readonly IJogoService _jogoService;

        public JogosController(IJogoService jogoService)
        {
            _jogoService = jogoService;
        }

        //Task melhorar a performance em requisições Web...
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
        public async Task<ActionResult<List<JogoViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1,[FromQuery, Range(1,50)] int quantidade = 5) 
        {
            var jogos = await _jogoService.Obter(pagina,quantidade);

            if (jogos.Count() == 0)

                return NoContent();
                       
            return Ok(jogos);
        }

        /// <summary>
        /// Buscar um jogo pelo seu Id
        /// </summary>
        /// <param name="idJogo">Id do jogo buscado</param>
        /// <response code="200">Retorna o jogo filtrado</response>
        /// <response code="204">Caso não haja jogo com este id</response>s
        [HttpGet("{IdJogo:guid}")]
        public async Task<ActionResult<List<JogoViewModel>>> Obter([FromRoute]Guid IdJogo)
        {
            var jogos = await _jogoService.Obter(IdJogo);

            if (jogos == null)

                return NoContent();

            return Ok(jogos);
        }

        [HttpPost]
        public async Task<ActionResult<JogoViewModel>> InserirJogo([FromBody] JogoInputModel jogoInputModel)
        {
            try
            {
                var jogo = await _jogoService.Inserir(jogoInputModel);
                return Ok(jogo);
            }
            catch (JogoJaCadastradoException ex)
            {
                return UnprocessableEntity("Já existe um jogo com este nome para esta produtora");
            }
            return Ok();
        }

        [HttpPut("{IdJogo:guid}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid IdJogo, [FromBody] JogoInputModel jogoInputModel)
        {
            try
            {
                await _jogoService.Atualizar(IdJogo, jogoInputModel);

                return Ok();
            }
            catch (JogoJaCadastradoException ex)
            {
                return NotFound("Não existe esse jogo");
            }           
        }

        [HttpPatch("{IdJogo:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid IdJogo, [FromBody] double preco)
        {
            try
            {
                await _jogoService.Atualizar(IdJogo, preco);

                return Ok();
            }
            catch (JogoJaCadastradoException ex)
            {
                return NotFound("Não existe esse jogo");
            }
        }

        [HttpDelete("{IdJogo:guid}")]
        public async Task<ActionResult> ApagarJogo([FromRoute]Guid idjogo)
        {
            try
            {
                await _jogoService.Remover(idjogo);

                return Ok();
            }
            catch (JogoJaCadastradoException ex)
            {
                return NotFound("Não existe esse jogo");
            }
        }
    }
}
