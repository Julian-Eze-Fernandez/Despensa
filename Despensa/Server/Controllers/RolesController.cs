using Despensa.BD.Data;
using Despensa.BD.Data.Entity;
using Despensa.Shared.DTO;
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
            var lista = await context.Roles.ToListAsync();
            if (lista == null || lista.Count == 0)
            {
                return BadRequest("No hay roles cargados.");
            }

            return lista;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Rol?>> Get(int id)
        {
            var existe = await context.Roles.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound($"El Rol {id} no existe.");
            }
            return await context.Roles.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(RolDTO rolDTO) 
        {
            try
            {
                Rol nuevaentidad = new Rol();
                nuevaentidad.NombreRol = rolDTO.NombreRol;

                await context.AddAsync(nuevaentidad);
                await context.SaveChangesAsync();
                return Ok("Se cargo correctamente el Rol.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(RolDTO rolDTO, int id)
        {
            //comprobar que ese id exista en la base de datos
            var exist = await context.Roles.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return BadRequest("El Rol no existe.");
            }

            Rol entidad = new Rol();
            entidad.Id = id;
            entidad.NombreRol = rolDTO.NombreRol;

            //Actualizar
            context.Update(entidad);
            await context.SaveChangesAsync();
            return Ok("Actualizado con Exito.");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id) 
        {
            var existe = await context.Roles.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound($"El rol de id={id} no existe.");
            }

            context.Remove(new Rol() { Id=id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
