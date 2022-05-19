using Mapster;
using Microsoft.AspNetCore.Mvc;
using UserService.DTOs;
using UserService.Interfaces;
using UserService.Models;

namespace UserService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository userRepo;

        public UserController(IUserRepository userRepo)
        {
            this.userRepo = userRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddUserDTO userDTO)
        {
            var user = userDTO.Adapt<User>();
            user.Id = Guid.NewGuid();
            var savedUser = await userRepo.Add(user);
            return Ok(savedUser);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await userRepo.Get(id);
            return Ok(user);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await userRepo.GetAllUsers();
            return Ok(users);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] User user)
        {
            await userRepo.Update(user);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid guid)
        {
            await userRepo.Delete(guid);
            return Ok();
        }

        /*  [HttpGet]
            [Route("getAllByClassId/{id}")]
            public async Task<IActionResult> GetAllUsersByClassId(int id)
            {
                var users = await userRepo.GetAllUsersByClassId(id);

                return Ok(users);
            }*/



    }
}
