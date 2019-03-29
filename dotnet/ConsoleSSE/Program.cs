using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EvtSource;
using static System.Net.Http.WinHttpHandler;

namespace ConsoleSSE
{
    class Program
    {
        //
        // Use Microsoft's WinHttpHandler (via nuget) to enable HTTP/2
        //   - extend SendAsync to set HTTP version
        //   - pass Http2CustomHandler to instantiation of HttpClient()
        //
        public class Http2CustomHandler : WinHttpHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
            {
                request.Version = new Version("2.0");
                return base.SendAsync(request, cancellationToken);
            }
        }
        static void Main(string[] args)
        {
            string url = "";

            if (args.Length > 0)
            {
                url = args[0];
            }
            else
            {
                Console.WriteLine("Usage: ConsoleSSE <Server-Sent Events URL");
                System.Environment.Exit(1);
            }

            var evt = new EventSourceReader(new Uri(url), new Http2CustomHandler()).Start();

            evt.MessageReceived += (object sender, EventSourceMessageEventArgs e) => {
                Console.WriteLine($"{e.Message}");
            };
            evt.Disconnected += async (object sender, DisconnectEventArgs e) => {
                Console.WriteLine($"Retry: {e.ReconnectDelay} - Error");
                await Task.Delay(e.ReconnectDelay);
                evt.Start(); // Reconnect to the same URL
            };

            while (true)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
