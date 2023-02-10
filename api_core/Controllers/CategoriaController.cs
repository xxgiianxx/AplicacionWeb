using api_core.Models;
using api_core.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace api_core.Controllers
{
    public class CategoriaController : Controller
    {       
        private IServicio_API _servicioApi;


        public CategoriaController(IServicio_API servicioApi)
        {
            _servicioApi = servicioApi;
        }

        public async Task<IActionResult> Lista()
        {
            List<ResultadoCategoria> lista = await _servicioApi.ListaCategoria();
            return View(lista);
        }


        public async Task<IActionResult> Editar(int IdCategoria)
        {

            Categoria categoria = new Categoria();

            ViewBag.Accion = "Nueva Categoria";

            if (IdCategoria != null)
            {

                ViewBag.Accion = "Editar Producto";
                categoria = await _servicioApi.ObtenerCategoria(IdCategoria);
            }

            return View(categoria);
        }


    }
}
