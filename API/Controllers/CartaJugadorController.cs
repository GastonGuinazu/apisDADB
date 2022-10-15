using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

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
            CodJugador = newCartaJugador.CodJugador,
            IdCarta = newCartaJugador.IdCarta,
            IdUsuario = newCartaJugador.IdUsuario
        };
        return Ok(cartaJugadorModel);
    }


    // [HttpGet]
    // public async Task<ActionResult<CartasJugador>> Get()
    // {
    //     var cartasJugador = await _context.CartasJugadors.Select(x => 
    //     new CartasJugadorModel
    //     {
    //         CodJugador = x.CodJugador,
    //         IdCarta = x.IdCarta,
    //         IdUsuario = x.IdUsuario

    //     }).ToListAsync();
    //     return Ok(cartasJugador);
    // }

    [HttpGet("{id}")]
    public async Task<ActionResult<CartasJugadorModel>> Get(int id)
    {
        List<CartasJugador> cartas = (from c in _context.CartasJugadors.Where(x => x.IdUsuario == id) select c).ToList();
        if (cartas.Count == 0)
        {
            return NotFound($"No se encontraron cartas con el id {id}");
        }
        foreach (CartasJugador carta in cartas)
        {
            var cartaJugadorModel = new CartasJugadorModel
            {
                CodJugador = carta.CodJugador,
                IdCarta = carta.IdCarta,
                IdUsuario = carta.IdUsuario
            };

        }
        await _context.SaveChangesAsync();
        return Ok(cartas);
    }


    [HttpDelete("{id}")]
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
