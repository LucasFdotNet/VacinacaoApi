using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using VacinacaoApi.Data;
using VacinacaoApi.DTOs;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly VacinacaoContext _context;

    public AuthController(IConfiguration configuration, VacinacaoContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
    {
        var pessoa = await _context.Pessoas
            .FirstOrDefaultAsync(p => p.NumeroIdentificacao == loginRequest.NumeroIdentificacao);

        if (pessoa == null || pessoa.Senha != loginRequest.Senha)
        {
            return Unauthorized("Credenciais inválidas.");
        }

        // gera o token para credencial válida
        var token = GenerateJwtToken(pessoa.Id.ToString());

        return Ok(new { token = token });
    }

    private string GenerateJwtToken(string userId)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(2), // Token expira em 2 horas
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}