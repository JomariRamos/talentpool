using Microsoft.EntityFrameworkCore;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using TalentPoolingContext dbContext =
            scope.ServiceProvider.GetRequiredService<TalentPoolingContext>();

        dbContext.Database.Migrate();
    }
}