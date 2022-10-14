using System;
using System.Collections.Generic;

namespace API.Data
{
    public partial class Usuario
    {
        public Usuario()
        {
            CartasCroupiers = new HashSet<CartasCroupier>();
            CartasJugada = new HashSet<CartasJugada>();
            CartasJugadors = new HashSet<CartasJugador>();
            CartasSinJugars = new HashSet<CartasSinJugar>();
            Sesiones = new HashSet<Sesione>();
        }

        public int IdUsuario { get; set; }
        public string? Usuario1 { get; set; }
        public string? Pass { get; set; }

        public virtual ICollection<CartasCroupier> CartasCroupiers { get; set; }
        public virtual ICollection<CartasJugada> CartasJugada { get; set; }
        public virtual ICollection<CartasJugador> CartasJugadors { get; set; }
        public virtual ICollection<CartasSinJugar> CartasSinJugars { get; set; }
        public virtual ICollection<Sesione> Sesiones { get; set; }
    }
}
