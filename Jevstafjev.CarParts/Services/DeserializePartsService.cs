using CsvHelper.Configuration;
using CsvHelper;
using Jevstafjev.CarParts.Models;
using System.Globalization;

namespace Jevstafjev.CarParts.Services
{
    public class DeserializePartsService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DeserializePartsService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public List<Part> Deserialize()
        {
            List<Part> parts;

            string filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "spareparts.txt");
            using (var reader = new StreamReader(filePath))
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                    Delimiter = "\t"
                };

                using (var csv = new CsvReader(reader, config))
                {
                    parts = csv.GetRecords<Part>().ToList();
                }
            }

            return parts;
        }
    }
}
