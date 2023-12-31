﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Despensa.Shared.DTO
{
    public class UsuarioDTO
    {
        [Required(ErrorMessage = "El DNI del USUARIO es Obligatorio")]
        public int DNI { get; set; }

        [Required(ErrorMessage = "El NOMBRE del USUARIO es Obligatorio")]
        [MaxLength(40, ErrorMessage = "Solo se aceptan hasta 40 caracteres en el NOMBRE del USUARIO")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El APELLIDO del USUARIO es Obligatorio")]
        [MaxLength(40, ErrorMessage = "Solo se aceptan hasta 40 caracteres en el APELLIDO del USUARIO")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El NUMERO del TELÉFONO es Obligatorio")]
        [MaxLength(20, ErrorMessage = "Solo se aceptan hasta 20 caracteres en el TELÉFONO")]
        public string Telefono { get; set; }

        //Relaciones
        [Required(ErrorMessage = "El Rol del Usuario es Obligatorio")]
        public int RolId { get; set; }
    }
}
