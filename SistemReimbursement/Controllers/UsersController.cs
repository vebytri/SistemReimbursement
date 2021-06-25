using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemReimbursement.Base;
using SistemReimbursement.Models;
using SistemReimbursement.Repository.Data;
using SistemReimbursement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemReimbursement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController<User, UserRepository, int>
    {
        UserRepository repo;
        public UsersController(UserRepository user) : base(user) { 
            this.repo = user;

        }

        [HttpPost]
        [Route("register")]
        public ActionResult Register(RegisterVM register)
        {

            var get = repo.Register(register);

            if (get > 0)
            {
                return Ok("Berhasil Mendaftar");
            }
            else
            {
                return BadRequest("Email telah di daftarkan");
            }
        }

        [Route("Login")]
        [HttpPost]
        public ActionResult Login(LoginVM login)
        {
            var ceklogin = repo.Login(login);
            if (ceklogin == 404)
            {
                return BadRequest("Email Tidak Ditemukan!");
            }
            else if (ceklogin == 401)
            {
                return BadRequest("Password Salah");
            }
            else if (ceklogin == 1)
            {
                //return Ok($"Login Berhasil \n Token : {repo.GenerateToken(loginVM)}");
                //return Ok(new JWTokenVM { Token = repo.GenerateToken(loginVM), Messages = "Login Sukses" });
                return Ok( "Login Sukses");



            }
            else
            {
                return BadRequest("Login Gagal");
            }

        }
    }
}
