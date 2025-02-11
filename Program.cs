using AutoMapper;
using Labo_Cts_backend.Application.IServices;
using Labo_Cts_backend.Application.Mappings;
using Labo_Cts_backend.Application.Services;
using Labo_Cts_backend.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using Labo_Cts_backend.Application.Validators;
using Labo_Cts_backend.Domain.IRepositories;
using Labo_Cts_backend.Infrastructure.Repositories;
using Labo_Cts_backend.Shared.Models;
using Labo_Cts_backend.Infrastructure.Services;
using Serilog;
using Serilog.Events;
using Labo_Cts_backend.Api.Middleware;
using Labo_Cts_backend.Shared.IServices;
using Labo_Cts_backend.Shared.Services;
using System.Reflection;
using FluentValidation;
using StackExchange.Redis;



var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://+:5000");
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder.Services.AddDbContext<LaboratoireCtsContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("LaboratoireCTS")));

/*builder.Services.AddDbContextFactory<LaboratoireCtsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LaboratoireCTS")));*/
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = builder.Configuration.GetValue<string>("Redis:ConnectionString");
    return ConnectionMultiplexer.Connect(configuration);
});



builder.Services.AddAutoMapper(typeof(BainProfile));
builder.Services.AddAutoMapper(typeof(ArticleProfile));
builder.Services.AddAutoMapper(typeof(ArticleVersionProfile));
builder.Services.AddAutoMapper(typeof(GammesChimiquesAlternativeProfile));
builder.Services.AddAutoMapper(typeof(ParametresVersionProfile));
builder.Services.AddAutoMapper(typeof(AlternativeVersionProfile));
builder.Services.AddAutoMapper(typeof(ParametreChimiqueProfile));
builder.Services.AddAutoMapper(typeof(PosteChargeProfile));
builder.Services.AddAutoMapper(typeof(GammeChimiqueVersionProfile));
builder.Services.AddAutoMapper(typeof(RatioArticleProfile));
builder.Services.AddAutoMapper(typeof(PlanDemandeInterventionProfile));

builder.Services.Configure<ExternalApiOptions>(builder.Configuration.GetSection("ExternalApiOptions"));
// Ajouter ExternalApiService avec HttpClient
builder.Services.AddHttpClient<ExternalApiService>();

builder.Services.AddControllers()
     .AddJsonOptions(options =>
     {
         options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
         options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
     });

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<LoggingActionFilter>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Inclure les commentaires XML
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

    // Ajouter une description pour l'API
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Labo CTS Backend API",
        Description = "API de gestion Laboratoire Atelier Galvanoplastie",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Salem Ketata",
            Email = "ketatasalem@outlook.com"
        }
    });
});

// Application
builder.Services.AddScoped<IGammesChimiquesAlternativeRepository, GammesChimiquesAlternativeRepository>();
builder.Services.AddScoped<IGammesChimiquesAlternativeService, GammesChimiquesAlternativeService>();
builder.Services.AddScoped<IGammesChimiquesVersionRepository, GammesChimiquesVersionRepository>();
builder.Services.AddScoped<IArticlesVersionRepository, ArticlesVersionRepository>();
builder.Services.AddScoped<IParametresVersionRepository, ParametresVersionRepository>();
builder.Services.AddScoped<IBainRepository, BainRepository>();
builder.Services.AddScoped<IBainService, BainService>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IGammeManagerService, GammeManagerService>();
builder.Services.AddScoped<IParametreChimiqueRepository, ParametreChimiqueRepository>();
builder.Services.AddScoped<IParametreChimiqueService, ParametreChimiqueService>();
builder.Services.AddScoped<IPosteChargeService, PosteChargeService>();
builder.Services.AddScoped<IPosteChargeRepository, PosteChargeRepository>();
builder.Services.AddScoped<IRatioArticleRepository, RatioArticleRepository>();
builder.Services.AddScoped<IPlanDemandeInterventionRepository, PlanDemandeInterventionRepository>();
builder.Services.AddScoped<IPlanDemandeInterventionService, PlanDemandeInterventionService>();
//builder.Services.AddScoped<IPlanDemandeInterventionRepository, PlanDemandeInterventionRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Shared
builder.Services.AddScoped<ITimeZoneService, TimeZoneService>();
builder.Services.AddScoped<ICommonService, CommonService>();
builder.Services.AddSingleton<RedisCacheService>();


// Enregistrer les validateurs automatiquement
builder.Services.AddValidatorsFromAssemblyContaining<BainCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ArticleUpdateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AlternativeCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ArticleVersionCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ParametreVersionCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<BainUpdateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AlternativeUpdateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PlanDemandeInterventionCreateValidator>();

// Ajouter FluentValidation comme middleware ASP.NET Core
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug() // Configure le niveau minimal de log
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();



var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Labo CTS Backend API v1");
    });


app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseMiddleware<Labo_Cts_backend.Api.Middleware.ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();
Console.WriteLine("Lancement de l'application...");
app.Run();
