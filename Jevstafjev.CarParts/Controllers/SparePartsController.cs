using Jevstafjev.CarParts.Models;
using Jevstafjev.CarParts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Jevstafjev.CarParts.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SparePartsController : ControllerBase
    {
        private readonly UploadPartsService _uploadPartsService;
        private readonly DeserializePartsService _deserializePartsService;

        public SparePartsController(UploadPartsService uploadPartsService, DeserializePartsService deserializePartsService)
        {
            _uploadPartsService = uploadPartsService;
            _deserializePartsService = deserializePartsService;
        }

        [HttpGet("[action]")]
        public List<Part> GetAll(int page)
        {
            List<Part> parts = _deserializePartsService.Deserialize();

            int position = (page - 1) * 30;
            return parts
                .Skip(position)
                .Take(30)
                .ToList();
        }

        [HttpGet("[action]")]
        public List<Part> Search(string keyWord, int page)
        {
            List<Part> parts = _deserializePartsService.Deserialize();

            int position = (page - 1) * 30;
            return parts
                .Where(x => x.Name.Contains(keyWord) || x.SerialNumber.Contains(keyWord))
                .Skip(position)
                .Take(30)
                .ToList();
        }

        [HttpPost("[action]")]
        public IActionResult Upload(IFormFile file)
        {
            bool isSucces = _uploadPartsService.TryUpload(file);
            if (!isSucces)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}