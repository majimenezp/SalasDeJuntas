using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using FluentNHibernate.Mapping;
namespace Dominio.Mappings
{
    public class UsuarioMapping:ClassMap<Usuario>
    {
        public UsuarioMapping()
        {
            Table("Usuarios");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.NombreCompleto).Length(100).Not.Nullable();
            Map(x => x.UserName).Length(100).Not.Nullable();
        }
    }
}
