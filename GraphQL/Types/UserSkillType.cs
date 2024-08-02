public class UserSkillType : ObjectType<UserSkill>
{
    protected override void Configure(IObjectTypeDescriptor<UserSkill> descriptor)
    {
        
        descriptor.Description("Represents a User SKill.");
    }
}