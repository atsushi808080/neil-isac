using System;

namespace Core.Entities;

public class News : BaseEntity
{
    public required string Title { get; set; }
    public string Description { get; set; } = "";
    public string Link { get; set; } = "";
    public bool IsEnable { get; set; }
    public DateTime CreateDate { get; set; }
    public required string CreateIp { get; set; }
    public DateTime UpdateDate { get; set; }
    public string UpdateIp { get; set; } = "";
    public int ClickCount { get; set; }
    public decimal Sort { get; set; }
}
