using Microsoft.AspNetCore.Components.Forms;
using Recipi_PWA.Models;
using System.Reflection.PortableExecutable;

namespace Recipi_PWA.Services
{
    public class MediaUploadService : DefaultHttpService, IMediaUploadService
    {
        public MediaUploadService(HttpClient httpClient, StateContainer _state) : base(httpClient, _state)
        {
        }

        public async Task<HttpResponseMessage> GetPresignedUrl() => await client.GetAsync("/api/MediaUpload");

        public async Task<HttpResponseMessage> UploadToS3(string presignedUrl, IBrowserFile file)
        {
            var streamContent = new StreamContent(file.OpenReadStream());
            streamContent.Headers.Add("Content-Type", "image/png");
            return await new HttpClient().PutAsync(presignedUrl, streamContent);
        }
    }
}
