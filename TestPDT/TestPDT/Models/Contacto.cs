using System;
using System.Collections.Generic;

#nullable disable

namespace TestPDT.Models
{
    public partial class Contacto
    {
        public int Id { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Curp { get; set; }
    }
}
