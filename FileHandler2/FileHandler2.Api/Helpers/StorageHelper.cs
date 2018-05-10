using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileHandler2.Api.Dto;
using FileHandler2.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace FileHandler2.Api.Helpers
{
    public static class StorageHelper
    {

        public static bool IsImage(IFormFile file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }

            string[] formats = new string[] { ".jpg", ".png", ".gif", ".jpeg" };

            return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }

        public static async Task<UploadResultDto> UploadFileToStorage(Stream fileStream, string fileName, AzureStorageConfig _storageConfig)
        {
            // Create storagecredentials object by reading the values from the configuration (appsettings.json)
            StorageCredentials storageCredentials = new StorageCredentials(_storageConfig.AccountName, _storageConfig.AccountKey);

            // Create cloudstorage account by passing the storagecredentials
            CloudStorageAccount storageAccount = new CloudStorageAccount(storageCredentials, true);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Get reference to the blob container by passing the name by reading the value from the configuration (appsettings.json)
            CloudBlobContainer container = blobClient.GetContainerReference(_storageConfig.FileHandlerContainer);

            // Get the reference to the block blob from the container
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

            // Upload the file
            await blockBlob.UploadFromStreamAsync(fileStream);
            
            UploadResultDto result = new UploadResultDto()
            {
                FileUrl = blockBlob.Uri.AbsoluteUri,
                Success = true
            };

            return await Task.FromResult(result);
        }

        public static async Task<MemoryStream> GetFileContent(AzureStorageConfig _storageConfig, Guid fileUid)
        {

            // Create storagecredentials object by reading the values from the configuration (appsettings.json)
            StorageCredentials storageCredentials = new StorageCredentials(_storageConfig.AccountName, _storageConfig.AccountKey);

            // Create cloudstorage account by passing the storagecredentials
            CloudStorageAccount storageAccount = new CloudStorageAccount(storageCredentials, true);

            // Create blob client
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Get reference to the container
            CloudBlobContainer container = blobClient.GetContainerReference(_storageConfig.FileHandlerContainer);

            // Set the permission of the container to public
            await container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{fileUid}");

            MemoryStream memStream = new MemoryStream();
            await blockBlob.DownloadToStreamAsync(memStream);

            return memStream;
        }

    }
}
