using AutoMapper;
using Catalog.API.DTOs;
using Catalog.Core.Entities;
using Catalog.Core.Interfaces;
using Catalog.Core.Pagination;
using Catalog.Core.Search;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Catalogue.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
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
            [FromQuery] PaginationParameters paginationParameters,
            [FromQuery] SearchParameters searchParameters)
        {
            var pagedProducts = await _productService
                .GetProducts(paginationParameters, searchParameters);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedProducts.PaginationMetadata));

            return Ok(_mapper.Map<IEnumerable<ProductDto>>(pagedProducts.Collection));
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            var product = await _productService.GetProduct(id);
            if (product is null)
                return NotFound();

            return Ok(_mapper.Map<ProductDto>(product));
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Post(ProductForCreationDto productDto)
        {
            var productToSave = _mapper.Map<Product>(productDto);
            var productToReturn = await _productService.AddProduct(productToSave);

            return new CreatedAtRouteResult("GetProduct",
                new { id = productToReturn.Id },
                _mapper.Map<ProductDto>(productToReturn));
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
            var productToReturn = await _productService.UpdateProduct(id, productToUpdate);
            if (productToReturn is null)
                return NotFound();

            return Ok(_mapper.Map<ProductDto>(productToReturn));
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
