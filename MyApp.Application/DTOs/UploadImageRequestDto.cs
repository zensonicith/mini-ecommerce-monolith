using Microsoft.AspNetCore.Http;

namespace MyApp.Application.DTOs;

public class UploadImageRequestDto
{
    public IFormFile? Image { get; set; }
}