using api_core.Models;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using api_core.Models.Request;
using api_core.Models.Response;

namespace api_core.Servicios
{
    public class Servicio_API : IServicio_API
    {
        private static string _usuario;
        private static string _clave;
        private static string _baseurl;
        private static string _token;

        public Servicio_API()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            _usuario = builder.GetSection("ApiSettings:usuario").Value;
            _clave = builder.GetSection("ApiSettings:clave").Value;
            _baseurl = builder.GetSection("ApiSettings:baseUrl").Value;
        }

        public async Task Autenticar()
        {

            var cliente = new HttpClient();

            cliente.BaseAddress = new Uri(_baseurl);

            var credenciales = new Credencial() { Name = _usuario, Password = _clave };

            var content = new StringContent(JsonConvert.SerializeObject(credenciales), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync("api/Usuario/authenticate", content);

            var json_respuesta = await response.Content.ReadAsStringAsync();

            var resultado = JsonConvert.DeserializeObject<ResultadoCredencial>(json_respuesta);

            _token = resultado.Token;
        }

        public async Task<List<Producto>> ListaProducto()
        {

            List<Producto> lista = new List<Producto>();
            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.GetAsync("api/Producto/ListaProductos");

            if (response.IsSuccessStatusCode)
            {

                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_respuesta);
                lista = resultado.listaProducto;
            } 

            return lista;
           
        }


        public async Task<List<ProductoCategoria>> ListaProductoCategoria()
        {

            List<ProductoCategoria> lista = new List<ProductoCategoria>();

            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.GetAsync("api/Producto/ListaProductoCategoria");

            if (response.IsSuccessStatusCode)
            {

                var json_respuesta = await response.Content.ReadAsStringAsync();
                var dynamicObject = JsonConvert.DeserializeObject<List<ProductoCategoria>>(json_respuesta)!;
                lista = dynamicObject;
            }

            return lista;

        }

        public async Task<Producto> ObtenerProducto(int idProducto)
        {
            Producto objeto = new Producto();
            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.GetAsync($"api/Productos/Obtener/{idProducto}");

            if (response.IsSuccessStatusCode)
            {

                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_respuesta);
                objeto = resultado.objetoProducto;
            }
            return objeto;
        }

        public async Task<ProductoCategoria> ObtenerProductoCategoria(int Id)
        {
            ProductoCategoria objeto = new ProductoCategoria();
            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.GetAsync($"api/Producto/ObtenerProductoCategoria/{Id}");

            if (response.IsSuccessStatusCode)
            {

             
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var dynamicObject = JsonConvert.DeserializeObject<ProductoCategoria>(json_respuesta)!;
                objeto = dynamicObject;
            }
            return objeto;
        }

        public async Task<bool> GuardarProducto(ProductoRequest objeto)
        {
            bool respuesta = false;
            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync("api/Producto/InsertaProducto", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<bool> GuardarProductoCategoria(ProductoCategoria objeto)
        {
            bool respuesta = false;
            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync("api/Producto/InsertaProducto", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }



        public async Task<bool> EditarProducto(ProductoRequest objeto)
        {
            bool respuesta = false;
            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await cliente.PutAsync($"api/Producto/ModificarProducto", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }


        public async Task<bool> EditarProductoCategoria(ProductoCategoria objeto)
        {
            bool respuesta = false;
            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await cliente.PutAsync($"api/Producto/ModificarProducto/{objeto.Id}", content);



            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }


        public async Task<bool> EliminarProducto(int Id)
        {
            bool respuesta = false;
            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);

            var response = await cliente.DeleteAsync($"api/Producto/EliminarProducto/{Id}");

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;

        }


        public async Task<List<ResultadoCategoria>> ListaCategoria()
        {

            List<ResultadoCategoria> lista = new List<ResultadoCategoria>();
            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.GetAsync("api/Categoria/ListaCategorias");

            if (response.IsSuccessStatusCode)
            {

                var json_respuesta = await response.Content.ReadAsStringAsync();
                var  dynamicObject = JsonConvert.DeserializeObject<List<ResultadoCategoria>>(json_respuesta)!;
                lista = dynamicObject;
            }

            return lista;

        }
        public async Task<bool> GuardarCategoria(Categoria objeto)
        {
            bool respuesta = false;
            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync("api/categoria/InsertaCategoria", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }
        public async Task<bool> EditarCategoria(Categoria objeto)
        {
            bool respuesta = false;
            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await cliente.PutAsync($"api/Categoria/ModificarProducto", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }


        public async Task<Categoria> ObtenerCategoria(int IdCategoria)
        {
            Categoria objeto = new Categoria();
            await Autenticar();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.GetAsync($"api/Categoria/Obtener/{IdCategoria}");

            if (response.IsSuccessStatusCode)
            {

                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_respuesta);
                objeto = resultado.objetoCategoria;
            }
            return objeto;
        }


    }
}
