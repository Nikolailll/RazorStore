using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace RazorStore.Services;

public class UploadPhoto : IUploadPhoto
{
    public UploadPhoto()
    {
        
    }
    public List<string> UploadPhotos( IEnumerable<IFormFile> Photo, IWebHostEnvironment host)
    {
        List<string> uniqName = new();
        if (Photo != null)
        {
            foreach (var i in Photo)
            {
                var path = Path.Combine(host.WebRootPath, "images");
                var uniqNames = Guid.NewGuid().ToString() + "_" + i.FileName;
                uniqName.Add(uniqNames);
                var filePath = Path.Combine(path, uniqNames);
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    i.CopyTo(fs);
                }
            }
            

        }
        return uniqName;

    }
}