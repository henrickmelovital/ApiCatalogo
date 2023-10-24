using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCatalogo.Models
{
    [Table("CTL_Categoria")]
    public class Categoria
    {
        // Uma boa pratica é iniciar a coleção:
        public Categoria()
        {
            ProdutosCategorias = new Collection<Produto>();
        }

        [Key]
        public int CategoriaId { get; set; }

        [Required]
        [StringLength(80)]
        public string Nome { get; set; }

        [Required]
        [StringLength (300)]
        public string ImagemUrl { get; set; }

        public ICollection<Produto> ProdutosCategorias { get; set; }
    }
}
