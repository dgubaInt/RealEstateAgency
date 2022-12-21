using Microsoft.AspNetCore.Http;
using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Interfaces;
using System.Net;

namespace RealEstateAgency.Service.ImageService
{
    public class ImageService : IImageService
    {
        private readonly IGenericRepository<Photo> _photoRepository;

        public ImageService(IGenericRepository<Photo> photoRepository)
        {
            _photoRepository = photoRepository;
        }

        public bool UploadImage(IFormFile file, string fileName)
        {
            try
            {
                var request = (FtpWebRequest)WebRequest.Create("" + fileName);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential("RealEstate", "RealEstate");

                byte[] buffer = new byte[1024];
                var stream = file.OpenReadStream();
                byte[] fileContents;

                using (var ms = new MemoryStream())
                {
                    int read;
                    while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                    fileContents = ms.ToArray();
                }

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(fileContents, 0, fileContents.Length);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteImage(string imageName)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("" + imageName);
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                request.Credentials = new NetworkCredential("RealEstate", "RealEstate");
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                response.Close();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public string DownloadImage(string imageName)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("" + imageName);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential("RealEstate", "RealEstate");
                request.UsePassive = true;
                request.UseBinary = true;
                request.EnableSsl = false;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                string base64String;

                using (MemoryStream stream = new MemoryStream())
                {
                    response.GetResponseStream().CopyTo(stream);
                    base64String = Convert.ToBase64String(stream.ToArray(), 0, stream.ToArray().Length);
                }
                return "data:image/png;base64," + base64String;
            }
            catch
            {
                throw;
            }
        }

        public async Task AddAsync(Photo photo)
        {
            await _photoRepository.AddAsync(photo);
        }

        public async Task<IEnumerable<Photo>> GetAllAsync()
        {
            return await _photoRepository.GetAllAsync();
        }
    }
}
