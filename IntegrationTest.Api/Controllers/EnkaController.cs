
using EnkaSharp;
using EnkaSharp.Entities.Genshin.Abstractions;
using EnkaSharp.Entities.Genshin.Raw;
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

        [HttpGet("/userinfo")]
        public async Task<IActionResult> GetUserInfo(long uid)
        {
            EnkaGenshinInfo genshinInfo = await _enkaClient.Genshin.GetGenshinInfoAsync(uid);
            return Ok(genshinInfo);
        }
        
        
        [HttpGet("/user")]
        public async Task<IActionResult> GetUser(long uid)
        {
            EnkaGenshinData genshinData = await _enkaClient.Genshin.GetGenshinDataAsync(uid);
            return Ok(genshinData);
        }
        
        
    }
}
