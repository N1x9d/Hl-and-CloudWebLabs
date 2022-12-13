using AspNetCore.Yandex.ObjectStorage;
using System.IO;
using System.Threading.Tasks;

namespace WebApplicationLab
{
    public class PictureModel
    {
        public string ImageLinc { get; set; }

        public string ImageName { get; set; }

        public string PrImageLinc { get; set; }

        public string PrImageName { get; set; }
    }
}
