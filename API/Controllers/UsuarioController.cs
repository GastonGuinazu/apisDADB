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
    public UsuarioController(tpi_dabdContext context,  IOptions<AppSettings> appSettings)
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
            Pass = user.pass

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


        await _context.SaveChangesAsync();
        return Ok(usr);
    }

}
