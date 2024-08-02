using Microsoft.EntityFrameworkCore;

public class Query
{
    [UseDbContext(typeof(TalentPoolingContext))]
    [UseFiltering]
    [UseSorting]
    public IQueryable<User> GetUsers([Service] TalentPoolingContext context) =>
        context.Users;

    [UseDbContext(typeof(TalentPoolingContext))]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Skill> GetSkills([Service] TalentPoolingContext context) =>
        context.Skills;


    [UseDbContext(typeof(TalentPoolingContext))]
    [UseFiltering]
    [UseSorting]
    public async Task<UserSkill?> GetUserSkill(int userId, int skillId, [Service] TalentPoolingContext context) =>
        await context.UserSkills
                      .Include(us => us.User)
                      .Include(us => us.Skill)
                      .FirstOrDefaultAsync(us => us.UserId == userId && us.SkillId == skillId);

    [UseDbContext(typeof(TalentPoolingContext))]
    [UseFiltering]
    [UseSorting]
    public IQueryable<UserSkill> GetUserSkills([Service] TalentPoolingContext context) =>
        context.UserSkills
               .Include(us => us.User)
               .Include(us => us.Skill);


    // Implement custom search functions
}