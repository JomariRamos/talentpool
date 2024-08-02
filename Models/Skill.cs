public class Skill
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public ICollection<UserSkill>? Users { get; set; }
}