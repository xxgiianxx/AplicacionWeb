using System.ComponentModel.DataAnnotations.Schema;

namespace api_core.Models
{
    public class Categoria
    {
        public int CategoriaID { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public  List<Producto> Productos { get; set; }

    }
}
