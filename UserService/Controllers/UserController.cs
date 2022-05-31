using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserService.AsyncDataService;
using UserService.DTOs;
using UserService.Interfaces;
using UserService.Models;
using UserService.SyncDataServices.Http;

namespace UserService.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository userRepo;
        private readonly ILogger<UserController> logger;
        private readonly IMapper _mapper;
        private readonly IMailDataClient _mailDataClient;
        private readonly IMessageBusClient _messageBusClient;

        public UserController(
            IUserRepository userRepo,
            ILogger<UserController> logger,
            IMapper mapper,
            IMessageBusClient messageClient,
            IMailDataClient mailDataClient)
        {
            this.userRepo = userRepo;
            this.logger = logger;
            this._mapper = mapper;
            this._mailDataClient = mailDataClient;
            this._messageBusClient = messageClient;
        }

        [HttpPost]
        public async Task<ActionResult<UserReadDto>> Add([FromBody] UserCreatDto userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            user.Id = Guid.NewGuid();
            var savedUser = await userRepo.Add(user);
            var userReadDto = _mapper.Map<UserReadDto>(savedUser);

            // Send Sync Message
            try
            {
                await _mailDataClient.SendUserToMail(userReadDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send : {ex.Message}");
            }


            return Ok(userReadDto);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await userRepo.Get(id);
            return Ok(_mapper.Map<UserReadDto>(user));
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await userRepo.GetAllUsers();
            return Ok(_mapper.Map<List<UserReadDto>>(users));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserReadDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
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

        [HttpGet]
        [Route("test")]
        public async Task<IActionResult> test(Guid guid)
        {
            var testUSer = new User(Guid.NewGuid(), "test","testEmail", "testpassword", "TestTeacher", 1  );
            return Ok(testUSer);
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
