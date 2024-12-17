using AngularDotNetApp.Server.Model;
using AngularDotNetApp.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Enable cors
builder.Services.AddCors();
builder.Services.AddScoped<UserNameFileService>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
//Use cors
app.UseCors(builder => builder
.AllowAnyHeader()
.AllowAnyOrigin()
.AllowAnyMethod());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.MapControllers();
// API Endpoint
app.MapPost("/api/submit", async (UserName username, UserNameFileService fileService) =>
{
    //Check validity of the username
    if((username == null)
        || (String.IsNullOrEmpty(username.LastName))
        || (String.IsNullOrEmpty(username.FirstName)))
    {
        return Results.BadRequest(new { message = "Empty parameters submit" });
    }

    // Add submission time
    username.SubmitTime = DateTime.Now;

    // Save submission
    try
    {
        await fileService.SaveUserNameAsync(username);
    }
    catch(Exception ex)
    {
    }

    return Results.Ok(new { message = "Submit and operated successful" });
})
.WithName("SubmitUser");

app.MapFallbackToFile("/index.html");

app.Run();
