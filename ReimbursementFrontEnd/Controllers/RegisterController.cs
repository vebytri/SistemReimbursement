using Microsoft.AspNetCore.Mvc;
using ReimbursementFrontEnd.Base;
using ReimbursementFrontEnd.Repository.Data;
using SistemReimbursement.Models;
using SistemReimbursement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementFrontEnd.Controllers
{
    public class RegisterController : BaseController<User, UserRepository, int>
    {
        UserRepository repo;
        RegisterRepository registerRep;


        public RegisterController(UserRepository repository, RegisterRepository registerRep) : base(repository)
        {
            this.repo = repository;
            this.registerRep = registerRep;


        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult regis(RegisterVM register)
        {

            var get = registerRep.Register(register);

            if (get != null)
            {
                //return Ok("Berhasil Register");
                return RedirectToAction("index","login");
            }
            else
            {
                return BadRequest("Register Gagal");
            }
        }
    }
}
