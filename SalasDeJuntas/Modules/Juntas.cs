using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Dominio;
namespace SalasDeJuntas.Modules
{
    public class Juntas:NancyModule
    {
        public Juntas():base("Juntas")
        {
            Get["/{Id}"] = x =>
            {
                var junta=DAL.Instance.ObtenerJunta((int)x.Id);
                return View["Detalle.cshtml",junta];
            };
            Get["/nueva"] = x =>
            {
                var junta = new Entidades.Junta();
                return View["Nueva.cshtml",junta];
            };
        }
    }
}