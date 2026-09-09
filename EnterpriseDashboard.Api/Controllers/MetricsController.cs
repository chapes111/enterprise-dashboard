using EnterpriseDashboard.Api.Data;
using EnterpriseDashboard.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseDashboard.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MetricsController : ControllerBase
{
    private readonly AppDbContext _context;

    public MetricsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetMetrics()
    {
        var metrics = await _context.Metrics.OrderByDescending(m => m.CreatedAt).ToListAsync();
        return Ok(metrics);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMetric([FromBody] MetricDto dto)
    {
        var metric = new Metric
        {
            MetricName = dto.MetricName,
            Value = dto.Value,
            TenantId = dto.TenantId,
        };

        _context.Metrics.Add(metric);
        await _context.SaveChangesAsync();

        return Ok(metric);
    }
}

public record MetricDto(string MetricName, decimal Value, string TenantId);
