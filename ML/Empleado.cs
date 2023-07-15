﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Empleado
    {
        public string NumeroEmpleado { get; set; }
        public string Rfc { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string FechaNacimiento { get; set; }
        public string Nss { get; set; }
        public string FechaIngreso { get; set; }
        public string? Foto { get; set; }
        public Empresa Empresa { get; set; }
        public List<Object>? Empleados { get; set; }
        public string Action { get; set; }


        public string NombreCompleto { get; set; }

    }
}
