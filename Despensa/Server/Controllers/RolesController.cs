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

        [HttpPut("{id:int}")] // api/roles/2
        public async Task<ActionResult> Put(Rol rol, int id)
        {
            if (id != rol.Id)
            {
                return BadRequest("El id del rol no corresponde");
            }

            var existe = await context.Roles.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound($"El rol de id={id} no existe");
            }

            context.Update(rol);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id) 
        {
            var existe = await context.Roles.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound($"El rol de id={id} no existe");
            }

            context.Remove(new Rol() { Id=id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
