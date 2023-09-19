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
            var lista = await context.Pagos.ToListAsync();
            if (lista == null || lista.Count == 0)
            {
                return BadRequest("No hay pagos cargados.");
            }

            return lista;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Pago?>> Get(int id)
        {
            var existe = await context.Pagos.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound($"El Pago {id} no existe.");
            }
            return await context.Pagos.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(PagoDTO pagoDTO)
        {
            try
            {
                var existe = await context.Usuarios.AnyAsync(x => x.Id == pagoDTO.UsuarioId);
                if (!existe)
                {
                    return NotFound($"El usuario de id={pagoDTO.UsuarioId} no existe.");
                }

                Pago entidad = new Pago();

                entidad.ProveedorId = pagoDTO.ProveedorId;
                entidad.UsuarioId = pagoDTO.UsuarioId;
                entidad.Descripcion = pagoDTO.Descripcion;
                entidad.Monto = pagoDTO.Monto;
                entidad.TipoPago = pagoDTO.TipoPago;

                await context.AddAsync(entidad);
                await context.SaveChangesAsync();
                //return pepe.Id;
                return Ok($"Se le asigno el monto {entidad.Monto} al proveedor {entidad.ProveedorId}.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(PagoDTO pagoDTO, int id)
        {
            //comprobar que ese id exista en la base de datos
            var exist = await context.Pagos.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return BadRequest("El Pago no existe.");
            }

            Pago entidad = new Pago();
            entidad.Id = id;
            entidad.Descripcion = pagoDTO.Descripcion;
            entidad.Monto = pagoDTO.Monto;
            entidad.TipoPago = pagoDTO.TipoPago;
            entidad.UsuarioId = pagoDTO.UsuarioId;
            entidad.ProveedorId = pagoDTO.ProveedorId;

            //Actualizar
            context.Update(entidad);
            await context.SaveChangesAsync();
            return Ok("Actualizado con Exito.");
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
