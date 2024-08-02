public class UserSkill
{
    public int UserId { get; set; }
    public required User User { get; set; }
    public int SkillId { get; set; }
    public required Skill Skill { get; set; }
    public ProficiencyLevel ProficiencyLevel { get; set; }
}

public enum ProficiencyLevel
{
    Beginner,
    Intermediate,
    Advanced,
    Expert
}