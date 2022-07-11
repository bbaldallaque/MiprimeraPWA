using System;
using System.Collections.Generic;

namespace PWA.WEB.Models
{
    public partial class Pai
    {
        public Pai()
        {
            Autors = new HashSet<Autor>();
        }

        public int Iidpais { get; set; }
        public string? Nombre { get; set; }
        public int? Bhabilitado { get; set; }

        public virtual ICollection<Autor> Autors { get; set; }
    }
}
