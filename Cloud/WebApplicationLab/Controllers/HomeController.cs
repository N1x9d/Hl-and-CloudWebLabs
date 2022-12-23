using Amazon.S3;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AspNetCore.Yandex.ObjectStorage.Object.Responses;
using AspNetCore.Yandex.ObjectStorage;
using System.IO;
using Nancy.Json;

namespace WebApplicationLab.Controllers
{

    [ApiController]
    [Route("/")]
    public class HomeController : Controller
    {
        public static AllDataModel allDataForFront = new AllDataModel();
        public HomeController(YandexStorageService yandexObjectStorage)
        {
            _objectStoreService = yandexObjectStorage;
        }

        static AmazonS3Config configsS3 = new AmazonS3Config
        {
            ServiceURL = "https://s3.yandexcloud.net"
        };
        public static string AwsAccessKey = "ajepl08362ntkdvulaeg";
        public static string AwsSecretAccessKey = "AQVN2agQMwr9o3_qqdDKu82OI-H1a6CG6zANtLqg";
        private YandexStorageService _objectStoreService;

        [HttpPost("/Send")]
        public async Task<IActionResult> Get(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                var pic = new PictureModel { ImageName = file.FileName };
                S3ObjectPutResponse response = await _objectStoreService.ObjectService.PutAsync(fileBytes, file.FileName);
                var res = await response.ReadResultAsStringAsync();
                var uri = res.Value;
                pic.ImageLinc = uri;
                allDataForFront.pictures.Add(pic);
                // act on the Base64 data
            }

            //var list =await s3client.ListBucketsAsync();

            //foreach (var bucket in list.Buckets)
            //{
            //    lbBuckets.Items.Add(bucket.BucketName);
            //}

            return Ok("");
        }


        [HttpGet ("/GetAll")]
        public async Task<IActionResult> Get()
        {
            allDataForFront.pictures.Add(allDataForFront.pictures[0]);
            var json = new JavaScriptSerializer().Serialize(allDataForFront);
            //foreach (var picture in pictures)
            //{
            //    var a = await picture.SaveImageInLocal(_objectStoreService);
            //}
            return Ok(json);
        }

        [HttpGet ("/Result")]
        public IActionResult GetResult()
        {
            return Ok(Json(""));
        }
    }
}
