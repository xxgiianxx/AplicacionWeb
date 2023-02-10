using api_core.Models;
using api_core.Models.Request;
using api_core.Models.Response;

namespace api_core.Servicios
{
    public interface IServicio_API
    {
         Task Autenticar();
         Task<List<Producto>> ListaProducto();
        Task<List<ProductoCategoria>> ListaProductoCategoria();

        Task<Producto> ObtenerProducto(int idProducto);
        Task<ProductoCategoria> ObtenerProductoCategoria(int Id);

        Task<bool> GuardarProducto(ProductoRequest objeto);
        Task<bool> GuardarProductoCategoria(ProductoCategoria objeto);

        Task<bool> EditarProducto(ProductoRequest objeto);
        Task<bool> EditarProductoCategoria(ProductoCategoria objeto);

        Task<bool> EliminarProducto(int Id);

        Task<List<ResultadoCategoria>> ListaCategoria();
        Task<bool> GuardarCategoria(Categoria objeto);
        Task<bool> EditarCategoria(Categoria objeto);
        Task<Categoria> ObtenerCategoria(int IdCategoria);

    }
}
