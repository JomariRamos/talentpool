using HotChocolate.Data.Filters;

public class UserSkillFilterInputType : FilterInputType<UserSkill>
{
    protected override void Configure(IFilterInputTypeDescriptor<UserSkill> descriptor)
    {
        descriptor.Field(us => us.ProficiencyLevel).Type<EnumOperationFilterInputType<ProficiencyLevel>>(); // Filter by ProficiencyLevel
        descriptor.Field(us => us.User.Name).Type<StringOperationFilterInputType>(); // Filter by User Name
        descriptor.Field(us => us.Skill.Name).Type<StringOperationFilterInputType>(); // Filter by Skill Name
    }
}
