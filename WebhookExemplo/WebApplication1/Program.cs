using Microsoft.AspNetCore.HttpLogging;
using Webhook.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<WebhookService>();

builder.Services.AddW3CLogging(logging =>
{
    logging.LoggingFields = W3CLoggingFields.All;
    logging.FlushInterval = TimeSpan.FromSeconds(2);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseW3CLogging(); //ativando log

app.MapPost("/subscribe", (WebhookService ws, Subscription sub)
    => ws.Subscribe(sub));
app.MapPost("/publish", async (WebhookService ws, PublishRequest req)
    => await ws.PublishMessage(req.Topic, req.Message));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

record PublishRequest(string Topic, object Message);
