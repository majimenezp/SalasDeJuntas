using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Entidades
{
   public class Junta
    {
        public virtual int Id { get; set; }

        [IgnoreDataMember]
        public virtual Sala Sala { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual string Comentario { get; set; }

       [IgnoreDataMember]
        public virtual TimeSpan HoraInicio { get; set; }

       [IgnoreDataMember]
        public virtual TimeSpan HoraFin { get; set; }

       public virtual string HoraInicioJs { get { return HoraInicio.ToString(@"hh\:mm"); } }
       public virtual string HoraFinJs { get { return HoraFin.ToString(@"hh\:mm"); } }
        public virtual DateTime Fecha { get; set; }

        public virtual string Hora()
        {
            return HoraInicio.ToString(@"hh\:mm") +" - "+ HoraFin.ToString(@"hh\:mm");
        }

    }
}
