using System;
using System.Collections.Generic;

namespace API.Data
{
    public partial class Carta
    {
        public Carta()
        {
            CartasCroupiers = new HashSet<CartasCroupier>();
            CartasJugada = new HashSet<CartasJugada>();
            CartasJugadors = new HashSet<CartasJugador>();
            CartasSinJugars = new HashSet<CartasSinJugar>();
        }

        public string? Id { get; set; } = null!;
        public string? Carta1 { get; set; }
        public int? Valor { get; set; }

        public virtual ICollection<CartasCroupier> CartasCroupiers { get; set; }
        public virtual ICollection<CartasJugada> CartasJugada { get; set; }
        public virtual ICollection<CartasJugador> CartasJugadors { get; set; }
        public virtual ICollection<CartasSinJugar> CartasSinJugars { get; set; }
    }
}
