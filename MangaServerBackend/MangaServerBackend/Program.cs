using MangaServerBackend;
using MangaServerBackend.Controllers;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<service>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin() 
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();





if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

string mangaLibraryPath = builder.Configuration["MangaLibraryPath"];
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(mangaLibraryPath), 
    RequestPath = "/static-manga-files" 
});

//app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();
app.UseAuthorization();

app.MapControllers();


app.Run();
