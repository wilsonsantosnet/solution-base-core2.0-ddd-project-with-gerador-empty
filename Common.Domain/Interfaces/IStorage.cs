using System.IO;
using System.Threading.Tasks;

namespace Common.Domain.Interfaces
{
    public interface IStorage
    {
        Task Upload(byte[] imageBytes, string containerName, string filename);
        Task<MemoryStream> GetStream(string folder, string fileName);
        Task<MemoryStream> Download(string folder, string fileName);
        Task Delete(string containerName, string fileName);
    }
}