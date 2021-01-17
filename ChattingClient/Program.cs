using Grpc.Core;
using Grpc.Net.Client;
using GRPChatProject;
using System;
using System.Threading.Tasks;

namespace ChattingClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("Please enter your name: ");
            var username = Console.ReadLine();

            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new ChatService.ChatServiceClient(channel);

            using (var chat = client.Join())
            {
                _ = Task.Run(async () =>
                {
                    while (await chat.ResponseStream.MoveNext())
                    {
                        var response = chat.ResponseStream.Current;
                        Console.WriteLine($"{response.User} : {response.Text}");
                    }
                }
                );
            }

        }
    }
}
