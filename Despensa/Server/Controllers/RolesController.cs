using Despensa.BD.Data;
using Despensa.BD.Data.Entity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Despensa.Server.Controllers
{
    [ApiController]
    [Route("api/Roles")]

    public class RolesController : ControllerBase
    {
        private readonly Context context;

        public RolesController(Context context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Rol>>> Get()
        {
            return await context.Roles.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Rol rol) 
        {
            //return BadRequest("ERROR DE PRUEBA");
            context.Add(rol);
            await context.SaveChangesAsync();
            return rol.Id;
        }
    }
}
