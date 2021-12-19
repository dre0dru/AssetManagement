namespace AddressableAssets.Downloaders.Queue
{
    //TODO download queue factory? but how it can survive scene reloads
    //TODO queue is instance only via some builder as an extensions interface for factory, maybe add progress tracking as a extension too
    //TODO or not, it will expose dependencies on implementation
    //TODO additional interface for status?
    public interface IAssetsDownloadQueue<TDownloadResult>
    {
        void Enqueue(IStartableDownload<TDownloadResult> downloadable);
    }
}
