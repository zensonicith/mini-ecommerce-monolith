using MyApp.Domain.Enum;

namespace MyApp.Domain.Entities;

public class Role
{
    public int Id { get; set; }
    public ERole RoleType { get; set; }
    public string Description { get; set; }
}