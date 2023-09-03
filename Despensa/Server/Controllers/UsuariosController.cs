using Despensa.BD.Data;
using Despensa.BD.Data.Entity;
using Despensa.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Despensa.Server.Controllers
{
    [ApiController]
    [Route("api/Usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly Context context;

        public UsuariosController(Context context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get()
        {
            var pepe = await context.Usuarios.ToListAsync();
            if (pepe==null || pepe.Count == 0)
            {
                return BadRequest("No existen usuarios");
            }
            return pepe;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Usuario?>> Get(int id)
        {
            var existe = await context.Usuarios.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound($"El usuario {id} no existe");
            }
            return await context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(UsuarioDTO entidad)
        {
            try
            {
                var existe = await context.Roles.AnyAsync(x => x.Id == entidad.RolId);
                if (!existe)
                {
                    return NotFound($"El rol de id={entidad.RolId} no existe");
                }

                Usuario pepe = new Usuario();

                pepe.RolId = entidad.RolId;
                pepe.DNI = entidad.DNI;
                pepe.Nombre = entidad.Nombre;
                pepe.Apellido = entidad.Apellido;
                pepe.Telefono = entidad.Telefono;

                await context.AddAsync(pepe);
                await context.SaveChangesAsync();
                return pepe.Id;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")] // api/roles/2
        public async Task<ActionResult> Put(Usuario entidad, int id)
        {
            if (id != entidad.Id)
            {
                return BadRequest("El id del Usuario no corresponde");
            }

            var existe = await context.Usuarios.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound($"El Usuario de id={id} no existe");
            }

            context.Update(entidad);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Usuarios.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound($"El Usuario de id={id} no existe");
            }
            Usuario pepe = new Usuario();
            pepe.Id = id;

            context.Remove(pepe);

            await context.SaveChangesAsync();

            return Ok();
        }


    }
}
