using System;
using Grpc.Net.Client;
using GrpcDrinks; // Assuming this is the namespace generated for your gRPC service

using var channel = GrpcChannel.ForAddress("https://localhost:3002");
var client = new DrinksGrpc.DrinksGrpcClient(channel);

var data = client.GetDrinks(new GetDrinkRequest());
foreach (var item in data.Drinks)
{
    Console.WriteLine(item);
}