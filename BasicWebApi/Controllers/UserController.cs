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
        [HttpPut]
        public async Task<ActionResult<string>> UpdateUser(User user)
        {
            var findUser = await _db.Users.FindAsync(user.Id);
            if (findUser == null)
            {
                return NotFound("User not found");
            }
            
            findUser.Name = user.Name;
            findUser.Email = user.Email;
            findUser.Mobile = user.Mobile;
            findUser.Password = user.Password;
            await _db.SaveChangesAsync();
            return Ok("User successfully updated");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteUser(int id)
        {
            var findUser = await _db.Users.FindAsync(id);
            if (findUser == null)
            {
                return NotFound("User not found");
            }
            
            _db.Users.Remove(findUser);
            await _db.SaveChangesAsync();
            return Ok("User successfully deleted");
        }

        [HttpGet("search/{key}")]
        public async Task<ActionResult<IEnumerable<User>>> SearchUser(string key)
        {
            var user = await _db.Users.Where(s => s.Name.StartsWith(key)).ToListAsync();
                if (user.Any())
                {
                    return Ok(user);
                }
            return NotFound("No data Found");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
