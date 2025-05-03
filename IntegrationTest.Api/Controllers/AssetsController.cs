using EnkaSharp;
using EnkaSharp.AssetHandlers.Genshin;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly IEnkaClient _client;

        public AssetsController(IEnkaClient client)
        {
            _client = client;
        }

        [HttpGet("/localization")]
        public IActionResult GetLocalization()
        {
            Dictionary<string, Dictionary<string, string?>>? localization = _client.GetAssets().Localization;
            return Ok(localization?["ru"]);
        }


        [HttpGet("/characters")]
        public IActionResult GetCharacters()
        {
            Dictionary<string, CharacterData>? characters = _client.GetAssets().Characters;
            return Ok(characters);
        }

        [HttpGet("/namecards")]
        public IActionResult GetNameCards()
        {
            Dictionary<string, NameCard>? namecards = _client.GetAssets().NameCards;
            return Ok(namecards);
        }
        
        [HttpGet("/pfps")]
        public IActionResult GetProfilePictures()
        {
            Dictionary<string, ProfilePicture>? profilePictures = _client.GetAssets().ProfilePictures;
            return Ok(profilePictures);
        }
    }
}