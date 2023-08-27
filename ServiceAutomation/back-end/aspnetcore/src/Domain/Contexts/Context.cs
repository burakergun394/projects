using Domain.Shared.Enums;

namespace Domain.Contexts;

public class Context
{
    public string TenantCode { get; set; }
    public string UserCode { get; set; }
    public Language Language { get; set; }
}
