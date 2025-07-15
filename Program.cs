var builder = WebApplication.CreateBuilder(args);

// Подключаем поддержку контроллеров
builder.Services.AddControllers();

var app = builder.Build();

// Поддержка HTTPS
app.UseHttpsRedirection();

// Подключаем контроллеры
app.MapControllers();

app.Run();
