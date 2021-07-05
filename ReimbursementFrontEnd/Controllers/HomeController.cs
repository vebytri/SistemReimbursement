using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ReimbursementFrontEnd.Base;
using ReimbursementFrontEnd.Models;
using ReimbursementFrontEnd.Repository.Data;
using SistemReimbursement.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using SistemReimbursement.ViewModels;
using System.Net.Http;

namespace ReimbursementFrontEnd.Controllers
{
    [Authorize]

    public class HomeController : BaseController<User, UserRepository, int>
    {
      UserRepository repository;

        public HomeController(UserRepository repository) : base(repository)
        {
            this.repository = repository;
      
        }


        [AllowAnonymous]
        public IActionResult Index()
        {
            //ViewBag.session = HttpContext.Session.GetString("JWToken");
            var token= HttpContext.Session.GetString("JWToken");
            //string apiResponse = token.ToString();
            //var token1 = JsonConvert.DeserializeObject(token);

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;

            ViewBag.sessionNik = tokenS.Claims.First(claim => claim.Type == "NIK").Value;
            ViewBag.sessionRole = tokenS.Claims.First(claim => claim.Type == "role").Value;
            ViewBag.sessionName = tokenS.Claims.First(claim => claim.Type == "FirstName").Value;
            ViewBag.sessionName2 = tokenS.Claims.First(claim => claim.Type == "LastName").Value;
            

            return View();
        }
 

        [AllowAnonymous]
        public async Task<JsonResult>getsemuadata()
        {
            var result = await repository.GetAllProfile();
            return Json(result);
        }

       
        public async Task<JsonResult> GetAllroles()
        {
            string Baseurl = "https://localhost:44383/";
            List<RoleVM> EmpInfo = new List<RoleVM>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);

                using (var response = await client.GetAsync("api/roles"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    EmpInfo = JsonConvert.DeserializeObject<List<RoleVM>>(apiResponse);
                }
                //returning the employee list to view
                return Json(EmpInfo);
            }
        }
            [AllowAnonymous]
        public async Task<IActionResult> TbEmployee()
        {
          
            var token = HttpContext.Session.GetString("JWToken");
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;

            ViewBag.sessionNik = tokenS.Claims.First(claim => claim.Type == "NIK").Value;
            ViewBag.sessionRole = tokenS.Claims.First(claim => claim.Type == "role").Value;
            ViewBag.sessionName = tokenS.Claims.First(claim => claim.Type == "FirstName").Value;
            
            string Baseurl = "https://localhost:44383/";
            List<RoleVM> EmpInfo = new List<RoleVM>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);

                using (var response = await client.GetAsync("api/roles"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    EmpInfo = JsonConvert.DeserializeObject<List<RoleVM>>(apiResponse);
                }
            }
            ViewBag.data = await repository.GetAllProfile();

