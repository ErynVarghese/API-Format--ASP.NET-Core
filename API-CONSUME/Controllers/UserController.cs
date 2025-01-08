using Microsoft.AspNetCore.Mvc;
using API_Consume.Models;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.IO;



namespace API_Consume.Controllers
{
    public class UserController : Controller
    {

        Uri baseAddress = new Uri("https://localhost:7142/api");

        private readonly HttpClient _client;

        public UserController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<UserModel> users = new List<UserModel>();

            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress
                + "/user/GetList").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                users =JsonConvert.DeserializeObject<List<UserModel>>(data);
            }

            return View(users);
        }



        [HttpGet]
        public IActionResult AddCreateAPI()
        {
            return View();
        }

        [HttpPost]

        public IActionResult AddCreateAPI(UserModel obj)
        {
            try
            {

                string data = JsonConvert.SerializeObject(obj);
                Debug.WriteLine(data);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress
                    + "/user/AddCreate", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAll");

                } else
                {
                    var errorContent = response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine($"ErrorMe: {errorContent}"); 
                   
                }

            } catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return View();
        }

        [HttpGet]

        public async Task<IActionResult> Edit(int id)
        {
            UserModel obj = new UserModel();

            HttpResponseMessage response = _client.GetAsync($"{_client.BaseAddress}/user/GetUser/{id}").Result;


            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                obj = JsonConvert.DeserializeObject<UserModel>(data);

                var imageResponse = await ViewUserImage(id); // Call the ViewUserImage method

                if (imageResponse is ViewResult viewResult && viewResult.ViewData["ImagePath"] != null)
                {
                    ViewBag.ImagePath = viewResult.ViewData["ImagePath"]; // Set the image path in ViewBag
                }
                else
                {
                    ViewBag.ImagePath = null; // No image found
                }

            }
            else
            {
                var errorContent = response.Content.ReadAsStringAsync().Result;
                Debug.WriteLine($"Error: {errorContent}");

            }
            return View(obj);
        }


        [HttpPost]

        public IActionResult Edit(UserModel obj)
        {
            string data = JsonConvert.SerializeObject(obj);
            Debug.WriteLine(data);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _client.PutAsync(_client.BaseAddress
                + "/user/Put", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAll");
            } else
            {
                return View(obj);
            }
        }




        [HttpGet]
        public IActionResult UploadImage(int id)
        {
            // Pass the id to the view using ViewBag or ViewData
            ViewBag.EmployeeId = id;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UploadImage(int id, IFormFile fileUpload)
        {
            try
            {
                using (var content = new MultipartFormDataContent())


                    if (fileUpload != null && fileUpload.Length > 0)
                    {
                        using (var fileStream = fileUpload.OpenReadStream())
                        {
                            var fileContent = new StreamContent(fileStream);
                            fileContent.Headers.ContentType = new MediaTypeHeaderValue(fileUpload.ContentType);
                            content.Add(fileContent, "fileUpload", fileUpload.FileName);


                            HttpResponseMessage response = await _client.PostAsync($"{_client.BaseAddress}/user/UploadImage/{id}", content);

                            if (response.IsSuccessStatusCode)
                            {
                                TempData["Success"] = "Uploaded successfully!";
                                return RedirectToAction("GetAll");
                            }
                            else
                            {
                                var errorContent = await response.Content.ReadAsStringAsync();
                                Debug.WriteLine($"Error: {errorContent}");
                            }
                        }
                    }
                    else
                    {
                        TempData["Error"] = "Upload a valid image.";
                    }
                
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred: " + ex.Message;
                Debug.WriteLine($"Error: {ex}");
            }

            return View();
        }



        [HttpGet]
        public async Task<IActionResult> ViewUserImage(int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"{_client.BaseAddress}/user/GetUserImage/{id}");

            if (response.IsSuccessStatusCode)
            {
                string imagePath = await response.Content.ReadAsStringAsync();
                ViewBag.ImagePath = imagePath; 
            }
            else
            {
                TempData["Error"] = "Image not found.";
            }

            return View(); 
        }



        [HttpGet]
        public IActionResult Delete(int id = 0)
        {

            HttpResponseMessage response = _client.DeleteAsync($"{_client.BaseAddress}/user/DeleteRepo/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                
            }

            return RedirectToAction("GetAll");
        }
    }
}
