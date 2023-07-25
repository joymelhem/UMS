namespace DomainLibrary.Entities;

public class Branch
{
    public long Id { get; set; }
    
    public string? Name { get; set; }
    
    public string? ConnectionString { get; set; }
}