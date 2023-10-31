using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace RazorStore.Services;

public interface IUploadPhoto
{
   public List<string> UploadPhotos(IEnumerable<IFormFile> Photo, IWebHostEnvironment host);

}