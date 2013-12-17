using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
   public class Junta
    {
        public virtual int Id { get; set; }
        public virtual Sala Sala { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual string Comentario { get; set; }
        public virtual TimeSpan HoraInicio { get; set; }
        public virtual TimeSpan HoraFin { get; set; }
        public virtual DateTime Fecha { get; set; }

        public virtual string Hora()
        {
            return HoraInicio.ToString(@"hh\:mm") +" - "+ HoraFin.ToString(@"hh\:mm");
        }

    }
}
