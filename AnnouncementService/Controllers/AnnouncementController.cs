using AnnouncementService.DTOs;
using AnnouncementService.Interfaces;
using AnnouncementService.Models;
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
        public async Task<ActionResult<AnnouncementReadDto>> Add([FromBody] AnnouncementCreateDto announcementCreateDto)
        {
            var announcement = _mapper.Map<Announcement>(announcementCreateDto);
            announcement.Id = Guid.NewGuid();
            announcement.Date = DateTime.Now.ToUniversalTime();
            var savedUser = await _announcementRepository.Add(announcement);
            var announcementReadDto = _mapper.Map<AnnouncementReadDto>(savedUser);

            return Ok(announcementReadDto);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var announcement = await _announcementRepository.GetById(id);
            return Ok(_mapper.Map<AnnouncementReadDto>(announcement));
        }

        [HttpGet]
        [Route("getbyannouncementid/{id}")]
        public async Task<IActionResult> GetAllAnnouncementByClassId(int id)
        {
            var announcement = await _announcementRepository.GetAllAnnouncementByClassId(id);
            return Ok(_mapper.Map<List<AnnouncementReadDto>>(announcement));
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAllAnnouncements()
        {
            var announcements = await _announcementRepository.GetAll();
            return Ok(_mapper.Map<List<AnnouncementReadDto>>(announcements));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AnnouncementReadDto announcementReadDto)
        {
            var announcement = _mapper.Map<Announcement>(announcementReadDto);
            await _announcementRepository.Update(announcement);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _announcementRepository.Delete(id);
            return Ok();
        }
        /*
                [HttpGet]
                [Route("test")]
                public IActionResult Test()
                {
                    var testUSer = new User(Guid.NewGuid(), "test", "testEmail", "testpassword", "TestTeacher", 1);
                    return Ok(testUSer);
                }*/
    }
}
