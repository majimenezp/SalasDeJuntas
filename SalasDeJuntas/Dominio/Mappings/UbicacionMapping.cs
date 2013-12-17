using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using FluentNHibernate.Mapping;
namespace Dominio.Mappings
{
    public class UbicacionMapping:ClassMap<Ubicacion>
    {
        public UbicacionMapping()
        {
            Table("Ubicaciones");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Nombre).Not.Nullable().Length(300);
        }
    }
}
