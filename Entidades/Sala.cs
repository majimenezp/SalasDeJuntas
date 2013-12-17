using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class Sala
    {
        public virtual int Id { get; set; }
        public virtual string Nombre { get; set; }
        public virtual Ubicacion Ubicacion { get; set; }
        public virtual IList<Junta> Juntas { get; set; }

        public virtual IList<Junta> Proximas { get; set; }

    }
}
