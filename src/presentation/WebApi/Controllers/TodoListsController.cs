using CleanArchitecture.Application.Features.TodoLists.Commands.CreateTodoList;
using CleanArchitecture.Application.Features.TodoLists.Commands.DeleteTodoList;
using CleanArchitecture.Application.Features.TodoLists.Commands.UpdateTodoList;
using CleanArchitecture.Application.Features.TodoLists.Queries.ExportTodos;
using CleanArchitecture.Application.Features.TodoLists.Queries.GetTodos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class TodoListsController : ControllerBase
    {
        private readonly ILogger<TodoListsController> _logger;
        private readonly IMediator _mediator;
        
        public TodoListsController(
            ILogger<TodoListsController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        // GET: TodoLists
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            _logger.LogDebug($"Get TodoLists");

            var response = await _mediator.Send(new GetTodosQuery());

            return Ok(response);
        }

        // GET TodoLists/export/5
        [HttpGet("export/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogDebug($"export todo lists {id}");
            
            var response = await _mediator.Send(new ExportTodosQuery() { ListId = id });

            Response.Headers.Add("Content-Disposition", $"attachment; filename={response.FileName}");
            return new FileContentResult(response.Content, response.ContentType);
        }

        // POST TodoLists
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateTodoListCommand cmd)
        {
            _logger.LogDebug($"Create todo list {cmd}");

            var response = await _mediator.Send(cmd);
            return Ok(response);
        }

        // PUT TodoLists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateTodoListCommand cmd)
        {
            _logger.LogDebug($"Update TodoList {id} : {cmd}");

            cmd.Id = id;
            var response = await _mediator.Send(cmd);
            return Ok(response);
        }

        // DELETE TodoLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogDebug($"Delete TodoList {id}");

            var cmd = new DeleteTodoListCommand() { Id = id };
            var response = await _mediator.Send(cmd);
            return Ok(response);
        }
    }
}
