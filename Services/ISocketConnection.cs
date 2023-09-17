namespace Recipi_PWA.Services
{
    public interface ISocketConnection : IDefaultHttpService
    {
       public delegate void msgReceivedCallback(string msg);
       Task<string> EstablishConnection(msgReceivedCallback msgRec);
       Task<Boolean> SendMessageAsync(string msg, int sendAttempts = 10);
    }
}