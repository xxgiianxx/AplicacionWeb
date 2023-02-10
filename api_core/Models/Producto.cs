namespace api_core.Models
{
    public class Producto
    {
        public int Id { get; set; }

        public string Descripcion { get; set; } = string.Empty;

        public int CategoriaId { get; set; }

        public decimal Precio { get; set; }

        public decimal Cantidad { get; set; }


        public virtual Categoria categoria { get; set; }
    }
}
