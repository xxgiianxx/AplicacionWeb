using System.ComponentModel.DataAnnotations;

namespace api_core.Models.Request
{
    public class ProductoRequest
    {        public int Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int CategoriaID { get; set; }
        public int Cantidad { get; set; }

    }
}
