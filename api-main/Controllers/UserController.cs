using API_main.Models;
using Microsoft.AspNetCore.Mvc;
using API_main.Repositories;
using Microsoft.AspNetCore.Http; 
using System.IO; 
using System.Threading.Tasks;
using System.Diagnostics;
using System;


namespace API_main.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
        {

        UserRepo repo = new UserRepo();

        public IActionResult GetList()
        {
            try
            {
                List<UserModel> oList = repo.GetAll();
                return Ok(oList);
                
            }
            catch (Exception)
            {
                throw;
            }
        }



        [HttpGet("{id}")]
        public ActionResult GetUser(int id)
        {
            UserModel model = new UserModel();

             model = repo.GetByID(id);
           


                return Ok(model);
        }

        [HttpPut]

        public IActionResult Put(UserModel obj)
        {
            try
            {
                obj.Modifiedby = 1;
                obj.ModifiedDate = DateTime.Now;

                string result = repo.Update(obj);

                if (result == "Success")
                {
                    return Ok("User updated");
                }
                else
                {
                    return BadRequest( "Failed to update..." );
                }
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost]
        public IActionResult AddCreate(UserModel obj)
        {

                try
                {

                        string code = repo.GetMaxUserCode();
                        if (obj.UserTypeID == 3)
                        {
                            obj.User_code = "CL-" + code;
                        }
                        else
                        {
                            obj.User_code = "SE-" + code;
                        }

                        obj.Createdby = 1;
                        obj.CreatedDate = DateTime.Now;

                    string result = repo.Create(obj);
                    if (result == "Success")
                    {
                        return Ok("User created");
                    }
                    else
                    {
                        return BadRequest("Failed to create");
                    }
                    
                }
                catch (Exception ex)
                {
                      return BadRequest(ex.Message);
                }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteRepo(int id)
        {
       
                try
                {
                    string result = repo.Delete(id);
                    if (result == "Success")
                    {
                        return Ok("Deleted successfully");
                    }
                    else
                    {
                        return BadRequest("Delete failed!");
                    }
                }
                catch (Exception ex)
                {
                        return BadRequest(ex.Message);
            }
            }





        [HttpPost("{id}")]
        public async Task<IActionResult> UploadImage(int id, IFormFile fileUpload)
        {
            if (fileUpload != null && fileUpload.Length > 0)
            {
                string fileName = Path.GetFileName(fileUpload.FileName);
                string uploadPath = "D:\\Users\\Eryn\\source\\repos\\API-main\\API-Consume\\wwwroot\\UploadedImages\\";

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                string filePath = Path.Combine(uploadPath, fileName);

                if (System.IO.File.Exists(filePath))
                {
                    return Conflict("A file with the same name already exists. Please rename the file and try again.");
                  
                }

                try
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await fileUpload.CopyToAsync(stream);
                    }

         
                    string result = repo.AddImage(id, fileName);

                    if (result == "Success")
                    {
                        return Ok("Uploaded successfully!");
                    }
                    else
                    {
                        return BadRequest("Failed to update the student's image.");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest( "Error saving the image: " + ex.Message);
                }
            }
            else
            {
                return BadRequest("Upload a valid image.");
            }
        }



        [HttpGet("{id}")]
        public IActionResult GetUserImage(int id)
        {
            string imageName = repo.GetEmployeeImage(id); 

            if (!string.IsNullOrEmpty(imageName) && imageName != "not")
            {
                string imagePath = Url.Content("~/UploadedImages/" + imageName);
                Debug.WriteLine($"Image Name: {imageName}, Image Path: {imagePath}");
                return Ok( imagePath);
            }

            return NotFound(); 
        }


    }

}


