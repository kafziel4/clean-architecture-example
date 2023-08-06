using AutoMapper;
using Catalog.API.DTOs;
using Catalog.Core.Entities;
using Catalog.Core.Interfaces;
using Catalog.Core.Pagination;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Catalogue.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(
            IProductService productService,
            IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll(
            [FromQuery] PaginationParameters parameters)
        {
            var (productEntities, paginationMetadata) = await _productService.GetProducts(parameters);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<ProductDto>>(productEntities));
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            var productEntity = await _productService.GetProduct(id);
            if (productEntity is null)
                return NotFound();

            return Ok(_mapper.Map<ProductDto>(productEntity));
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Post(ProductForCreationDto productDto)
        {
            var productToSave = _mapper.Map<Product>(productDto);
            var productEntity = await _productService.AddProduct(productToSave);

            return new CreatedAtRouteResult("GetProduct",
                new { id = productEntity.Id },
                _mapper.Map<ProductDto>(productEntity));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDto>> Put(int id, ProductForUpdateDto productDto)
        {
            if (id != productDto.Id)
            {
                ModelState.AddModelError("id", "Id mismatch.");
                return BadRequest(ModelState);
            }

            var productToUpdate = _mapper.Map<Product>(productDto);
            var productEntity = await _productService.UpdateProduct(id, productToUpdate);
            if (productEntity is null)
                return NotFound();

            return Ok(_mapper.Map<ProductDto>(productEntity));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var isRemoved = await _productService.RemoveProduct(id);
            if (!isRemoved)
                return NotFound();

            return NoContent();
        }
    }
}
