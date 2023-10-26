using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public ProdutosController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            try
            {
                var produtos = _appDbContext.Produtos.ToList();

                if (produtos is null)
                {
                    return NotFound("Produtos não encontrados...");
                }

                return produtos;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError
                   , "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        // Rota nomeada:
        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            try
            {
                var produto = _appDbContext.Produtos.FirstOrDefault(P => P.ProdutoId == id);

                if (produto is null)
                {
                    return NotFound("Produto não encontrado...");
                }

                return produto;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError
                   , "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            try
            {
                if (produto is null)
                {
                    return BadRequest(); // 400
                }

                _appDbContext.Produtos.Add(produto);
                _appDbContext.SaveChanges();

                return new CreatedAtRouteResult("ObterProduto",
                    new { id = produto.ProdutoId }, produto);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError
                   , "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpPut("id:int")]
        public ActionResult Put(int id, Produto produto)
        {
            try
            {
                if (id != produto.ProdutoId)
                {
                    return BadRequest(); // 400
                }

                // Entidade precisa ser persistida
                _appDbContext.Entry(produto).State = EntityState.Modified;
                _appDbContext.SaveChanges();

                return Ok(produto);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError
                   , "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpDelete("id:int")]
        public ActionResult Delete(int id)
        {
            try
            {
                var produto = _appDbContext.Produtos.FirstOrDefault(p => p.ProdutoId == id);

                if (produto is null)
                {
                    return NotFound(); // 404
                }

                _appDbContext.Produtos.Remove(produto);
                _appDbContext.SaveChanges();

                return Ok(produto);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError
                    , "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }


    }
}
