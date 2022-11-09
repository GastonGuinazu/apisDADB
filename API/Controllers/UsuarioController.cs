using System.Xml.XPath;
using System.Xml.Linq;
using System.Security.Cryptography.X509Certificates;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;




using Pomelo.EntityFrameworkCore.MySql;


using API.Data;
using API.Models;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly tpi_dabdContext _context;
    private readonly AppSettings _appSettings;
    public UsuarioController(tpi_dabdContext context, IOptions<AppSettings> appSettings)
    {
        _context = context;
        _appSettings = appSettings.Value;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<UsuarioModel>> Create(UsuarioCreateModel user)
    {
        var newUsuario = new Usuario
        {
            Usuario1 = user.usuario,
            Pass = user.pass,
            GanadasJugador = 0,
            GanadasCroupier = 0,
            BlackJackJugador = 0,
            BlackJackCroupier = 0


        };
        _context.Usuarios.Add(newUsuario);
        await _context.SaveChangesAsync();


        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.Name, newUsuario.IdUsuario.ToString()),
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        var usr = new UsuarioModel
        {
            idUsuario = newUsuario.IdUsuario,
            usuario = newUsuario.Usuario1,
            Token = tokenHandler.WriteToken(token)
        };

        return Ok(usr);
    }


    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<CartasJugadorModel>> Create(UsuarioModel userModel)
    {
        var user = _context.Usuarios.SingleOrDefault(x => x.Usuario1 == userModel.usuario && x.Pass == userModel.pass);

        if (user == null)
        {
            return Unauthorized($"Usuario o contraseña incorrecta");
        }

        //si el usuario existe, se genera token
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.Name, user.IdUsuario.ToString()),
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        var usr = new UsuarioModel
        {
            idUsuario = user.IdUsuario,
            usuario = user.Usuario1,
            Token = tokenHandler.WriteToken(token)
        };

        var newSesion = new Sesione
        {

            IdUsuario = usr.idUsuario

        };
        _context.Sesiones.Add(newSesion);



        await _context.SaveChangesAsync();
        return Ok(usr);
    }

    [HttpPut("PutGanadasJugador{id}")]
    [Authorize]
    public async Task<ActionResult<UsuarioModel>> ganadasJugador(int id)
    {
        var jugadorGanada = await _context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == id);

        if (jugadorGanada == null)
        {
            return NotFound("No se encontro el usuario");
        }
        else
        {
            jugadorGanada.GanadasJugador = jugadorGanada.GanadasJugador + 1;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }

    [HttpGet("GanadasJugador")]
    [Authorize]
    public async Task<ActionResult<UsuarioModel>> ganadasPorJugador(int id)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == id);

        if (usuario == null)
        {
            return NotFound("No se encontro el usuario");
        }
        else
        {
            var usr = new UsuarioReporteDosModel
            {
                idUsuario = usuario.IdUsuario,
                ganadasJugador = usuario.GanadasJugador,
                ganadasCroupier = usuario.GanadasCroupier

            };

            await _context.SaveChangesAsync();
            return Ok(usr);
        }
    }

    [HttpGet("GanadasBlackjack")]
    [Authorize]
    public async Task<ActionResult<UsuarioModel>> ganadasPorBlackjack(int id)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == id);

        if (usuario == null)
        {
            return NotFound("No se encontro el usuario");
        }
        else
        {
            var usr = new UsuarioReporteTresModel
            {
                idUsuario = usuario.IdUsuario,
                blackJackJugador = usuario.BlackJackJugador,
                blackJackCroupier = usuario.BlackJackCroupier

            };

            await _context.SaveChangesAsync();
            return Ok(usr);
        }
    }

    [HttpGet("TotalGanadasPerdidas")]
    [Authorize]
    public async Task<ActionResult<Reporteganadasperdida>> ganadasPerdidasTotal()
    {
        var vistaReporte = await _context.Reporteganadasperdidas.Select(x =>
        new ReporteGanadasPerdidasModel
        {
            totalGanadas = x.TotalGanadas,
            totalPerdidas = x.TotalPerdidas

        }).ToListAsync();
        return Ok(vistaReporte);
    }

    [HttpGet("RankingGanadas")]
    [Authorize]
    public async Task<ActionResult<Reporteranking>> rankingGanadas()
    {
        var vistaReporte = await _context.Reporterankings.Select(x =>
        new ReporterankingModel
        {
            usuario = x.Usuario,
            ganadas = x.Ganadas

        }).ToListAsync();
        return Ok(vistaReporte);
    }

    [HttpPut("PutGanadasCroupier{id}")]
    [Authorize]
    public async Task<ActionResult<UsuarioModel>> ganadasCroupier(int id)
    {
        var croupierGanada = await _context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == id);

        if (croupierGanada == null)
        {
            return NotFound("No se encontro el usuario");
        }
        else
        {
            croupierGanada.GanadasCroupier = croupierGanada.GanadasCroupier + 1;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }


    [HttpPut("PutJugadorBlackjack{id}")]
    [Authorize]
    public async Task<ActionResult<UsuarioModel>> blackJackJugador(int id)
    {
        var jugadorBlackJack = await _context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == id);

        if (jugadorBlackJack == null)
        {
            return NotFound("No se encontro el usuario");
        }
        else
        {
            jugadorBlackJack.BlackJackJugador = jugadorBlackJack.BlackJackJugador + 1;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }

    [HttpPut("PutCroupierBlackjack{id}")]
    [Authorize]
    public async Task<ActionResult<UsuarioModel>> blackJackCroupier(int id)
    {
        var croupierBlackJack = await _context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == id);

        if (croupierBlackJack == null)
        {
            return NotFound("No se encontro el usuario");
        }
        else
        {
            croupierBlackJack.BlackJackCroupier = croupierBlackJack.BlackJackCroupier + 1;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }

}
