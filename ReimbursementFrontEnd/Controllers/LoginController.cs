using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ReimbursementFrontEnd.Base;
using ReimbursementFrontEnd.Models;
using ReimbursementFrontEnd.Repository.Data;
using SistemReimbursement.Models;
using SistemReimbursement.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementFrontEnd.Controllers
{
    public class LoginController : BaseController<User, UserRepository, int>
    {
       UserRepository repository;
        LoginRepository loginRep;


        public LoginController(UserRepository repository, LoginRepository loginRep) : base(repository)
        {
            this.repository = repository;
            this.loginRep = loginRep;

        }
        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult Register()
        //{
        //    return View();
        //}


        [HttpPost]
        public async Task<IActionResult> Auth(LoginVM loginVM)
        {
            var jwToken = await loginRep.Auth(loginVM);
            if (jwToken == null)
            {
     
                return RedirectToAction("index");
            }

            HttpContext.Session.SetString("JWToken", jwToken.Token);
            return RedirectToAction("index", "home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");

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