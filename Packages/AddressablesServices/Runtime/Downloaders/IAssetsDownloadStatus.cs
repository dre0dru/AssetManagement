namespace AddressableAssets.Downloaders
{
    public interface IAssetsDownloadStatus<TDownloadStatus>
    {
        TDownloadStatus DownloadStatus { get; }
    }
}
