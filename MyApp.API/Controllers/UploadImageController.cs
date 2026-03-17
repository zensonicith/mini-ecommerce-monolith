using Microsoft.AspNetCore.Mvc;
using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;

namespace MyApp.API.Controllers;

[ApiController]
[Route("api/uploads")]
public class UploadImageController(IFileStorageService storageService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> UploadImage([FromForm] UploadImageRequestDto requestDto,
        CancellationToken ct = default)
    {
        var result = await storageService.UploadImageAsync(requestDto, "product", ct);
        return Ok(result);
    }
}
