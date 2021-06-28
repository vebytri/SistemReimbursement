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

namespace ReimbursementFrontEnd.Controllers
{
    //[Authorize]

    public class HomeController : BaseController<User, UserRepository, int>
    {
      UserRepository repository;

        public HomeController(UserRepository repository) : base(repository)
        {
            this.repository = repository;
        }


        //[AllowAnonymous]
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
            ViewBag.sessionRole = tokenS.Claims.First(claim => claim.Type == "Role").Value;
            ViewBag.sessionName = tokenS.Claims.First(claim => claim.Type == "FirstName").Value;



            //ViewBag.session = HttpContext.Session.GetString("JWToken");

            //HttpContext.Session.SetString("SessionName", "Farras");
            //HttpContext.Session.SetString("JWToken", jwToken.Token);



            return View();
        }
        //[AllowAnonymous]
        //public IActionResult Home()
        //{
        //    return View();
        //}

        [AllowAnonymous]
        public async Task<JsonResult>getsemuadata()
        {
            var result = await repository.GetAllProfile();
            return Json(result);
        }

        public IActionResult Employee()
        {
            ViewBag.session = HttpContext.Session.GetString("FirstName");
            return View();
        }

        public IActionResult Manager()
        {
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