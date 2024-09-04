using AlbumProject.Models;
using AlbumProject.Persistence.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlbumProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;
        private readonly IAuthenticationService _authenticationService;

        public AlbumController(IAlbumService albumService, IAuthenticationService authenticationService)
        {
            _albumService = albumService;
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Album>> GetAllAlbums()
        {
            var albums = _albumService.GetAllAlbums();
            return Ok(albums);
        }

        [Authorize]
        [HttpGet("my-albums")]
        public ActionResult<IEnumerable<Album>> GetMyAlbums()
        {
            var userId = int.Parse(User.Identity.Name);
            var albums = _albumService.GetAlbumsByUser(userId);
            return Ok(albums);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAlbum([FromBody] Album album)
        {
            var userId = int.Parse(User.Identity.Name);
            album.UserId = userId;

            var createdAlbum = await _albumService.CreateAlbum(album);
            return CreatedAtAction(nameof(GetAllAlbums), new { id = createdAlbum.AlbumId }, createdAlbum);
        }

        [Authorize]
        [HttpDelete("{albumId}")]
        public async Task<IActionResult> DeleteAlbum(int albumId)
        {
            var user = await _authenticationService.Authenticate(User.Identity.Name, null);

            await _albumService.DeleteAlbum(albumId, user);
            return NoContent();
        }
    }
}
