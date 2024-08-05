using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace TalentPoolingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeedController : ControllerBase
    {
        private readonly TalentPoolingContext _context;

        public SeedController(TalentPoolingContext context)
        {
            _context = context;
        }

        [HttpPost("seed")]
        public async Task<IActionResult> SeedData()
        {
            if (await _context.Users.AnyAsync())
            {
                return BadRequest("Database already seeded.");
            }

            // Seed data
            SeedData(_context);
            await _context.SaveChangesAsync();

            return Ok("Database seeded successfully.");
        }

        private void SeedData(TalentPoolingContext context)
        {
            // Clear existing data
            context.Users.RemoveRange(context.Users);
            context.Skills.RemoveRange(context.Skills);
            context.UserSkills.RemoveRange(context.UserSkills);

            // Add sample users
            var users = new[]
            {
                new User { Id = 1, Name = "Alice", Role = UserRole.Developer },
                new User { Id = 2, Name = "Bob", Role = UserRole.Manager },
                new User { Id = 3, Name = "Charlie", Role = UserRole.Developer },
                new User { Id = 4, Name = "David", Role = UserRole.Developer },
                new User { Id = 5, Name = "Eve", Role = UserRole.Manager },
                new User { Id = 6, Name = "Frank", Role = UserRole.Developer },
                new User { Id = 7, Name = "Grace", Role = UserRole.Manager },
                new User { Id = 8, Name = "Hank", Role = UserRole.Developer },
                new User { Id = 9, Name = "Ivy", Role = UserRole.Manager },
                new User { Id = 10, Name = "Jack", Role = UserRole.Developer }
            };

            context.Users.AddRange(users);

            // Add sample skills
            var skills = new[]
            {
                new Skill { Id = 1, Name = "C#", Description = "C# programming" },
                new Skill { Id = 2, Name = "JavaScript", Description = "JavaScript programming" },
                new Skill { Id = 3, Name = "Python", Description = "Python programming" },
                new Skill { Id = 4, Name = "Java", Description = "Java programming" },
                new Skill { Id = 5, Name = "C++", Description = "C++ programming" },
                new Skill { Id = 6, Name = "Ruby", Description = "Ruby programming" },
                new Skill { Id = 7, Name = "PHP", Description = "PHP programming" },
                new Skill { Id = 8, Name = "Go", Description = "Go programming" },
                new Skill { Id = 9, Name = "Swift", Description = "Swift programming" },
                new Skill { Id = 10, Name = "Kotlin", Description = "Kotlin programming" }
            };

            context.Skills.AddRange(skills);

            // Save changes to ensure IDs are set
            context.SaveChanges();

            // Retrieve entities to ensure they are tracked
            var random = new Random();
            var proficiencyLevels = Enum.GetValues(typeof(ProficiencyLevel)).Cast<ProficiencyLevel>().ToArray();

            // Add sample user skills
            var userSkills = users
                .SelectMany(user => skills
                    .Select(skill => new UserSkill
                    {
                        UserId = user.Id,
                        SkillId = skill.Id,
                        ProficiencyLevel = proficiencyLevels[random.Next(proficiencyLevels.Length)],
                        User = user, // Set the navigation property
                        Skill = skill // Set the navigation property
                    }))
                .OrderBy(x => random.Next()) // Randomize user skills
                .Take(30) // Take 30 random user skills
                .ToList();

            context.UserSkills.AddRange(userSkills);

            context.SaveChanges();
        }
    }
}
