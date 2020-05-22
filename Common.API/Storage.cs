using Common.Domain.Base;
using Common.Domain.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Common.Api
{
    public class Storage : IStorage
    {
        private CloudBlobContainer _cloudBlobContainer;
        private readonly ConfigStorageConnectionStringBase _configCacheConnectionStringBase;

        public Storage(IOptions<ConfigStorageConnectionStringBase> configCacheConnectionStringBase)
        {
            this._configCacheConnectionStringBase = configCacheConnectionStringBase.Value;
        }

        public async Task Upload(byte[] imageBytes, string containerName, string filename)
        {
            if (CloudStorageAccount.TryParse(this._configCacheConnectionStringBase.Default, out CloudStorageAccount storageAccount))
            {
                await this.ConfigCloudBlobContainer(containerName, storageAccount);
                var blob = _cloudBlobContainer.GetBlockBlobReference(filename);
                await blob.UploadFromByteArrayAsync(imageBytes, 0, imageBytes.Length);
            }

        }

        public async Task<MemoryStream> Download(string containerName, string fileName)
        {
            if (CloudStorageAccount.TryParse(this._configCacheConnectionStringBase.Default, out CloudStorageAccount storageAccount))
            {
                await this.ConfigCloudBlobContainer(containerName, storageAccount);
                var cloudBlockBlob = _cloudBlobContainer.GetBlockBlobReference(fileName);
                using (MemoryStream ms = new MemoryStream())
                {
                    await cloudBlockBlob.DownloadToStreamAsync(ms);
                    return ms;
                }
            }

            throw new InvalidOperationException("Storage error connect");
        }

        public async Task Delete(string containerName, string fileName)
        {
            if (CloudStorageAccount.TryParse(this._configCacheConnectionStringBase.Default, out CloudStorageAccount storageAccount))
            {
                await this.ConfigCloudBlobContainer(containerName, storageAccount);
                var cloudBlockBlob = _cloudBlobContainer.GetBlockBlobReference(fileName);
                await cloudBlockBlob.DeleteAsync();
            }
        }

        private async Task ConfigCloudBlobContainer(string containerName, CloudStorageAccount storageAccount)
        {
            var cloudBlobClient = storageAccount.CreateCloudBlobClient();
            _cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName.ToLower());
            await _cloudBlobContainer.CreateIfNotExistsAsync();
            var permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };
            await _cloudBlobContainer.SetPermissionsAsync(permissions);
        }
        public async Task<MemoryStream> GetStream(string containerName, string fileName)
        {
            if (CloudStorageAccount.TryParse(this._configCacheConnectionStringBase.Default, out CloudStorageAccount storageAccount))
            {
                await this.ConfigCloudBlobContainer(containerName, storageAccount);
                var cloudBlockBlob = _cloudBlobContainer.GetBlockBlobReference(fileName);
                var ms = new MemoryStream();
                await cloudBlockBlob.DownloadToStreamAsync(ms);
                return ms;
            }

            throw new InvalidOperationException("Storage error connect");
        }

    }
}
