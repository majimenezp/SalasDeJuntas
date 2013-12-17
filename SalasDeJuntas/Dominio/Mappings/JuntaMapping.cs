using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using FluentNHibernate.Mapping;

namespace Dominio.Mappings
{
    public class JuntaMapping:ClassMap<Junta>
    {
        public JuntaMapping()
        {
            Table("Juntas");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Descripcion).Not.Nullable().Length(2000);
            Map(x => x.Comentario).Not.Nullable().Length(4000);
            Map(x => x.Fecha).Not.Nullable();
            Map(x => x.HoraInicio).Not.Nullable();
            Map(x => x.HoraFin).Not.Nullable();
            References(x => x.Sala).Fetch.Join().Not.LazyLoad();

        }
    }
}
