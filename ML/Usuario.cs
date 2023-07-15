using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {

        public int IdUsuario { get; set; }
        //[Required(ErrorMessage ="Campos Requeridos")]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Solo se aceptan letras")] //valida solo letras
        public string? Nombre { get; set; }
        //[Required(ErrorMessage = "Campos Requeridos")]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Solo se aceptan letras")] //valida solo letras
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        [Required(ErrorMessage = "Campos Requeridos")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Correo Electronico Invalido")]  //Valida Correos Electronicos
        public string? CorreoElectronico { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Imagen { get; set; }
        public Direccion? Direccion { get; set; }
        public bool Estatus { get; set; }
            
        public int? Matricula { get; set; }
        public Rol? Rol { get; set; }
        [Required(ErrorMessage = "Campos Requeridos")]
        [RegularExpression(@"^(?![_ -])(?:(?![_ -]{2})[\w -]){5,16}(?<![_ -])$", ErrorMessage = "Username Invalido")]   //5 a 16 caracteres de longitud  Puede utilizar caracteres alfanuméricos en mayúsculas y minúsculas
                                                                                    //Puede usar guiones bajos, guiones y espacios, pero no puede usar dos seguidos ni comenzar o comenzar 
                                                                                    //el nombre de usuario con ellos.Esto es lo que estoy usando, pero el límite de longitud
        public string? Username { get; set; }
        [Required(ErrorMessage = "Campos Requeridos")]
        [RegularExpression(@"^(?:(?=.*?\p{N})(?=.*?[\p{S}\p{P} ])(?=.*?\p{Lu})(?=.*?\p{Ll}))[^\p{C}]{8,16}$")] //8 a 16 caracteres de longitud Al menos un carácter especial " !"#$%&'()*+,-./:;<=>?@[]^_`{|}~"
                                                                                                               //al menos un numero Al menos un carácter en mayúscula Al menos un carácter en minúscula
        public string? Password { get; set; }
        //[RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\\d\\d$")] //valida formato  dd/mm/yyyy
        public string? FechaNacimiento { get; set; }
        public string? Sexo { get; set; }
        [Required(ErrorMessage = "Campos Requeridos")]
        [RegularExpression(@"^\d{10}$", ErrorMessage ="Solo se aceptan 10 digitos")] //valida que contenga solo 10 digitos
        public string? Telefono { get; set; }
        [Required(ErrorMessage = "Campos Requeridos")]
        [RegularExpression(@"^\d{10}$", ErrorMessage ="Solo se aceptan 10 digitos")]
        public string? Celular { get; set; }
        [Required(ErrorMessage = "Campos Requeridos")]
        //[RegularExpression(@"/^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$/")]
        public string? Curp { get; set; }
        public List<Object>? Usuarios { get; set; }
    }
}
