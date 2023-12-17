using Microsoft.AspNetCore.Mvc;
using WcfClientProxy;
using WcfService.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddWcfProxyClient<IWorkService>("http://localhost:4435/WorkService.svc");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
	"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/proxytest", ([FromServices] IWorkService workServices) =>
{
	var response2 = workServices.DoWork(new long[] { 1, 2, 3, 5, 7, 8 });
	return $"Response via proxy: {string.Join(", ", response2.Select(r => $"Id: {r.Id}, Name: {r.Name}"))}";
})
.WithName("Proxy");

app.Run();
