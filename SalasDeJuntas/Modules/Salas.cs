using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Dominio;
namespace SalasDeJuntas.Modules
{
    public class Salas:NancyModule
    {
        public Salas():base("Salas")
        {
            Get["/"] = x =>
            {

                return View["Index.cshtml"];
            };
            Get["/{id}"] = x =>
            {
                var sala = DAL.Instance.ObtenerSala(x.id);
                return View["Detalle.cshtml"];
            };
        }
    }
}