using Cysharp.Threading.Tasks;

namespace AddressableAssets.Downloaders
{
    public interface IStartableDownload<TDownloadResult>
    {
        UniTask<TDownloadResult> StartDownloadAsync();
    }
}
