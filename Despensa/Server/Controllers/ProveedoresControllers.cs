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
            return await context.Proveedores.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Proveedor?>> Get(int id)
        {
            var existe = await context.Proveedores.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound($"El Proveedor {id} no existe");
            }
            return await context.Proveedores.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Proveedor proveedor)
        {
            //return BadRequest("ERROR DE PRUEBA");
            context.Add(proveedor);
            await context.SaveChangesAsync();
            return proveedor.Id;
        }

        [HttpPut("{id:int}")] // api/roles/2
        public async Task<ActionResult> Put(Proveedor proveedor, int id)
        {
            if (id != proveedor.Id)
            {
                return BadRequest("El id del Proveedor no corresponde");
            }

            var existe = await context.Proveedores.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound($"El Proveedor de id={id} no existe");
            }

            context.Update(proveedor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Proveedores.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound($"El Proveedor de id={id} no existe");
            }

            context.Remove(new Proveedor() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
