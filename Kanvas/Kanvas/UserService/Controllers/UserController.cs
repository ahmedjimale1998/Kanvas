using Mapster;
using Microsoft.AspNetCore.Mvc;
using UserService.DTOs;
using UserService.Interfaces;
using UserService.Models;

namespace UserService.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository userRepo;
        private readonly ILogger<UserController> logger;

        public UserController(IUserRepository userRepo, ILogger<UserController> logger)
        {
            this.userRepo = userRepo;
            this.logger = logger;
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
            this.logger.LogInformation("Get All Users");
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
        [Route("{id}")]
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
