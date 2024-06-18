using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EletricaMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EletricaMvc.Controllers
{
    public class ClientesController : Controller
    {
        public string uribase = "";


        [HttpGet]
        public async Task<ActionResult> IndexAsync()
        {
            try
            {
                string uriComplementar = "GetAll";
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await httpClient.GetAsync(uribase + uriComplementar);
                string serialized = await response.Content.ReadAsStringAsync();

                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<UsuarioViewModel> listaPersonagens = await Task.Run(() =>
                        JsonConvert.DeserializeObject<List<UsuarioViewModel>>(serialized));
                        
                        return View(listaPersonagens);    
                }
                    
                
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("index");
            }
        }
       


  [HttpPost]
        public async Task<ActionResult> CreateAsync(UsuarioViewModel p)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var content = new StringContent(JsonConvert.SerializeObject(p));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.GetAsync(uribase);
                string serialized = await response.Content.ReadAsStringAsync();

                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {   
                    TempData["Mensagem"] = string.Format("Cliente {0}, Id {1} salvo com sucesso!", p.NomeCliente, serialized);
                    return RedirectToAction("index");       
                }
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("index");
            }
        } 

      




       [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> DetailsAsync(int? id)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await httpClient.GetAsync(uribase + id.ToString);
                string serialized = await response.Content.ReadAsStringAsync();

                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {   
                    UsuarioViewModel p = await Task.Run(() =>
                    JsonConvert.DeserializeObject<UsuarioViewModel>(serialized));
                    return View(p);       
                }
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("index");
            }
        }




















    }
}