using Despensa.BD.Data;
using Despensa.BD.Data.Entity;
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
        public async Task <ActionResult<List<Usuario>>> Get()
        {
             return await context.Usuarios.ToListAsync();
        }
    }
}
