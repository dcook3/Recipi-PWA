public interface IIndexedDbAccessor: IAsyncDisposable
{
    Task<T> GetValueAsync<T>(string collectionName, int id);
    Task InitializeAsync();
    Task SetValueAsync<T>(string collectionName, T value);
}