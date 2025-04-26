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

        [HttpGet("/hoyos")]
        public async Task<IActionResult> Get(string userName)
        {
            Hoyos user = await _enkaClient.User.GetHoyosAsync(userName);
            return Ok(user);
        }
        
        [HttpGet("/snapshot")]
        public async Task<IActionResult> GetSnapshot(string userName)
        {
            Snapshot user = await _enkaClient.User.GetSnapshotAsync(userName);
            return Ok(user);
        }
        
    }
}
