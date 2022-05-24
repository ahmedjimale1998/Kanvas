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

        public MailController(IMailRepository repository)
        {
            this.Repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MailDTO mailDTO)
        {
            var directory = Directory.GetCurrentDirectory();
            var mail = mailDTO.Adapt<Mail>();
            mail.Id = Guid.NewGuid();
            var savedMail = await Repository.Add(mail);
            return Ok(savedMail);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var mail = await Repository.Get(id);
            return Ok(mail);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAllUsers()
        {
            var mails = await Repository.GetAll();
            return Ok(mails);
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(Guid guid)
        {
            await Repository.Delete(guid);
            return Ok();
        }

    }
}
