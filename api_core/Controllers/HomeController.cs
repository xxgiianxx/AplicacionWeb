using api_core.Models;
using api_core.Servicios;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using api_core.Models.Request;
using api_core.Models.Response;

namespace api_core.Controllers
{
    public class HomeController : Controller
    {
        private IServicio_API _servicioApi;

        public HomeController(IServicio_API servicioApi)
        {
            _servicioApi = servicioApi;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductoCategoria> lista = await _servicioApi.ListaProductoCategoria();
            return View(lista);
        }



        public async Task<IActionResult> Producto(int idProducto)
        {

            Producto modelo_producto = new Producto();

            ViewBag.Accion = "Nuevo Producto";

            if (idProducto != null)
            {

                ViewBag.Accion = "Editar Producto";
                modelo_producto = await _servicioApi.ObtenerProducto(idProducto);
            }
            

            List<ResultadoCategoria> lista = await _servicioApi.ListaCategoria();
            ViewBag.Categoria = lista;

            return View(modelo_producto);
        }

        public async Task<IActionResult> ProductoCategoria(int Id)
        {

            ProductoCategoria modelo_producto = new ProductoCategoria();

            ViewBag.Accion = "Nuevo Producto";

            if (Id != null)
            {

                ViewBag.Accion = "Editar Producto";
                modelo_producto = await _servicioApi.ObtenerProductoCategoria(Id);


                ViewBag.CategoriaID = modelo_producto.CategoriaID;
            }


            List<ResultadoCategoria> lista = await _servicioApi.ListaCategoria();
            ViewBag.Categoria = lista;

            ViewBag.CategoriaID = 0;



            return View(modelo_producto);
        }


        [HttpPost]
        public async Task<IActionResult> GuardarCambiosProducto(ProductoRequest ob_producto)
        {

            bool respuesta;

            if (ob_producto.Id == null || ob_producto.Id==0)
            {
                respuesta = await _servicioApi.GuardarProducto(ob_producto);
            }
            else
            {
                respuesta = await _servicioApi.EditarProducto(ob_producto);
            }


            if (respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();

        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambiosProductoCategoria(ProductoCategoria ob_producto)
        {

            bool respuesta;

            if (ob_producto.Id == null || ob_producto.Id == 0)
            {
                respuesta = await _servicioApi.GuardarProductoCategoria(ob_producto);
            }
            else
            {
                respuesta = await _servicioApi.EditarProductoCategoria(ob_producto);
            }

            if (respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();

        }


        [HttpGet]
        public async Task<IActionResult> Eliminar(int Id)
        {

            var respuesta = await _servicioApi.EliminarProducto(Id);

            if (respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();


        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambiosCategoria(Categoria obj)
        {

            bool respuesta;

            if (obj.CategoriaID == 0)
            {
                respuesta = await _servicioApi.GuardarCategoria(obj);
            }
            else
            {
                respuesta = await _servicioApi.EditarCategoria(obj);
            }

            if (respuesta)
                return RedirectToAction("Lista","Categoria");
            else
                return NoContent();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}