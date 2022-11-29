using Cysharp.Threading.Tasks;

namespace Dre0Dru.AddressableAssets.Downloaders
{
    public interface IStartableDownload<TDownloadResult>
    {
        UniTask<TDownloadResult> StartDownloadAsync();
    }
}
