using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Despensa.BD.Data.Entity
{
    public class Rol
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El NOMBRE del ROL es Obligatorio")]
        [MaxLength(20, ErrorMessage = "Solo se aceptan hasta 20 caracteres en el NOMBRE del ROL")]
        public string NombreRol { get; set; }

        //Relaciones
        public List<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
