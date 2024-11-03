var builder = WebApplication.CreateBuilder(args);

// Agregar CORS y definir la política
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin() // O usa .WithOrigins("http://localhost:3000") para un dominio específico
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
// Aplicar la política de CORS antes de definir las rutas
app.UseCors("AllowAllOrigins");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Configuración de WebSockets (si tienes endpoints WebSocket)
app.UseWebSockets();

// Configuración de Socket.IO
var socket = new SocketIOClient.SocketIO("http://localhost:3000");
// Conectarse al servidor de Socket.IO
await socket.ConnectAsync();

// Escuchar eventos de Socket.IO
socket.On("message", response =>
{
    Console.WriteLine("Mensaje recibido del servidor Socket.IO: " + response.GetValue<string>());
});

// Opcional: Emitir un mensaje al servidor de Socket.IO
await socket.EmitAsync("message", "Mensaje desde el servidor .NET");

app.MapGet("/", () => "Servidor ASP.NET Core con Socket.IO y Swagger");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
