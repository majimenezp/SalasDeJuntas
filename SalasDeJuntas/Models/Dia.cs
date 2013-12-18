using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalasDeJuntas.Models
{
    public class Dia
    {
        public int Id { get; set; }
        public DateTime fecha { get; set; }

        public Junta[] juntas { get; set; }

    }
}