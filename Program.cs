// TalentPoolingApi/Program.cs
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adding Controllers for REST API support. This is primarily for Seed Data API. 
// The is also for future needs where we might use both REST and GraphQL APIs within the same project.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // Required for Swagger
builder.Services.AddSwaggerGen(); // Add Swagger generation services

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Register DbContextFactory
// builder.Services.AddDbContextFactory<TalentPoolingContext>(options =>
//     options.UseInMemoryDatabase("TalentPoolingDatabase"));

builder.Services.AddDbContextFactory<TalentPoolingContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// builder.Services.AddStackExchangeRedisCache(options =>
//     options.Configuration = builder.Configuration.GetConnectionString("Cache"));

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddType<UserType>()
    .AddType<SkillType>()
    .AddType<UserSkillType>()
    .AddType<UserSkillFilterInputType>()
    .AddFiltering()
    .AddSorting()
    .AddProjections()
    .AddErrorFilter<CustomErrorFilter>();

builder.Logging.AddConsole(); // Add console logging

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enable Swagger in development
    app.UseSwaggerUI(); // Enable Swagger UI in development
    app.ApplyMigrations();
    app.UseDeveloperExceptionPage();
}


app.UseCors("AllowAll");

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
    endpoints.MapControllers();
});

app.Run();