            ViewBag.allrole = EmpInfo;
            List<RegisterVM> dataperson = ViewBag.data;
            List<RoleVM> datarole = ViewBag.allrole;
            return View();
        }
        [Authorize(Roles = "Employee")]
        public IActionResult Employee()
        {
            //ViewBag.session = HttpContext.Session.GetString("JWToken");
            var token = HttpContext.Session.GetString("JWToken");
            //string apiResponse = token.ToString();
            //var token1 = JsonConvert.DeserializeObject(token);

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;

            ViewBag.sessionNik = tokenS.Claims.First(claim => claim.Type == "NIK").Value;
            ViewBag.sessionRole = tokenS.Claims.First(claim => claim.Type == "role").Value;
            ViewBag.sessionNameFirst = tokenS.Claims.First(claim => claim.Type == "FirstName").Value;
            ViewBag.sessionNameLast = tokenS.Claims.First(claim => claim.Type == "LastName").Value;
            ViewBag.sessionManagerNik= tokenS.Claims.First(claim => claim.Type == "ManagerNik").Value;


            return View();
        }
        //[AllowAnonymous]
        ////[Authorize(Roles = "Finance")]
        //public IActionResult Admin()
        //{
        //    //ViewBag.session = HttpContext.Session.GetString("JWToken");
        //    var token = HttpContext.Session.GetString("JWToken");
        //    //string apiResponse = token.ToString();
        //    //var token1 = JsonConvert.DeserializeObject(token);

        //    var handler = new JwtSecurityTokenHandler();
        //    var jsonToken = handler.ReadToken(token);
        //    var tokenS = jsonToken as JwtSecurityToken;

        //    ViewBag.sessionNik = tokenS.Claims.First(claim => claim.Type == "NIK").Value;
        //    ViewBag.sessionRole = tokenS.Claims.First(claim => claim.Type == "role").Value;
        //    ViewBag.sessionName = tokenS.Claims.First(claim => claim.Type == "FirstName").Value;

        //    return View();
        //}
        [Authorize(Roles = "Finance")]
        public IActionResult Finance()
        {
            //ViewBag.session = HttpContext.Session.GetString("JWToken");
            var token = HttpContext.Session.GetString("JWToken");
            //string apiResponse = token.ToString();
            //var token1 = JsonConvert.DeserializeObject(token);

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;

            ViewBag.sessionNik = tokenS.Claims.First(claim => claim.Type == "NIK").Value;
            ViewBag.sessionRole = tokenS.Claims.First(claim => claim.Type == "role").Value;
            ViewBag.sessionName = tokenS.Claims.First(claim => claim.Type == "FirstName").Value;

            return View();
        }
        [Authorize(Roles = "Manager")]
        public IActionResult Manager()
        {
            //ViewBag.session = HttpContext.Session.GetString("JWToken");
            var token = HttpContext.Session.GetString("JWToken");
            //string apiResponse = token.ToString();
            //var token1 = JsonConvert.DeserializeObject(token);

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;

            ViewBag.sessionNik = tokenS.Claims.First(claim => claim.Type == "NIK").Value;
            ViewBag.sessionRole = tokenS.Claims.First(claim => claim.Type == "role").Value;
            ViewBag.sessionName = tokenS.Claims.First(claim => claim.Type == "FirstName").Value;

            return View();
        }
        ///untuk upload files
        [HttpPost]
        public async Task<IActionResult> upload(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\Files\\");
            var filePaths = new List<string>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    // full path to file in temp location					
                    var filePath = Path.Combine(basePath, formFile.FileName);
                    filePaths.Add(filePath);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            return Ok(new { count = files.Count, size, filePaths });
        }
        [HttpPost]
        public async Task<IActionResult> testupload(IFormFile file)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot\\Files\\");

            var filePath = Path.Combine(basePath, file.FileName);
            var length = file.Length;
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return Ok(filePath);
        }
        public async Task<IActionResult> uploadimgprofile(IFormFile file)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot\\Files\\Profile\\");

            var filePath = Path.Combine(basePath, file.FileName);
            var length = file.Length;
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return Ok(filePath);
        }
        [Authorize(Roles = "Finance")]
        //[AllowAnonymous]

        public IActionResult HistoryAll()
        {
            //ViewBag.session = HttpContext.Session.GetString("JWToken");
            var token = HttpContext.Session.GetString("JWToken");
            //string apiResponse = token.ToString();
            //var token1 = JsonConvert.DeserializeObject(token);

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;

            ViewBag.sessionNik = tokenS.Claims.First(claim => claim.Type == "NIK").Value;
            ViewBag.sessionRole = tokenS.Claims.First(claim => claim.Type == "role").Value;
            ViewBag.sessionName = tokenS.Claims.First(claim => claim.Type == "FirstName").Value;

            return View();
        }

        [Authorize(Roles = "Finance")]
        //[AllowAnonymous]

        public IActionResult HistoryApp()
        {
            //ViewBag.session = HttpContext.Session.GetString("JWToken");
            var token = HttpContext.Session.GetString("JWToken");
            //string apiResponse = token.ToString();
            //var token1 = JsonConvert.DeserializeObject(token);

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;

            ViewBag.sessionNik = tokenS.Claims.First(claim => claim.Type == "NIK").Value;
            ViewBag.sessionRole = tokenS.Claims.First(claim => claim.Type == "role").Value;
            ViewBag.sessionName = tokenS.Claims.First(claim => claim.Type == "FirstName").Value;

            return View();
        }
        [Authorize(Roles = "Finance")]
        //[AllowAnonymous]

        public IActionResult HistoryRej()
        {
            //ViewBag.session = HttpContext.Session.GetString("JWToken");
            var token = HttpContext.Session.GetString("JWToken");
            //string apiResponse = token.ToString();
            //var token1 = JsonConvert.DeserializeObject(token);

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;

            ViewBag.sessionNik = tokenS.Claims.First(claim => claim.Type == "NIK").Value;
            ViewBag.sessionRole = tokenS.Claims.First(claim => claim.Type == "role").Value;
            ViewBag.sessionName = tokenS.Claims.First(claim => claim.Type == "FirstName").Value;

            return View();
        }


        public async  Task<IActionResult> ViewProfile()
        {
            //ViewBag.session = HttpContext.Session.GetString("JWToken");
            var token = HttpContext.Session.GetString("JWToken");
            //string apiResponse = token.ToString();
            //var token1 = JsonConvert.DeserializeObject(token);

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;

            ViewBag.sessionNik = tokenS.Claims.First(claim => claim.Type == "NIK").Value;
            ViewBag.sessionRole = tokenS.Claims.First(claim => claim.Type == "role").Value;
            ViewBag.sessionName = tokenS.Claims.First(claim => claim.Type == "FirstName").Value;
            var id = int.Parse(tokenS.Claims.First(claim => claim.Type == "NIK").Value);
            ViewBag.data = await repository.GetProfilbyId(id);

            return View();
        }
        public async Task<IActionResult> EditProfile()
        {
            //ViewBag.session = HttpContext.Session.GetString("JWToken");
            var token = HttpContext.Session.GetString("JWToken");
            //string apiResponse = token.ToString();
            //var token1 = JsonConvert.DeserializeObject(token);

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;

            ViewBag.sessionNik = tokenS.Claims.First(claim => claim.Type == "NIK").Value;
            ViewBag.sessionRole = tokenS.Claims.First(claim => claim.Type == "role").Value;
            ViewBag.sessionName = tokenS.Claims.First(claim => claim.Type == "FirstName").Value;
            var id = int.Parse(tokenS.Claims.First(claim => claim.Type == "NIK").Value);
            ViewBag.data = await repository.GetProfilbyId(id);

            return View();
        }
        public async Task<IActionResult> UpdatePassword()
        {
            //ViewBag.session = HttpContext.Session.GetString("JWToken");
            var token = HttpContext.Session.GetString("JWToken");
            //string apiResponse = token.ToString();
            //var token1 = JsonConvert.DeserializeObject(token);

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;

            ViewBag.sessionNik = tokenS.Claims.First(claim => claim.Type == "NIK").Value;
            ViewBag.sessionRole = tokenS.Claims.First(claim => claim.Type == "role").Value;
            ViewBag.sessionName = tokenS.Claims.First(claim => claim.Type == "FirstName").Value;
            var id = int.Parse(tokenS.Claims.First(claim => claim.Type == "NIK").Value);
            ViewBag.data = await repository.GetProfilbyId(id);

            return View();
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