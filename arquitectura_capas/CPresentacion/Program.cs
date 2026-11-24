using CDatos;
using CReglas;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages()
    .AddMvcOptions(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

string cadenaConexion = builder.Configuration.GetConnectionString("BDHospital")
    ?? throw new InvalidOperationException("Falta la cadena de conexión 'BDHospital' en appsettings.json.");

builder.Services.AddScoped(_ => new CConexion(cadenaConexion));
builder.Services.AddScoped<CDCitas>();
builder.Services.AddScoped<CDPacientes>();
builder.Services.AddScoped<CDMedicos>();
builder.Services.AddScoped<CRegCitas>();
builder.Services.AddScoped<CRegPacientes>();
builder.Services.AddScoped<CRegMedicos>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
