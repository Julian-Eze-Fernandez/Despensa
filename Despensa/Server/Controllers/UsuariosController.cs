using Despensa.BD.Data;
using Despensa.BD.Data.Entity;
using Despensa.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

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
            var lista = await context.Usuarios.ToListAsync();
            if (lista == null || lista.Count == 0)
            {
                return BadRequest("No hay usuarios cargados.");
            }

            return lista;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Usuario?>> Get(int id)
        {
            var existe = await context.Usuarios.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound($"El Usuario {id} no existe.");
            }
            return await context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(UsuarioDTO usuarioDTO)
        {
            try
            {
                Usuario entidad = new Usuario();

                entidad.RolId = usuarioDTO.RolId;
                entidad.DNI = usuarioDTO.DNI;
                entidad.Nombre = usuarioDTO.Nombre;
                entidad.Apellido = usuarioDTO.Apellido;
                entidad.Telefono = usuarioDTO.Telefono;

                await context.AddAsync(entidad);
                await context.SaveChangesAsync();
                return Ok("Se cargo correctamente el Usuario.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(UsuarioDTO usuarioDTO, int id)
        {
            //comprobar que ese id exista en la base de datos
            var exist = await context.Usuarios.AnyAsync(e => e.Id == id);
            if (!exist)
            {
                return BadRequest("El Usuario no existe.");
            }

            Usuario entidad = new Usuario();
            entidad.Id = id;
            entidad.DNI = usuarioDTO.DNI;
            entidad.Nombre = usuarioDTO.Nombre;
            entidad.Apellido = usuarioDTO.Apellido;
            entidad.Telefono = usuarioDTO.Telefono;
            entidad.RolId = usuarioDTO.RolId;

            //actualizar
            context.Update(entidad);
            await context.SaveChangesAsync();
            return Ok("Actualizado con Exito.");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Usuarios.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound($"El Usuario de id={id} no existe.");
            }
            Usuario entidad = new Usuario();
            entidad.Id = id;

            context.Remove(entidad);

            await context.SaveChangesAsync();

            return Ok();
        }


    }
}
