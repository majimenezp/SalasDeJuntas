using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FluentNHibernate.Cfg;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Linq;
using NHibernate.Criterion;
using FluentNHibernate.Cfg.Db;
using Entidades;

namespace Dominio
{
    public sealed partial class DAL
    {
        private static readonly Lazy<DAL> instance = new Lazy<DAL>(() => new DAL());

        ISessionFactory currentSession;
        private DAL()
        {
            this.currentSession = Fluently.Configure().Database(MsSqlConfiguration.MsSql2008.ConnectionString(C => C.FromConnectionStringWithKey("Conexion")))
                .Mappings(M => M.FluentMappings.AddFromAssemblyOf<DAL>())
                .ExposeConfiguration(cfg =>
                {
                    BuildSchema(cfg);
                })
                .BuildSessionFactory();
        }

        private void BuildSchema(Configuration cfg)
        {
            new SchemaUpdate(cfg).Execute(false, true);
        }

        public static DAL Instance { get { return instance.Value; } }

        
    }
}
