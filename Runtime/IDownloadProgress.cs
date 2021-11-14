using Cysharp.Threading.Tasks;

namespace AddressablesServices
{
    //TODO move to queue settings
    public enum DownloadFailureBehaviour
    {
        None,
        Restart
    }
    
    public interface IDownloadProgress
    {
        UniTask<long> GetDownloadSize();

        UniTask<bool> IsDownloaded();
    }
}