namespace Core.Models
{
    // Model that represents an image upload result from Cloudinary
    public class ImageUploadResult
    {
        public string PublicId { get; set; }
        public string Url { get; set; }
    }
}