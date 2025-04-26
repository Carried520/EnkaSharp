using Enka.Client;
using Enka.Client.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnkaController : ControllerBase
    {
        private readonly IEnkaClient _enkaClient;

        public EnkaController(IEnkaClient enkaClient)
        {
            _enkaClient = enkaClient;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string userName)
        {
            EnkaUser user = await _enkaClient.User.GetEnkaUserAsync(userName);
            return Ok(user);
        }
    }
}
