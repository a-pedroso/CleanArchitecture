using CleanArchitecture.Application.Features.TodoItems.Commands.CreateTodoItem;
using CleanArchitecture.Application.Features.TodoItems.Commands.DeleteTodoItem;
using CleanArchitecture.Application.Features.TodoItems.Commands.UpdateTodoItem;
using CleanArchitecture.Application.Features.TodoItems.Commands.UpdateTodoItemDetail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArchitecture.WebUI.Controllers
{
    [Authorize]
    public class TodoItemsController : ApiController
    {
        private readonly IMediator _mediator;

        public TodoItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateTodoItemCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateTodoItemCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPut("[action]")]
        public async Task<ActionResult> UpdateItemDetails(int id, UpdateTodoItemDetailCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteTodoItemCommand { Id = id });

            return NoContent();
        }
    }
}
