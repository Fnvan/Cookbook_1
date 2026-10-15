using Cookbook_1.Abstractions;
using Cookbook_1.Repositories;
using Cookbook_1.Services;
using Microsoft.Extensions.DependencyInjection;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSwaggerGen();   
builder.Services.AddSingleton<IRecipeRepo, RecipeRepo>();
builder.Services.AddSingleton<IIngredientService, IngredientService>();
builder.Services.AddSingleton<IIngredientRepo, IngredientRepo>();
builder.Services.AddSingleton<IRecipeService, RecipeService>();



var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseExceptionHandler("/error");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
