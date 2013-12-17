using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using FluentNHibernate.Mapping;

namespace Dominio.Mappings
{
    public class SalaMapping:ClassMap<Sala>
    {
        public SalaMapping()
        {
            Table("Salas");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Nombre).Length(100).Not.Nullable();
            References(x => x.Ubicacion).Not.LazyLoad();
            HasMany(x => x.Juntas).LazyLoad();
            HasMany(x => x.Proximas).LazyLoad().ApplyFilter<FiltroProximaJunta>();
        }
    }

    public class FiltroProximaJunta : FilterDefinition
    {
        public FiltroProximaJunta()
        {
            WithName("FiltroProximaJunta")
                .WithCondition("Fecha>=:hoy AND Fecha<:manania")
                .AddParameter("hoy", NHibernate.NHibernateUtil.DateTime)
                .AddParameter("manania", NHibernate.NHibernateUtil.DateTime);
        }
    }
}
