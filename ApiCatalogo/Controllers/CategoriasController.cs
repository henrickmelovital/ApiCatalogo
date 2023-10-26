using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public CategoriasController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            try
            {
                // O método de extensão Include() permite carregar entidades relacionadas
                var categoriaProdutos = _appDbContext.Categorias.Include(P => P.ProdutosCategorias).AsNoTracking().ToList();

                if (categoriaProdutos is null)
                {
                    return NotFound("Categorias e produtos não encontradas...");
                }

                return Ok(categoriaProdutos);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError
                    , "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            try
            {
                var categorias = _appDbContext.Categorias.AsNoTracking().ToList();

                if (categorias is null)
                {
                    return NotFound("Categorias não encontradas");
                }

                return categorias;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError
                    , "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            try
            {
                var categoria = _appDbContext.Categorias.AsNoTracking().FirstOrDefault(P => P.CategoriaId == id);

                if (categoria is null)
                {
                    return NotFound("Categoria não encontrada...");
                }
                return Ok(categoria);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError
                    , "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            try
            {
                if (categoria is null)
                {
                    return BadRequest();
                }

                _appDbContext.Categorias.Add(categoria);
                _appDbContext.SaveChanges();

                return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError
                    , "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            try
            {
                if (id != categoria.CategoriaId)
                {
                    return BadRequest();
                }

                _appDbContext.Entry(categoria).State = EntityState.Modified;
                _appDbContext.SaveChanges();
                return Ok(categoria);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError
                    , "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Categoria> Delete(int id)
        {
            try
            {
                var categoria = _appDbContext.Categorias.FirstOrDefault(P => P.CategoriaId == id);

                if (categoria is null)
                {
                    return NotFound();
                }
                _appDbContext.Categorias.Remove(categoria);
                _appDbContext.SaveChanges();
                return Ok(categoria);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError
                    , "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }
    }
}
