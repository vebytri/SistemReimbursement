using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
  //  [Authorize]
    [EnableCors("AllowOrigin")]
    public class UsersController : BaseController<User, UserRepository, int>
    {
        UserRepository repo;
        public UsersController(UserRepository user) : base(user) { 
            this.repo = user;
        }
      //  [AllowAnonymous]
        [HttpPost("register")]
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
        [HttpPost]
        [Route("forgotpassword")]
        public ActionResult forgotpassword(LoginVM login)
        {

            var get = repo.forgotpassword(login);

            if (get > 0)
            {
                return Ok("Send Email Reset Password Berhasil");
            }
            else
            {
                return BadRequest("Email Tidak Ada");
            }
        }
        [HttpPost]
        [Route("updatepassword")]
        public ActionResult updatepassword(LoginVM login)
        {

            var get = repo.updatepassword(login);

            if (get > 0)
            {
                return Ok("Update Password Berhasil");
            }
            else
            {
                return BadRequest("Gagal Update Password");
            }
        }
        // [AllowAnonymous]

        [HttpPost("Login")]
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
                //return Ok($"Login Berhasil \n Token : {repo.GenerateToken(login)}");
                return Ok(new JWTokenVM { Token = repo.GenerateToken(login), Message = "Login Sukses" });
                //return Ok( "Login Sukses");
            }
            else
            {
                return BadRequest("Login Gagal");
            }

        }
        [HttpPost("cekpassword")]
        public ActionResult cekpassword(LoginVM login)
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
                //return Ok($"Login Berhasil \n Token : {repo.GenerateToken(login)}");
                return Ok("Password match");
                //return Ok( "Login Sukses");
            }
            else
            {
                return BadRequest("Login Gagal");
            }

        }

        [HttpPost("updateuser")]
        public ActionResult updateuser(RegisterVM user)
        {
            var update = repo.updateprofiles(user);
            if (update == 0)
            {
                return Ok("Tidak ada perubahan");
            }
            if (update == 1)
            {
                return Ok(update);
            }
            else
            {
                return BadRequest("gagalupdate");
            }

        }
    }
}
