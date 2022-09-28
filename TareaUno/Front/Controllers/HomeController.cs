using Front.Models;
using Front.Permission;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Front.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        //public async Task<ActionResult> Index2()
        //{
        //    List<RepoGitHub> listado = new List<RepoGitHub>();
        //    using (var client = new HttpClient())
        //    {
        //        //Passing service base url
        //        client.BaseAddress = new Uri("https://api.github.com/");
        //        client.DefaultRequestHeaders.Clear();
        //        //Define request data format
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Add("User-Agent", "request");
        //        //Sending request to find web api REST service resource GetAllEmployees using HttpClient
        //        HttpResponseMessage Res = await client.GetAsync("users/blackmiaool/repos?per_page=10");
        //        //Checking the response is successful or not which is sent using HttpClient
        //        if (Res.IsSuccessStatusCode)
        //        {
        //            //Storing the response details recieved from web api
        //            var EmpResponse = Res.Content.ReadAsStringAsync().Result;
        //            //Deserializing the response recieved from web api and storing into the Employee list
        //            listado = JsonConvert.DeserializeObject<List<RepoGitHub>>(EmpResponse);
        //        }
        //        //returning the employee list to view
        //        return View(listado);
        //    }
        //}


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario oUsuario)
        {
            try
            {
                var _apiUrl = "https://localhost:44356/api/Acceso/Valida";

                WebClient client = new WebClient();
                client.Headers["Content-type"] = "application/json";
                //client.Headers["User-Agent"] = "request";
                client.Encoding = Encoding.UTF8;

                VM_Usuario data = new VM_Usuario();
                data.Mail = oUsuario.Correo;
                data.Password = oUsuario.Clave;

                // convert to JSON
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);

                //POST
                string respuestaJson = client.UploadString(_apiUrl, json);

                //GET
                //string respuestaJson = client.DownloadString(url);

                RequestJson response = JsonConvert.DeserializeObject<RequestJson>(respuestaJson);

                if (response.status == 1)
                {
                    Session["UserSession"] = oUsuario;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["Mensaje"] = "usuario no encontrado";
                    return View();
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                ViewData["Mensaje"] = "usuario no encontrado";
                return View();
            }


        }

        [ValidaSession]
        public ActionResult Index()
        {
            List<RepoGitHub> response = new List<RepoGitHub>();
            try
            {
                var url = "https://api.github.com/users/blackmiaool/repos?per_page=10";
                List<RepoGitHub> e = new List<RepoGitHub>();

                WebClient client = new WebClient();
                client.Headers["Content-type"] = "application/json";
                client.Headers["User-Agent"] = "request";
                client.Encoding = Encoding.UTF8;
                //POST
                //string respuestaJson = client.UploadString(_apiUrl, _parametrosEntrada);

                //GET
                string respuestaJson = client.DownloadString(url);

                JavaScriptSerializer jser = new JavaScriptSerializer();
                response = jser.Deserialize<List<RepoGitHub>>(respuestaJson);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }


            return View(response);
        }
    }

    public class Data
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string token { get; set; }
    }

    public class Resultado
    {
        public int code { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }

    public class RequestJson
    {
        public int status { get; set; }
        public string mensaje { get; set; }
        public Resultado resultado { get; set; }
    }

}