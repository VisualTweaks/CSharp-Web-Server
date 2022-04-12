using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Colorful;
using Console = Colorful.Console;
using System.Drawing;

namespace TestTool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "TestTool";
            void Log(string message)
            {
                Console.WriteLine("[{0}] {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), message, Color.SpringGreen);
            }

            void StartCommandHandler()
            {
                Log("Command handler started");
                while (true)
                {
                    string command = Console.ReadLine();
                    if (command == "exit")
                    {
                        Log("Command handler stopped");
                        return;
                    }
                    else if (command == "ping")
                    {
                        Log("Pinging...");
                        try
                        {
                            using (var client = new TcpClient())
                            {
                                client.Connect(IPAddress.Loopback, 80);
                                Log("Ping success");
                            }
                            Log("Pong");
                        }
                        catch (Exception ex)
                        {
                            Log("Ping failed: " + ex.Message);
                        }
                    }
                    else if (command == "flood")
                    {
                        FloodClients();
                    }
                    else if (command == "clients")
                    {
                        GetClients();
                    }
                    else if (command == "clear")
                    {
                        Console.Clear();
                    }
                    else
                    {
                        Log("Unknown command");
                    }
                }
            }

            void StartServer()
            {
                Log("Starting server...");
                var server = new TcpListener(IPAddress.Any, 80);
                server.Start();
                Log("Server started.");
                while (true)
                {
                    StartClient();
                }
            }

            void FloodClients()
            {
                Log("Creating clients...");
                var clients = new List<TcpClient>();
                while (true)
                {
                    var client = new TcpClient();
                    clients.Add(client);
                    Log("Client connected");
                }
            }

            void CreateClient()
            {
                var clients = new List<TcpClient>();
                var client = new TcpClient();
                clients.Add(client);
            }

            void GetClients()
            {
                Log("Getting clients...");
                Log("Clients: 0");
            }

            void StartClient()
            {
                Log("Starting client...");
                var client = new TcpClient();
                client.Connect(IPAddress.Loopback, 80);
                CreateClient();
                Log("Client connected.");
                StartCommandHandler();

            }

            StartServer();
        }
    }
}
