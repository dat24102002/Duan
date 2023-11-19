using AppData.IRepositories;
using AppData.Models;
using AppData.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiaTriController : ControllerBase
    {
        private readonly IAllRepository<GiaTri> repos;
        AssignmentDBContext context = new AssignmentDBContext();
        public GiaTriController()
        {
            repos = new AllRepository<GiaTri>(context, context.GiaTris);
        }
        // GET: api/<GiaTriController>
        [HttpGet]
        public List<GiaTri> Get()
        {
            return repos.GetAll();
        }

        // GET api/<GiaTriController>/5
        [HttpGet("{id}")]
        public GiaTri Get(Guid id)
        {
            return repos.GetAll().FirstOrDefault(x => x.ID == id);
        }

        // POST api/<GiaTriController>
        [HttpPost("Create_GiaTri")]
        public bool Post(string ten, Guid idThuocTinh)
        {
            GiaTri gTri = new GiaTri();
            gTri.ID = Guid.NewGuid();
            gTri.Ten = ten;
            gTri.IdThuocTinh = idThuocTinh;
            return repos.Add(gTri);
        }

        // PUT api/<GiaTriController>/5
        [HttpPut("Update-GiaTri")]
        public bool Put(Guid id, string ten, Guid idThuocTinh)
        {
            var gTri = repos.GetAll().FirstOrDefault(x => x.ID == id);
            if (gTri != null)
            {
                gTri.Ten = ten;
                gTri.IdThuocTinh = idThuocTinh;
                return repos.Update(gTri);
            }
            else
            {
                return false;
            }
        }

        // DELETE api/<GiaTriController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var gTri = repos.GetAll().FirstOrDefault(x => x.ID == id);
            if (gTri != null)
            {
                return repos.Delete(gTri);
            }
            else
            {
                return false;
            }
        }
    }
}
