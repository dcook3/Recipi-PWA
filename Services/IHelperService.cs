using Recipi_PWA.Models;

namespace Recipi_PWA.Services
{
    public interface IHelperService<T>
    {
        string FormatDescription(string desc, List<T> ings);
    }
}