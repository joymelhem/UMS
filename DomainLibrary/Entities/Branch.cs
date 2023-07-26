namespace DomainLibrary.Entities;

public class Branch
{
    public long Id { get; set; }
    
    public string? Name { get; set; }
    public virtual ICollection<User> Users { get; set; } = new List<User>();

}