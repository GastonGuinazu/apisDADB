﻿using System;
using System.Collections.Generic;

namespace API.Data
{
    public partial class CartasSinJugar
    {
        public int CodSinJugar { get; set; }
        public string? IdCarta { get; set; }
        public int? IdUsuario { get; set; }

        public virtual Carta? IdCartaNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}
