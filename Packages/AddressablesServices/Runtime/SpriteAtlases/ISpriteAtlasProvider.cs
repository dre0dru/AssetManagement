namespace AddressableAssets.SpriteAtlases
{
    public interface ISpriteAtlasProvider
    {
        void SubscribeToAtlasManagerRequests();
        void UnsubscribeFromAtlasManagerRequests();
        void UnloadSpriteAtlases();
    }
}
