using Biblia.Integracao;
using Biblia.Integracao.Interfaces;
using Biblia.Integracao.Refit;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IBibliaIntegracao, BibliaIntegracao>();


builder.Services.AddRefitClient<IBibliaIntegracaoRefit>().ConfigureHttpClient(b =>
{
    b.BaseAddress = new Uri("https://bible.com.br");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Biblia/Index");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Biblia}/{action=ObterTraducao}/{id?}");

app.Run();
