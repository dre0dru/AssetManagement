namespace Dre0Dru.AddressableAssets.Downloaders
{
    public interface IAssetsDownloadStatus<TDownloadStatus>
    {
        TDownloadStatus DownloadStatus { get; }
    }
}
