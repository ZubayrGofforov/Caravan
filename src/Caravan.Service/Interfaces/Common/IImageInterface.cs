using Microsoft.AspNetCore.Http;

namespace Caravan.Service.Interfaces.Common;

public interface IImageInterface
{
    public Task<string> SaveImageAsync(IFormFile file);
}
