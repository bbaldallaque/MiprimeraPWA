using System;
using System.Collections.Generic;

namespace PWA.WEB.Models
{
    public partial class Libro
    {
        public Libro()
        {
            DetalleReservas = new HashSet<DetalleReserva>();
        }

        public int Iidlibro { get; set; }
        public string? Titulo { get; set; }
        public string? Resumen { get; set; }
        public int? Numpaginas { get; set; }
        public int? Stock { get; set; }
        public int? Bhabilitado { get; set; }
        public int? Iidautor { get; set; }
        public string? Nombrearchivo { get; set; }
        public byte[]? Archivo { get; set; }

        public virtual Autor? IidautorNavigation { get; set; }
        public virtual ICollection<DetalleReserva> DetalleReservas { get; set; }
    }
}
