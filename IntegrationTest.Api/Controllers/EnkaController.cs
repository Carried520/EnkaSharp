
using EnkaSharp;
using EnkaSharp.Entities;
using EnkaSharp.Entities.Base;
using Microsoft.AspNetCore.Http.HttpResults;
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

        [HttpGet("/userinfo")]
        public async Task<IActionResult> GetUserInfo(long uid)
        {
            EnkaInfo info = await _enkaClient.User.GetUserInfoAsync(uid);
            return Ok(info);
        }
        
        
        [HttpGet("/user")]
        public async Task<IActionResult> GetUser(long uid)
        {
            EnkaRestUser restUser = await _enkaClient.User.GetUserAsync(uid);
            return Ok(restUser);
        }
        
    }
}
