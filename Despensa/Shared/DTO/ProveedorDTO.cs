using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Despensa.Shared.DTO
{
    public class ProveedorDTO
    {
        [Required(ErrorMessage = "El NOMBRE del PROVEEDOR es Obligatorio")]
        [MaxLength(40, ErrorMessage = "Solo se aceptan hasta 40 caracteres en el NOMBRE")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El APELLIDO del PROVEEDOR es Obligatorio")]
        [MaxLength(40, ErrorMessage = "Solo se aceptan hasta 40 caracteres en el APELLIDO")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "La RAZON SOCIAL del PROVEEDOR es Obligatoria")]
        [MaxLength(40, ErrorMessage = "Solo se aceptan hasta 40 caracteres en la RAZON SOCIAL")]
        public string RazonSocial { get; set; }

        [Required(ErrorMessage = "El NUMERO del TELÉFONO es Obligatorio")]
        [MaxLength(20, ErrorMessage = "Solo se aceptan hasta 20 caracteres en el TELÉFONO")]
        public string Telefono { get; set; }
    }
}
