using AutoMapper;
using CleanArchitecture.Application.Common.DTO;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Features.Products.Commands.CreateProduct;
using CleanArchitecture.Domain.Entities;
using NUnit.Framework;
using System;

namespace CleanArchitecture.Application.UnitTests.Common.Mappings
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductProfile>();
                cfg.AddProfile<TodoListProfile>();
                cfg.AddProfile<TodoItemProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [Test]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }
        
        [Test]
        [TestCase(typeof(Product), typeof(ProductDTO))]
        [TestCase(typeof(CreateProductCommand), typeof(Product))]
        [TestCase(typeof(TodoList), typeof(TodoListDTO))]
        [TestCase(typeof(TodoItem), typeof(TodoItemDTO))]
        [TestCase(typeof(TodoItem), typeof(ExportTodoItemFileRecordDTO))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            var instance = Activator.CreateInstance(source);

            _mapper.Map(instance, source, destination);
        }
    }
}
