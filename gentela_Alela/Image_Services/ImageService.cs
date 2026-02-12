namespace gentela_Alela.Image_Services
{
    public class ImageService
    {
        public const string DefaultImage = "/Photos/default-image.png/";
        public const string ImageProfile = "/Photos/Img/Photo/";

        public static async Task<string> SaveImageAsync(IFormFile file, string uploadPath)
        {
            var fileName=$"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            using var fileStream=new FileStream(Path.Combine(uploadPath, fileName), FileMode.Create);

            await  file.CopyToAsync(fileStream);

            return fileName;

        }

        public static void DeleteImage(string webroothPath,string? oldfilefromdb)
        {
            if (string.IsNullOrWhiteSpace(oldfilefromdb))
                return;
                               
            var filename = oldfilefromdb.Split(@"/").LastOrDefault();
            if (filename == null || DefaultMethod(filename))
                return;

            var weblocation = $"{webroothPath}{oldfilefromdb}";
            var old = Path.Combine(weblocation);
            if (File.Exists(old))
                File.Delete(old);

        }

        private static bool DefaultMethod(string? filename)
        {
            string? defaultImage = DefaultImage.Split(@"/").Last();
            return filename!.Trim().ToLower() == defaultImage!.Trim().ToLower();
        }
    }
}
