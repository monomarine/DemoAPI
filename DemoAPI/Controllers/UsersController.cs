using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DemoAPI.Models;
using System.Collections;
using Microsoft.AspNetCore.Http.HttpResults;
using DemoAPI.Repositories;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;
        public UsersController (IUserRepository repo)
        {
            _repo = repo;
        }
     
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(_repo.GetAllUsers());
        }
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _repo.GetUserById(id);

            if (user == null)
                return NotFound();
            else
                return Ok(user);
        }

        [HttpPost] 
        public ActionResult<User> CreateUser([FromBody] User user)
        {
            if (user is null)
                return BadRequest("объект пользователя пришел пустым");

            if (String.IsNullOrEmpty(user.Email) ||
                String.IsNullOrEmpty(user.Login))
                return BadRequest("пустое поле почты или логина");

            var newUser = _repo.AddUser(user);

            return CreatedAtAction(nameof(CreateUser), 
                new {Id = newUser.Id}, newUser);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            if (_repo.DeleteUser(id))
                return NoContent();
            else
                return NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult<User> UpdateUser(int id, [FromBody] User user)
        {
            if (user is null)
                return BadRequest("некорретные данные для обновления");
            if( id != user.Id)
                return BadRequest("несовпадения по id");

            var updateUser = _repo.UpdateUser(id, user);
            return Ok(updateUser);

        }
    }
}
