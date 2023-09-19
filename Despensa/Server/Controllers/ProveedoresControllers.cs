using Despensa.BD.Data;
using Despensa.BD.Data.Entity;
using Despensa.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Despensa.Server.Controllers
{
    [ApiController]
    [Route("api/Proveedores")]
    public class ProveedoresControllers : ControllerBase
    {
        private readonly Context context;

        public ProveedoresControllers(Context context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Proveedor>>> Get()
        {
            var lista = await context.Proveedores.ToListAsync();
            if (lista == null || lista.Count == 0)
            {
                return BadRequest("No hay proveedores cargados.");
            }

            return lista;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Proveedor?>> Get(int id)
        {
            var existe = await context.Proveedores.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound($"El Proveedor {id} no existe.");
            }
            return await context.Proveedores.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(ProveedorDTO proveedorDTO)
        {
            try
            {
                Proveedor entidad = new Proveedor();

                entidad.Nombre = proveedorDTO.Nombre;
                entidad.Apellido = proveedorDTO.Apellido;
                entidad.RazonSocial = proveedorDTO.RazonSocial;
                entidad.Telefono = proveedorDTO.Telefono;

                await context.AddAsync(entidad);
                await context.SaveChangesAsync();
                return Ok("Se cargo correctamente el Proveedor.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(ProveedorDTO proveedorDTO, int id)
        {
            //comprobar que ese id exista en la base de datos
            var exist = await context.Proveedores.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return BadRequest("El Proveedor no existe.");
            }

            Proveedor entidad = new Proveedor();
            entidad.Id = id;
            entidad.Nombre = proveedorDTO.Nombre;
            entidad.Apellido = proveedorDTO.Apellido;
            entidad.RazonSocial = proveedorDTO.RazonSocial;
            entidad.Telefono = proveedorDTO.Telefono;

            //Actualizar
            context.Update(entidad);
            await context.SaveChangesAsync();
            return Ok("Actualizado con Exito.");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Proveedores.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound($"El Proveedor de id={id} no existe.");
            }

            context.Remove(new Proveedor() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
