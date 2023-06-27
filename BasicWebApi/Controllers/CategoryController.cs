using BasicWebApi.Data;
using BasicWebApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BasicWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDataContext _db;
        public CategoryController(ApplicationDataContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return Ok(await _db.Categories.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            return Ok(await _db.Categories.FindAsync(id));
        }
        [HttpPost]
        public async Task<ActionResult<string>> AddCategory(Category category)
        {
            var addCategory = new Category
            {
                Name = category.Name,
                DisplayOrder = category.DisplayOrder
            };
            _db.Categories.Add(addCategory);
            await _db.SaveChangesAsync();
            return Ok("Category successfully added");
        }
        [HttpPut]
        public async Task<ActionResult<string>> UpdateCategory(Category category)
        {
            var findCategory = await _db.Categories.FindAsync(category.Id);
            if (findCategory == null)
            {
                return NotFound("Category not found");
            }

            findCategory.Name = category.Name;
            findCategory.DisplayOrder = category.DisplayOrder;
            await _db.SaveChangesAsync();
            return Ok("Category successfully updated");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteCategory(int id)
        {
            var findCategory = await _db.Categories.FindAsync(id);
            if (findCategory == null)
            {
                return NotFound("Category not found");
            }

            _db.Categories.Remove(findCategory);
            await _db.SaveChangesAsync();
            return Ok("Category successfully deleted");
        }

        [HttpGet("search/{key}")]
        public async Task<ActionResult<IEnumerable<Category>>> SearchCategory(string key)
        {
            var categories = await _db.Categories.Where(s => s.Name.StartsWith(key)).ToListAsync();
            if (categories.Any())
            {
                return Ok(categories);
            }
            return NotFound("No data Found");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
