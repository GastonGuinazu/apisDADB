namespace API.Models;

public class UsuarioModel
{
    public int idUsuario { get; set; }
    public string? usuario { get; set; }
    public string? pass { get; set; }
    public int? ganadasJugador { get; set; }
    public int? ganadasCroupier { get; set; }
    public int? blackJackJugador { get; set; }
    public int? blackJackCroupier { get; set; }

     public string? Token { get; set; }

}
