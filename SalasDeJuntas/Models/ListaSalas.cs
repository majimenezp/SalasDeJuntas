using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalasDeJuntas.Models
{
    public class UbicacionSala
    {
        public string NombreUbicacion { get; set; }
        public List<Sala> Salas { get; set; }

        internal static List<UbicacionSala> CrearListado(List<Sala> salas)
        {
            List<UbicacionSala> lista = new List<UbicacionSala>();
            foreach (var ubicacion in salas.Select(x => x.Ubicacion).Distinct())
            {
                lista.Add(new UbicacionSala()
                {
                    NombreUbicacion = ubicacion.Nombre,
                    Salas = salas.Where(x => x.Ubicacion == ubicacion).ToList()
                });
            }
            return lista;
        }
    }
}