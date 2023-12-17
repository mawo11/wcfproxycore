using WcfClientProxy;
using WcfService.Contracts;

Console.WriteLine("Wcf Test");

WcfClient<IWorkService> client = new("http://localhost:4435/WorkService.svc");

var response = client.Channel.DoWork(new long[] { 1, 2, 3 });
Console.WriteLine($"Response: {string.Join(", ", response.Select(r => $"Id: {r.Id}, Name: {r.Name}"))}");


var proxy = WcfProxy<IWorkService>.Create("http://localhost:4435/WorkService.svc", null);
var response2 = proxy.DoWork(new long[] { 1, 2, 3, 5, 7, 8 });
Console.WriteLine($"Response via proxy: {string.Join(", ", response2.Select(r => $"Id: {r.Id}, Name: {r.Name}"))}");
