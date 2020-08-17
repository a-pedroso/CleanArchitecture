using CleanArchitecture.Application.Features.TodoLists.Commands.CreateTodoList;
using CleanArchitecture.Application.Features.TodoLists.Commands.DeleteTodoList;
using CleanArchitecture.Application.Features.TodoLists.Commands.UpdateTodoList;
using CleanArchitecture.Application.Features.TodoLists.Queries.ExportTodos;
using CleanArchitecture.Application.Features.TodoLists.Queries.GetTodos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArchitecture.WebUI.Controllers
{
    [Authorize]
    public class TodoListsController : ApiController
    {
        private readonly IMediator _mediator;

        public TodoListsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<ActionResult<TodosVm>> Get()
        {
            return await _mediator.Send(new GetTodosQuery());
        }

        [HttpGet("{id}")]
        public async Task<FileResult> Get(int id)
        {
            var vm = await _mediator.Send(new ExportTodosQuery { ListId = id });

            return File(vm.Content, vm.ContentType, vm.FileName);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateTodoListCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateTodoListCommand command)
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
            await _mediator.Send(new DeleteTodoListCommand { Id = id });

            return NoContent();
        }
    }
}
