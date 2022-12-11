using AspNetCore.Yandex.ObjectStorage;
using System.IO;
using System.Threading.Tasks;

namespace WebApplicationLab
{
    public class PictureModel
    {
        public string ImageLinc { get; set; }

        public string ImageName { get; set; }

        public byte[] PrImageData { get; set; }

        public string PrImageName { get; set; }

        public async Task<string> SaveImageInLocal(YandexStorageService yandexObjectStorage)
        {
            var result = yandexObjectStorage.ObjectService.GetAsync(ImageName).Result;
            if (result.IsSuccessStatusCode)
            {
                var bytes = await result.ReadAsByteArrayAsync();
                PrImageData = bytes.Value;
                File.WriteAllBytes(ImageName, PrImageData);
                return Path.Combine( Directory.GetCurrentDirectory(), ImageName);
            }
            return null;
        }
    }
}
