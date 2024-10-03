using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UploadController : ControllerBase
{
    private readonly string _imageDirectory = "/var/www/images";

    private readonly string[] _permittedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
    private readonly string[] _permittedMimeTypes = {"image/jpeg", "image/png", "image/gif"};
    private readonly long _fileSizeLimit = 5 * 1024 * 1024;

    [HttpPost]
    public async Task<IActionResult> UploadImage(IFormFile[] images)
    {
        if (images == null || images.Length == 0)
        {
            return BadRequest("No image file provided.");
        }

        long totalSize = images.Sum(f => f.Length);

        if (totalSize > _fileSizeLimit)
        {
            return BadRequest($"Total file size exceeds the 5 MB limit. Total size : {totalSize / 1024 / 1024}MB.");
        }

        var uploadedFiles = new string[images.Length];
        int index = 0;

        foreach (var image in images)
        {
            var extension = Path.GetExtension(image.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(extension) || !_permittedExtensions.Contains(extension))
            {
                return BadRequest(
                    $"Invalid file extension for {image.FileName}. Only .jpg, .jpeg, .png and .git are allowed.");
            }

            if (!_permittedMimeTypes.Contains(image.ContentType.ToLower()))
            {
                return BadRequest(
                    $"Invalid MIME type for {image.FileName}. OnlyJPEG, PNG, and GIF formats are allowed.");
            }

            if (!Directory.Exists(_imageDirectory))
            {
                Directory.CreateDirectory(_imageDirectory);
            }

            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(_imageDirectory, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
            
            uploadedFiles[index++] = $"/images/{fileName}";

        }
        return Ok(new { Message = "Files uploaded successfully.", Paths = uploadedFiles });
    }
}