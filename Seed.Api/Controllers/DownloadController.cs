using Common.Domain.Base;
using Common.Domain.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO;
using System.Threading.Tasks;

namespace Seed.Api.Controllers
{
    [Route("api/document/download")]
    public class DownloadController : Controller
    {

        private readonly ILogger _logger;
        private readonly IHostingEnvironment _env;
        private readonly string _uploadRoot;
        private readonly IStorage _storage;
        private readonly ConfigSettingsBase _configSettingsBase;

        public DownloadController(ILoggerFactory logger, IHostingEnvironment env,IOptions<ConfigSettingsBase> configSettingsBase, IStorage storage)
        {
            this._logger = logger.CreateLogger<DownloadController>();
            this._env = env;
            this._uploadRoot = "upload";
            this._storage = storage;
            this._configSettingsBase = configSettingsBase.Value;
        }

      
        [HttpGet("{folder}/{fileName}")]
        public async Task<IActionResult> Get(string folder, string fileName)
        {
            if (this._configSettingsBase.EnabledStorage)
				return await StorageSystemDonwload(folder, fileName);

            return await FileSystemDonwload(folder, fileName);
        }

        private async Task<IActionResult> StorageSystemDonwload(string folder, string fileName)
        {
            var ms = await this._storage.Download(folder, fileName);
            return File(ms.ToArray(), getContentType(fileName));
        }

        private async Task<IActionResult> FileSystemDonwload(string folder, string fileName)
        {
            var uploads = Path.Combine(this._env.ContentRootPath, this._uploadRoot, folder);
            var filePath = $"{uploads}\\{fileName}";
            byte[] bytes;

            if (System.IO.File.Exists(filePath))
            {
                using (FileStream SourceStream = System.IO.File.Open(filePath, FileMode.Open))
                {
                    bytes = new byte[SourceStream.Length];
                    await SourceStream.ReadAsync(bytes, 0, (int)SourceStream.Length);
                }
                return File(bytes, getContentType(filePath));
            }

            var fileVazio = $"{uploads}\\vazio.png";

            using (FileStream SourceStream = System.IO.File.Open(fileVazio, FileMode.Open))
            {
                bytes = new byte[SourceStream.Length];
                await SourceStream.ReadAsync(bytes, 0, (int)SourceStream.Length);
            }

            return File(bytes, "image/png");
        }

        private string getContentType(string filePath)
        {
            switch (Path.GetExtension(filePath).ToLower())
            {
                case ".pdf":
                    return "application/pdf";

                case ".png":
                    return "image/png";

                case ".jpg":
                    return "image/jpg";

                case ".gif":
                    return "image/gif";

                case ".jpeg":
                    return "image/jpeg";

                case ".bmp":
                    return "image/bmp";

                case ".webp":
                    return "image/webp";

                case ".txt":
                    return "text/plain";

                case ".html":
                    return "text/html";

                case ".css":
                    return "text/css";

                case ".js":
                    return "text/javascript";

                case ".ppt":
                    return "application/vnd.mspowerpoint";

                case ".xml":
                    return "application/xml";

                case ".xmls":
                    return "application/xmls";

                case ".xlsx":
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                case ".xls":
                    return "application/vnd.ms-excel";

                case ".doc":
                    return "application/octet-stream";

                default:
                    return string.Empty;
            }
        }
    }
}
