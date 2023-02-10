using System.ComponentModel.DataAnnotations;

namespace api_core.Models.Response
{
    public class ProductoCategoria
    {
        public int Id { get; set; }
        [Required]
        public string Descripcion { get; set; } = string.Empty;
        public int CategoriaID { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public decimal Precio { get; set; }

        public int Cantidad { get; set; }
    }
}
