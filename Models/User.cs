public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public UserRole Role { get; set; }
    public ICollection<UserSkill>? Skills { get; set; }
}

public enum UserRole
{
    Admin,
    Manager,
    Developer
}