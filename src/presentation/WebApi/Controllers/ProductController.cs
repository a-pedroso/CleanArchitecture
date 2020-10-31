using CleanArchitecture.Application.Features.Products.Commands.CreateProduct;
using CleanArchitecture.Application.Features.Products.Commands.DeleteProductById;
using CleanArchitecture.Application.Features.Products.Commands.UpdateProduct;
using CleanArchitecture.Application.Features.Products.Queries.GetAllProducts;
using CleanArchitecture.Application.Features.Products.Queries.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMediator _mediator;

        public ProductController(
            ILogger<ProductController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        // GET: /Product
        [HttpGet]
        public async Task<IActionResult> Get(int index, int size)
        {
            _logger.LogDebug($"Get Products. index:{index} size:{size} ");

            var qry = new GetAllProductsQuery()
            {
                PageNumber = index + 1,
                PageSize = size
            };

            var response = await _mediator.Send(qry);

            return Ok(response);
        }

        // GET /Product/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogDebug($"Get Product {id}");

            var response = await _mediator.Send(new GetProductByIdQuery() { Id = id });
            return Ok(response);
        }

        // POST /Product
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductCommand cmd)
        {
            _logger.LogDebug($"Create Product {cmd}");

            var response = await _mediator.Send(cmd);
            return Ok(response);
        }

        // PUT /Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateProductCommand cmd)
        {
            _logger.LogDebug($"Update Product {id} : {cmd}");

            cmd.Id = id;
            var response = await _mediator.Send(cmd);
            return Ok(response);
        }

        // DELETE /Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogDebug($"Delete Product {id}");

            var cmd = new DeleteProductByIdCommand() { Id = id };
            var response = await _mediator.Send(cmd);
            return Ok(response);
        }
    }
}
