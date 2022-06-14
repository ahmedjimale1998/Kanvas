using AutoMapper;
using MailService.DTOs;
using MailService.Interfaces;
using MailService.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MailService.Controller
{

    [Route("/api/[controller]")]
    [ApiController]
    public class MailController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IMailRepository Repository;
        private readonly IMapper _mapper;

        public MailController(
            IMailRepository repository,
            IMapper mapper)
        {
            this.Repository = repository;
            this._mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MailCreatDto mailDTO)
        {
            var mail = _mapper.Map<Mail>(mailDTO);
            mail.Id = Guid.NewGuid();
            var savedMail = await Repository.Add(mail);
            return Ok(_mapper.Map<MailReadDto>(mailDTO));
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var mail = await Repository.Get(id);

            return Ok(_mapper.Map<MailReadDto>(mail));
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAllUsers()
        {
            var mails = await Repository.GetAll();
            return Ok(_mapper.Map<List<MailReadDto>>(mails));
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid guid)
        {
            await Repository.Delete(guid);
            return Ok();
        }

    }
}
