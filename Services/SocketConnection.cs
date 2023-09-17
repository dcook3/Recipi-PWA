using Microsoft.VisualBasic;
using Recipi_PWA;
using Recipi_PWA.Models;
using System.ComponentModel;
using System.IO;
using System.Net.WebSockets;
using System.Text;

namespace Recipi_PWA.Services
{
    public class SocketConnection : DefaultHttpService, ISocketConnection
    {
        private Boolean isConnected = false;
        private readonly ClientWebSocket webSocket = new ClientWebSocket();
        private readonly CancellationTokenSource cts = new CancellationTokenSource();

        public SocketConnection(HttpClient httpClient, StateContainer _state) : base(httpClient, _state) { }

        public delegate void msgReceivedCallback(string msg);

        public async Task<string> EstablishConnection(ISocketConnection.msgReceivedCallback msgRec)
        {
            Uri serviceUri = new Uri("ws://localhost:5232/api/UserMessaging/connect");
            try
            {
                await webSocket.ConnectAsync(serviceUri, cts.Token);
                isConnected = true;
                _ = ReceiveLoop(msgRec);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                isConnected = false;
                return ex.ToString();
            }
            return "success";
        }

        protected async Task ReceiveLoop(ISocketConnection.msgReceivedCallback msgRec)
        {
            var buffer = new ArraySegment<byte>(new byte[1024]);
            while (!cts.IsCancellationRequested)
            {
                var received = await webSocket.ReceiveAsync(buffer, cts.Token);
                var receivedAsText = Encoding.UTF8.GetString(buffer.Array, 0, received.Count);
                msgRec(receivedAsText);
            }
        }

        public async Task<Boolean> SendMessageAsync(string msg, int sendAttempts = 10)
        {
            for (int i = 0; i < sendAttempts; i++)
            {
                if (isConnected)
                {
                    var dataToSend = new ArraySegment<byte>(Encoding.UTF8.GetBytes(msg));
                    await webSocket.SendAsync(dataToSend, WebSocketMessageType.Text, true, cts.Token);
                    return true;
                }
                await Task.Delay(100);
            }
            return false;
        }

        public Boolean ConnectionEstablished()
        {
            return isConnected;
        }

        public void Dispose()
        {
            cts.Cancel();
            isConnected = false;
            _ = webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "User has logged off", CancellationToken.None);
        }
    }
}
