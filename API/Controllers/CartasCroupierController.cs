using System.IO.Compression;
using System.Data.Common;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartasCroupierController : ControllerBase
{
    private readonly tpi_dabdContext _context;
    public CartasCroupierController(tpi_dabdContext context)
    {
        _context = context;
    }

    
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<CartasCroupierModel>> Create(CartasCroupierCreateModel carta)
    {
        var newCartaCroupier = new CartasCroupier
        {
            IdCarta = carta.idCarta,
            IdUsuario = carta.idUsuario

        };
        _context.CartasCroupiers.Add(newCartaCroupier);
        await _context.SaveChangesAsync();

        var cartaCroupierModel = new CartasCroupierModel
        {   
            //CodCroupier = newCartaCroupier.CodCroupier,         
            idCarta = newCartaCroupier.IdCarta,
            //idUsuario = newCartaCroupier.IdUsuario

        };
        return Ok(cartaCroupierModel);
    }


    [HttpGet]
    [Authorize]
    public async Task<ActionResult<CartasCroupier>> Get()
    {
        var cartasCroupier = await _context.CartasCroupiers.Select(x => 
        new CartasCroupierModel
        {
            //CodCroupier = x.CodCroupier,
            idCarta = x.IdCarta,
            //idUsuario = x.IdUsuario
            
        }).ToListAsync();
        return Ok(cartasCroupier);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<List<CartaModel>>> Get(int id)
    {
        var sentencia = _context.Cartas.Join(
            _context.CartasCroupiers,
            cartas => cartas.Id,
            cartasCroupier => cartasCroupier.IdCarta,
            (cartas, cartasCroupier) =>
             new {Carta = cartas, CartasCroupier = cartasCroupier})
             .Where(entity => entity.CartasCroupier.IdUsuario == id)
             .Select(entity => entity.Carta).ToList();
        var croupier = new List<CartaModel>();
        foreach(var c in sentencia){
            var carta = new CartaModel();
            carta.id = c.Id;
            carta.carta = c.Carta1;
            carta.valor = c.Valor; 
            croupier.Add(carta);
        }
        return Ok (croupier);       
    }  

   

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult> Delete(int id)
    {
         List <CartasCroupier> cartas = (from c in _context.CartasCroupiers.Where(x =>x.IdUsuario==id)  select c ).ToList();
         if (cartas.Count == 0)
            {
                return NotFound($"No se encontraron cartas para el usuario con el id {id}");
            }
          foreach(CartasCroupier carta in cartas )
          {
              _context.CartasCroupiers.Remove(carta);
              
          }
        await _context.SaveChangesAsync();
       return Ok();
    }

    

}
