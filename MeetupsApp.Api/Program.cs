using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using MeetupsApp.Api.Repositories;
using MeetupsApp.Api.Models;

var builder = WebApplication.CreateBuilder(args);

//Add API services 
builder.Services.AddControllers(options => { options.SuppressAsyncSuffixInActionNames = false; });
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MeetupsApp API", Version = "v1" });
});
builder.Services.AddDbContext<MeetupsAppContext>(options => options.UseSqlite("Data Source=meetups.db"));
builder.Services.AddSingleton<IMeetupsRepository, SqliteDbMeetupsRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddCors();

var app = builder.Build();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
if(app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");
});

app.MapControllerRoute(
    name: "default",
    pattern: "/api/{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");;

app.Run();