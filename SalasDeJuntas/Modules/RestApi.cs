using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using SalasDeJuntas.Models;
using System.Globalization;
using Dominio;
using Entidades;
namespace SalasDeJuntas.Modules
{
    public class RestApi:NancyModule
    {
        public RestApi():base("API")
        {
            Get["/dias/{Id}"] = x =>
            {
                int idSala = Request.Query.IdSala;
                Dia dia = new Dia();
                dia.fecha = DateTime.ParseExact((string)x.Id, "ddMMyyyy", CultureInfo.InvariantCulture);
                dia.Id = Convert.ToInt32(dia.fecha.ToString("ddMMyyyy"));
                dia.juntas = DAL.Instance.ObtenerJuntasSalaPorFecha(idSala, dia.fecha);
                return Response.AsJson(dia);
            };
        }
    }
}