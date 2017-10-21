using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ProductWebsite.Models;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace ProductWebsite.Controllers
{
    public class HomeController : Controller
    {

        static Products products = new Products();
        byte[] credentials = Encoding.ASCII.GetBytes("scott:tiger");
        public string Model { get; private set; }

        [HttpPost]
       public async Task<ActionResult> CreateProduct(FormCollection form)
        {
            
            var product = new Product();
            product.Brand = form["Brand"].ToString();
            product.Model = form["Model"].ToString();
            product.Description = form["Description"].ToString();           

            string json = JsonConvert.SerializeObject(product);
            var client = CreateHTTPClient();
            HttpResponseMessage response = await client.PostAsync("api/products", new StringContent(json, Encoding.UTF8, "application/json"));

            response = client.GetAsync("api/products").Result;
            if (response.IsSuccessStatusCode)
            {
                using (HttpContent content = response.Content)
                {
                    Task<string> json2 = content.ReadAsStringAsync();
                    products.listOfProducts = JsonConvert.DeserializeObject<List<Product>>(json2.Result);

                }

            }
            else
            {
                
            }
            return View("ViewProduct", products);
        }

        [HttpGet]
        public ActionResult CreateProduct(string id)
        {           
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> ViewProduct()
        {

            var client = CreateHTTPClient();
            HttpResponseMessage response = client.GetAsync("api/products").Result;
            if(response.IsSuccessStatusCode)
            {
                using (HttpContent content = response.Content)
                {
                    Task<string> json = content.ReadAsStringAsync();
                    products.listOfProducts = JsonConvert.DeserializeObject<List<Product>>(json.Result);

                }

            }
            else
            {

            }
            return View(products);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {

            var client = CreateHTTPClient();
            HttpResponseMessage response = await client.DeleteAsync ("api/products/" + id);
         
            
            response =  client.GetAsync("api/products").Result;
            if (response.IsSuccessStatusCode)
            {
                using (HttpContent content = response.Content)
                {
                    Task<string> json = content.ReadAsStringAsync();
                    products.listOfProducts = JsonConvert.DeserializeObject<List<Product>>(json.Result);

                }

            }
            else
            {

            }
            return View("ViewProduct", products);
        }

        [HttpGet]
        public async Task<ActionResult> Update(string id)
        {
           
            Product product = new Product();
            var client = CreateHTTPClient();
            HttpResponseMessage response = await client.GetAsync("api/products/" + id);

            if (response.IsSuccessStatusCode)
            {
                using (HttpContent content = response.Content)
                {
                    Task<string> json = content.ReadAsStringAsync();
                    product = JsonConvert.DeserializeObject<Product>(json.Result);
                }

            }
            return View(product);
        }

        [HttpPost]
        public async Task<ActionResult> Update(FormCollection form)
        {
            
            var product = new Product();
            product.Brand = form["Brand"].ToString();
            product.Model = form["Model"].ToString();
            product.Description = form["Description"].ToString();
            product.Id = form["ID"].ToString();
            string json = JsonConvert.SerializeObject(product);
            var client = CreateHTTPClient();

            HttpResponseMessage response =  await client.PutAsync("api/products/"+product.Id, new StringContent(json, Encoding.UTF8, "application/json"));


            HttpResponseMessage response2 = client.GetAsync("api/products").Result;
            if (response.IsSuccessStatusCode)
            {
                using (HttpContent content = response2.Content)
                {
                    Task<string> json2 = content.ReadAsStringAsync();
                    products.listOfProducts = JsonConvert.DeserializeObject<List<Product>>(json2.Result);

                }

            }
            else
            {

            }
            return View("ViewProduct", products);
        }

        public HttpClient CreateHTTPClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:52424/");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentials));
            return client;
        }



    }
}