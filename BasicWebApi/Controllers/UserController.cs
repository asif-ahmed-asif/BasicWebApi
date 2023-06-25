using BasicWebApi.Data;
using BasicWebApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BasicWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly ApplicationDataContext _db;
        public UserController(ApplicationDataContext db) 
        { 
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return Ok(await _db.Users.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            return Ok(await _db.Users.FindAsync(id));
        }
        [HttpPost]
        public async Task<ActionResult<string>> AddUser(User user)
        {
            var addUser = new User
            {
                Name = user.Name,
                Email = user.Email,
                Mobile = user.Mobile,
                Password = user.Password
            };
            _db.Users.Add(addUser);
            await _db.SaveChangesAsync();
            return Ok("User successfully added");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
