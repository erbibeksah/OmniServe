var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddLogging();

// api versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Api-Version"));
})
.AddMvc() // This is needed for controllers
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddTransient<GlobalExceptionMiddleware>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("https://localhost:54683", "https://localhost:7192", "https://localhost:7252")
              .AllowAnyMethod()
              .AllowCredentials()
              .WithHeaders("Authorization", "Refresh-Token", "Content-Type")
              .WithExposedHeaders("Authorization", "Refresh-Token", "Content-Type");
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseRouting();
app.UseDefaultFiles();
app.MapStaticAssets();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
