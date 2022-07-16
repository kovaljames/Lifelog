using Lifelog.Domain.Api.Services;
using Lifelog.Domain.Commands;
using Lifelog.Domain.Entities;
using Lifelog.Domain.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace Lifelog.Domain.Api.Controllers;

[ApiController]
[Route("v1/accounts")]
public class AccountController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login(
        [FromBody] LoginCommand command,
        [FromServices] TokenService tokenService,
        [FromServices] UserHandler handler)
    {
        if (!ModelState.IsValid)
            return BadRequest(new GenericCommandResult(
                false, "Modelstate inválido!", ModelState.Values));
        
        var result = (GenericCommandResult) handler.Handle(command);

        if (result.Data == null)
            return StatusCode(401,
                new GenericCommandResult(false, 
                    "Usuário ou senha inválidos!", null));
        
        var user = (User)result.Data;

        if (!PasswordHasher.Verify(user.Password, command.Password))
            return StatusCode(401,
                new GenericCommandResult(false, 
                    "Usuário ou senha inválidos!", null));

        try
        {
            var token = tokenService.GenerateToken(user);
            return Ok(new GenericCommandResult(true,
                "Login feito com sucesso!" , token));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new GenericCommandResult(
                false, "ACC02 - Falha interna do servidor!", ex.Message));
        }
    }
    
    [HttpPost("create")]
    public IActionResult Post(
        [FromBody] CreateUserCommand command,
        [FromServices] EmailService emailService,
        [FromServices] UserHandler handler)
    {
        if (!ModelState.IsValid)
            return BadRequest(new GenericCommandResult(
                false, "Modelstate inválido!", ModelState.Values));

        command.Password = PasswordHasher.Hash(command.Password); // encrypt password

        try
        {
            // Save user
            var result = (GenericCommandResult) handler.Handle(command);
            
            // Send confirmation email
            var isSent = emailService.Send(
                command.Name,
                command.Email,
                "Bem-vindo ao Lifelog!",
                $"<p>Clique no link para confirmar sua conta:</p>");
            
            if (isSent)
                return Ok(result);
            
            return StatusCode(500, new GenericCommandResult(
                false,
                "ACC03 - Não foi possível enviar o e-mail de confirmação!",
                null)); 
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(400, new GenericCommandResult(
                false,
                "ACC01 - E-mail já cadastrado!", ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new GenericCommandResult(
                false, "ACC02 - Falha interna do servidor!", ex.Message));
        }
    }

    [Route("update")]
    [HttpPut]
    [Authorize]
    public IActionResult Update(
        [FromBody] UpdateUserCommand command,
        [FromServices] UserHandler handler)
    {
        if (!ModelState.IsValid)
            return BadRequest(new GenericCommandResult(
                false, "Modelstate inválido!", ModelState.Values));
        
        try
        {
            // Edit user
            var result = (GenericCommandResult)handler.Handle(command);
            
            return Ok(new GenericCommandResult(true,
                "Usuário modificado com sucesso!" , result));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new GenericCommandResult(
                false, "ACC02 - Falha interna do servidor!", ex.Message));
        }
    }
    
    [Route("delete")]
    [HttpDelete]
    [Authorize]
    public IActionResult Delete(
        [FromQuery] int id,
        [FromServices] UserHandler handler)
    {
        if (!ModelState.IsValid)
            return BadRequest(new GenericCommandResult(
                false, "Modelstate inválido!", ModelState.Values));
        
        try
        {
            // Remove user
            var result = (GenericCommandResult)handler.Handle(id);
            
            return Ok(new GenericCommandResult(true,
                "Usuário deletado com sucesso!" , result));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new GenericCommandResult(
                false, "ACC02 - Falha interna do servidor!", ex.Message));
        }
    }
}