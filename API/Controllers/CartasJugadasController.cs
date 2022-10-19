using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.AspNetCore.Authorization;
namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartasJugadasController : ControllerBase
{
    private readonly tpi_dabdContext _context;
    public CartasJugadasController(tpi_dabdContext context)
    {
        _context = context;
    }


    [HttpPost]
    [Authorize]
    public async Task<ActionResult<CartasJugadasModel>> Create(CartasJugadasCreateModel carta)
    {
        var newCartaJugada = new CartasJugada
        {
            IdCarta = carta.idCarta,
            IdUsuario = carta.idUsuario

        };
        _context.CartasJugadas.Add(newCartaJugada);
        await _context.SaveChangesAsync();

        var cartaJugadaModel = new CartasJugadasModel
        {   
            //codJugadas = newCartaJugada.CodJugadas,         
            idCarta = newCartaJugada.IdCarta,
            //idUsuario = newCartaJugada.IdUsuario

        };
        return Ok(cartaJugadaModel);
    }


    [HttpGet]
    [Authorize]
    public async Task<ActionResult<CartasJugada>> Get()
    {
        var cartasJugadas = await _context.CartasJugadas.Select(x => 
        new CartasJugadasModel
        {
            //codJugadas = x.CodJugadas,
            idCarta = x.IdCarta,
            //idUsuario = x.IdUsuario
            
        }).ToListAsync();
        return Ok(cartasJugadas);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<List<CartaModel>>> Get(int id)
    {
        var sentencia = _context.Cartas.Join(
            _context.CartasJugadas,
            cartas => cartas.Id,
            cartasJugadas => cartasJugadas.IdCarta,
            (cartas, cartasJugadas) =>
             new {Carta = cartas, CartasJugada = cartasJugadas})
             .Where(entity => entity.CartasJugada.IdUsuario == id)
             .Select(entity => entity.Carta).ToList();
        var cartasJugadas = new List<CartaModel>();
        foreach(var c in sentencia){
            var carta = new CartaModel();
            carta.id = c.Id;
            carta.carta = c.Carta1;
            carta.valor = c.Valor; 
            cartasJugadas.Add(carta);
        }
        return Ok (cartasJugadas);       
    }

    // [HttpGet("{id}")]
    // public async Task<ActionResult<CartasJugadasModel>> Get(int id)
    // {
    //     List<CartasJugada> cartas = (from c in _context.CartasJugadas.Where(x => x.IdUsuario == id) select c).ToList();
    //     if (cartas.Count == 0)
    //     {
    //         return NotFound($"No se encontraron cartas con el id {id}");
    //     }
    //     foreach (CartasJugada carta in cartas)
    //     {
    //         var cartasJugadasModel = new CartasJugadasModel
    //         {
    //             codJugadas = carta.CodJugadas,
    //             idCarta = carta.IdCarta,
    //             idUsuario = carta.IdUsuario
    //         };

    //     }
    //     await _context.SaveChangesAsync();
    //     return Ok(cartas);
    // }

    [HttpDelete("BorrarTodo/{id}")]
    [Authorize]
    public async Task<ActionResult> Delete(int id)
    {
         List <CartasJugada> cartas = (from c in _context.CartasJugadas.Where(x =>x.IdUsuario==id)  select c ).ToList();
         if (cartas.Count == 0)
            {
                return NotFound($"No se encontraron cartas para el usuario con el id {id}");
            }
          foreach(CartasJugada carta in cartas )
          {
              _context.CartasJugadas.Remove(carta);
              
          }
        await _context.SaveChangesAsync();
       return Ok();
    }

    [HttpDelete("{idUsuario}")]
    [Authorize]
    public async Task<ActionResult> Delete(int idUsuario, string idCarta)
    {
         List <CartasJugada> cartas = (from c in _context.CartasJugadas.Where(x =>x.IdUsuario==idUsuario && x.IdCarta== idCarta)  select c ).ToList();
         if (cartas.Count == 0)
            {
                return NotFound($"No se encontraron cartas para el usuario con el id {idUsuario}");
            }
          foreach(CartasJugada carta in cartas )
          {
              _context.CartasJugadas.Remove(carta);
              
          }
        await _context.SaveChangesAsync();
       return Ok();
    }
}
