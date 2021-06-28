using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReimbursementFrontEnd.Base;
using ReimbursementFrontEnd.Models;
using ReimbursementFrontEnd.Repository.Data;
using SistemReimbursement.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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