using CleanArchitecture.Application.Features.TodoItems.Commands.CreateTodoItem;
using CleanArchitecture.Application.Features.TodoItems.Commands.DeleteTodoItem;
using CleanArchitecture.Application.Features.TodoItems.Commands.UpdateTodoItem;
using CleanArchitecture.Application.Features.TodoItems.Commands.UpdateTodoItemDetail;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class TodoItemsController : ControllerBase
    {
        private readonly ILogger<TodoItemsController> _logger;
        private readonly IMediator _mediator;

        public TodoItemsController(
            ILogger<TodoItemsController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        // POST TodoItems
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateTodoItemCommand cmd)
        {
            _logger.LogDebug($"Create todo item {cmd}");

            var response = await _mediator.Send(cmd);
            return Ok(response);
        }

        // PUT TodoItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateTodoItemCommand cmd)
        {
            _logger.LogDebug($"Update TodoItem {id} : {cmd}");

            cmd.Id = id;
            var response = await _mediator.Send(cmd);
            return Ok(response);
        }

        // PUT TodoItems/detail/5
        [HttpPut("detail/{id}")]
        public async Task<IActionResult> PutDetail(int id, [FromBody] UpdateTodoItemDetailCommand cmd)
        {
            _logger.LogDebug($"Update TodoItem {id} : {cmd}");

            cmd.Id = id;
            var response = await _mediator.Send(cmd);
            return Ok(response);
        }

        // DELETE TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogDebug($"Delete Todoitem {id}");

            var cmd = new DeleteTodoItemCommand() { Id = id };
            var response = await _mediator.Send(cmd);
            return Ok(response);
        }
    }
}
