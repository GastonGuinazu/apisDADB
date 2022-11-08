using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;


namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartaJugadorController : ControllerBase
{
    private readonly tpi_dabdContext _context;
    public CartaJugadorController(tpi_dabdContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<CartasJugadorModel>> Create(CartasJugadorCreateModel carta)
    {
        var newCartaJugador = new CartasJugador
        {
            IdCarta = carta.IdCarta,
            IdUsuario = carta.IdUsuario

        };
        _context.CartasJugadors.Add(newCartaJugador);
        await _context.SaveChangesAsync();

        var cartaJugadorModel = new CartasJugadorModel
        {
            //CodJugador = newCartaJugador.CodJugador,
            idCarta = newCartaJugador.IdCarta,
            //IdUsuario = newCartaJugador.IdUsuario
        };
        return Ok(cartaJugadorModel);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<List<CartaModel>>> Get(int id)
    {
        var sentencia = _context.Cartas.Join(
            _context.CartasJugadors,
            cartas => cartas.Id,
            cartasJugador => cartasJugador.IdCarta,
            (cartas, cartasJugador) =>
             new { Carta = cartas, CartasJugador = cartasJugador })
             .Where(entity => entity.CartasJugador.IdUsuario == id)
             .Select(entity => entity.Carta).ToList();
        var cartaJugador = new List<CartaModel>();
        foreach (var c in sentencia)
        {
            var carta = new CartaModel();
            carta.id = c.Id;
            carta.carta = c.Carta1;
            carta.valor = c.Valor;
            cartaJugador.Add(carta);
        }
        return Ok(cartaJugador);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult> Delete(int id)
    {
        List<CartasJugador> cartas = (from c in _context.CartasJugadors.Where(x => x.IdUsuario == id) select c).ToList();
        if (cartas.Count == 0)
        {
            return NotFound($"No se encontraron cartas para el usuario con el id {id}");
        }
        foreach (CartasJugador carta in cartas)
        {
            _context.CartasJugadors.Remove(carta);

        }
        await _context.SaveChangesAsync();
        return Ok();
    }
}
