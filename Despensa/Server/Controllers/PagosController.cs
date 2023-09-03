using Despensa.BD.Data;
using Despensa.BD.Data.Entity;
using Despensa.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Despensa.Server.Controllers
{
    [ApiController]
    [Route("api/Pagos")]
    public class PagosController : ControllerBase
    {
        private readonly Context context;

        public PagosController(Context context)
        {
            this.context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<Pago>>> Get()
        {
            return await context.Pagos.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Pago?>> Get(int id)
        {
            var existe = await context.Pagos.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound($"El pago {id} no existe");
            }
            return await context.Pagos.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(PagoDTO entidad)
        {
            try
            {
                var existe = await context.Usuarios.AnyAsync(x => x.Id == entidad.UsuarioId);
                if (!existe)
                {
                    return NotFound($"El usuario de id={entidad.UsuarioId} no existe");
                }

                Pago pepe = new Pago();

                pepe.ProveedorId = entidad.ProveedorId;
                pepe.UsuarioId = entidad.UsuarioId;
                pepe.Descripcion = entidad.Descripcion;
                pepe.Monto = entidad.Monto;
                pepe.TipoPago = entidad.TipoPago;

                await context.AddAsync(pepe);
                await context.SaveChangesAsync();
                //return pepe.Id;
                return Ok($"Se le asigno el monto {pepe.Monto} al proveedor de id: {pepe.ProveedorId} ");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")] // api/pagos/2
        public async Task<ActionResult> Put(Pago entidad, int id)
        {
            if (id != entidad.Id)
            {
                return BadRequest("El id del Pago no corresponde");
            }

            var existe = await context.Pagos.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound($"El Pago de id={id} no existe");
            }

            context.Update(entidad);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Pagos.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound($"El Pago de id={id} no existe");
            }

            context.Remove(new Pago() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
