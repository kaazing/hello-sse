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
            // Create custom handler for HTTP/2
            var h2handler = new Http2CustomHandler();

            // Set HTTP/2 to use only 1 connection per server to force connection sharing
            h2handler.MaxConnectionsPerServer = 1;

            if (args.Length == 0)
            {
                Console.WriteLine("Usage: ConsoleSSE <Server-Sent Events URL> [<Server-Sent Events URL>] ...");
                System.Environment.Exit(1);
            }
            
            for (int i = 0; i < args.Length; i++)
            {
                var evt = new EventSourceReader(new Uri(args[i]), h2handler).Start();

                evt.MessageReceived += (object sender, EventSourceMessageEventArgs e) => {
                    Console.WriteLine($"{e.Message}");
                };
                evt.Disconnected += async (object sender, DisconnectEventArgs e) =>
                {
                    // Default ReceiveDataTimeout is 30 seconds, thus is EventSource data is not received for 30 seconds
                    // this will be triggered to reconnect.  If you'd like to set a longer timeout, add
                    // h2handler.ReceiveDataTimeout = new TimeSpan(days, hours, mins, sec) after h2handler is
                    // instantiated above.  Given reconnects are automatic, the Console.WriteLine message
                    // isn't required, but left in for informational purposes.
                    Console.WriteLine($"Receive Timeout: {e.ReconnectDelay}ms - Reconnecting...");
                    await Task.Delay(e.ReconnectDelay);
                    evt.Start(); // Reconnect to the same URL
                };
            }

            while (true)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
