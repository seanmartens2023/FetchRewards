namespace FetchDevSample.Storage
{
    public static class StorageConfig
    {
        public static readonly IStorage Instance = new PocoStorage();
    }
}
