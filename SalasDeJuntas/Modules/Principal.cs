using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Dominio;
using SalasDeJuntas.Models;
namespace SalasDeJuntas.Modules
{
    public class Principal:NancyModule
    {
        public Principal()
        {
            Get["/"] = x =>
            {
                var salas = DAL.Instance.ObtenerSalas();
                List<UbicacionSala> listado = UbicacionSala.CrearListado(salas);
                var modelo = new { Lista=listado};
                return View["Index.cshtml",modelo];
            };
        }
    }
}