using Microsoft.Identity.Client;
using System.Net.Http.Headers;

HttpClient client = new HttpClient();

var clientId = "86787070-9231-4a7b-863a-8573e65edbab";
var clientSecret = "";
var scope = "api://040d7919-d422-4710-84dc-66c418bd1e67/.default";
var tenantId = "fdded263-93bc-4d52-bce1-fece3a117fb5";
var authority = $"https://login.microsoftonline.com/{tenantId}";

var app = ConfidentialClientApplicationBuilder
    .Create(clientId)
    .WithAuthority(authority)
    .WithClientSecret(clientSecret)
    .Build();

var result = await app.AcquireTokenForClient(new string[] { scope }).ExecuteAsync();
Console.WriteLine($"Access Token: {result.AccessToken}");

client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
var response = await client.GetAsync("http://localhost:5272/api/todolist");
var content = await response.Content.ReadAsStringAsync();

Console.WriteLine($"Your response is: {response.StatusCode}");