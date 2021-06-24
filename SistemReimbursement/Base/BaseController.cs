using API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemReimbursement.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class where Repository : IRepository<Entity, Key>
    {
        Repository repo;
        public BaseController(Repository repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        public ActionResult<Entity> Get()
        {
            var get = repo.Get();
            if (get != null)
            {
                return Ok(get);
            }
            else
            {
                return NotFound("Data tidak ada");
            }
        }
        [HttpGet("{key}")]  //gunakan jika menggunakan swagger
                            //   [Route("{key}")] //jng gunakan jika menggunakan swagger
        public ActionResult<Entity> Get(Key key)
        {
            var get = repo.Get(key);
            if (get != null)
            {
                return Ok(get);
            }
            else
            {
                return NotFound("Data tidak ditemukan");
            }
        }
        [HttpPost]
        public ActionResult<Entity> Post(Entity entity)
        {
            var post = repo.Insert(entity);
            if (post != 0)
            {
                return Ok(post);
            }
            else
            {
                return BadRequest("Terjadi Kesalahan ketika insert");
            }
        }
        [HttpDelete("{key}")]
        public ActionResult<Entity> Delete(Key key)
        {
            var del = repo.Delete(key);
            if (del != 0)
            {
                return Ok(del);
            }
            else
            {
                return NotFound("Data tidak bisa dihapus");
            }
        }
        [HttpPut]
        public ActionResult<Entity> Put(Entity entity)
        {
            var update = repo.Update(entity);
            if (update != 0)
            {
                return Ok(update);
            }
            else
            {
                return NotFound("Update gagal, data tidak ditemukan");
            }
        }
    }
}
