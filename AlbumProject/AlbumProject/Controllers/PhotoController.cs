using AlbumProject.Models;
using AlbumProject.Persistence.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlbumProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _photoService;
        private readonly IAuthenticationService _authenticationService;

        public PhotoController(IPhotoService photoService, IAuthenticationService authenticationService)
        {
            _photoService = photoService;
            _authenticationService = authenticationService;
        }

        [HttpGet("album/{albumId}")]
        public async Task<ActionResult<IEnumerable<Photo>>> GetPhotosByAlbum(int albumId)
        {
            var photos = await _photoService.GetPhotosByAlbum(albumId);
            return Ok(photos);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPhoto([FromBody] Photo photo)
        {
            var addedPhoto = await _photoService.AddPhoto(photo);
            return CreatedAtAction(nameof(GetPhotosByAlbum), new { albumId = addedPhoto.AlbumId }, addedPhoto);
        }

        [Authorize]
        [HttpPost("{photoId}/like")]
        public async Task<IActionResult> LikePhoto(int photoId)
        {
            var user = await _authenticationService.Authenticate(User.Identity.Name, null);
            await _photoService.LikePhoto(photoId, user);
            return NoContent();
        }

        [Authorize]
        [HttpPost("{photoId}/dislike")]
        public async Task<IActionResult> DislikePhoto(int photoId)
        {
            var user = await _authenticationService.Authenticate(User.Identity.Name, null);
            await _photoService.DislikePhoto(photoId, user);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{photoId}")]
        public async Task<IActionResult> DeletePhoto(int photoId)
        {
            var user = await _authenticationService.Authenticate(User.Identity.Name, null);
            await _photoService.DeletePhoto(photoId, user);
            return NoContent();
        }
    }
}
