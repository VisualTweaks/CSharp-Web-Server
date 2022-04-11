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
                Console.WriteLine("[{0}] {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), message, Color.Orange);
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
                    //accept a connection
                    var client = server.AcceptTcpClient();
                    Log("Client connected.");
                    StartCommandHandler();
                }
            }
            StartServer();
        }
    }
}
