namespace Recipi_PWA
{
    public interface IDefaultHttpService
    {
        void SetToken(string token);
        bool TokenSet();
    }
}