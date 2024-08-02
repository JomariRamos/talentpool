using Microsoft.EntityFrameworkCore;

public class Mutation
{
    public async Task<User> CreateUser(UserInput input, [Service] TalentPoolingContext context)
    {
        var user = new User
        {
            Name = input.Name,
            Role = input.Role
        };
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return user;
    }

    public async Task<Skill> CreateSkill(SkillInput input, [Service] TalentPoolingContext context)
    {
        var skill = new Skill
        {
            Name = input.Name,
            Description = input.Description,
        };
        context.Skills.Add(skill);
        await context.SaveChangesAsync();
        return skill;
    }



    public async Task<UserSkill> CreateUserSkill(UserSkillInput input, [Service] TalentPoolingContext context)
    {
       // Fetch related entities from the database
        var user = await context.Users.FindAsync(input.UserId);
        var skill = await context.Skills.FindAsync(input.SkillId);

        if (user == null || skill == null)
        {
            throw new Exception("User or Skill not found");
        }

        var userSkill = new UserSkill
        {
            UserId = input.UserId,
            SkillId = input.SkillId,
            ProficiencyLevel = input.ProficiencyLevel,
            User = user, // Set the User navigation property
            Skill = skill // Set the Skill navigation property
        };

        context.UserSkills.Add(userSkill);
        await context.SaveChangesAsync();
        return userSkill;
    }

    public async Task<UserSkill> UpdateUserSkill(UserSkillInput input, [Service] TalentPoolingContext context)
    {
        var userSkill = await context.UserSkills
                                      .FirstOrDefaultAsync(us => us.UserId == input.UserId && us.SkillId == input.SkillId);

        if (userSkill == null) throw new Exception("UserSkill not found");

        userSkill.ProficiencyLevel = input.ProficiencyLevel;

        context.UserSkills.Update(userSkill);
        await context.SaveChangesAsync();
        return userSkill;
    }

    public async Task<bool> DeleteUserSkill(int userId, int skillId, [Service] TalentPoolingContext context)
    {
        var userSkill = await context.UserSkills
                                      .FirstOrDefaultAsync(us => us.UserId == userId && us.SkillId == skillId);

        if (userSkill == null) return false;

        context.UserSkills.Remove(userSkill);
        await context.SaveChangesAsync();
        return true;
    }
}