namespace MVCProject.Services.ImgAddingService
{
    public class FileService : IFileService
    {
        public async Task<string> SaveFileAsync(IFormFile file, string folderName)
        {
            string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            string fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filePath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
        }

        public void DeleteFile(string? fileName, string folderName)
        {
            if (string.IsNullOrEmpty(fileName)) return;

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
