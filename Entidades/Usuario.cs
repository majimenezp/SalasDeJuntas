using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class Usuario
    {
        public virtual int Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string NombreCompleto { get; set; }
    }
}
