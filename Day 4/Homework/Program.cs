using Day_4;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();

app.UseRequestCulture();
app.MapGet("/", () => "Hello World!");

app.Run();
