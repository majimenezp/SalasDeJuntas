using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Linq;
using NHibernate.Criterion;
using NHibernate;
using System.Diagnostics;
namespace Dominio
{
    public sealed partial class DAL
    {
        public List<Sala> ObtenerSalas()
        {
            List<Sala> resultado = new List<Sala>();
            using (var sesion = this.currentSession.OpenSession())
            {
                sesion.EnableFilter("FiltroProximaJunta").SetParameter("hoy",DateTime.Now.Date).SetParameter("manania",DateTime.Now.Date.AddDays(1));
                using (var trans = sesion.BeginTransaction())
                {
                    resultado=sesion.Query<Sala>().Fetch(x=>x.Proximas).OrderBy(x => x.Ubicacion).ThenBy(x => x.Nombre).ToList();
                    trans.Commit();
                }
            }
            return resultado;
        }

        public Sala ObtenerSala(int id)
        {
            Sala resultado;
            
            using (var sesion = this.currentSession.OpenSession())
            {
                using (var trans = sesion.BeginTransaction())
                {
                    resultado = sesion.Get<Sala>(id);
                    trans.Commit();
                }
            }
            return resultado;
        }

        public Junta[] ObtenerJuntasSalaPorFecha(int idSala, DateTime fecha)
        {
            Junta[] resultado = new Junta[]{};
            using (var sesion = this.currentSession.OpenSession())
            {
                using (var trans = sesion.BeginTransaction())
                {
                    resultado=sesion.Query<Junta>().Where(x => x.Fecha.Date == fecha && x.Sala.Id == idSala).ToArray();
                    trans.Commit();
                }
            }
            return resultado;
        }


    }

    public class SQLInterceptor:EmptyInterceptor
    {
        public override NHibernate.SqlCommand.SqlString OnPrepareStatement(NHibernate.SqlCommand.SqlString sql)
        {
            Debug.WriteLine(sql);
            return base.OnPrepareStatement(sql);
        }
    }
}
