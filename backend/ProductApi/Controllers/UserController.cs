using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Services;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowAngularApp")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetById(int id)
        {
            var user = _service.GetById(id);
            if (user is null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public ActionResult<User> Create(User user)
        {
            var created = _service.Create(user);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, User user)
        {
            var success = _service.Update(id, user);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var success = _service.Delete(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}