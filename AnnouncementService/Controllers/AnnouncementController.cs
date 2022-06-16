using AnnouncementService.DTOs;
using AnnouncementService.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnnouncementService.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class AnnouncementController : Controller
    {
        private readonly IAnnouncementRepository _announcementRepository;
        private readonly ILogger<AnnouncementController> _logger;
        private readonly IMapper _mapper;

        public AnnouncementController(
            IAnnouncementRepository announcementRepository,
            ILogger<AnnouncementController> logger,
            IMapper mapper)
        {
            _announcementRepository = announcementRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<AnnouncementReadDto>> Add([FromBody] AnnouncementCreateDto userDTO)
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

            // Send Async Message
            try
            {
                var userPublishedDto = _mapper.Map<UserPublishedDto>(userReadDto);
                userPublishedDto.Event = "User_Published";
                _messageBusClient.PublishNewUser(userPublishedDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send asynchronously: {ex.Message}");
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
        public IActionResult Test()
        {
            var testUSer = new User(Guid.NewGuid(), "test", "testEmail", "testpassword", "TestTeacher", 1);
            return Ok(testUSer);
        }
    }
