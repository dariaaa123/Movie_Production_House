using Microsoft.EntityFrameworkCore;
using Tema1PS.View.Components;
using Tema1PS.DataBase;
using Tema1PS.Model.Repositories;
using Tema1PS.Model.RepositoryPack;
using Tema1PS.Presenter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register Repository and Service
// Register repositories
builder.Services.AddScoped<ActorRepository>();
builder.Services.AddScoped<DirectorRepository>();
//builder.Services.AddScoped<Repository>();
builder.Services.AddScoped<ScreenWriterRepository>();
builder.Services.AddScoped<MovieRepository>();
builder.Services.AddScoped<MoviesActorsRepository>();


// Register presenters with generic interface
builder.Services.AddScoped<ActorPresenter>();
builder.Services.AddScoped<DirectorPresenter>();
builder.Services.AddScoped<ScreenWriterPresenter>();
builder.Services.AddScoped<MoviePresenter>();
builder.Services.AddScoped<IMovieGUI>(sp => 
{
    var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
    return (IMovieGUI)httpContextAccessor.HttpContext?.RequestServices.GetService(typeof(IMovieGUI));
});


builder.Services.AddScoped<IEmployeeGUI>(sp => 
{
    var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
    return (IEmployeeGUI)httpContextAccessor.HttpContext?.RequestServices.GetService(typeof(IEmployeeGUI));
});


builder.Services.AddRazorPages();
builder.Services.AddDbContext<ProdHouseContext>(options =>
{
    options.UseSqlServer("Server=localhost,1433;Database=ProdHouseDB;User Id=sa;Password=YourPassword123!;TrustServerCertificate=True");
});
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
