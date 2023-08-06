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
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryWithoutProductsDto>>> GetAll(
            [FromQuery] PaginationParameters paginationParameters,
            [FromQuery] SearchParameters searchParameters)
        {
            var pagedCategories = await _categoryService
                .GetCategories(paginationParameters, searchParameters);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedCategories.PaginationMetadata));

            return Ok(_mapper.Map<IEnumerable<CategoryWithoutProductsDto>>(pagedCategories.Collection));
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<ActionResult<ICategoryDto>> Get(int id, bool includeProducts = false)
        {
            Category? category;
            if (includeProducts)
                category = await _categoryService.GetCategoryWithProducts(id);
            else
                category = await _categoryService.GetCategory(id);

            if (category is null)
                return NotFound();

            if (includeProducts)
                return Ok(_mapper.Map<CategoryWithProductsDto>(category));

            return Ok(_mapper.Map<CategoryWithoutProductsDto>(category));
        }

        [HttpPost]
        public async Task<ActionResult<CategoryWithoutProductsDto>> Post(CategoryForCreationDto categoryDto)
        {
            var categoryToSave = _mapper.Map<Category>(categoryDto);
            var categoryToReturn = await _categoryService.AddCategory(categoryToSave);

            return new CreatedAtRouteResult("GetCategory",
                new { id = categoryToReturn.Id },
                _mapper.Map<CategoryWithoutProductsDto>(categoryToReturn));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryWithoutProductsDto>> Put(int id, CategoryForUpdateDto categoryDto)
        {
            if (id != categoryDto.Id)
            {
                ModelState.AddModelError("id", "Id mismatch.");
                return BadRequest(ModelState);
            }

            var categoryToUpdate = _mapper.Map<Category>(categoryDto);
            var categoryToReturn = await _categoryService.UpdateCategory(id, categoryToUpdate);
            if (categoryToReturn is null)
                return NotFound();

            return Ok(_mapper.Map<CategoryWithoutProductsDto>(categoryToReturn));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var isRemoved = await _categoryService.RemoveCategory(id);
            if (!isRemoved)
                return NotFound();

            return NoContent();
        }
    }
}
