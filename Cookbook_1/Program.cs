using Cookbook_1;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplication(builder.Configuration); 

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseExceptionHandler("/error");


app.MapControllers();

app.Run();
