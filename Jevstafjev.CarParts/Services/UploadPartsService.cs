namespace Jevstafjev.CarParts.Services
{
    public class UploadPartsService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UploadPartsService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public bool TryUpload(IFormFile file)
        {
            string fileExtension = Path.GetExtension(file.FileName);
            if (fileExtension != ".txt")
            {
                return false;
            }

            string filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "spareparts.txt");
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return true;
        }
    }
}
