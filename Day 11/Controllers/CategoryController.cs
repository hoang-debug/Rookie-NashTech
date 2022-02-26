using Day_11.Models;
using Day_11.Services;
using Microsoft.AspNetCore.Mvc;

namespace Day_11.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{

    private readonly ILogger<CategoryController> _logger;
    private readonly ICategoryService _categoryService;

    public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService)
    {
        _logger = logger;
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var entities = await _categoryService.GetAllAsync();
        var results = from item in entities
                      select new CategoryViewModel
                      {
                          Id = item.Id,
                          Name = item.Name,
                          Products = from p in item.Products
                                     select new ProductViewModel
                                     {
                                         Id = p.Id,
                                         Name = p.Name,
                                         Manufacturer = p.Manufacturer
                                     }
                      };
        return new JsonResult(results);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CategoryCreateModel model)
    {
        try
        {
            var entity = new Data.Entities.Category
            {
                Name = model.Name,
                Products = (from p in model.Products
                            select new Data.Entities.Product
                            {
                                Name = p.Name,
                                Manufacturer = p.Manufacturer
                            }).ToList()
            };

            var result = await _categoryService.AddAsync(entity);
            return new JsonResult(new CategoryViewModel
            {
                Id = result.Id,
                Name = result.Name,
                Products = from p in result.Products
                           select new ProductViewModel
                           {
                               Id = p.Id,
                               Name = p.Name,
                               Manufacturer = p.Manufacturer
                           }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOneAsync(int id)
    {
        var result = await _categoryService.GetOneAsync(id);
        return new JsonResult(new CategoryViewModel
        {
            Id = result.Id,
            Name = result.Name,
            Products = from p in result.Products
                       select new ProductViewModel
                       {
                           Id = p.Id,
                           Name = p.Name,
                           Manufacturer = p.Manufacturer
                       }
        });
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> EditAsync(int id, CategoryEditModel model)
    {
        var entity = await _categoryService.GetOneAsync(id);


        entity.Name = model.Name;
        // IEnumerable<ProductViewModel> enumerable = from p in entity.Products
        //                                            select new ProductViewModel
        //                                            {
        //                                                Id = p.Id,
        //                                                Name = p.Name,
        //                                                Manufacturer = p.Manufacturer
        //                                            };
        var result = await _categoryService.EditAsync(entity);
        return new JsonResult(new CategoryEditModel
            {
                Name = result.Name
            });
    }

    [HttpDelete]
    [Route("{id}")]

    public async Task<IActionResult> RemoveAsync(int id)
    {
        var entity = await _categoryService.GetOneAsync(id);

        await _categoryService.RemoveAsync(id, entity);
        return Ok();
    }
}
