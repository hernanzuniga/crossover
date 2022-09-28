using Back.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Back.Controllers
{
    public class AccesoController : ApiController
    {
        //[HttpGet]
        //public VM_Response HolaMundo()
        //{
        //    VM_Response vm = new VM_Response();
        //    vm.Status = 1;
        //    vm.Mensaje = "Hola Mundo";

        //    return vm;
        //}

        //[HttpPost]
        //public VM_Response ValidarUsuario([FromBody]VM_Usuario user)
        //{
        //    VM_Response vm = new VM_Response();
        //    vm.Status = 1;
        //    vm.Mensaje = "Hola Mundo";
        //    vm.Resultado = user;

        //    return vm;
        //}

        [HttpPost]
        public VM_Response Valida(VM_Usuario user)
        {
            VM_Response vm = new VM_Response();
            

            RespuestaServicio response = new RespuestaServicio();
            try
            {
                var _apiUrl = "http://restapi.adequateshop.com/api/authaccount/login";

                WebClient client = new WebClient();
                client.Headers["Content-type"] = "application/json";
                //client.Headers["User-Agent"] = "request";
                client.Encoding = Encoding.UTF8;

                dynamic data = new reqUser();
                data.email = "nano@gmail.com";
                data.password = 112233;

                // convert to JSON
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);

                //POST
                string respuestaJson = client.UploadString(_apiUrl, json);

                //GET
                //string respuestaJson = client.DownloadString(url);

                response = JsonConvert.DeserializeObject<RespuestaServicio>(respuestaJson);

                vm.Status = 1;
                vm.Mensaje = "";
                vm.Resultado = response;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return vm;
        }

        //[HttpPost]
        //public IHttpActionResult Valida(VM_Usuario user)
        //{
        //    VM_Response vm = new VM_Response();
        //    vm.Status = 1;
        //    vm.Mensaje = "Hola Mundo";
        //    vm.Resultado = user;

        //    RespuestaServicio response = new RespuestaServicio();
        //    try
        //    {
        //        var _apiUrl = "http://restapi.adequateshop.com/api/authaccount/login";

        //        WebClient client = new WebClient();
        //        client.Headers["Content-type"] = "application/json";
        //        //client.Headers["User-Agent"] = "request";
        //        client.Encoding = Encoding.UTF8;

        //        dynamic data = new reqUser();
        //        data.email = "nano@gmail.com";
        //        data.password = 112233;

        //        // convert to JSON
        //        string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);

        //        //POST
        //        string respuestaJson = client.UploadString(_apiUrl, json);

        //        //GET
        //        //string respuestaJson = client.DownloadString(url);

        //        response = JsonConvert.DeserializeObject<RespuestaServicio>(respuestaJson);
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = ex.Message;
        //    }
        //    return Ok("éxito");
        //}
    }

    public class reqUser
    {
        public string email { get; set; }
        public int password { get; set; }
    }

    public class Data
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }

    public class RespuestaServicio
    {
        public int code { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }
}
