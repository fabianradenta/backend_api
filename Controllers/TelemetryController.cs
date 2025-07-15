using Microsoft.AspNetCore.Mvc;
using TelemetryApi.Models; // Pastikan namespace model benar

namespace TelemetryApi.Controllers;

[ApiController]
[Route("api/[controller]")] // Ini akan membuat URL menjadi /api/telemetry
public class TelemetryController : ControllerBase
{
    // Logger untuk debugging di sisi server
    private readonly ILogger<TelemetryController> _logger;

    public TelemetryController(ILogger<TelemetryController> logger)
    {
        _logger = logger;
    }

    // Endpoint ini akan menerima HTTP POST ke /api/telemetry/upload
    [HttpPost("upload")]
    public IActionResult UploadTelemetryData([FromBody] TelemetryData data)
    {
        try
        {
            // Cek jika data yang diterima valid
            if (data == null || string.IsNullOrEmpty(data.device_id))
            {
                return BadRequest("Data tidak valid atau device_id kosong.");
            }

            // --- LOGIKA UTAMA ANDA DI SINI ---
            // 1. Tampilkan data yang diterima di konsol server untuk verifikasi
            _logger.LogInformation($"Data diterima dari device: {data.device_id} pada {data.timestamp}");
            _logger.LogInformation($"Lokasi: {data.latitude}, {data.longitude} | Speed: {data.speed} km/h");

            // 2. Simpan 'data' ke database utama server Anda (SQL Server, PostgreSQL, dll.)
            
            // 3. (Opsional) Jika Anda masih ingin menggunakan RabbitMQ di backend:
            //    Di sinilah Anda akan mem-publish pesan ke RabbitMQ.
            //    Aplikasi Flutter tidak perlu tahu tentang ini.

            // Jika semuanya berhasil, kembalikan status 200 OK
            return Ok(new { message = $"Data untuk device {data.device_id} berhasil diterima." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Terjadi error saat memproses data telemetri.");
            // Kembalikan status 500 Internal Server Error jika ada masalah
            return StatusCode(500, "Terjadi kesalahan di server.");
        }
    }
}