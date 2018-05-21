using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Common.Domain.Base;
using System.IO;
using System.Threading.Tasks;

namespace Common.API
{
    public class Upload
    {
        private FilterBase _filter;
        private string _rootFolder;

        public Upload(FilterBase filter)
        {
            this._filter = filter;
            this._rootFolder = "upload";
        }

        public async virtual Task<List<string>> Save(string rootPath, string folder, ICollection<IFormFile> files)
        {
            var uploadPath = this.GetUploadPath(rootPath, folder);
            var fileSuccess = new List<string>();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var newFileName = string.Format("{0}{1}", Guid.NewGuid().ToString(), Path.GetExtension(file.FileName));
                    using (var fileStream = new FileStream(Path.Combine(uploadPath, newFileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    fileSuccess.Add(newFileName);
                }
            }

            return fileSuccess;
        }

        public virtual string GetUploadPath(string rootPath, string folder)
        {
            var uploads = Path.Combine(rootPath, makeFolderUpload(folder));
            if (!Directory.Exists(uploads))
                Directory.CreateDirectory(uploads);
            return uploads;
        }

      
        public async Task<Boolean> Delete(string rootPath, string folder, string fileName)
        {
            var uploads = this.GetUploadPath(rootPath, folder);
            await Task.Run(() => {
                new FileInfo(Path.Combine(uploads, fileName)).Delete();
            });
            return true;
        }


        private string makeFolderUpload(string folder)
        {
            return this._rootFolder + "\\" + folder;
        }

    }
}
