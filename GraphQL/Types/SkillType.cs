public class SkillType : ObjectType<Skill>
{
    protected override void Configure(IObjectTypeDescriptor<Skill> descriptor)
    {
        descriptor.Description("Represents a skill.");
    }
}

