using FreeCourse.Services.PhotoStock.Dtos;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomBaseController
    {
        [HttpPost("photosave")]
        public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken)
        {
            if (photo == null || photo.Length == 0)
            {
                return CreateActionResultInstance(ResponseDto<NoContent>.Fail("photo is empty", 400));

            }
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);

            using var stream = new FileStream(path, FileMode.Create);

            await photo.CopyToAsync(stream, cancellationToken);


            //http://www.photostock.api.com/photos/123asdf.jpg
            var returnPath = "photos/" + photo.FileName;

            PhotosDto photoDto = new PhotosDto { PhotoURL = returnPath };

            return CreateActionResultInstance(ResponseDto<PhotosDto>.Success(photoDto, 200));
        }

        [HttpDelete("photodelete")]
        public IActionResult PhotoDelete(string photoName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoName);
            if(!System.IO.File.Exists(path))
            {
                return CreateActionResultInstance(ResponseDto<NoContent>.Fail("photo not found", 404));
            }
            System.IO.File.Delete(path);

            return CreateActionResultInstance(ResponseDto<NoContent>.Success(204));
        }

    }
}
