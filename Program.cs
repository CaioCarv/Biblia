using Biblia.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

// Adiciona suporte para controladores com views
builder.Services.AddScoped<MySqlRepository>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var logger = provider.GetRequiredService<ILogger<MySqlRepository>>(); // Adicionando o logger
    string connectionString = configuration.GetConnectionString("DefaultConnection");
    return new MySqlRepository(connectionString, logger);
});

builder.Services.AddControllersWithViews();

// Configuração do aplicativo
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapGet("/", context =>
{
    context.Response.Redirect("/Biblia");
    return Task.CompletedTask;
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Biblia}/{action=Index}/{id?}");

app.Run();
