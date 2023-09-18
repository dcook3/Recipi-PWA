namespace Recipi_PWA.Services
{
    public interface ISocketConnection : IDefaultHttpService
    {
       public delegate void msgReceivedCallback(string msg);
       Task<string> EstablishConnection(int id1, int id2, msgReceivedCallback msgRec);
       Task<Boolean> SendMessageAsync(string msg, int sendAttempts = 10);
    }
}