using System.ComponentModel.DataAnnotations;
using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Services;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Catalog.Controllers
{
    
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize(Policy = "CatalogScope")]
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();

            return CreateActionResultInstance(categories);
        }

        //categories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            return CreateActionResultInstance(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryDto categoryDto)
        {
            var response=await _categoryService.CreateAsync(categoryDto);

            return CreateActionResultInstance(response);
        }



    }
}
