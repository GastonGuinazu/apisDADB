using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartasSinJugarController : ControllerBase
{
    private readonly tpi_dabdContext _context;
    public CartasSinJugarController(tpi_dabdContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<CartasSinJugarModel>> Create(CartasSinJugarCreateModel carta)
    {
        var newCartasSinJugar = new CartasSinJugar
        {
            IdCarta = carta.IdCarta,
            IdUsuario = carta.IdUsuario

        };
        _context.CartasSinJugars.Add(newCartasSinJugar);
        await _context.SaveChangesAsync();

        var cartaSinJugarModel = new CartasSinJugarModel
        {
            //CodSinJugar = newCartasSinJugar.CodSinJugar,         
            idCarta = newCartasSinJugar.IdCarta,
            //IdUsuario = newCartasSinJugar.IdUsuario

        };
        return Ok(cartaSinJugarModel);
    }


    [HttpGet]
    public async Task<ActionResult<CartasSinJugar>> Get()
    {
        var cartasSinJugar = await _context.CartasSinJugars.Select(x =>
        new CartasSinJugarModel
        {
            //CodSinJugar = x.CodSinJugar,
            idCarta = x.IdCarta,
            //IdUsuario = x.IdUsuario

        }).ToListAsync();
        return Ok(cartasSinJugar);
    }
    
     [HttpDelete("{idUsuario}")]
    public async Task<ActionResult> Delete(int idUsuario, string idCarta)
    {
         List <CartasSinJugar> cartas = (from c in _context.CartasSinJugars.Where(x =>x.IdUsuario==idUsuario && x.IdCarta== idCarta)  select c ).ToList();
         if (cartas.Count == 0)
            {
                return NotFound($"No se encontraron cartas para el usuario con el id {idUsuario}");
            }
          foreach(CartasSinJugar carta in cartas )
          {
              _context.CartasSinJugars.Remove(carta);
              
          }
        await _context.SaveChangesAsync();
       return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List<CartaModel>>> Get(int id)
    {
        var sentencia = _context.Cartas.Join(
            _context.CartasSinJugars,
            cartas => cartas.Id,
            cartasSinJugar => cartasSinJugar.IdCarta,
            (cartas, cartasSinJugar) =>
             new { Carta = cartas, CartasSinJugar = cartasSinJugar })
             .Where(entity => entity.CartasSinJugar.IdUsuario == id)
             .Select(entity => entity.Carta).ToList();
        var cartaSinJugar = new List<CartaModel>();
        foreach (var c in sentencia)
        {
            var carta = new CartaModel();
            carta.id = c.Id;
            carta.carta = c.Carta1;
            carta.valor = c.Valor;
            cartaSinJugar.Add(carta);
        }
        return Ok(cartaSinJugar);
    }
    // [HttpGet("{id}")]
    // public async Task<ActionResult<CartasSinJugar>> Get(int id)
    // {
    //     List<CartasSinJugar> cartas = (from c in _context.CartasSinJugars.Where(x => x.IdUsuario == id) select c).ToList();
    //     if (cartas.Count == 0)
    //     {
    //         return NotFound($"No se encontraron cartas con el id {id}");
    //     }
    //     foreach (CartasSinJugar carta in cartas)
    //     {
    //         var cartasSinJugarModel = new CartasSinJugarModel
    //         {
    //             CodSinJugar = carta.CodSinJugar,
    //             IdCarta = carta.IdCarta,
    //             IdUsuario = carta.IdUsuario
    //         };

    //     }
    //     await _context.SaveChangesAsync();
    //     return Ok(cartas);
    // }

    [HttpDelete("BorrarTodo/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        List<CartasSinJugar> cartas = (from c in _context.CartasSinJugars.Where(x => x.IdUsuario == id) select c).ToList();
        if (cartas.Count == 0)
        {
            return NotFound($"No se encontraron cartas para el usuario con el id {id}");
        }
        foreach (CartasSinJugar carta in cartas)
        {
            _context.CartasSinJugars.Remove(carta);

        }
        await _context.SaveChangesAsync();
        return Ok();
    }

}
