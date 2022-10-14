using System;
using System.Collections.Generic;

namespace API.Data
{
    public partial class Sesione
    {
        public int IdSesion { get; set; }
        public int? IdUsuario { get; set; }

        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}
