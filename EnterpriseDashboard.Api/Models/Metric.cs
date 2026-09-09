using System.ComponentModel.DataAnnotations.Schema;

namespace EnterpriseDashboard.Api.Models;

[Table("metrics")]
public class Metric
{
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("metric_name")]
    public string MetricName { get; set; } = string.Empty;

    [Column("value")]
    public decimal Value { get; set; }

    [Column("tenant_id")]
    public string TenantId { get; set; } = string.Empty;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
