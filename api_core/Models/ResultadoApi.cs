namespace api_core.Models
{
    public class ResultadoApi
    {
        public string mensaje { get; set; }

        public List<Producto> listaProducto { get; set; }
        public List<Categoria> listaCategoria { get; set; }

        public Producto objetoProducto { get; set; }
        public Categoria objetoCategoria { get; set; }

    }
}
