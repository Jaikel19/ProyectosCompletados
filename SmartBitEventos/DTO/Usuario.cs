using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class Usuario
    {
        public Usuario()
        { }

        public int? Id { get; set; }

        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Contiene caracteres inválidos")]
        [Required(ErrorMessage = "Nombre requerido")]
        public string Nombre { get; set; }

        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Contiene caracteres inválidos")]
        [Required(ErrorMessage = "Apellido requerido")]
        public string Apellido { get; set; }

        //[RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Seguir formato de email, ejemplo: usuario@dominio.com")]
       // [Required(ErrorMessage = "Email requerido")]
       
        public string Email { get; set; }

       // [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$", ErrorMessage = "Debe contener 8 caracteres 1 mayúscula y 1 número")]
        //[Required(ErrorMessage = "Contraseña requerida")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }


        [RegularExpression(@"^\d{8}$", ErrorMessage = "Debe ser un número  de 8 dígitos")]
        [Required(ErrorMessage = "teléfono requerido")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Debe ser un número  de 10 dígitos")]
        [Required(ErrorMessage = "Cédula  requerida")]
        [Display(Name = "Cédula")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "Imagen de cédula requerida")]
        public string CedulaImagen { get; set; }
        public bool CuentaConfirmada { get; set; }
        public int Rol { get; set; }
        public bool Activo { get; set; }
        public bool Bloqueado { get; set; }
        public string NombreUsuario { get; set; }
        public int IdUsuarioCreacion { get; set; }
        [Required(ErrorMessage = "Seleccionar dirección en el mapa")]

        public string Latitud { get; set; }
        [Required(ErrorMessage = "Seleccionar dirección en el mapa")]
        public string Longitud { get; set; }

    }
}
