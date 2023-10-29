using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Despensa.BD.Data.Entity
{
    public class Pago
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La DESCRIPCIÓN es Obligatoria")]
        [MaxLength(250, ErrorMessage = "Solo se aceptan hasta 250 caracteres en la DESCRIPCIÓN del PAGO")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El MONTO es Obligatorio")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "El TIPO del PAGO es Obligatorio")]
        [MaxLength(15, ErrorMessage = "Solo se aceptan hasta 15 caracteres en el TIPO de PAGO")]
        public string TipoPago { get; set; }

        //Relaciones
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int ProveedorId { get; set; }
        public Proveedor Proveedor { get; set; }
    }
}
