using Microsoft.AspNetCore.Mvc;
using TelemetryApi.Models;

namespace TelemetryApi.Controllers;

[ApiController]
[Route("api/[controller]")] // URL jadi /api/telemetry
public class TelemetryController : ControllerBase
{
    // Logger untuk debug di server side
    private readonly ILogger<TelemetryController> _logger;

    public TelemetryController(ILogger<TelemetryController> logger) {
        _logger = logger;
    }

    // endpoint untuk menerima data telemetri dari aplikasi Flutter
    [HttpPost("upload")]
    public IActionResult UploadTelemetryData([FromBody] TelemetryData data)
    {
        try
        {
            // cek validitas data
            if (data == null || string.IsNullOrEmpty(data.device_id))
            {
                return BadRequest("Data tidak valid atau device_id kosong.");
            }

            _logger.LogInformation($"Data diterima dari device: {data.device_id} pada {data.timestamp}");
            _logger.LogInformation($"Lokasi: {data.latitude}, {data.longitude} | Speed: {data.speed} km/h");
            return Ok(new { message = $"Data untuk device {data.device_id} berhasil diterima." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Terjadi error saat memproses data telemetri.");
            return StatusCode(500, "Terjadi kesalahan di server.");
        }
    }
}