using DomainLibrary.Interfaces;

namespace Persistence.Repositories;

public class TenantContext : ITenantContext
{
    public long branchid { get; set; }
}