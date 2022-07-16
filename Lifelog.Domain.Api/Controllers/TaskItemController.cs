using Lifelog.Domain.Commands;
using Lifelog.Domain.Handlers;
using Lifelog.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lifelog.Domain.Api.Controllers
{
    [ApiController]
    [Route("v1/tasks")]
    [Authorize]
    public class TaskItemController : ControllerBase
    {
        [Route("create")]
        [HttpPost]
        public IActionResult Create(
            [FromBody] CreateTaskItemCommand command,
            [FromServices] TaskItemHandler handler)
        {
            if (!ModelState.IsValid)
                return BadRequest(new GenericCommandResult(
                    false, "Modelstate inválido!", ModelState.Values));

            // App Login
            command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            
            // Get User Timezone
            command.DateInit = command.DateInit.Date.AddHours(-1);

            try
            {
                return Ok((GenericCommandResult)handler.Handle(command));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new GenericCommandResult(
                    false,
                    "ACC01 - E-mail ou slug já cadastrado!",
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
            [FromServices] ITaskItemRepository repository,
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 25)
        {
            try
            {
                // App Login
                var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

                var tasks = repository.GetAll(user);

                return Ok(new GenericCommandResult(
                    true,
                    "Tarefas",
                    new { total = tasks.Count(), page, pageSize, tasks }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GenericCommandResult(
                    false, "ACC02 - Falha interna do servidor!",
                    ex.Message));
            }
        }

        [Route("title")]
        [HttpGet]
        public IActionResult GetAllByTitle(
            string title,
            [FromServices] ITaskItemRepository repository,
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 25)
        {
            try
            {
                var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

                var tasks = repository.GetAllByTitle(user, title);
                
                return Ok(new GenericCommandResult(
                    true,
                    "Tarefas",
                    new { total = tasks.Count(), page, pageSize, tasks }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GenericCommandResult(
                    false, "ACC02 - Falha interna do servidor!",
                    ex.Message));
            }
        }

        [Route("done")]
        [HttpGet]
        public IActionResult GetAllDone(
            [FromServices] ITaskItemRepository repository)
        {
            // App Login
            var user = User.Identity != null ? User.Identity.Name :
                User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            return Ok(new GenericCommandResult(
                true, "Todas as tarefas", repository.GetAllDone(user)));
        }

        [Route("undone")]
        [HttpGet]
        public IActionResult GetAllUndone(
            [FromServices] ITaskItemRepository repository)
        {
            // App Login
            var user = User.Identity != null ? User.Identity.Name :
                User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            return Ok(new GenericCommandResult(
                true, "Todas as tarefas", repository.GetAllUndone(user)));
        }

        [Route("date/{date}")]
        [HttpGet]
        public IActionResult GetAllByDate(
            [FromServices] ITaskItemRepository repository,
            [FromRoute] DateTime date)
        {
            // App Login
            var user = User.Identity != null ? User.Identity.Name :
                User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            return Ok(new GenericCommandResult(
                true,
                "Todas as tarefas",
                repository.GetByPeriod(user, date, true)));
        }

        [Route("update")]
        [HttpPut]
        public IActionResult Update(
            [FromBody] UpdateTaskItemCommand command,
            [FromServices] TaskItemHandler handler)
        {
            // App Login
            command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            try
            {
                return Ok((GenericCommandResult)handler.Handle(command));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new GenericCommandResult(
                    false,
                    "ACC01 - E-mail ou slug já cadastrado!",
                    ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GenericCommandResult(
                    false, "ACC02 - Falha interna do servidor!",
                    ex.Message));
            }
        }
        
        [Route("update/mark-as-done")]
        [HttpPut]
        public IActionResult MarkTaskItemAsDone(
            [FromBody] MarkTaskItemAsDoneCommand command,
            [FromServices] TaskItemHandler handler)
        {
            // App Login
            command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            try
            {
                return Ok((GenericCommandResult)handler.Handle(command));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new GenericCommandResult(
                    false,
                    "ACC01 - E-mail ou slug já cadastrado!",
                    ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GenericCommandResult(
                    false, "ACC02 - Falha interna do servidor!",
                    ex.Message));
            }
        }
        
        [Route("update/mark-as-undone")]
        [HttpPut]
        public IActionResult MarkTaskItemAsUndone(
            [FromBody] MarkTaskItemAsUndoneCommand command,
            [FromServices] TaskItemHandler handler)
        {
            // App Login
            command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

            try
            {
                return Ok((GenericCommandResult)handler.Handle(command));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new GenericCommandResult(
                    false,
                    "ACC01 - E-mail ou slug já cadastrado!",
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
            [FromBody] DeleteTaskItemCommand command,
            [FromServices] TaskItemHandler handler)
        {
            if (!ModelState.IsValid)
                return BadRequest(new GenericCommandResult(
                    false, "Modelstate inválido!", ModelState.Values));
            
            command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        
            try
            {
                // Remove user
                var result = (GenericCommandResult)handler.Handle(command);
            
                return Ok(new GenericCommandResult(true,
                    "Tarefa deletada com sucesso!" , result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GenericCommandResult(
                    false, "ACC02 - Falha interna do servidor!", ex.Message));
            }
        }
    }
}