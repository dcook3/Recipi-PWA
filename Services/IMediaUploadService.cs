using Microsoft.AspNetCore.Components.Forms;

namespace Recipi_PWA.Services
{
    public interface IMediaUploadService : IDefaultHttpService
    {
        Task<HttpResponseMessage> GetPresignedUrl();
        Task<HttpResponseMessage> UploadToS3(string presignedUrl, IBrowserFile file);
    }
}