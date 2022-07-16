using Lifelog.Domain.Infra.Repositories;
using Lifelog.Domain.Commands;
using Lifelog.Domain.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lifelog.Domain.Api.Controllers;

[ApiController]
[Route("v1/projects")]
[Authorize]
public class ProjectController : ControllerBase
{
    [Route("create")]
    [HttpPost]
    public IActionResult Create(
        [FromBody] CreateProjectCommand command,
        [FromServices] ProjectHandler handler)
    {
        if (!ModelState.IsValid)
            return BadRequest(new GenericCommandResult(
                false, "Modelstate inválido!", ModelState.Values));
        
        var user = User.Identity != null ? User.Identity.Name :
            User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

        try
        {
            return Ok((GenericCommandResult)handler.Handle(command));
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, new GenericCommandResult(
                false,
                "ACC01 - Slug já cadastrado!",
                ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new GenericCommandResult(
                false, "ACC02 - Falha interna do servidor!",
                ex.Message));
        }
    }
    
    [Route("")]
    [HttpGet]
    public IActionResult GetAll(
        [FromServices] ProjectRepository repository,
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 25)
    {
        try
        {
            var user = User.Identity != null ? User.Identity.Name :
                User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            
            var projects = repository.GetAll(user);
            
            return Ok(new GenericCommandResult(
                true,
                "Projetos",
                new { total = projects.Count(), page, pageSize, projects }));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new GenericCommandResult(
                false, "ACC02 - Falha interna do servidor!", ex.Message));
        }
    }
    
    [Route("active")]
    [HttpGet]
    public IActionResult GetAllActive(
        [FromServices] ProjectRepository repository,
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 25)
    {
        try
        {
            var user = User.Identity != null ? User.Identity.Name :
                User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            
            var projects = repository.GetAllActive(user);
            
            return Ok(new GenericCommandResult(
                true,
                "Projetos ativos",
                new { total = projects.Count(), page, pageSize, projects }));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new GenericCommandResult(
                false, "ACC02 - Falha interna do servidor!", ex.Message));
        }
    }
    
    [Route("inactive")]
    [HttpGet]
    public IActionResult GetAllInactive(
        [FromServices] ProjectRepository repository,
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 25)
    {
        try
        {
            var user = User.Identity != null ? User.Identity.Name :
                User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            
            var projects = repository.GetAllInactive(user);
            
            return Ok(new GenericCommandResult(
                true,
                "Projetos inativos",
                new { total = projects.Count(), page, pageSize, projects }));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new GenericCommandResult(
                false, "ACC02 - Falha interna do servidor!", ex.Message));
        }
    }
    
    [Route("update")]
    [HttpPut]
    public IActionResult Update(
        [FromBody] UpdateProjectCommand command,
        [FromServices] ProjectHandler handler)
    {
        // App Login
        var user = User.Identity != null ? User.Identity.Name :
            User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

        command.User = user;

        try
        {
            return Ok((GenericCommandResult)handler.Handle(command));
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, new GenericCommandResult(
                false,
                "ACC01 - Slug já cadastrado!",
                ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new GenericCommandResult(
                false, "ACC02 - Falha interna do servidor!",
                ex.Message));
        }
    }
    
    [Route("delete")]
    [HttpDelete]
    public IActionResult Delete(
        [FromBody] DeleteProjectCommand command,
        [FromServices] ProjectHandler handler)
    {
        if (!ModelState.IsValid)
            return BadRequest(new GenericCommandResult(
                false, "Modelstate inválido!", ModelState.Values));
    
        try
        {
            var result = (GenericCommandResult)handler.Handle(command);
        
            return Ok(new GenericCommandResult(true,
                "Projeto deletado com sucesso!" , result));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new GenericCommandResult(
                false, "ACC02 - Falha interna do servidor!", ex.Message));
        }
    }
}