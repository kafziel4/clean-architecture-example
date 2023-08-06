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
            [FromQuery] PaginationParameters parameters)
        {
            var (categoryEntities, paginationMetadata) = await _categoryService.GetCategories(parameters);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<CategoryWithoutProductsDto>>(categoryEntities));
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<ActionResult<ICategoryDto>> Get(int id, bool includeProducts = false)
        {
            Category? categoryEntity;
            if (includeProducts)
                categoryEntity = await _categoryService.GetCategoryWithProducts(id);
            else
                categoryEntity = await _categoryService.GetCategory(id);

            if (categoryEntity is null)
                return NotFound();

            if (includeProducts)
                return Ok(_mapper.Map<CategoryWithProductsDto>(categoryEntity));

            return Ok(_mapper.Map<CategoryWithoutProductsDto>(categoryEntity));
        }

        [HttpPost]
        public async Task<ActionResult<CategoryWithoutProductsDto>> Post(CategoryForCreationDto categoryDto)
        {
            var categoryToSave = _mapper.Map<Category>(categoryDto);
            var categoryEntity = await _categoryService.AddCategory(categoryToSave);

            return new CreatedAtRouteResult("GetCategory",
                new { id = categoryEntity.Id },
                _mapper.Map<CategoryWithoutProductsDto>(categoryEntity));
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
            var categoryEntity = await _categoryService.UpdateCategory(id, categoryToUpdate);
            if (categoryEntity is null)
                return NotFound();

            return Ok(_mapper.Map<CategoryWithoutProductsDto>(categoryEntity));
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
