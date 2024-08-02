public record UserInput(string Name, UserRole Role);
public record SkillInput(string Name, string Description);

public record UserSkillInput(int UserId, int SkillId, ProficiencyLevel ProficiencyLevel);